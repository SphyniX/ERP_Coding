using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Pool<T> where T : new()
{
    private readonly Stack<T> m_Stack = new Stack<T>();
    private readonly System.Action<T> m_ActionOnGet;
    private readonly System.Action<T> m_ActionOnRelease;
    private readonly int m_Limit;

    public int countAll { get; private set; }
    public int countActive { get { return countAll - countInactive; } }
    public int countInactive { get { return m_Stack.Count; } }

    public Pool(System.Action<T> actionOnGet, System.Action<T> actionOnRelease, int limit = 0)
    {
        m_ActionOnGet = actionOnGet;
        m_ActionOnRelease = actionOnRelease;
        m_Limit = limit;
    }

    public T Get()
    {        
        lock(m_Stack) {
            T element;
            if (m_Stack.Count == 0) {
                element = new T();
                countAll++;
            } else {
                element = m_Stack.Pop();
            }
            if (m_ActionOnGet != null)
                m_ActionOnGet(element);
            return element;
        }        
    }

    public void Release(T element)
    {
        lock (m_Stack) {
            // Pool is full.
            if (m_Limit > 0) {
                if (m_Stack.Count == m_Limit) {
                    countAll--;
                    return;
                }
                Assert.IsTrue(m_Stack.Count < m_Limit);
            }

            if (m_Stack.Count > 0 && ReferenceEquals(m_Stack.Peek(), element))
                Debug.LogErrorFormat("Internal error. Trying to destroy {0} that is already released to pool.", element);
            if (m_ActionOnRelease != null)
                m_ActionOnRelease(element);
            m_Stack.Push(element);
        }
    }

    public void Clear()
    {
        m_Stack.Clear();
		countAll = 0;
    }
}

public static class ObjPool<T> where T : new()
{
    // Object pool to avoid allocations.
    private static readonly Pool<T> s_Pool = new Pool<T>(null, null);

    public static T Get()
    {
        return s_Pool.Get();
    }

    public static void Release(T toRelease)
    {
        s_Pool.Release(toRelease);
    }

    public static void Clear()
    {
        s_Pool.Clear();
    }

    public static string s_Info { get { return string.Format("Obj<{0}>: {1}/{2}", typeof(T).Name, s_Pool.countActive, s_Pool.countAll); } }
}

public static class ListPool<T>
{
    // Object pool to avoid allocations.
    private static readonly Pool<List<T>> s_ListPool = new Pool<List<T>>(null, l => l.Clear());

    public static List<T> Get()
    {
        return s_ListPool.Get();
    }

    public static void Release(List<T> toRelease)
    {
        s_ListPool.Release(toRelease);
    }

    public static void Clear()
    {
        s_ListPool.Clear();
    }

    public static string s_Info { get { return string.Format("List<{0}>: {1}/{2}", typeof(T).Name, s_ListPool.countActive, s_ListPool.countAll); } }
}

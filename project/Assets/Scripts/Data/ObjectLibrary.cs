using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;

public class ObjectLibrary : MonoBehaviour
{
    [FormerlySerializedAs("Objects"), SerializeField]
    private List<Object> m_Objects = new List<Object>();
    public List<Object> Objects { get { return m_Objects; } }
    public Object Get(System.Type type, string name)
    {
        for (int i = 0; i < Objects.Count; ++i) {
            var o = Objects[i];
            var otype = o.GetType();
            if (o.name == name && (type == null || type == otype)) {
                return o;
            }
        }
        LogMgr.W("Get {0}<{1}> = NULL", name, type);
        return null;
    }
    public T Get<T>(string name) where T : UnityEngine.Object
    {
        var type = typeof(T);
        return Get(type, name) as T;
    }
}

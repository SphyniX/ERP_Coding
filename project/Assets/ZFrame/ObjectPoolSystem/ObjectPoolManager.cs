using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ObjectPoolManager
// Author: William Ravaine - spk02@hotmail.com (Spk on Unity forums)
// Date: 15-11-09
//
// <LEGAL BLABLA>
// This code package is provided "as is" with no express or implied warranty of any kind. You may use this code for both
// commercial and non-commercial use without any restrictions. Any modification and/or redistribution of this code should
// include the original author's name, contact information and also this paragraph.
// </LEGAL BLABLA>
//
// The goal of this class is to avoid costly runtime memory allocation for objects that are created and destroyed very often during
// gameplay (e.g. projectile, enemies, etc). It achieves this by recycling "destroyed" objects from an internal cache instead of physically
// removing them from memory via Object.Destroy().
//
// To use the ObjectPoolManager, you simply need to replace your regular object creation & destruction calls by ObjectPoolManager.CreatePooled()
// and ObjectPoolManager.DestroyPooled(). Here's an exemple:
//
// 1) Without using the ObjectPoolManager:
// Projectile bullet = Instanciate( bulletPrefab, position, rotation ) as Projectile;
// Destroy( bullet.gameObject );
// 
// 2) Using the ObjetPoolManager:
// Projectile bullet = ObjectPoolManager.CreatePooled( bulletPrefab.gameObject, position, rotation ).GetComponent<Bullet>();
// ObjectPoolManager.DestroyPooled( bullet.gameObject );
//
// When a recycled object is revived from the cache, the ObjectPoolManager calls its Start() method again, so this object can reset itself as
// if it just got newly created.
//
// When using the ObjectPoolManager with your objects, you need to keep several things in mind:
// 1. You need to be in full control of the creation and destruction of the object (so they go through ObjectPoolManager). This means you shouldn't
//	  use it on objects that use exotic destruction methods (e.g. auto-destroy option on particle effects) because the ObjectPoolManager will
//	  not be able to recycle the object 
// 2. When they get revived from the ObjectPoolManager cache, the pooled objects are responsible for re-initializing themselves as if they had
//	  just been newly created via a regular call Instantiate(). So look out for any dynamic component additions and modifications of the initial
//	  object public fields during gameplay

public class ObjectPoolManager : MonoBehaviour
{
	// Only one ObjectPoolManager can exist. We use a singleton pattern to enforce this.
	#region Singleton Access

	static ObjectPoolManager instance = null;
    private static ObjectPoolManager Instance
	{
		get
		{
			if( !instance )
			{
#if false
				// check if an ObjectPoolManager is already available in the scene graph
				instance = FindObjectOfType( typeof( ObjectPoolManager ) ) as ObjectPoolManager;

				// nope, create a new one
				if( !instance )
				{
					GameObject obj = new GameObject( "ObjectPoolManager" );
					instance = obj.AddComponent<ObjectPoolManager>();                    
				}
#else
                GameObject obj = new GameObject("ObjectPoolManager");
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<ObjectPoolManager>();
#endif
			}
			return instance;
		}
	}

    private static ObjectPoolManager m_ScenePool = null;
    private static ObjectPoolManager ScenePool
    {
        get
        {
            if (!m_ScenePool) {
                GameObject obj = new GameObject("SceneObjectPoolMgr");
                DontDestroyOnLoad(obj);
                m_ScenePool = obj.AddComponent<ObjectPoolManager>();
            }
            return m_ScenePool;
        }
    }
	public static void ReleaseScenePool()
	{
		if (m_ScenePool) Destroy(m_ScenePool.gameObject);
	}

	#endregion

	#region Public fields

	// turn this on to activate debugging information
	public bool debug = false;
	public Color debugColor = Color.white;

	// the GUI block where the debugging info will be displayed
	public Rect debugGuiRect = new Rect( 60, 90, 160, 400 );

	#endregion

	#region Private fields

	// This maps a prefab to its ObjectPool
	Dictionary<GameObject, ObjectPool> prefab2pool;

	// This maps a game object instance to the ObjectPool that created/recycled it
	Dictionary<GameObject, ObjectPool> instance2pool;

	#endregion

	#region Public Interface (static for convenience)

	// Create a pooled object. This will either reuse an object from the cache or allocate a new one
    static GameObject CreateGameObject(ObjectPoolManager mgr, GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return mgr.InternalCreate(prefab, position, rotation, null, -1);
    }

	public static GameObject CreatePooled( GameObject prefab, Vector3 position, Quaternion rotation )
	{
        return CreateGameObject(Instance, prefab, position, rotation);
	}
    public static GameObject CreatePooledScenely(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return CreateGameObject(ScenePool, prefab, position, rotation);
    }

	// Destroy the object now
    static void DestroyGameObject(ObjectPoolManager mgr, GameObject obj )
    {
        if (mgr) {
            mgr.InternalDestroy(obj);
        } else {
            Destroy(obj);
        }
    }

	// Destroy the object after <delay> seconds have elapsed
    static void DestroyGameObject(ObjectPoolManager mgr, GameObject obj, float delay, bool ignoreTimescale)
    {
        if (mgr) {
            if (ignoreTimescale) {
                mgr.StartCoroutine(mgr.InternalDestroyIgnoreTimescale(obj, delay));
            } else {
                mgr.StartCoroutine(mgr.InternalDestroy(obj, delay));
            }
        } else {
            // NOT support <ignoreTimescale>
            Destroy(obj, delay);
        }
    }
	public static void DestroyPooled( GameObject obj, float delay = 0f)
	{
        if (delay > 0) {
            DestroyGameObject(instance, obj, delay, true);
        } else {
            DestroyGameObject(instance, obj);
        }
	}
    public static void DestroyPooledScenely(GameObject obj, float delay = 0f)
    {
        if (delay > 0) {
            DestroyGameObject(m_ScenePool, obj, delay, false);
        } else {
            DestroyGameObject(m_ScenePool, obj);
        }
    }

    private static bool IsObjectPooled(ObjectPoolManager mgr, GameObject obj)
    {
        return mgr.instance2pool.ContainsKey(obj);
    }

    public static bool IsPooled(GameObject obj)
    {
        return instance && IsObjectPooled(instance, obj);
    }

    public static bool IsPooledScenely(GameObject obj)
    {
        return m_ScenePool && IsObjectPooled(m_ScenePool, obj);
    }

    private static GameObject AddChild(ObjectPoolManager mgr, GameObject parent, GameObject child, int siblingIndex)
    {
        GameObject go = null;
        if (child != null) {
            go = mgr.InternalCreate(child, Vector3.zero, Quaternion.identity, parent ? parent.transform : null, siblingIndex);
            if (go != null) {
                Transform t = go.transform;
                if (parent != null) {
                    go.layer = parent.layer;
                }
                t.localScale = Vector3.one;
                go.name = child.name;
            }
        }
        return go;
    }
    public static GameObject AddChild(GameObject parent, GameObject child, int siblingIndex = -1)
    {
        return AddChild(Instance, parent, child, siblingIndex);
    }
    public static GameObject AddChildScenely(GameObject parent, GameObject child, int siblingIndex = -1)
    {
        return AddChild(ScenePool, parent, child, siblingIndex);
    }

	#endregion

	#region Private implementation

	// Constructor
	void Awake()
	{
		prefab2pool = new Dictionary<GameObject, ObjectPool>();
		instance2pool = new Dictionary<GameObject, ObjectPool>();
	}

	private ObjectPool CreatePool( GameObject prefab )
	{
		GameObject obj = new GameObject( prefab.name + " Pool" );
		ObjectPool pool = obj.AddComponent<ObjectPool>();
		pool.Prefab = prefab;
		return pool;
	}

    private GameObject InternalCreate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent, int siblingIndex)
	{
		ObjectPool pool;

		if( !prefab2pool.ContainsKey( prefab ) )
		{
			//Debug.LogWarning( "Spawn non-pooled object " + prefab.name );
			pool = CreatePool( prefab );
			pool.gameObject.transform.parent = this.gameObject.transform;
			prefab2pool[prefab] = pool;
		}
		else
		{
			//Debug.Log( "Spawn object " + prefab.name );
			pool = prefab2pool[prefab];
		}

		// create a new object or reuse an existing one from the pool
        GameObject obj = pool.Instanciate(position, rotation, parent, siblingIndex);

		// remember which pool this object was created from
		instance2pool[obj] = pool;

		return obj;
	}

	private void InternalDestroy( GameObject obj )
	{
        if (obj != null) {
            ObjectPool pool;
            instance2pool.TryGetValue(obj, out pool);
            if (pool) {
                // 要避免反复回收
                if (!obj.transform.IsChildOf(pool.cachedTransform)) {
                    if (obj.activeInHierarchy) {
                        obj.SendMessage("OnRecycle", SendMessageOptions.DontRequireReceiver);
                    }
                    pool.Recycle(obj);
                } else {
                    if (!pool.Constains(obj)) {
                        LogMgr.W("一个被回收的对象不被管理: {0}", obj.name);
                        Object.Destroy(obj);
                    } else {
                        LogMgr.I("尝试回收一个已经被回收的对象: {0}", obj.name);
                    }
                }
            } else {
                // This object was not created through the ObjectPoolManager, give a warning and destroy it the "old way"
                LogMgr.W("{0}#Destroying non-pooled object {1}", name, obj.name);
                Object.Destroy(obj);
            }
        }
	}

	// must be run as coroutine
    private IEnumerator InternalDestroyIgnoreTimescale(GameObject obj, float delay)
	{
        if (obj != null) {
            var t = Time.realtimeSinceStartup + delay;
            do { yield return null; } while (Time.realtimeSinceStartup < t);
            InternalDestroy(obj);
        }
	}
    private IEnumerator InternalDestroy(GameObject obj, float delay)
    {
        if (obj != null) {
            yield return new WaitForSeconds(delay);
            InternalDestroy(obj);
        }
    }

	#endregion

	void OnDestroy()
	{
        if (instance == this) {
            instance = null;
        }
	}

	void OnGUI()
	{
#if UNITY_EDITOR
		if( debug )
		{
			GUILayout.BeginArea( debugGuiRect );
			GUILayout.BeginVertical();

            GUI.color = debugColor;
			GUILayout.Label( "Pools: " + prefab2pool.Count );

			foreach( ObjectPool pool in prefab2pool.Values )
				GUILayout.Label( pool.Prefab.name + ": " + pool.Count );

			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
#endif
	}
}
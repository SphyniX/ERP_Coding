using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The ObjectPool is the storage class for pooled objects of the same kind (e.g. "Pistol Bullet", or "Enemy A")
// This is used by the ObjectPoolManager and is not meant to be used separately
public class ObjectPool : MonoBehavior
{
	// The type of object this pool is handling	
	public GameObject Prefab { get; set;  }

	// This stores the cached objects waiting to be reactivated
	Queue<GameObject> pool;	

	// How many objects are currently sitting in the cache
	public int Count
	{
		get { return pool.Count; }
	}

	public void Awake()
	{
		pool = new Queue<GameObject>();
	}

    public bool Constains(GameObject obj)
    {
        return pool.Contains(obj);
    }

    public GameObject Instanciate(Vector3 position, Quaternion rotation, Transform parent, int siblingIndex)
	{
        // Try to pull one from the cache
		GameObject obj = pool.Count > 0 ? pool.Dequeue() : null;

		// if we don't have any object already in the cache, create a new one
		if( !obj )
		{
			obj = Object.Instantiate(Prefab) as GameObject;
            initObjectTransform(obj, parent, position, rotation, siblingIndex);
		}
		else // else use the pulled one
		{
			// reactivate the object
            initObjectTransform(obj, parent, position, rotation, siblingIndex);
			StartCoroutine(Restart(obj));
		}

		return obj;
	}

	// put the object in the cache and deactivate it
	public void Recycle( GameObject obj )
	{
        // deactivate the object
#if UNITY_3_5
		obj.active = false;
#else
        obj.SetActive(false);
#endif
        
        // put the recycled object in this ObjectPool's bucket
        obj.transform.SetParent(cachedTransform, false);
        // put object back in cache for reuse later (Avoid duplication enqueue)
        if (!pool.Contains(obj)) {
            pool.Enqueue(obj);
        }
	}

    void initObjectTransform(GameObject obj, Transform parent, Vector3 position, Quaternion rotation, int siblingIndex)
	{
		var t = obj.transform;
        t.SetParent(parent, false);
        t.localRotation = rotation;
        var rect = t as RectTransform;
        if (rect) {
            rect.anchoredPosition3D = position;
        } else {
            t.localPosition = position;
        }

        if (parent) {
            var childCount = parent.childCount;
            if (siblingIndex < 0) {
                t.SetSiblingIndex(childCount + siblingIndex);
            } else {
                t.SetSiblingIndex(siblingIndex);
            }
        }
	}

	IEnumerator Restart(GameObject obj)
	{
		if (obj && !pool.Contains(obj)) {
#if UNITY_3_5
			obj.SetActiveRecursively( true );
#else
			obj.SetActive(true);
#endif
            // 如果该对象创建后没有调用过Start，此处会导致调用两次Start
			obj.SendMessage( "Start", SendMessageOptions.DontRequireReceiver );
			yield return 1;
		}
	}
}

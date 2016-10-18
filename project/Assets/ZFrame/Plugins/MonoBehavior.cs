using UnityEngine;
using System.Collections;

public class MonoBehavior : MonoBehaviour {
	
	Transform mTrans;
	public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
}

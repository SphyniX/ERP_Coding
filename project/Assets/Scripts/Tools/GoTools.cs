using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GoTools
{
    public static void SetParent(this GameObject self, GameObject parent, bool worldPositionStays = true)
    {
        var trans = self.transform;
        if (parent) {
            self.layer = parent.layer;
            trans.SetParent(parent.transform, worldPositionStays);
        }
        if (!worldPositionStays) {
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
            var rect = trans as RectTransform;
            if (rect) {
                rect.anchoredPosition3D = Vector3.zero;
            } else {
                trans.localPosition = Vector3.zero;
            }
        }
    }
        
	public static GameObject AddChild(GameObject parent, GameObject prefab = null)
	{
		GameObject go = prefab ? Object.Instantiate<GameObject>(prefab) : new GameObject();
		if (go) {
            go.SetActive(true);
            go.SetParent(parent, false);
            go.name = prefab ? prefab.name : "GameObject";
		}
		return go;
	}

    public static GameObject AddChild(GameObject parent, string path)
    {
        return AddChild(parent, AssetsMgr.A.Load<GameObject>(path));
    }

    public static T NewChild<T>(GameObject parent, string name = null) where T : Component
    {
        GameObject go = new GameObject();
        if (!string.IsNullOrEmpty(name)) go.name = name;
        go.SetParent(parent);
        return go.AddComponent<T>();
    }
    
    public static T NeedComponent<T>(this GameObject self) where T : Component
    {
        T c = self.GetComponent<T>();
        if (c == null) {
            c = self.AddComponent<T>();
        }
        return c;
    }

    public static GameObject NeedChild(this GameObject parent, string path)
    {
        GameObject go = null;
        var trans = parent.transform.Find(path);
        if (trans) {
            go = trans.gameObject;
        } else {
            var slash = path.LastIndexOf('/');
            if (slash < 0) {
                go = new GameObject(path);                
                go.layer = parent.layer;

                var t = go.transform;
                t.SetParent(parent.transform);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
            } else {
                go = parent.NeedChild(path.Substring(slash + 1));
            }
        }
        return go;
    }

    public static Camera FindCameraForLayer(this GameObject self)
    {
        int layer = self.layer;
        int layerMask = 1 << layer;

        if (self.transform is RectTransform) {
            var canvas = self.GetComponentInParent<Canvas>();
            if (canvas && canvas.worldCamera) return canvas.worldCamera;
        }

        Camera cam = Camera.main;
		if (cam == null) {
			var goCam = GameObject.FindWithTag(TAGS.MainCamera);
			if (goCam) cam = goCam.GetComponent<Camera>();
		}
		if (cam && cam.isActiveAndEnabled && (cam.cullingMask & layerMask) != 0) return cam;

        Camera[] cameras = new Camera[Camera.allCamerasCount];
        int camerasFound = Camera.GetAllCameras(cameras);
        for (int i = 0; i < camerasFound; ++i) {
            cam = cameras[i];
            if (cam && cam.isActiveAndEnabled && (cam.cullingMask & layerMask) != 0)
                return cam;
        }
        return null;
    }

    public static GameObject AddForever(GameObject prefab)
    {
        GameObject go = AddChild(null, prefab);
        Object.DontDestroyOnLoad(go);
        return go;
    }

    
    
}

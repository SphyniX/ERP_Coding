using UnityEngine;
using System.Collections;

public static class TransTools {
    public static string GetHierarchy(this Transform self, Transform root = null)
    {
        if (self == root) return string.Empty;

        var obj = self;
        string path = obj.name;
        obj = obj.parent;
        while (obj && obj != root) {
            path = obj.name + "/" + path;
            obj = obj.parent;
        }
        
        return path;
    }

	public static string GetHierarchy(this Component self, Transform root = null)
	{
		return self.transform.GetHierarchy(root);
	}

    /// <summary>
    /// 把self的位置对齐到target
    /// </summary>
    public static void Overlay(this Transform self, Transform target)
    {
        if (target) {
            Camera tarCam = target.gameObject.FindCameraForLayer();
            self.Overlay(target.position, tarCam);
        }
    }

    public static void Overlay(this Transform self, Vector3 target, Camera tarCam)
    {
        var rect = self as RectTransform;
        if (rect) {
            Camera selfCam = self.gameObject.FindCameraForLayer();
            Vector3 pos = tarCam.WorldToScreenPoint(target);

            Vector2 anchoredPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(self.parent as RectTransform, pos, selfCam, out anchoredPos)) {
                rect.anchoredPosition = anchoredPos;
            }
        } else {
            float z = self.position.z;
            Camera selfCam = self.gameObject.FindCameraForLayer();
            var pos = tarCam.WorldToViewportPoint(target);
            pos.z = z;

            self.position = selfCam.ViewportToWorldPoint(pos);
        }
    }

    public static Transform FindByName(this Transform self, string name)
    {
        Transform ret = null;
        for (int i = 0; i < self.childCount; ++i) {
            var t = self.GetChild(i);
            if (t.name == name) {
                ret = t;
            } else {
                ret = t.FindByName(name);
            }
            if (ret) break;
        }
        return ret;
    }

    public static void AttachParent(this Transform self, Transform parent, bool worldPositionStays = true)
    {
        if (parent) {
            self.gameObject.layer = parent.gameObject.layer;
            self.SetParent(parent, worldPositionStays);
        }
        if (!worldPositionStays) {
            self.localRotation = Quaternion.identity;
            self.localScale = Vector3.one;
            var rect = self as RectTransform;
            if (rect) {
                rect.anchoredPosition3D = Vector3.zero;
            } else {
                self.localPosition = Vector3.zero;
            }
        }
    }
}

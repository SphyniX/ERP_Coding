#region

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#endregion

public static class TransformX {
    internal static List<Transform> root;

    public static List<Transform> RootT {
        get {
            return root ?? (root = RootGOs()
                .Select(g => g.transform)
                .ToList());
        }
    }

    internal static bool hasFocusOnHierarchy {
        get {
            EditorWindow fw = EditorWindow.focusedWindow;
            if (fw == null) return false;

            //Debug.Log(fw.GetType().ToString());

            return fw.GetType()
                .ToString() ==
#if UNITY_4_5 || UNITY_4_6
                "UnityEditor.SceneHierarchyWindow";
#else
                "UnityEditor.HierarchyWindow";
#endif
        }
    }

    internal static IEnumerable<GameObject> RootGOs() {
        var prop = new HierarchyProperty(HierarchyType.GameObjects);
        var expanded = new int[0];
        while (prop.Next(expanded)) yield return prop.pptrValue as GameObject;
    }

    public static List<T> xGetChildren<T>(this Component t, bool deep = false, bool includeMe = false,
        bool activeOnly = false) where T : Component {
        var children = new List<T>();
        if (t == null) return children;

        if (includeMe && (t.gameObject.activeSelf || !activeOnly)) {
            var c = t.GetComponent<T>();
            if (c != null) children.Add(c);
        }

        foreach (Transform child in t.transform) {
            if (deep) children.AddRange(xGetChildren<T>(child, true, includeMe, activeOnly));
            if ((deep && includeMe) || (!child.gameObject.activeSelf && activeOnly)) continue;

            var c = child.GetComponent<T>();
            if (c != null) children.Add(c);
        }

        return children;
    }

    public static List<T> xGetSibling<T>(this Component t, bool includeMe = false, bool activeOnly = false)
        where T : Component {
        var siblings = new List<T>();
        if (t == null) return siblings;

        if (includeMe && (t.gameObject.activeSelf || !activeOnly)) {
            var c = t.GetComponent<T>();
            if (c != null) siblings.Add(c);
        }

        List<T> result;

        if (t.transform.parent == null) {
            List<Transform> list = RootT;
            result = new List<T>();
            for (int i = 0; i < list.Count; i++) {
                if (!includeMe && list[i].transform == t.transform) continue;
                var c = list[i].GetComponent<T>();
                if (c != null) result.Add(c);
            }
        } else {
            result = t.transform.parent.xGetChildren<T>();
            if (includeMe) return result;
            var c = t.GetComponent<T>();
            if (c != null) result.Remove(c);
        }

        return result;
    }

    public static GameObject[] xGetParents(this GameObject go) {
        var p = go.transform.parent;
        var list = new List<GameObject>();
        while (p != null) {
            list.Add(p.gameObject);
            p = p.parent;
        }
        return list.ToArray();
    }

    public static Transform[] xGetParents(this Transform t, bool reverse = false) {
        var p = t.parent;
        var list = new List<Transform>();
        while (p != null) {
            list.Add(p);
            p = p.parent;
        }

        if (reverse) list.Reverse();
        return list.ToArray();
    }


    public static int xParentCount(this GameObject go) {
        if (go == null || go.transform == null) return 0;
        Transform p = go.transform.parent;
        int cnt = 0;

        while (p != null) {
            cnt++;
            p = p.parent;
        }
        return cnt;
    }

    public static bool xHasChild(this GameObject go) { return go.transform.childCount > 0; }

    internal static bool xHasSibling(this GameObject go) {
        Transform p = go.transform.parent;
        return p != null ? (p.childCount > 1) : (RootT.Count > 1);
    }

    public static void xForeachSibling(this GameObject go, Action<GameObject> func) {
        xForeachSibling2(
            go, item => {
                func(item);
                return true;
            });
    }

    public static void xForeachSibling2(this GameObject go, Func<GameObject, bool> func) {
        Transform p = go.transform.parent;

        if (p != null) {
            foreach (Transform t in go.transform.parent) {
                if (t.gameObject == go) continue;
                if (!func(t.gameObject)) break;
            }
        } else {
            IEnumerable<GameObject> rootGOs = RootGOs();
            foreach (GameObject child in rootGOs) {
                if (child == go) continue;
                if (!func(child)) break;
            }
        }
    }

    public static void xForeachParent(this GameObject go, Action<GameObject> func) {
        xForeachParent2(
            go, item => {
                func(item);
                return true;
            });
    }

    public static void xForeachParent2(this GameObject go, Func<GameObject, bool> func) {
        Transform p = go.transform.parent;
        while (p != null) {
            if (!func(p.gameObject)) break;
            p = p.parent;
        }
    }

    public static void xForeachChild<T>(this Transform p, Func<T, bool> action, bool deep = false) where T : Component {
        foreach (Transform child in p) {
            var t = child.GetComponent<T>();
            if (deep) child.xForeachChild(action, true);

            if (t == null) continue;
            if (!action(t)) return; //stop if enough
        }
    }

    public static void xForeachChild2(this GameObject go, Func<GameObject, bool> action, bool deep = false) {
        go.transform.xForeachChild<Transform>(t => action(t.gameObject), deep);
    }

    public static void xForeachChild(this GameObject go, Action<GameObject> action, bool deep = false) {
        go.transform.xForeachChild<Transform>(
            t => {
                action(t.gameObject);
                return true;
            }, deep);
    }

    public static void xReparent(this Transform t, string undo, Transform parent) {
        if (t == null || t == parent || t.parent == parent) return;
        t.gameObject.layer = parent.gameObject.layer;
        Undo.SetTransformParent(t.transform, parent, undo);
    }

    public static void xSetLocalTransform(this Transform t, string undo, Vector3? pos = null, Vector3? scl = null,
        Vector3? rot = null) {
        Undo.RecordObject(t, undo);
        if (scl != null) t.localScale = scl.Value;
        if (rot != null) t.localEulerAngles = rot.Value;
        if (pos != null) t.localPosition = pos.Value;
    }

    public static void xSetLocalPosition(this Transform t, Vector3 pos, string undo) { t.xSetLocalTransform(undo, pos); }

    public static void xSetLocalScale(this Transform t, Vector3 scl, string undo) {
        t.xSetLocalTransform(undo, null, scl);
    }

    public static void xSetLocalRotation(this Transform t, Vector3 rot, string undo) {
        t.xSetLocalTransform(undo, null, null, rot);
    }

    public static void xResetLocalTransform(this Transform t, string undo) {
        xSetLocalTransform(t, undo, Vector3.zero, Vector3.one, Vector3.zero);
    }

    public static void xResetLocalPosition(this Transform t, string undo) { t.xSetLocalTransform(undo, Vector3.zero); }

    public static void xResetLocalScale(this Transform t, string undo) { t.xSetLocalTransform(undo, null, Vector3.one); }

    public static void xResetLocalRotation(this Transform t, string undo) {
        t.xSetLocalTransform(undo, null, null, Vector3.zero);
    }

    public static Transform xNewTransform(this string name, string undo, Transform p, Vector3? pos = null,
        Vector3? scl = null, Vector3? rot = null) {
        Transform t = new GameObject {name = name}.transform;
        Undo.RegisterCreatedObjectUndo(t.gameObject, undo);
        t.xReparent(undo, p);
        t.xSetLocalTransform(undo, pos ?? Vector3.zero, scl ?? Vector3.one, rot ?? Vector3.zero);
        return t;
    }

    public static GameObject xNewPrimity(this PrimitiveType type, string name, string undo, Transform p,
        Vector3? pos = null, Vector3? scl = null, Vector3? rot = null) {
        GameObject primity = GameObject.CreatePrimitive(type);
        Undo.RegisterCreatedObjectUndo(primity, undo);
        primity.transform.xReparent(undo, p);
        primity.name = name;
        primity.transform.xSetLocalTransform(undo, pos ?? Vector3.zero, scl ?? Vector3.one, rot ?? Vector3.zero);
        return primity;
    }
}
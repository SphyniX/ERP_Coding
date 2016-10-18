using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class SceneViewX {
    internal static SceneView current {
        get {
            if (EditorWindow.focusedWindow != null && EditorWindow.focusedWindow.GetType() == typeof (SceneView)) return (SceneView) EditorWindow.focusedWindow;
            return SceneView.lastActiveSceneView ?? (SceneView) SceneView.sceneViews[0];
        }
    }

    internal static Camera sceneCamera {
        get { return current.camera; }
    }

    public static void Refresh() { //hacky way to force SceneView increase drawing frame
        var t = Selection.activeTransform
                ?? ((Camera.main != null) ? Camera.main.transform : new GameObject("$t3mp$").transform);

        var op = t.position;
        t.position += new Vector3(1, 1, 1); //make some dirty
        t.position = op;

        if (t.name == "$t3mp$") Object.DestroyImmediate(t.gameObject, true);
    }

    private static T GetAnimT<T>(string name) {
        if (current == null) return default(T);
        var animT = typeof (SceneView).GetField(name)
            .GetValue(current);
        return (T) animT.GetType()
            .GetField("m_Value")
            .GetValue(animT);
    }

    private static void SetAnimT<T>(string name, T value) {
        if (current == null) return;

        var animT = current.xGetField(name);
#if UNITY_4_5 || UNITY_4_6 || UNITY_5
        animT.xSetProperty("target", value);
#else
	    animT.xInvoke("BeginAnimating", null, null, value, animT.xGetField("m_Value"));
#endif
        //object animT = typeof(SceneView).GetField(name, _flags).GetValue(current);
        //var info = animT.GetType().GetField("m_Value", _flags);
        //animT.GetType().GetMethod("BeginAnimating", _flags).Invoke(animT, new object[] { value, (T)info.GetValue(animT) });
    }

    public static Vector3 m_Position {
        get { return GetAnimT<Vector3>("m_Position"); }
        set { SetAnimT("m_Position", value.xFixNaN()); }
    }

    public static Quaternion m_Rotation {
        get { return GetAnimT<Quaternion>("m_Rotation"); }
        set { SetAnimT("m_Rotation", value); }
    }

    public static float cameraDistance {
        get { return (float) current.xGetProperty("cameraDistance"); }
    }

    public static bool orthographic {
        get { return current.camera.orthographic; }
        set {
            //current.camera.orthographic = value;
#if UNITY_4_5 || UNITY_4_6 || UNITY_5
            SetAnimT("m_Ortho", value);
#else
                SetAnimT("m_Ortho", value ? 1f : 0f);
#endif
        }
    }

    public static float m_Size {
        get { return GetAnimT<float>("m_Size"); }
        set { SetAnimT("m_Size", (Single.IsInfinity(value) || (Single.IsNaN(value)) || value == 0) ? 100f : value); }
    }
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SerializeX {
    internal static SerializedProperty[] xGetSerializedProperties(this Object go) {
        var so = new SerializedObject(go);
        so.Update();
        var result = new List<SerializedProperty>();

        var iterator = so.GetIterator();
        while (iterator.NextVisible(true)) result.Add(iterator.Copy());
        return result.ToArray();
    }

    internal static Dictionary<string, object> xGetDump(this SerializedObject obj) {
        var iterator = obj.GetIterator();
        var first = true;
        var result = new Dictionary<string, object>();

        var isHidden = obj.targetObject.xGetFlag(HideFlags.HideInInspector);
        if (isHidden) Debug.Log(obj + ": is Hidden");

        while (iterator.NextVisible(first)) {
            first = false;
            result.Add(iterator.name, iterator.propertyType);
        }

        return result;
    }
}
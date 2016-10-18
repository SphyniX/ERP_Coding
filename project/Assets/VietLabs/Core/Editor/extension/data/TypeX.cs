using System;
using System.Collections.Generic;
using UnityEngine;

public static class TypeX {
    public static Type WindZoneT {
        get { return "UnityEngine.WindZone".xGetTypeByName("UnityEngine"); }
    }

    public static Type WindZoneModeT {
        get { return "UnityEngine.WindZoneMode".xGetTypeByName("UnityEngine"); }
    }

    public static Type BaseProjectWindowT {
        get { return "UnityEditor.BaseProjectWindow".xGetTypeByName("UnityEditor"); }
    }

    public static Type FilteredHierarchyT {
        get { return "UnityEditor.FilteredHierarchy".xGetTypeByName("UnityEditor"); }
    }

    public static Type SearchableEditorWindowT {
        get { return "UnityEditor.SearchableEditorWindow".xGetTypeByName("UnityEditor"); }
    }

    public static Type SearchFilterT {
        get { return "UnityEditor.SearchFilter".xGetTypeByName("UnityEditor"); }
    }

    public static Type TreeViewT {
        get { return "UnityEditor.TreeView".xGetTypeByName("UnityEditor"); }
    }

    public static Type ITreeViewDataSourceT {
        get { return "UnityEditor.ITreeViewDataSource".xGetTypeByName("UnityEditor"); }
    }


    private static Dictionary<string, Type> _typeDict;

    public static Type xGetTypeByName(this string className, string classPackage) {
        if (_typeDict == null) _typeDict = new Dictionary<string, Type>();
        var hasCache = _typeDict.ContainsKey(className);
        var def = hasCache ? _typeDict[className] : null;

        if (hasCache) {
            if (def != null) return def;
            _typeDict.Remove(className);
        }

        def = Types.GetType(className, classPackage);
        if (def != null) _typeDict.Add(className, def);
        else Debug.LogWarning(string.Format("Type <{0}> not found in package <{1}>", className, classPackage));

        return def;
    }

    public static bool xIsEquals<T>(this T a, T b) {
        return EqualityComparer<T>.Default.Equals(a, b);
    }
}
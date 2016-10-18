using UnityEditor;
using UnityEngine;

public static class PrefabX {
    public static void xBreakPrefab(this GameObject go, string tempName = "vlb_dummy.prefab") {
        var go2 = PrefabUtility.FindRootGameObjectWithSameParentPrefab(go);

        PrefabUtility.DisconnectPrefabInstance(go2);
        var prefab = PrefabUtility.CreateEmptyPrefab("Assets/" + tempName);
        PrefabUtility.ReplacePrefab(go2, prefab, ReplacePrefabOptions.ConnectToPrefab);
        PrefabUtility.DisconnectPrefabInstance(go2);
        AssetDatabase.DeleteAsset("Assets/" + tempName);

        //temp fix to hide Inspector's dirty looks
        Selection.instanceIDs = new int[] {};
    }
    public static void xSelectPrefab(this GameObject go) {
        var prefab = PrefabUtility.GetPrefabParent(PrefabUtility.FindRootGameObjectWithSameParentPrefab(go));
        Selection.activeObject = prefab;
        EditorGUIUtility.PingObject(prefab.GetInstanceID());
    }
}
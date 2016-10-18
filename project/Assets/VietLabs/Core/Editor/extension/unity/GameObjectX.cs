using UnityEngine;

public static class GameObjectX {
    /*internal static bool xIsCombined(this GameObject go) { return go.HasFlagChild(HideFlags.HideInHierarchy); }*/
    internal static bool xIsLock(this GameObject go) { return go.xGetFlag(HideFlags.NotEditable); }
}
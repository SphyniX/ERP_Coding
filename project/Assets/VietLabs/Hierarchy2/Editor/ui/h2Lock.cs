using UnityEditor;
using UnityEngine;

public class h2Lock : h2Icon 
{
    protected override string getUndoName(bool set, h2IGroup group = h2IGroup.Target, h2IValue value = h2IValue.Same) {
        var str = set ? "Lock " : "Unlock ";
        if (group == h2IGroup.Target) return str + target.name;

        var g = group == h2IGroup.Selection ? "Selection" : "Siblings";

        if (value != h2IValue.InvertTarget) return "Toggle " + str + g;
        return set ? "Unlock " : "Lock " + g; 
    }

    protected override GenericMenu GetMenu(GameObject go)
    {
        var menu = new GenericMenu();
        menu.xAdd("Deep Lock children", () => go.hSetLock(true, true, "Deep lock children"));
        menu.xAdd("Deep Unlock children", () => go.hSetLock(false, true, "Deep unlock children"));
        return menu;
    }

    protected override bool autoSetChildren { get { return true; }}
    protected override bool Get(GameObject go) { return go.xGetFlag(HideFlags.NotEditable); }
    protected override void Set(GameObject go, bool value, string undoName) {
        if (!string.IsNullOrEmpty(undoName)) Undo.RecordObject(go, undoName);
        go.xSetFlag(HideFlags.NotEditable, value);
    }
    public void Draw(Rect rect, GameObject ptarget) {
        var value = Get(ptarget);
        base.Draw(rect, ptarget, value ? "lock" : "dot");
    }

}

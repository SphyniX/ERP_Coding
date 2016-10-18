using UnityEditor;
using UnityEngine;

public class h2Static : h2Icon
{
    protected override string getUndoName(bool set, h2IGroup group = h2IGroup.Target, h2IValue value = h2IValue.Same) {
        if (group == h2IGroup.Target) return (set ? "Make " : "Clear ") + target.name + " static";

        var g = group == h2IGroup.Selection ? "Selection" : "Siblings";

        if (value != h2IValue.InvertTarget) return "Toggle static " + g;
        return set ? "Set static " : "Clear static " + g;
    }

    /*protected override GenericMenu GetMenu(GameObject go) {
        var menu = new GenericMenu();
        menu.xAdd("Deep set children static", () => go.SetStatic(true, true, "Deep lock children"));
        menu.xAdd("Deep clear children static", () => go.hSetLock(false, true, "Deep unlock children"));
        return menu;
    }*/

    protected override bool autoSetChildren { get { return true; }}
    protected override bool Get(GameObject go) { return go.isStatic; }
    protected override void Set(GameObject go, bool value, string undoName) {
        if (!string.IsNullOrEmpty(undoName)) Undo.RecordObject(go, undoName);
        go.isStatic = value;
    }
    public void Draw(Rect rect, GameObject ptarget) {
        var value = Get(ptarget);
        base.Draw(rect, ptarget, value ? "lighting" : "dot");
    }

}

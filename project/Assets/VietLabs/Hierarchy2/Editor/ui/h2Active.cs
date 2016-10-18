using UnityEditor;
using UnityEngine;

public class h2Active : h2Icon {

    //static readonly ColorHSB ColorActive        = new ColorHSB(0.15f, 0.65f, 1f, 1f);
    //static readonly ColorHSB ColorActiveSelf    = new ColorHSB(0.15f, 0.65f, 0.7f, 1f);
    //static readonly ColorHSB ColorInActive      = new ColorHSB(0.15f, 0f, 0.5f, 1f);
    protected override string getUndoName(bool set, h2IGroup group = h2IGroup.Target, h2IValue value = h2IValue.Same) {
        if (group == h2IGroup.Target) return (set ? "Active " : "Deactive ") + target.name;
        var g = group == h2IGroup.Selection ? "Selection" : "Siblings";
        if (value != h2IValue.InvertTarget) return "Toggle Active " + g;
        return set ? "Deactive " : "Active " + g;
    }

    protected override GenericMenu GetMenu(GameObject go) {
        var menu = new GenericMenu();
        menu.xAdd("Deep Active children", ()=>go.hSetActiveChildren(true, false));
        menu.xAdd("Deep Deactive children", ()=>go.hSetActiveChildren(false, false));
        return menu;
    }

    protected override bool autoSetParent { get { return true; }}

    public void Draw(Rect r, GameObject go) {
        base.Draw(r, go, go.activeInHierarchy ? "eye" : go.activeSelf ? "eye_dis" : "dot", (go.activeSelf && !go.activeInHierarchy) ? h2Settings.color_ActiveHalf : (Color?)null);
    }

    //protected override void Set<T1>(T1 go, bool value, string undoName) { base.Set(go, value, undoName); }

    protected override void Set(GameObject go, bool value, string undoName) {
        if (undoName != null) Undo.RecordObject(go, undoName);
        go.SetActive(value);
        //    if (value && activeParents) {
        //        go.xForeachParent2(p => {
        //            if (undoKey != null) p.xRecordUndo(undoKey);
        //            p.SetActive(true);
        //            return !p.activeInHierarchy;
        //        });    
        //    }
    }
    protected override bool Get(GameObject go) { return go.activeSelf; }
}
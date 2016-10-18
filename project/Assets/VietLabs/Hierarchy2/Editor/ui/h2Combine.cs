using UnityEngine;

public class h2Combine : h2Icon/*RectGUIToggle<GameObject>*/
{
    public int maxChildCount;

    protected override string getUndoName(bool set, h2IGroup group = h2IGroup.Target, h2IValue value = h2IValue.Same) {
        var str = set ? "Combine " : "Expand ";
        if (group == h2IGroup.Target) {//single
            return str + target.name;
        }

        var g = group == h2IGroup.Siblings ? "Siblings" : group == h2IGroup.Selection ? "Selection" : "";

        if (value != h2IValue.InvertTarget) return "Toggle " + str + g;
        return set ? "Expand " : "Combine " + g;
    }

    public void Draw(Rect r, GameObject go) {
        var t = go.transform;
        if (t.childCount == 0) return;

        target = go;

        var value = Get(go);
	    var n = t.childCount;
	    
	    //Debug.Log(go + ":"+ n);
        if (r.Contains(Event.current.mousePosition)) ReadModifier().ReadMouse().Check(); 
        r.xMiniButton( n <= 999 ? "" +t.childCount : "999+", false, 1f, value);
	    if (maxChildCount < n) maxChildCount = n;
    }
    protected override void Set(GameObject go, bool value, string undoName) {
        go.xForeachChild(
            child => {
                //if (undoName != null) child.xRecordUndo(undoName, true);
                child.xSetFlag(HideFlags.HideInHierarchy, value);
            }
       );

#if UNITY_4_5 || UNITY_4_6 || UNITY_5
        //workaround for Hierarchy not update
        var old = go.activeSelf;
        go.SetActive(!old);
        go.SetActive(old);
#endif
    }
    protected override bool Get(GameObject go) {
        return go != null && go.HasFlagChild(HideFlags.HideInHierarchy);
    }
}

using System.Linq;
using UnityEditor;
using UnityEngine;
using vietlabs;

public enum h2IValue
{
    Same,
    InvertTarget,
    ToggleEach
}

public enum h2IGroup
{
    Target,
    Selection,
    Siblings,
    Children,
    Parents
}

public class h2Icon : vlbGEHandler<GameObject>
{
    virtual protected string getUndoName(bool set, h2IGroup group = h2IGroup.Target, h2IValue value = h2IValue.Same) { return null; }

    virtual protected bool autoSetChildren { get { return false; } }// LMB also set children, to clear children use Ctrl + LMB (lock)
    virtual protected bool autoSetParent { get { return false; } }// LMB also set parents, to clear parents use Ctrl + LMB (visible)
    virtual protected bool Get(GameObject go) { return false; }
    virtual protected void Set(GameObject go, bool value, string undoName) { }
    virtual protected GenericMenu GetMenu(GameObject go) { return null; }
    virtual protected void Draw(Rect rect, GameObject ptarget, string icon, Color? c = null)
    {
        target = ptarget;

        if (string.IsNullOrEmpty(icon)) return;

        if (c == null) c = (Get(ptarget) ? ColorHSL.yellow.dS(-0.2f).xProSkinAdjust() : ColorHSL.gray.xProSkinAdjust());
        using (GuiX.GUIColor(c.Value))
        {
            GUI.DrawTexture(rect, EditorResource.GetTexture2D(icon));
        }

        if (rect.Contains(Event.current.mousePosition)) {
            d = 0;
            ReadModifier().ReadMouse().Check();
        }
    }



    protected GameObject[] GetSiblings() {
        return target.transform.xGetSibling<Transform>().Select(item => item.gameObject).ToArray();
    }
    protected GameObject[] GetChildren() {
        return target.transform.xGetChildren<Transform>(true).Select(item => item.gameObject).ToArray();
    }
    protected GameObject[] GetParents() { return target.xGetParents(); }
    protected GameObject[] GetSelection() { return Selection.gameObjects; }


    protected void SetTargetInGroup(bool v, GameObject[] arr = null, string undoName = null, h2IValue b = h2IValue.Same, bool useEvent = true)
    {//Action<T> a, 
        if (useEvent) Event.current.Use();
        Set(target, v, undoName);

        if (arr == null) return;

        foreach (var item in arr)
        {
            if (item.xIsEquals(target)) continue;
            Set(item, (b == h2IValue.Same) ? v : (b == h2IValue.InvertTarget) ? !v : !Get(item), undoName);
        }
    }


    protected override bool OnLMB()
    {
        var value = !Get(target);
        var _selection = GetSelection();
        var inSelection = _selection != null && _selection.Length > 1 && _selection.Contains(target);

        if (inSelection)
        {//Main target is in Selection : toogle all items together with the main target
            SetTargetInGroup(value, _selection, getUndoName(value, h2IGroup.Selection));
            return true;
        }

        if (value)
        {
            if (autoSetChildren)
            {
                SetTargetInGroup(true, GetChildren(), getUndoName(true));
                return true;
            }

            if (autoSetParent)
            {
                SetTargetInGroup(true, GetParents(), getUndoName(true));
                return true;
            }
        }

        SetTargetInGroup(value, null, getUndoName(value));
        return true;
    }
    protected override bool OnAltLMB()
    {
        var value = !Get(target);
        var _selection = GetSelection();
        var inSelection = _selection != null && _selection.Length > 1 && _selection.Contains(target);
        const h2IValue rgv = h2IValue.InvertTarget;

        if (inSelection)
        {
            SetTargetInGroup(value, _selection, getUndoName(value, h2IGroup.Selection, rgv), rgv);
            return true;
        }

        SetTargetInGroup(value, GetSiblings(), getUndoName(value, h2IGroup.Siblings, rgv), rgv);
        return true;
    }
    protected override bool OnCtrlLMB()
    {
        var value = !Get(target);
        var _selection = GetSelection();
        var inSelection = _selection != null && _selection.Length > 1 && _selection.Contains(target);

        if (inSelection)
        {
            SetTargetInGroup(value, null, getUndoName(value));
            return true;
        }

        if (!value)
        {
            if (autoSetChildren)
            { // auto clear children
                SetTargetInGroup(false, GetChildren(), getUndoName(false));
                return true;
            }

            if (autoSetParent)
            { // auto clear parent
                SetTargetInGroup(false, GetParents(), getUndoName(false));
                return true;
            }
        }

        SetTargetInGroup(value, GetSiblings(), getUndoName(value, h2IGroup.Siblings));
        return true;
    }
    protected override bool OnCtrlAltLMB()
    {
        var value = !Get(target);
        var _selection = GetSelection();
        var inSelection = _selection != null && _selection.Length > 1 && _selection.Contains(target);
        const h2IValue rgv = h2IValue.ToggleEach;

        if (inSelection)
        {
            SetTargetInGroup(value, null, getUndoName(value, h2IGroup.Selection, rgv), rgv);
            return true;
        }

        SetTargetInGroup(value, GetSiblings(), getUndoName(value, h2IGroup.Siblings, rgv), rgv);
        return true;
    }
    override protected bool OnRMB() {
        var menu = GetMenu(target);
        if (menu != null) {
            Event.current.Use();
            menu.ShowAsContext();
            return true;
        }
        return false;        
    }

    
}
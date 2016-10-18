using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using vietlabs;

public static class HierarchyX {
    public static GameObject renameGO;
    public static int renameStep;

    internal static bool HasFocusOnHierarchy
    {
        get
        {
            var fw = EditorWindow.focusedWindow;
            if (fw == null) return false;

            //Debug.Log(fw.GetType().ToString());

            return fw.GetType().ToString() ==
                #if UNITY_4_5 || UNITY_4_6
                    "UnityEditor.SceneHierarchyWindow";
                #else
                    "UnityEditor.HierarchyWindow";
                #endif
        }
    }

    internal static bool hHasStatic(this List<GameObject> list) {
        var hasStatic = false;
        foreach (var go in list) {
            hasStatic = go.isStatic;
            if (hasStatic) break;
        }
        return hasStatic;
    }

    private static List<object> GetChildrenTreeItem(this object treeItem, Type itemType, bool deep) {
        var tempList = treeItem.xGetField("m_Children", itemType);
        var result = new List<object>();

        //Debug.Log(treeItem + ":" + tempList);

        if (tempList is bool) return result;
        if (tempList == null) return result;

        var tempList2 = (IList) tempList;
        for (var i = 0; i < tempList2.Count; i++) {
            var item = tempList2[i];
            result.Add(item);

            if (deep) {
                //Debug.Log("deep start <" + (item==null) + ">");
                result.AddRange(item.GetChildrenTreeItem(itemType, true));
                //Debug.Log("deep end");
            }

            item.xSetField("m_Depth", 0, itemType);
            item.xSetField("m_Children", new List<object>().xToListT(itemType), itemType);
        }
        return result;
    }

    internal static int[] GetFilterInstanceIDs(Func<GameObject, bool> func) {
        var list = new List<GameObject>();
        foreach (var child in TransformX.RootT) {
            AppendChildren(child, ref list, true);
        }

        var result = new List<int>();
        for (var i = 0; i < list.Count; i++) {
            var c = list[i];
            var isValid = func(c);
            if (isValid) result.Add(c.GetInstanceID());

            //Debug.Log(i + ":" + list[i] + ":::" + isValid);
        }

        return result.ToArray();
        //return (list.Where(item => func(item)).Select(item => item.GetInstanceID())).ToArray();
    }

    internal static void SetSearchFilter(this EditorWindow window, int[] instanceIDs, string title) {
        if (window == null) {
            WindowX.xClearDefinitionCache();
            window = WindowX.Hierarchy;
        }

        if (instanceIDs.Length == 0) {
            window.xInvoke(
                "SetSearchFilter", null, null,
                new object[] {"Hierarchy2tempfixemptysearch", SearchableEditorWindow.SearchMode.All, false});
            window.xSetSearchFilterTerm("iso:" + title);
            return;
        }

/*#if UNITY_4_6
        Debug.Log("Before");
        
        //var sf = typeof(SearchableEditorWindow).xInvoke("CreateFilter", null, null, "iso:" +title, SearchableEditorWindow.SearchMode.All);
        window.xInvoke("SetSearchFilter", null, null, "iso:" + title, SearchableEditorWindow.SearchMode.All, false);
        window.xSetField("m_HasSearchFilterFocus", true);

        var treeView        = window.xGetField("m_TreeView");
        var ds              = treeView.xGetProperty("data");






        window.Repaint();

        Debug.Log("After");*/

#if UNITY_4_5 || UNITY_4_6  || UNITY_5
        //var treeViewSrcT    = "UnityEditor.TreeViewDataSource".xGetTypeByName("UnityEditor");
        var treeViewItemT = "UnityEditor.TreeViewItem".xGetTypeByName("UnityEditor");
        var treeView = WindowX.Hierarchy.xGetField("m_TreeView");
        var treeViewData = treeView.xGetProperty("data");
        var rootItem = treeViewData.xGetField("m_RootItem");
        var children = rootItem.GetChildrenTreeItem(treeViewItemT, true);
        var expandIds = treeViewData.xInvoke("GetExpandedIDs"); //save the expand state to restore

        foreach (var t in children) { // expand all children
            if (t != null) treeViewData.xInvoke("SetExpandedWithChildren", null, null, t, true);
        }

	    //Debug.Log("ids :: " + instanceIDs.Length);

        var children1 = (IList) treeViewData.xInvoke("GetVisibleRows");
        var childrenList = treeViewItemT.xNewListT();
        for (var i = 0; i < children1.Count; i++) {
            var child = children1[i];

            if (instanceIDs.Contains((int) child.xGetField("m_ID", treeViewItemT))) {
                child.xSetField("m_Depth", 0, treeViewItemT);
                childrenList.Add(child);
            }
        }

        // restore the expand state for children
        treeViewData.xInvoke("SetExpandedIDs", null, null, expandIds);

        window.xInvoke(
            "SetSearchFilter", null, null, new object[] {"iso:" + title, SearchableEditorWindow.SearchMode.All, false});
        treeViewData.xSetField("m_VisibleRows", childrenList.xToListT(treeViewItemT));
        treeView.xSetField("m_AllowRenameOnMouseUp", false);
        treeView.xInvoke("Repaint");
#else
	    var TBaseProjectWindow = "UnityEditor.BaseProjectWindow".xGetTypeByName("UnityEditor");
	    var TFilteredHierarchy = "UnityEditor.FilteredHierarchy".xGetTypeByName("UnityEditor");

	    //window.SetSearchFilter("iso:" + title);

			var instIDsParams = new object[] { instanceIDs };
	    	var fh = window.xGetField("m_FilteredHierarchy", TBaseProjectWindow);
	    var sf = (SearchFilter)fh.xGetField("m_SearchFilter", TFilteredHierarchy);

			sf.ClearSearch();
			sf.referencingInstanceIDs = instanceIDs;
	    fh.xInvoke("SetResults", TFilteredHierarchy, null, instIDsParams);

	    var arr = (object[])fh.xGetProperty("results", TFilteredHierarchy, null);//(FilteredHierarchyType.GetProperty("results").GetValue(fh, null));
			var list = new List<int>();

			//patch
			var nMissing = 0;
			foreach (var t in arr) {
				if (t == null) {
					nMissing++;
					continue;
				}
				var id = (int)t.xGetField("instanceID");
				if (!list.Contains(id)) list.Add(id);
			}

			if (nMissing > 0) Debug.LogWarning("Filtered result may not be correct, missing " + nMissing + " results, please help report it to unity3d@vietlab.net");
			instanceIDs = list.ToArray();

			//reapply
			sf.ClearSearch();
			sf.referencingInstanceIDs = instanceIDs;
	    fh.xInvoke("SetResults", TFilteredHierarchy, null, new object[] { instanceIDs });
			window.Repaint();
#endif
    }

    internal static bool IsExpanded(this GameObject go) {
        var mExpand =

#if UNITY_4_5 || UNITY_4_6
        (int[]) WindowX.Hierarchy.xGetField("m_TreeView").xGetProperty("data").xInvoke("GetExpandedIDs");
#else 
        (int[]) WindowX.Hierarchy.xGetField(
            "m_ExpandedArray", "UnityEditor.BaseProjectWindow".xGetTypeByName("UnityEditor")
        );
#endif

        return mExpand.Contains(go.GetInstanceID());
    }

    internal static bool IsRenaming() {
        var oFocus = EditorWindow.focusedWindow;

#if UNITY_4_5 || UNITY_4_6 || UNITY_5
        var result = false;
        var tvState = WindowX.Hierarchy.xGetField("m_TreeViewState");
        if (tvState != null) {
            var overlay = tvState.xGetField("m_RenameOverlay");
            if (overlay != null) result = (bool) overlay.xGetField("m_IsRenaming");
        }
#else 
            var hWindow = WindowX.Hierarchy;
	    var type = "UnityEditor.BaseProjectWindow".xGetTypeByName("UnityEditor");
	    var result = (int)hWindow.xGetField("m_RealEditNameMode", type) == 2;
#endif

        if (oFocus != null && oFocus != EditorWindow.focusedWindow) oFocus.Focus();

        return result;
    }

    internal static void Rename(this GameObject go) {
        var hWindow = WindowX.Hierarchy;

        if (Event.current != null && Event.current.keyCode == KeyCode.Escape) {
            renameGO = null;
            renameStep = 0;
            hWindow.Repaint();
            return;
        }

        if (renameGO != go) {
            renameGO = go;
            renameStep = 2;
        }

        Debug.Log("Rename : " + go + ":" + renameStep);

        if (!IsRenaming()) {
            //not yet in edit name mode, try to do it now
            Selection.activeGameObject = go;

#if UNITY_4_5 || UNITY_4_6  || UNITY_5
            var treeView = WindowX.Hierarchy.xGetField("m_TreeView");
            var data = treeView.xGetProperty("data");
            var gui = treeView.xGetProperty("gui");
            var item = data.xInvoke("FindItem", null, null, go.GetInstanceID());

            if (item != null && gui != null) gui.xInvoke("BeginRename", null, null, item, 0f);
#else
                var property = new HierarchyProperty(HierarchyType.GameObjects);
                property.Find(go.GetInstanceID(), null);

	        hWindow.xInvoke("BeginNameEditing", TypeX.BaseProjectWindowT, null, go.GetInstanceID());
	        hWindow.xSetField("m_NameEditString", go.name, TypeX.BaseProjectWindowT); //name will be missing without this line
                hWindow.Repaint();
#endif
        } else {
            if (Event.current == null) {
                renameStep = 2;
                //Debug.Log("How can Event.current be null ?");
                return;
            }

            if (Event.current.type == EventType.repaint && renameStep > 0) {
                renameStep--;
                //hWindow.Repaint();
            }

            if (Event.current.type != EventType.repaint && renameStep == 0) renameGO = null;
        }
        //}
    }

    //-------------------------------- FLAG ----------------------------

    internal static void SetDeepFlag(this GameObject go, HideFlags flag, bool value, bool includeMe = true,
        bool recursive = true) {
        if (includeMe) go.xSetFlag(flag, value);
        foreach (Transform t in go.transform) {
            if (recursive) SetDeepFlag(t.gameObject, flag, value);
            else t.gameObject.xSetFlag(flag, value);
        }
    }

    internal static bool HasFlagChild(this GameObject go, HideFlags flag) {
        return go.transform.xGetChildren<Transform>()
            .Any(item => item.gameObject.xGetFlag(flag));
    }

    internal static bool HasFlagChild(this List<GameObject> list, HideFlags flag) {
        //var has = false;

        /*foreach (var child in list)
        {
            child.ForeachChild2(child2 =>
            {
                has = child2.GetFlag(flag);
                return !has;
            });
            if (has) break;
        }*/

        return list.Any(item => item.HasFlagChild(flag));
    }

    internal static bool HasFlag(this List<GameObject> list, HideFlags flag) {
        var hasActive = false;
        foreach (var go in list) {
            hasActive = go.xGetFlag(flag);
            if (hasActive) break;
        }
        return hasActive;
    }

    internal static void SetDeepFlag(this List<GameObject> list, bool value, HideFlags flag, bool includeMe) {
        foreach (var go in list) {
            go.SetDeepFlag(flag, value, includeMe);
        }
    }

    //-------------------------------- ACTIVE ----------------------------

    internal static bool HasActiveChild(this GameObject go) {
        var has = false;
        go.xForeachChild2(
            child => {
                has = child.activeSelf;
                return !has;
            });
        return has;
    }

    internal static bool HasGrandChild(this GameObject go) {
        var has = false;
        go.xForeachChild2(
            child => {
                has = child.transform.childCount > 0;
                return !has;
            });
        return has;
    }


    internal static bool HasActiveSibling(this GameObject go) {
        var has = false;
        go.xForeachSibling2(
            sibl => {
                has = sibl.activeSelf;
                return !has;
            });
        return has;
    }

    internal static bool HasActiveParent(this GameObject go) {
        var has = false;
        go.xForeachParent2(
            p => {
                has = p.activeSelf;
                return !has;
            });
        return has;
    }

    internal static bool HasActive(this List<GameObject> list) {
        bool hasActive = false;
        foreach (var go in list) {
            hasActive = go.activeSelf;
            if (hasActive) break;
        }
        return hasActive;
    }

    internal static void SetActive(this List<GameObject> list, bool value, bool deep)
    {
        foreach (var go in list)
        {
            if (deep) go.hSetActiveChildren(value, false);
            if (go.activeSelf != value) go.SetActive(value);
        }
    }

    internal static void SetGOStatic(GameObject go, bool value, bool deep = false, string undoKey = "h@-auto") {
        if (undoKey == "h@-auto") undoKey = value ? "Static" : "UnStatic";

        go.xRecordUndo(undoKey, true);
        go.isStatic = value;

        if (deep) {
            go.xForeachChild(
                child => {
                    child.xRecordUndo(undoKey, true);
                    child.isStatic = value;
                }, true);
        }
    }

    internal static void ToggleStatic(this GameObject go, string undoKey = "h@-auto") {
        SetGOStatic(go, !go.isStatic, false, undoKey);
    }

    internal static void SetStatic(this GameObject go, bool invertMe, bool smartInvert) { //smart mode : auto-deepLock
        var isStatic = go.isStatic;
        var key = isStatic ? "Static" : "UnStatic";

        go.xForeachSelected(
            (item, idx) => SetGOStatic(
                item, (!invertMe || (item == go)) ? !isStatic : isStatic, // invert static 
                idx == -1 && smartInvert == isStatic, // deep-lock if isLock=true
                key));
    }

    internal static void InvertStatic(this GameObject go) {
        go.xForeachSelected((item, idx) => item.ToggleStatic("Invert Static"));
    }

    internal static void ToggleSiblingStatic(this GameObject go, bool deep = false) {
        var isStatic = go.isStatic;
        var key = isStatic ? "Static siblings" : "UnStatic siblings";

        go.ToggleStatic(key);
        go.xForeachSibling(sibl => sibl.ToggleStatic(key));
    }

    internal static void RecursiveStatic(bool value) {
        var key = value ? "Recursive Static" : "Recursive Unstatic";
        TransformX.RootT.ForEach(t => SetGOStatic(t.gameObject, value, true, key));
    }

    internal static Transform NextSibling(this Transform t) {
        if (t == null) {
            Debug.LogWarning("Transform should not be null ");
            return null;
        }

        var p = t.parent;
        if (t.parent != null) {
            var cnt = 0;
            while (p.GetChild(cnt) != t) cnt++;
            return (cnt < p.childCount - 1) ? p.GetChild(cnt + 1) : p.GetChild(0);
        }

        var rootList = TransformX.RootT;
        var idx = rootList.IndexOf(t);
        if (idx != -1) return rootList[(idx < rootList.Count - 1) ? idx + 1 : 0].transform;
        Debug.LogWarning("Root Object not in RootList " + t + ":" + rootList);
        return t;
    }

    internal static int numScript(this GameObject go)
    {
        if (go == null) return 0; //destroyed

        var list = go.GetComponents<MonoBehaviour>();
        if (list.Length == 0) return 0;

        var paths = h2Settings.ignoreScriptPaths;
        var cnt = 0;
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == null) continue;
            var mono = MonoScript.FromMonoBehaviour(list[i]);
            var monoPath = AssetDatabase.GetAssetPath(mono);

            for (var j = 0; j < paths.Length; j++)
            {
                if (monoPath.Contains(paths[j]))
                {
                    list[i] = null;
                    //Debug.Log("Ignoring ... " + monoPath);
                    break;
                }
            }

            if (list[i] != null) cnt++;
        }

        return cnt;
    }

    internal static int numScriptMissing(this GameObject go) {
        if (go == null) return 0;
        var list = go.GetComponents<MonoBehaviour>();
        var cnt = 0;
        if (list.Any(item => item == null)) cnt++;
        return cnt;
    }

    internal static void AppendChildren(Transform t, ref List<GameObject> list, bool deep = false) {
        list.Add(t.gameObject);
        foreach (Transform child in t) {
            if (child != t) {
                list.Add(child.gameObject);
                if (deep && child.childCount > 0) AppendChildren(child, ref list, true);
            }
        }
    }


    internal static string GetNewName(this string baseName, Transform p, string suffix = "") {
        var name = baseName.Contains(suffix) ? baseName : (baseName + suffix);
        if (p == null) return name;
        var namesList = new string[p.childCount];
        for (var i = 0; i < namesList.Length; i++) {
            namesList[i] = p.GetChild(i)
                .name;
        }

        if (!namesList.Contains(name)) return name;
        var counter = 1;
        while (namesList.Contains(name + counter)) counter++;
        return name + counter;
    }

    internal static void CreateEmptyChild(GameObject go, bool useEvent = false) {
        //var willPing = go.transform.childCount == 0 || !go.IsExpanded();

        TransformX.xNewTransform(name: "New".GetNewName(go.transform, "Empty"), undo: "NewEmptyChild", p: go.transform);
        //.PingAndUseEvent(willPing, useEvent);

        if (useEvent) Event.current.Use();
    }

    internal static void CreateEmptySibling(GameObject go, bool useEvent = false) {
        TransformX.xNewTransform(
            name: "New".GetNewName(go.transform, "Empty"), undo: "NewEmptySibling", p: go.transform.parent);
        //.PingAndUseEvent(false, useEvent);
        if (useEvent) Event.current.Use();
    }

    internal static void CreateParentAtMyPosition(GameObject go, bool useEvent = false) {
        Selection.activeGameObject = go;
        var goT = go.transform;
        var p = "NewEmpty".GetNewName(goT.parent, "_parent").xNewTransform(undo: "NewParent1", p: goT.parent, pos: goT.localPosition, scl: goT.localScale, rot: goT.localEulerAngles);

        goT.xReparent("NewParent1", p);
        //p.gameObject.RevealChildrenInHierarchy();

        if (useEvent) Event.current.Use();
    }

    internal static void CreateParentAtOrigin(GameObject go, bool useEvent = false) {
        Selection.activeGameObject = go;
        var goT = go.transform;
        var p = TransformX.xNewTransform(
            name: "NewEmpty".GetNewName(goT.parent, "_parent"), undo: "NewParent2", p: goT.parent);

        goT.xReparent("NewParent2", p);
        //p.gameObject.RevealChildrenInHierarchy();
        //p.Ping();
        if (useEvent) Event.current.Use();
    }

    public static void RevealChildrenInHierarchy(this GameObject go, bool pingMe = false) {
        if (go.transform.childCount == 0) return;
#if UNITY_4_5 || UNITY_4_6
        var tree = WindowX.Hierarchy.xGetField("m_TreeView");
        //var c = go.transform.childCount > 0 ? go.transform.GetChild(0).gameObject : go;
        //tree.Invoke("RevealNode", null, null, c.GetInstanceID());
        var item = tree.xInvoke("FindNode", null, null, go.GetInstanceID());
        if (item != null) {
            tree.xGetProperty("data")
                .xInvoke("SetExpanded", "UnityEditor.ITreeViewDataSource".xGetTypeByName("UnityEditor"), null, item, true);
        }
        //vlbEditorWindow.Hierarchy.Repaint();
#else
            foreach (Transform child in go.transform)
            {
                if (child == go.transform) continue;
                WindowX.Hierarchy.xInvoke("PingTargetObject", null, null, new object[] { child.GetInstanceID() });
                if (pingMe) WindowX.Hierarchy.xInvoke("PingTargetObject", null, null, new object[] { go.GetInstanceID() });
                return;
            }
#endif
    }
}
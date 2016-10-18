using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using vietlabs;
using Object = UnityEngine.Object;

internal static class h2Api {

    internal static bool IsMultiScene(this GameObject go) {
        return go != null && go.transform.IsMultiScene();
    }

    internal static bool IsMultiScene(this Transform t) {
        if (t == null || t.parent != null) return false; //Multiscene Objects must be Root
        var c = t.GetComponent("Multiscene");
        return c != null;
    }

    ///----------------------------------- LOCK ------------------------------------------------
    internal static void hSetLock(this GameObject go, bool value, bool deep = false, string undoKey = "h@-auto") {
        if (undoKey == "h@-auto") undoKey = value ? "Lock" : "UnLock";
        var usePatch = true; //TEMP PATCH : undo lock cause unity crash, wait for UNITY to fix it

        
        if (!usePatch) go.xRecordUndo(undoKey, true);
        go.xSetFlag(HideFlags.NotEditable, value);

        foreach (var c in go.GetComponents<Component>()) {
            if (!(c is Transform)) {
                c.xSetFlag(HideFlags.NotEditable, value);
                c.xSetFlag(HideFlags.HideInHierarchy, value);
            }
        }

        if (deep) {
            go.xForeachChild(
                child => {
                    child.xRecordUndo(undoKey, true);
                    child.xSetFlag(HideFlags.NotEditable, value);
                    foreach (var c in child.GetComponents<Component>()) {
                        if (!(c is Transform)) {
                            if (!usePatch) c.xSetFlag(HideFlags.NotEditable, value);
                            c.xSetFlag(HideFlags.HideInHierarchy, value);
                        }
                    }
                }, true);
        }
    }

    internal static void hToggleLock(this GameObject go, string undoKey = "h@-auto") {
        go.hSetLock(!go.xGetFlag(HideFlags.NotEditable), false, undoKey);
    }

    /*static internal void SetNaiveLock(this GameObject go, bool value, bool deep, bool invertMe) {
        var isLock = go.GetFlag(HideFlags.NotEditable);
        go.ForeachSelected((item, idx) => SetLock(item,
            (!invertMe || (item == go)) ? !isLock : isLock, deep)
        );
    }*/

/*    internal static void hRemoveMissingBehaviour(this GameObject go) {
        Debug.Log("hRemoveMissing ... " + go);

        var cList = go.GetComponents<Component>();
        for (var i = 0; i < cList.Length; i++) {
            if (cList[i] != null) continue;

            Debug.Log(i + ":" + cList[i]);
            var editor = Editor.CreateEditor(cList[i], typeof(NullEditor));
            Debug.LogWarning("missing found " + cList[i] +":"+ editor);
        }
    }*/

    internal static void hSetSmartLock(this GameObject go, bool invertMe, bool smartInvert) { //smart mode : auto-deepLock
        var isLock = go.xGetFlag(HideFlags.NotEditable);
        var key = isLock ? "Lock" : "Unlock";

        //Debug.Log("hSetSmartLock ... " + go);
        go.xForeachSelected(
            (item, idx) => item.hSetLock(
                (!invertMe || (item == go)) ? !isLock : isLock, // invert lock 
                idx == -1 && smartInvert == isLock, // deep-lock if isLock=true
                key));
    }

    internal static void hInvertLock(this GameObject go) {
        go.xForeachSelected((item, idx) => item.hToggleLock("Invert Lock"));
    }

    internal static void hToggleSiblingLock(this GameObject go, bool deep = false) {
        var isLock = go.xGetFlag(HideFlags.NotEditable);
        var key = isLock ? "Lock siblings" : "Unlock siblings";

        go.hToggleLock(key);
        go.xForeachSibling(sibl => sibl.hToggleLock(key));
    }

    internal static void hRecursiveLock(bool value) {
        var key = value ? "Recursive Lock" : "Recursive Unlock";
        TransformX.RootT.ForEach(rootGO => rootGO.gameObject.hSetLock(value, true, key));
    }

    ///----------------------------------- ACTIVE ------------------------------------------------
    internal static bool hSmartActiveGo(this GameObject go, bool value) {
        if (h2Settings.useDKVisible) {
            var c = go.GetComponent("dfControl");
            if (c != null) {
                c.xSetProperty("IsVisible", value);
                return true;
            }
        }
        go.SetActive(value);
        return false;
    }

    internal static bool hIsSmartActive(this GameObject go) {
        if (h2Settings.useDKVisible) {
            var c = go.GetComponent("dfControl");
            if (c != null) return (bool) c.xGetProperty("IsVisible");
        }
        return go.activeSelf;
    }

    internal static void hSetGOActive(this GameObject go, bool value, bool? activeParents = null,
        string undoKey = "h@-auto") {
        //activeParents == null : activeParents if setActive==true
        if (undoKey == "h@-auto") undoKey = value ? "Show GameObject" : "Hide GameObject";

        //if (!string.IsNullOrEmpty(undoKey)) Undo.RecordObject(go, undoKey);
        go.xRecordUndo(undoKey);
        go.SetActive(value);
        var smart = go.hSmartActiveGo(value);

        if (!smart && (activeParents ?? value) && !go.activeInHierarchy) {
            go.xForeachParent2(
                p => {
                    //if (!string.IsNullOrEmpty(undoKey)) Undo.RecordObject(p, undoKey);
                    p.xRecordUndo(undoKey);
                    p.SetActive(true);
                    return !p.activeInHierarchy;
                });
        }
    }

    internal static void hToggleActive(this GameObject go, bool invertMe, bool? activeParents = null) {
        var isActive = go.activeSelf;
        var key = isActive ? "Hide Selected GameObjects" : "Show Selected GameObjects";

        go.xForeachSelected(
            (item, idx) => item.hSetGOActive((!invertMe || (item == go)) ? !isActive : isActive, activeParents, key));
    }

    internal static void hSetActiveChildren(this GameObject go, bool value, bool? activeParents) {
        var key = value ? "Show Children" : "Hide Children";
        go.xForeachChild(child => child.hSetGOActive(value, activeParents, key), true);
        go.hSetGOActive(value, false, key);
    }

    internal static void hSetActiveSibling(this GameObject go, bool value, bool? activeParents = null) {
        var key = value ? "Show siblings" : "Hide siblings";
        go.xForeachSibling(item => item.hSetGOActive(value, activeParents, key));
        go.hSetGOActive(!value, false, key);
    }

    internal static void hSetActiveParents(this GameObject go, bool value) {
        var p = go.transform.parent;
        var key = value ? "Show Parents" : "Hide Parents";
        //if (go.activeSelf != value) go.SetActive(value);

        while (p != null) {
            if (p.gameObject.activeSelf != value) {
                p.gameObject.xRecordUndo(key);
                p.gameObject.SetActive(value);
            }
            p = p.parent;
        }
    }

    ///---------------------------------- SIBLINGS ------------------------------------------------
     
    internal static bool xIsCombined(this GameObject go) { return go.HasFlagChild(HideFlags.HideInHierarchy); }
    internal static void hSetCombine(this GameObject go, bool value, bool deep = false, string undoKey = "h@-auto") {
        if (undoKey == "h@-auto") undoKey = value ? "Combine GameObject" : "Expand GameObject";
        go.xForeachChild(
            child => {
                //Undo.RegisterCompleteObjectUndo(child, undoKey);
                child.xRecordUndo(undoKey, true);
                child.xSetFlag(HideFlags.HideInHierarchy, value);
            }, deep);

#if UNITY_4_5 || UNITY_4_6
        //workaround for Hierarchy not update
        var old = go.activeSelf;
        go.SetActive(!old);
        go.SetActive(old);
#endif
    }

    internal static void hToggleCombine(this GameObject go, bool deep = false) {
        var isCombined = go.xIsCombined();
        var key = isCombined ? "Combine Selected GameObjects" : "Expand Selected GameObjects";
        go.xForeachSelected((item, index) => item.hSetCombine(!isCombined, deep, key));
        /*if (isCombined && go.transform.childCount > 0) {
            go.transform.GetChild(0)
                .xPing();
        }*/
    }

    internal static void hToggleCombineChildren(this GameObject go) {
        var val = false;

        go.xForeachChild2(
            child => {
                val = child.xIsCombined();
                return !val;
            });

        var key = val ? "Expand Children" : "Combine Children";
        go.hSetCombine(false, false, key);
        go.xForeachChild(child => child.hSetCombine(!val, false, key));
    }

    internal static void hSetCombineSibling(this GameObject go, bool value) {
        var key = value ? "Expand Siblings" : "Combine siblings"; 

        go.hSetCombine(value, false, key);
        go.xForeachSibling(sibl => sibl.hSetCombine(!value, false, key));
        if (!value) go.RevealChildrenInHierarchy(true);
    }

    internal static void hRecursiveCombine(bool value) {
        var key = value ? "Recursive Combine" : "Recursive Expand";
        TransformX.RootT.ForEach(
            root => {
                var list = root.xGetChildren<Transform>(true);
                foreach (var child in list) {
                    child.xRecordUndo(key, true);
                }
                root.gameObject.SetDeepFlag(HideFlags.HideInHierarchy, value);
            });
    }

    ///------------------------ GOTO : SIBLING / PARENT / CHILD -------------------------------
    private static List<Transform> _pingList;

    internal static void hPingParent(this Transform t, bool useEvent = false) {
        var p = t.parent;
        if (p == null) return;

        //clear history when select other GO
        if (_pingList == null || (_pingList.Count > 0 && _pingList[_pingList.Count - 1].parent != t)) _pingList = new List<Transform>();

        _pingList.Add(t);
        p.xPingAndUseEvent(true, useEvent);
    }

    internal static void hPingChild(this Transform t, bool useEvent = false) {
        Transform pingT = null;

        if (_pingList == null) _pingList = new List<Transform>();

        if (_pingList.Count > 0) {
            var idx = _pingList.Count - 1;
            var c = _pingList[idx];
            _pingList.Remove(c);

            pingT = c;
        } else if (t.childCount > 0) pingT = t.GetChild(0);

        if (pingT != null) pingT.xPingAndUseEvent(true, useEvent);
    }

    internal static void hPingSibling(this Transform t, bool useEvent = false) {
        t.NextSibling()
            .xPingAndUseEvent(true, useEvent);
    }

    ///----------------------------------- CAMERA ---------------------------------------------------
    public static Camera ltCamera;

    public static CameraInfo ltCameraInfo;

    private static void hSceneCameraApply(bool orthor, Vector3 pos, Quaternion rot) {
        SceneViewX.orthographic = orthor;
        SceneViewX.m_Rotation = rot;
        SceneViewX.m_Position = pos;
        SceneViewX.Refresh();
    }

    private static void UpdateLookThrough() {
        if (ltCamera != null) {
            if (EditorApplication.isPaused) return;

            var sceneCam = SceneViewX.sceneCamera;
            var hasChanged = ltCamera.transform.position != sceneCam.transform.position
                             || ltCamera.orthographic != sceneCam.orthographic
                             || ltCamera.transform.rotation != sceneCam.transform.rotation;

            if (hasChanged) CameraLookThrough(ltCamera);
        } else EditorApplication.update -= UpdateLookThrough;
    }

    internal static void CameraLookThrough(Camera cam) {
        //Undo.RecordObject(cam, "LookThrough");
        var sceneCam = SceneViewX.sceneCamera;

        if (EditorApplication.isPlaying) {
            if (ltCameraInfo == null) {
                ltCameraInfo = new CameraInfo {
                    orthor = SceneViewX.orthographic,
                    mRotation = SceneViewX.m_Rotation,
                    mPosition = SceneViewX.m_Position
                };

                EditorApplication.update -= UpdateLookThrough;
                EditorApplication.update += UpdateLookThrough;
            }
        } else {
            ltCameraInfo = null;
            ltCamera = null;
        }

        sceneCam.CopyFrom(cam);
        var distance = SceneViewX.cameraDistance;
        hSceneCameraApply(
            cam.orthographic, cam.transform.position - (cam.transform.rotation*new Vector3(0f, 0f, -distance)),
            cam.transform.rotation);

        //Hierarchy2Utils.orthographic = cam.orthographic;
        //Hierarchy2Utils.m_Rotation = cam.transform.rotation;
        //Hierarchy2Utils.m_Position = cam.transform.position - (cam.transform.rotation * new Vector3(0f, 0f, -distance));
        //Hierarchy2Utils.Refresh();
    }

    internal static void CameraCaptureSceneView(Camera cam) {
        ltCamera = null;
        ltCameraInfo = null;

        //Undo.RecordObject(cam, "CaptureSceneCamera");
        cam.xRecordUndo("Capture Scene Camera");
        cam.CopyFrom(SceneViewX.sceneCamera);
    }

    internal static void ToggleLookThroughCamera(Camera c) {
        ltCamera = ltCamera == c ? null : c;

        if (ltCamera != null) CameraLookThrough(ltCamera);
        else if (ltCameraInfo != null) {
            hSceneCameraApply(ltCameraInfo.orthor, ltCameraInfo.mPosition, ltCameraInfo.mRotation);
            ltCameraInfo = null;
            ltCamera = null;
        }
    }

    ///----------------------------------- ISOLATE ---------------------------------------------------
    internal static void Isolate_MissingBehaviours(bool useEvent = false) {
        WindowX.Hierarchy.SetSearchFilter(
            HierarchyX.GetFilterInstanceIDs(item => item.numScriptMissing() > 0), "Missing");
        if (useEvent) Event.current.Use();
    }

    internal static void Isolate_ObjectsHasScript(bool useEvent = false) {
        WindowX.Hierarchy.SetSearchFilter(HierarchyX.GetFilterInstanceIDs(item => item.numScript() > 0), "Script");
        if (useEvent) Event.current.Use();
    }

    internal static void Isolate_SelectedObjects(bool useEvent = false) {
        WindowX.Hierarchy.SetSearchFilter(Selection.instanceIDs, "Selected");
        if (useEvent) Event.current.Use();
    }

    internal static void Isolate_LockedObjects(bool useEvent = false) {
        WindowX.Hierarchy.SetSearchFilter(
            HierarchyX.GetFilterInstanceIDs(item => item.xGetFlag(HideFlags.NotEditable)), "Locked");
        if (useEvent) Event.current.Use();
    }

    internal static void Isolate_InActiveObjects(bool useEvent = false) {
        WindowX.Hierarchy.SetSearchFilter(HierarchyX.GetFilterInstanceIDs(item => !item.activeSelf), "InActive");
        if (useEvent) Event.current.Use();
    }

    internal static void Isolate_CombinedObjects(bool useEvent = false) {
        WindowX.Hierarchy.SetSearchFilter(
            HierarchyX.GetFilterInstanceIDs(item => item.HasFlagChild(HideFlags.HideInHierarchy)), "Combined");
        if (useEvent) Event.current.Use();
    }

    internal static void Isolate_ComponentType(Type t) {
        WindowX.Hierarchy.SetSearchFilter(
            HierarchyX.GetFilterInstanceIDs(item => (item.GetComponent(t) != null)), t.ToString());
    }

    internal static void Isolate_Component(Component c) {
        WindowX.Hierarchy.SetSearchFilter(
            HierarchyX.GetFilterInstanceIDs(item => (item.GetComponent(c.GetType()) != null)), c.xGetTitle(false));
    }

    internal static void Isolate_Layer(string layerName) {
        var layer = LayerMask.NameToLayer(layerName);
        WindowX.Hierarchy.SetSearchFilter(HierarchyX.GetFilterInstanceIDs(item => item.layer == layer), layerName);
    }

    internal static void Isolate_Tag(string tagName) {
        WindowX.Hierarchy.SetSearchFilter(HierarchyX.GetFilterInstanceIDs(item => (item.tag == tagName)), tagName);
    }

    ///----------------------------------- RESET TRANSFORM -------------------------------------------
    internal static void ResetLocalPosition(GameObject go) {
        Selection.activeGameObject = go;
        go.transform.xResetLocalPosition("ResetPosition");
    }

    internal static void ResetLocalRotation(GameObject go) {
        Selection.activeGameObject = go;
        go.transform.xResetLocalRotation("ResetRotation");
    }

    internal static void ResetLocalScale(GameObject go) {
        Selection.activeGameObject = go;
        go.transform.xResetLocalScale("ResetScale");
    }

    internal static void ResetTransform(GameObject go) {
        Selection.activeGameObject = go;
        go.transform.xResetLocalTransform("ResetTransform");
    }

    ///----------------------------------- CREATE GO -------------------------------------------
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
        var p = TransformX.xNewTransform(
            name: "NewEmpty".GetNewName(goT.parent, "_parent"), undo: "NewParent1", p: goT.parent,
            pos: goT.localPosition, scl: goT.localScale, rot: goT.localEulerAngles);

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
}

internal class CameraInfo {
    public bool orthor;
    public Vector3 mPosition;
    public Quaternion mRotation;
}
/*
------------------------------------------------
    Hierarchy2 for Unity3d by VietLabs
------------------------------------------------
    version : 1.3.10
    release : 18 Dec 2014
    require : Unity3d 4.3+
    website : http://vietlabs.net/hierarchy2
--------------------------------------------------

Powerful extension to add the most demanding features
to Hierarchy panel packed in a single, lightweight,
concise and commented C# source code that fully 
integrated into Unity Editor 

--------------------------------------------------
*/

using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Component = UnityEngine.Component;
using Object = UnityEngine.Object;

namespace vietlabs {
    [InitializeOnLoad]
    internal class Hierarchy2 {
        //internal static bool HLFull = false;
        
        internal static bool UseOldDepthStyle       = false;

        internal static float DepthBarWidth         = 3f;

       /* [MenuItem("Window/Hierarchy2/Reset")]
        private static void Reset() {
            TransformX.RootT.ForEach(
                t => {
                    t.gameObject.hideFlags = 0;
                    t.gameObject.xForeachChild(child => { child.hideFlags = 0; });
                });
        }*/

        //---------------------------- ROOT CACHE ---------------------------------------

        static Hierarchy2() {
            //settings = h2Settings.LoadConfig();
            //EditorApplication.hierarchyWindowChanged    += h2Api.UpdateRoot;
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
            EditorApplication.playmodeStateChanged += OnPlayModeChanged;

            Undo.undoRedoPerformed += () => TransformX.RootT.ForEach(
                t => {
                    if (t == null) return;

                    t.gameObject.xForeachChild(
                        child => {
                            child.xToggleFlag(HideFlags.NotEditable);
                            child.xToggleFlag(HideFlags.NotEditable);
                        }, true);

                    WindowX.Hierarchy.Repaint();
                }
            );
        }

        private static void OnPlayModeChanged() {
            if (!EditorApplication.isPlaying && !EditorApplication.isPaused) { //stop playing
                h2Api.ltCamera = null;
                h2Api.ltCameraInfo = null;
            }
        }

        //-------------------------------- CONTEXT ---------------------------------------

        internal static void Context_BuiltIn(GenericMenu menu, GameObject go, string category = "") {
            menu.xAdd(
                category + "Copy %C", () => {
                    Selection.activeGameObject = go;
                    Unsupported.CopyGameObjectsToPasteboard();
                });

            menu.xAdd(
                category + "Paste %V", () => {
                    Selection.activeGameObject = go;
                    Unsupported.PasteGameObjectsFromPasteboard();
                });

            menu.xAddSep(category);
            menu.xAdd(
                category + "Rename _F2", () => {
                    Selection.activeGameObject = go;
                    go.Rename();
                });
            menu.xAdd(
                category + "Duplicate %D", () => {
                    Selection.activeGameObject = go;
                    Unsupported.DuplicateGameObjectsUsingPasteboard();
                });

            menu.xAdd(
                category + "Delete _Delete", () => {
                    Selection.activeGameObject = go;
                    Unsupported.DeleteGameObjectSelection();
                });
        }

        internal static void Context_Basic(GenericMenu menu, GameObject go, string category = "Edit/") {
            /*menu.xAdd(category + "Remove missing Behaviours", () => {
                var listT = TransformX.RootT;
                for (var i = 0; i < listT.Count; i++) {
                    var children = listT[i].xGetChildren<Transform>(true, true);
                    foreach (var t in children) {
                        t.gameObject.hRemoveMissingBehaviour();
                    }
                }

                WindowX.Hierarchy.Repaint();
            });

            menu.xAddSep(category);*/

            //basic tools
            menu.xAdd(category + "Lock _L", () => go.hSetSmartLock(false, false), go.xIsLock());
            menu.xAdd(category + "Visible _A , V", () => go.hToggleActive(false), go.activeSelf);
            menu.xAdd(category + "Combine Children _C", () => go.hToggleCombine(), go.xIsCombined());

            //goto tools
            menu.xAddSep(category);
            menu.xAdd(category + "Goto Parent _[", () => go.transform.hPingParent());
            menu.xAdd(category + "Goto Child _]", () => go.transform.hPingChild());
            menu.xAdd(category + "Goto Sibling _\\", () => go.transform.hPingSibling());

            //transform tools
            Context_Transform(menu, go, category);
        }

        internal static void Context_Transform(GenericMenu menu, GameObject go, string category = "Transform/") {
            var lcPos = go.transform.localPosition != Vector3.zero;
            var lcScl = go.transform.localScale != Vector3.one;
            var lcRot = go.transform.localRotation != Quaternion.identity;

            var cnt = (lcPos ? 1 : 0) + (lcScl ? 1 : 0) + (lcRot ? 1 : 0);
            if (cnt > 0) menu.xAddSep(category);

            menu.xAddIf(lcPos, category + "Reset Position #P", () => h2Api.ResetLocalPosition(go));
            menu.xAddIf(lcRot, category + "Reset Rotation #R", () => h2Api.ResetLocalRotation(go));
            menu.xAddIf(lcScl, category + "Reset Scale #S", () => h2Api.ResetLocalScale(go));

            if (cnt > 0) menu.xAddSep(category);
            menu.xAddIf(cnt > 0, category + "Reset Transform #T", () => h2Api.ResetTransform(go));
        }

        internal static void Context_Special(GenericMenu menu, GameObject go, string category = "Edit/") {
            // Prefab specific
            //var isPrefab = PrefabUtility.GetPrefabObject(go) != null;

            //Debug.Log("--->"+PrefabUtility.GetPrefabObject(go) +":"+ isPrefab);

            var t = PrefabUtility.GetPrefabType(go);

            if (t != PrefabType.None) {
                menu.xAddSep(category);
                menu.xAdd(
                    category + "Select Prefab",
                    (t == PrefabType.MissingPrefabInstance) ? (Action) null : go.xSelectPrefab);
                menu.xAdd(category + "Break Prefab #B", () => go.xBreakPrefab());
            }

            // Camera specific
            var cam = go.GetComponent<Camera>();
            if (cam != null) {
                menu.xAddSep(category);
                menu.xAdd(
                    category + ((h2Api.ltCamera != null) ? "Stop " : "") + "Look through #L",
                    () => h2Api.ToggleLookThroughCamera(cam));
                menu.xAdd(category + "Capture SceneView #C", () => h2Api.CameraCaptureSceneView(cam));
            }
        }

        internal static void Context_Components(GenericMenu menu, GameObject go) {
            //add scripts & components
            if (listScript != null && listScript.Count > 0) {
                var baseName = "Script _" + listScript.Count + "/";
                var cnt = 0;
                foreach (var item in listScript) {
                    var typeC = item.Key;
                    var name = ObjectNames.NicifyVariableName(typeC.Name);
                    menu.xAdd(baseName + name + " _" + item.Value, () => h2Api.Isolate_ComponentType(typeC));
                    cnt += item.Value;
                }

                menu.xAddSep(baseName + "");
                menu.xAdd(baseName + "Total " + cnt + " MonoBehaviours", null);
            }

            if (listScript != null && listComponent.Count>0) {
                var baseName = "Component _"+listComponent.Count+"/";
                var cnt = 0;
                foreach (var item in listComponent) {
                    var typeC = item.Key;
                    var name = ObjectNames.NicifyVariableName(typeC.Name);
                    menu.xAdd(baseName + name + " _" + item.Value, () => h2Api.Isolate_ComponentType(typeC));
                    cnt += item.Value;
                }
                menu.xAddSep(baseName + "");
                menu.xAdd(baseName + "Total " + cnt + " components", null);
            }


            /*var listTemp = go.GetComponents<Component>()
                .ToList();
            var scripts = new List<MonoBehaviour>();
            var compList = new List<Component>();
            var missing = 0;

            foreach (var c in listTemp) {
                if (c is Transform) continue;

                if (c == null) {
                    missing++;
                    continue;
                }

                if (c is MonoBehaviour) {
                    scripts.Add((MonoBehaviour) c);
                    continue;
                }

                compList.Add(c);
            }

            var total = scripts.Count + compList.Count + missing;
            var prefix = "Components [" + (total) + "]/";

            if (scripts.Count > 0) {
                foreach (var script in scripts) {
                    var behaviour = script;
                    var title = prefix + behaviour.xGetTitle() + "/";
                    menu.xAdd(title + "Reveal", script.xPing);
                    menu.xAdd(title + "Edit", script.xOpenScript);
                    menu.xAddSep(title);
                    menu.xAdd(title + "Isolate", () => h2Api.Isolate_Component(behaviour));
                }
            }

            if (compList.Count > 0) {
                if (scripts.Count > 0) menu.xAddSep(prefix);

                foreach (var c in compList) {
                    var comp = c;
                    menu.xAdd(prefix + comp.xGetTitle(), () => h2Api.Isolate_Component(comp));
                }
            }

            if (missing > 0) {
                if (compList.Count + scripts.Count > 0) {
                    menu.xAddSep(prefix);
                    menu.xAdd(prefix + "+" + missing + " Missing Behaviour" + (missing > 1 ? "s" : ""), null);
                } else menu.xAdd("+" + missing + " Missing Behaviour" + (missing > 1 ? "s" : ""), null);
            }*/

        }

        internal static void Context_Create(GenericMenu menu, GameObject go, string category = "Create/") {
            menu.xAdd("New Empty Child #N", () => h2Api.CreateEmptyChild(go));
            menu.xAdd("New Empty Sibling", () => h2Api.CreateEmptySibling(go));
            menu.xAdd(category + "Parent", () => h2Api.CreateParentAtMyPosition(go));
            menu.xAdd(category + "Parent at Origin", () => h2Api.CreateParentAtOrigin(go));

            menu.xAddSep(category);

            var list = new[] {"Quad", "Plane", "Cube", "Cylinder", "Capsule", "Sphere"};
            var key = new[] {" #1", " #2", " #3", " #4", " #5", " #6"};
            var types = new[] {
                PrimitiveType.Quad, PrimitiveType.Cube, PrimitiveType.Sphere, PrimitiveType.Plane, PrimitiveType.Cylinder,
                PrimitiveType.Capsule
            };

            for (var i = 0; i < types.Length; i++) {
                var type = types[i];
                var name = list[i];

                menu.xAdd(
                    category + name + key[i], () => {
                        Selection.activeGameObject = go;
                        TransformX.xNewPrimity(type, "New".GetNewName(go.transform, name), "New" + name, go.transform);
                    });
            }
        }


        internal static bool listCSDirty = true;
        internal static int missingCount;
        internal static Dictionary<Type, int> listComponent;
        internal static Dictionary<Type, int> listScript;

        internal static void Context_Isolate(GenericMenu menu, GameObject go, string category = "Isolate/") {
            if (missingCount > 0) {
                menu.xAdd(category + "Missing Behaviours [" + missingCount +"] &M", () => h2Api.Isolate_MissingBehaviours());
            }

            menu.xAdd(category + "Has Behaviour &B", () => h2Api.Isolate_ObjectsHasScript());
            if (Selection.instanceIDs.Length > 1) menu.xAdd(category + "Selected Objects &S", () => h2Api.Isolate_SelectedObjects());
            menu.xAddSep(category);
            menu.xAdd(category + "Locked Objects &L", () => h2Api.Isolate_LockedObjects());
            menu.xAdd(category + "InActive Objects &I", () => h2Api.Isolate_InActiveObjects());
            menu.xAdd(category + "Combined Objects &Y", () => h2Api.Isolate_CombinedObjects());
            menu.xAddSep(category);
        }

        internal static void Context_LayerTag(GenericMenu menu, GameObject go) {
            var type = "UnityEditorInternal.InternalEditorUtility".xGetTypeByName("UnityEditor");
            var layers = (string[])(type.GetProperty("layers", BindingFlags.Static | BindingFlags.Public)
                .GetValue(null, null));
            var tags = (string[])(type.GetProperty("tags", BindingFlags.Static | BindingFlags.Public)
                .GetValue(null, null));

            var layerName = LayerMask.LayerToName(go.layer);
            var prefix = "Layer   [" + go.layer + (!string.IsNullOrEmpty(layerName) ? "  " + layerName : "") + "]/";
            
            for (var i = 0; i < layers.Length; i++) {
                menu.xAdd(prefix + layers[i], () => h2Api.Isolate_Layer(layers[i]));
            }

            var tagName = go.tag;
            for (var i = 0; i < tags.Length; i++) {
                menu.xAdd("Tag      [" + tagName + "]/" + tags[i], () => h2Api.Isolate_Tag(tags[i])); //i == tags.xIndexOf(go.tag)
            }
        }


        //-------------------------------- SHORTCUTS ---------------------------------------

        

        //-------------------------------- FUNCTIONS ---------------------------------------

        /*internal static void EditLock(Rect r, GameObject go) {
            const HideFlags flag = HideFlags.NotEditable;
            var isSet = go.xGetFlag(flag);

            GUI.DrawTexture(r, SkinX.IcoLock(isSet));
            var leftMouseDown = r.xLMB_isDown();

            var evt = Event.current;

            if (leftMouseDown) {
                if (leftMouseDown.withoutModifier) go.hSetSmartLock(false, false);
                else if (leftMouseDown.with_Alt) {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) go.hSetSmartLock(true, false);
                    else go.hToggleSiblingLock();
                } else if (leftMouseDown.with_Ctrl) {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) go.hToggleLock();
                    else go.hSetSmartLock(false, true);
                }

                if (evt.type == EventType.used) WindowX.Inspector.Repaint();
            }


            var rightMouseDown = r.xRMB_isDown();
            if (rightMouseDown.withoutModifier) { //right-Click
                var menu = new GenericMenu();

                menu.xAdd("Toggle Lock", go.hInvertLock);
                //menu.Add("Toggle Lock Children",	go.InvertLock);
                menu.xAddSep("");
                menu.xAdd(
                    TransformX.RootGOs()
                        .ToList()
                        .HasFlag(flag), "Recursive Unlock", "Recursive Lock", has => h2Api.hRecursiveLock(!has));
                menu.ShowAsContext();
            }
        }*/

        

        /*internal static void EditActive(Rect r, GameObject go) {
            if (go == null) return;
            var isSet = go.activeSelf;
            GUI.DrawTexture(r, SkinX.IcoEye(isSet));

            var leftMouseDown = r.xLMB_isDown();

            if (leftMouseDown) {
                if (leftMouseDown.withoutModifier) go.hToggleActive(false);
                else if (leftMouseDown.with_Alt) {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) go.hToggleActive(true);
                    else go.hSetActiveSibling(isSet, false);
                } else if (leftMouseDown.with_Ctrl) {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) {
                        //go.ToggleActive(false);
                        //Toggle active me only
                    } else go.hSetActiveChildren(!isSet, false);
                }
            }

            var rightMouseDown = r.xRMB_isDown();
            if (rightMouseDown.withoutModifier) {
                Event.current.Use();
                var menu = new GenericMenu();
                if (go.xHasChild()) {
                    menu.xAdd(
                        go.HasActiveChild(), "Hide children", "Show children", has => go.hSetActiveChildren(!has, false));
                    menu.xAddIf(
                        go.HasGrandChild(), go.HasActiveChild(), "Hide all children", "Show all children",
                        has => go.hSetActiveChildren(!has, false));
                }
                menu.xAddIf(
                    go.xHasSibling(), go.HasActiveSibling(), "Hide siblings", "Show siblings",
                    has => go.xForeachSibling(item => item.SetActive(!has)));
                if (menu.GetItemCount() > 0) menu.xAddSep(null);
                if (go.transform.parent != null) menu.xAdd(go.HasActiveParent(), "Hide parents", "Show parents", (has) => go.hSetActiveParents(!has));
                menu.xAdd(
                    TransformX.RootGOs()
                        .ToList()
                        .HasActive(), "Recursive Hide", "Recursive Show", (has) => TransformX.RootGOs()
                            .ToList()
                            .SetActive(!has, true));
                menu.ShowAsContext();
            }
        }*/


        private static h2Active h2v;
        private static h2Lock h2l;
        private static h2Static h2s;
        private static h2Combine h2c;
        internal static void EditVisible(Rect r, GameObject go) {
            if (h2v == null) h2v = new h2Active();
            h2v.Draw(r, go);
        }
        internal static void EditLock2(Rect r, GameObject go) {
            if (h2l == null) h2l = new h2Lock();
            h2l.Draw(r, go);
        }
        internal static void EditStatic2(Rect r, GameObject go) {
            if (h2s == null) h2s = new h2Static();
            h2s.Draw(r, go);
        }


        /*internal static void EditCombine(Rect r, GameObject go) {
            if (go == null) return;
            const HideFlags flag = HideFlags.HideInHierarchy;
            var count = go.transform.childCount;
            if (count == 0) return; //don't display childCount if GO does not contains child

            //calculate size needed for display childCount text
            var isSet = go.HasFlagChild(flag);
            var w = (count < 10 ? 14 : count < 100 ? 18 : count < 1000 ? 28 : 33) + (isSet ? 6 : 0);
            r.x += r.width - w;
            r.width = w;

            var countStr = count < 1000 ? (string.Empty + count) : "999+";

            if (isSet) GUI.Label(r, countStr, EditorStyles.miniButtonMid);
            else GUI.Label(r, countStr);

            var leftMouseDown = r.xLMB_isDown();
            if (leftMouseDown) {
                if (leftMouseDown.withoutModifier) go.hToggleCombine();
                if (leftMouseDown.with_Alt) go.hSetCombineSibling(!isSet);
                if (leftMouseDown.with_Ctrl) go.hToggleCombineChildren();
            }

            if (r.xRMB_isDown()
                .withoutModifier) {
                var menu = new GenericMenu();
                menu.xAdd(
                    TransformX.RootGOs()
                        .ToList()
                        .HasFlagChild(flag), "Recursive Expand", "Recursive Combine",
                    has => h2Api.hRecursiveCombine(!has));
                menu.ShowAsContext();
            }
        }*/

        internal static void EditCombine2(Rect r, GameObject go) {
            if (go == null || go.transform.childCount == 0) return;
	        if (h2c == null) h2c = new h2Combine();
            h2c.Draw(r, go);
        }

        internal static void EditDepth(Rect r, GameObject go) {
            var c = go.xParentCount()/* - (HLFull ? 1 : 0)*/;

            if (c < 0) return; //don't highlight level 0 on full mode

            

            /*if (HLFull) {
                r.width = r.x + r.width;
                r.x = 0;
            } else {*/
                var w = c*DepthBarWidth + 4f;
                r.x = r.x + r.width - w;
                r.width = DepthBarWidth;
            /*}*/

            //Debug.Log("settings.h2_depthColors="+ settings.h2_depthColors);
            var depthColors = h2Settings.color_Depths;
            GUI.DrawTexture(r, depthColors[c % depthColors.Length].xGetTexture2D());
                //.Alpha(0.5f).Adjust(0.3f)
        }

        internal static void EditScript(Rect r, GameObject go) {
            var scriptColor = (go.numScriptMissing() > 0) ? h2Settings.ColorMissing.xProSkinAdjust(0.1f) :
                              (go.numScript() > 0) ? h2Settings.ColorValid.xProSkinAdjust(0.1f) : (Color?)null;

            if (scriptColor == null) return;

            var rr = UseOldDepthStyle ? r.w(20f).dx(-17f) : r.w(4f).dx(3f);
            GUI.DrawTexture(rr, scriptColor.Value.xGetTexture2D());
        }

        internal static void EditPrefab(Rect r, GameObject go) {
            var prefabType = PrefabUtility.GetPrefabType(go);
            if (prefabType == PrefabType.None) return;

            var isMissing = prefabType == PrefabType.MissingPrefabInstance;

            using (GuiX.DisableGroup(isMissing)) { 
                var color = (
                        prefabType == PrefabType.PrefabInstance ? ColorHSL.blue :
                        prefabType == PrefabType.ModelPrefabInstance ? ColorHSL.gray :
                        prefabType == PrefabType.MissingPrefabInstance ? ColorHSL.red :
                        prefabType == PrefabType.DisconnectedPrefabInstance ? ColorHSL.cyan :
                        ColorHSL.green
                    ).dS(-0.7f).xProSkinAdjust();

                var pname = "M";

                if (!isMissing) {
                    var rootP = PrefabUtility.FindRootGameObjectWithSameParentPrefab(go);
                    pname = rootP.name[0].ToString().ToUpperInvariant();
                }
 
                if (r.xMiniTag(pname, color)) {//isPrefabRoot ? "P" : ""
                    go.xSelectPrefab();
                }

            }
        }

        private static PropertyInfo layerInfo;
        private static PropertyInfo tagInfo;

        internal static void EditLayer(Rect r, GameObject go) {
            //var t = go.transform;
            //var p = t.parent;
            //if ((t.childCount==0 || !go.IsExpanded()) && p != null && go.layer == p.gameObject.layer && !Selection.gameObjects.Contains(go)) return;

            if (layerInfo == null) layerInfo = ("UnityEditorInternal.InternalEditorUtility")
                    .xGetTypeByName("UnityEditor").GetProperty("layers");

            var layers = (string[])layerInfo.GetValue(null, null);

            var idx = go.layer;

            for (var i = 1; i < layers.Length; i++) { //skip layer Default
                if (idx != LayerMask.NameToLayer(layers[i])) continue;
                var c = h2Settings.color_Layers[i % h2Settings.color_Layers.Length];
                //if (Selection.activeGameObject == go) c.a = 1f;
                r.xMiniTag(layers[i], c, false);

                /*using (GuiX.GUIBgColor(c)) {
                        
                }*/
                return;
            }

            if (idx >0) r.xMiniTag("L " + idx, ColorHSL.red.xProSkinAdjust());
        }

        internal static void EditTag(Rect r, GameObject go) {
            if (tagInfo == null) tagInfo = ("UnityEditorInternal.InternalEditorUtility")
                    .xGetTypeByName("UnityEditor").GetProperty("tags");

            var tags = (string[])tagInfo.GetValue(null, null);
            for (var i = 0; i < tags.Length; i++) {
                if (go.CompareTag("Untagged") || !go.CompareTag(tags[i])) continue;

                var c = h2Settings.color_Tags[i % h2Settings.color_Tags.Length];
                if (Selection.activeGameObject == go) c.a = 1f;

                //using (GuiX.GUIBgColor(c)) {
	            r.xMiniTag(go.tag, c, false);    
                //}
            }
        }

        internal static void EditStatic(Rect r, GameObject go) {
            if (go == null) return;
            GUI.DrawTexture(r, SkinX.IcoStatic(go.isStatic));
            var evt = Event.current;
            var leftMouseDown = r.xLMB_isDown();

            if (leftMouseDown) {
                if (leftMouseDown.noModifier) go.SetStatic(false, false);
                else if (leftMouseDown.with_Alt) {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) go.SetStatic(true, false);
                    else go.ToggleSiblingStatic();
                } else if (leftMouseDown.with_Ctrl) {
                    if (Selection.gameObjects.Contains(go) && Selection.gameObjects.Length > 1) go.ToggleStatic();
                    else go.SetStatic(false, false);
                }

                if (evt.type == EventType.used) WindowX.Inspector.Repaint();
            }

            if (r.xRMB_isDown()
                .noModifier) { //right-Click
                var menu = new GenericMenu();

                menu.xAdd("Toggle Static", go.InvertStatic);
                //menu.Add("Toggle Lock Children",	go.InvertLock);
                menu.xAddSep("");
                menu.xAdd(
                    TransformX.RootGOs()
                        .ToList()
                        .hHasStatic(), "Recursive Static", "Recursive UnStatic", has => HierarchyX.RecursiveStatic(!has));
                menu.ShowAsContext();
            }
        }

        //-------------------------------- GUI ---------------------------------------

        private static Camera LookThroughCam;
        
        private static int count;
        private static bool isVerbose;

	    private static bool drawing;
	    private static int lastDrawIndex;
	    private static float lastDrawY;
        //private static float selectedy;

        

        
        /*static void DelayRepaint() {
            EditorApplication.update -= DelayRepaint;
            WindowX.Hierarchy.Repaint();
        }*/





        //----------------------------------  PARENT INDICATOR   ------------------------------

        private static GameObject lastSelected;
        private static Transform[] parents;
        private static Transform multiSceneRoot;

        static void h2DrawParentIndicator(GameObject go, Rect rect) {
            if (_hasSearchFilter) return;

            if (lastSelected != Selection.activeGameObject) {
                //Debug.Log(Event.current + "::::" + lastSelected + ":" + Selection.activeGameObject);
                lastSelected = Selection.activeGameObject;

                parents = (lastSelected != null) ? lastSelected.transform.xGetParents(true) : null;

                if (parents != null && parents.Length > 0 ) {
                    multiSceneRoot = parents[0].IsMultiScene() ? parents[0] : null;
                    if (multiSceneRoot != null) parents = parents.xRemoveAt(0);
                }

                lastDrawIndex = -1;
                lastDrawY = 0;
                EditorX.xDelayCall(_h2.Repaint); 
            }

            if (parents != null) {
                var t = go.transform;
                if (multiSceneRoot != null && t.parent == multiSceneRoot) return;

                var idx     = parents.xIndexOf(t);
                var color   = ColorHSL.white.dS(-0.6f).xProSkinAdjust();
                //var color   = h2Settings.color_Depths[t.gameObject.xParentCount() % h2Settings.color_Depths.Length];

                if (idx != -1) { //parents
                    if (!drawing) {
                        drawing = true;
                    }

                    lastDrawIndex = idx;
                    lastDrawY = rect.y;

                    if (t.parent != null)
                    { //draw if not root
                        using (GuiX.GUIColor(color))
                        {
                            GUI.DrawTexture(rect.wh(16f, 16f).dx(-28f), EditorResource.GetTexture2D(
                                "corner_tr")
                                            );
                        }
                    }
                }
                else if (t.gameObject == lastSelected)
                {
                    drawing = false;
                    //selectedy = selectionRect.y;
                    lastDrawY = rect.y;

                    if (t.parent != null)
                    {
                        using (GuiX.GUIColor(color))
                        {
                            GUI.DrawTexture(rect.wh(16f, 16f).dx(-28f), EditorResource.GetTexture2D(
                                "corner_tr")
                                            );

                            if (t.childCount == 0)
                            {
                                GUI.DrawTexture(rect.wh(10f, 16f).dx(-12f).dy(1), EditorResource.GetTexture2D(
                                    "line_hz")
                                                );
                            }
                        }
                    }
                }
                else if (drawing || rect.y < lastDrawY)
                {//
                    //find same parent level
                    var p = t.parent;
                    var cnt = 0;
                    var idx2 = parents.xIndexOf(p);

                    while (p != null && idx2 == -1)
                    {
                        cnt++;
                        p = p.parent;
                        idx2 = parents.xIndexOf(p);
                    }

                    if (drawing && (idx2 < lastDrawIndex) && (rect.y > lastDrawY))
                    {
                        drawing = false;
                    }
                    else
                    {
                        if (drawing || p != null)
                        {
                            using (GuiX.GUIColor(color))
                            {

#if UNITY_4_5 || UNITY_4_6 || UNITY_5
                                var w = 14f;
#else
                                var w = 16f;
#endif
                                GUI.DrawTexture(rect.wh(16f, 16f).dx(-28f - cnt * w), EditorResource.GetTexture2D(
                                    "line_vt")
                                                );
                            }
                        }
                    }

                    //if (drawing && idx2 < lastDrawIndex && (lastDrawIndex > 0 && selectedy > 0) ){
                    //    Debug.LogWarning("end at <" + t + "> " + idx2 + ":" + lastDrawIndex + ":" + selectedy);
                    //    selectedy = selectionRect.y-1;
                    //    p = null;
                    //    drawing = false;
                    //}


                    //}
                }
            }
        }

        //----------------------------------  DRAW ICONS   ------------------------------

        internal static string _iconModes;
        internal static int currentIconMode;

        
        /*private static readonly string[] iconModes = {
            //default : script, visible, childCount, prefab
            "101011000",

            //compact : script, visible
            "001000000",

            //full : all
            "111111111"
        };*/

        static void h2DrawIcons(GameObject go, Rect r) {
            //Debug.Log(_iconModes.Length + ":" + currentIconMode);
            var offset = currentIconMode * (h2Settings.nIcons + 1);

            if (_iconModes[offset + 0] == '1') EditScript(RectX.SubRectRightRef(ref r, UseOldDepthStyle ? 0 : 10), go);
            if (_iconModes[offset + 1] == '1') EditLock2(RectX.SubRectRightRef(ref r, 16f), go);
            if (_iconModes[offset + 2] == '1') EditVisible(RectX.SubRectRightRef(ref r, 18f).dx(2).dw(-2), go);
            if (_iconModes[offset + 3] == '1') EditStatic2(RectX.SubRectRightRef(ref r, 16f), go);
            if (_iconModes[offset + 4] == '1') EditCombine2(RectX.SubRectRightRef(ref r, (h2c == null) ? 20f : h2c.maxChildCount < 100 ? 20f : h2c.maxChildCount < 1000 ? 28f : 36f), go);
            if (_iconModes[offset + 5] == '1') EditPrefab(RectX.SubRectRightRef(ref r, 14f), go);
            if (_iconModes[offset + 6] == '1') EditLayer(RectX.SubRectRightRef(ref r, 40f), go);
            if (_iconModes[offset + 7] == '1') EditTag(RectX.SubRectRightRef(ref r, 70f), go);
            if (_iconModes[offset + 8] == '1') EditDepth(RectX.SubRectRightRef(ref r, 0f), go);
        }

        static void h2CheckIconMode() {
            var e = Event.current;
            if (e.type != EventType.ScrollWheel || !e.shift) return;

            e.Use();
            currentIconMode = (currentIconMode + (e.delta.y > 0 ? 1 : -1) + h2Settings.nModes) % h2Settings.nModes;
            //Debug.Log(currentIconMode + ":" + _iconModes + ": Repaint ... ");
            _h2.Repaint();
        }

        //----------------------------------  SHORTCUT   ------------------------------

        private static void Key_Handler(Transform t)
        {
            var e = Event.current;

            //Debug.Log("KeyHandler ---> " + e.keyCode);

            if (e.type != EventType.keyDown) return;
            var go = t.gameObject;

            switch (e.keyCode)
            { //TO PARENT AND BACK
                case KeyCode.Comma:
                case KeyCode.LeftBracket:
                    t.hPingParent(true);
                    break;
                case KeyCode.Period:
                case KeyCode.RightBracket:
                    t.hPingChild(true);
                    break;
                case KeyCode.Backslash:
                    t.hPingSibling(true);
                    break;

                case KeyCode.L:
                    //Debug.Log("Key :: L");
                    go.hToggleLock();
                    Event.current.Use();

                    var oFocus = EditorWindow.focusedWindow;
                    WindowX.Inspector.Repaint();
                    if (EditorWindow.focusedWindow != oFocus && oFocus != null) oFocus.Focus();
                    break;

                case KeyCode.A:
                case KeyCode.V:
                    Event.current.Use();
                    go.hToggleActive(true);
                    break;

                case KeyCode.C:
                    Event.current.Use();
                    go.hToggleCombine();
                    Selection.activeGameObject = t.gameObject;
                    //vlbEditorWindow.Hierarchy.Focus();
                    break;
            }
        }

        private static void ShiftKey_Handler(GameObject go)
        {
            var e = Event.current;
            if (e.type != EventType.keyDown) return;
            if (!e.shift) return;

            var dict = new Dictionary<KeyCode, PrimitiveType> {
                {KeyCode.Alpha1, PrimitiveType.Quad},
                {KeyCode.Alpha2, PrimitiveType.Plane},
                {KeyCode.Alpha3, PrimitiveType.Cube},
                {KeyCode.Alpha4, PrimitiveType.Cylinder},
                {KeyCode.Alpha5, PrimitiveType.Capsule},
                {KeyCode.Alpha6, PrimitiveType.Sphere}
            };

            if (dict.ContainsKey(e.keyCode))
            {
                go.RevealChildrenInHierarchy();

                var type = dict[e.keyCode];
                type.xNewPrimity(
                    "New".GetNewName(go.transform.parent, type.ToString()), "New" + type + "Child", go.transform.parent)
                    .transform.xPingAndUseEvent();
                return;
            }

            switch (e.keyCode)
            {
                case KeyCode.N:
                    h2Api.CreateEmptyChild(go, true);
                    break;

                case KeyCode.L:
                    if (go.GetComponent<Camera>() != null) h2Api.CameraLookThrough(go.GetComponent<Camera>());
                    break;
                case KeyCode.C:
                    if (go.GetComponent<Camera>() != null) h2Api.CameraCaptureSceneView(go.GetComponent<Camera>());
                    break;
                case KeyCode.P:
                    h2Api.ResetLocalPosition(go);
                    break;
                case KeyCode.R:
                    h2Api.ResetLocalRotation(go);
                    break;
                case KeyCode.S:
                    h2Api.ResetLocalScale(go);
                    break;
                case KeyCode.T:
                    h2Api.ResetTransform(go);
                    break;
                case KeyCode.B:
                    go.xBreakPrefab();
                    break;
            }

            //if (evt.type == EventType.used) {
            //Selection.activeGameObject = null;
            //Hierarchy2Utils.RefreshInspector();
            //Selection.activeGameObject = go;
            //}
        }

        private static void AltKey_Handler(GameObject go)
        { //commands
            var e = Event.current;
            if (e.type != EventType.keyDown) return;
            if (!e.alt) return;

            switch (e.keyCode)
            {
                case KeyCode.M:
                    h2Api.Isolate_MissingBehaviours(true);
                    break;
                case KeyCode.B:
                    h2Api.Isolate_ObjectsHasScript(true);
                    break;
                case KeyCode.S:
                    h2Api.Isolate_SelectedObjects(true);
                    break;

                case KeyCode.L:
                    h2Api.Isolate_LockedObjects(true);
                    break;
                case KeyCode.I:
                case KeyCode.V:
                    h2Api.Isolate_InActiveObjects(true);
                    break;
                case KeyCode.Y:
                    h2Api.Isolate_CombinedObjects(true);
                    break;
            }
        }

        static void h2CheckShortcut(GameObject go) {
            if (h2Settings.use_Alt_Shortcut) AltKey_Handler(go);
            if (h2Settings.use_Shift_Shortcut) ShiftKey_Handler(go);
            if (h2Settings.use_Single_Shortcut) Key_Handler(go.transform);
        }

        //----------------------------------  RENAME LOCKED GO   ------------------------------


        private static GameObject RenameUnlock;
        static void h2RenameLockedGO(GameObject go) {
            if (go.xGetFlag(HideFlags.NotEditable) && _isRenaming) {
                RenameUnlock = go;
                go.xSetFlag(HideFlags.NotEditable, false);
            } else if (RenameUnlock != null && !_isRenaming) {
                RenameUnlock.xSetFlag(HideFlags.NotEditable, true);
                RenameUnlock = null;
            }
        }

        //----------------------------------  LOCKED GO   ------------------------------
        internal static bool AllowRenameLockedGO    = true;
        internal static bool AllowDragLockedGO      = false;
        


        //---------------------------------- TOGGLE USING HIERARCHY2 ------------------------------
        static bool h2IsEnabled() {
            var e = Event.current;
            if (e.type != EventType.keyUp || !e.alt || !e.control || !e.shift || e.keyCode != KeyCode.Alpha0) return h2Settings.enable;

            h2Settings.enable = !h2Settings.enable;
            e.Use();

            return h2Settings.enable;
        }

        //---------------------------------- HIERARCHY ITEM UPDATE ------------------------------

        private static Rect _lastH2IRect;

        //temporary variables
        private static EditorWindow _h2;
        private static bool _hasFocus;
        private static bool _isRenaming;
        private static bool _hasSearchFilter;

        private static bool _canRenameLocked;
        private static bool _skipDragLocked;

        private static bool _useShortcut;
        private static bool _enableIcons;

        //private static List<string> scriptList;
        //private static List<Component> componentList;

        static void OnBeforeDrawH2() {
            //run once for every event, do initial works that commonly used by other elements
            _h2                 = WindowX.Hierarchy;
            //Debug.Log("----> OnBeforeDrawH2 " + Event.current + ":" + _h2.wantsMouseMove);
            //_h2.wantsMouseMove  = false;

            _hasFocus           = HierarchyX.HasFocusOnHierarchy;
            _isRenaming         = HierarchyX.IsRenaming();
            //_multiSelection     = Selection.gameObjects.Length > 1;
            _hasSearchFilter    = !string.IsNullOrEmpty(_h2.xGetSearchFilterTerm());

            _skipDragLocked     = !AllowDragLockedGO && _hasFocus;
            _canRenameLocked    = AllowRenameLockedGO && _hasFocus;

            _useShortcut        = _hasFocus && h2Settings.enableShortcut && !_isRenaming;

            _enableIcons        = h2Settings.enableIcons;
            _iconModes          = h2Settings.iconModes;//.Split('.');

            if (listCSDirty) {
                listCSDirty = false;

                missingCount = 0;
                if (listComponent == null) {
                    listComponent = new Dictionary<Type, int>();
                } else {
                    listComponent.Clear();
                }
                if (listScript == null) {
                    listScript    = new Dictionary<Type, int>();
                } else {
                    listScript.Clear();
                }
 
                //Debug.Log("-----> refresh ... ");

                //maybe we should use async instead
                var listT = TransformX.RootT;
                foreach (var t in listT) {
                    if (t == null) continue;
                    var childList = t.xGetChildren<Transform>(true, true);
                    
                    //Debug.Log("---> "+ listT.Count + "-->" + childList.Length); 

                    foreach (var child in childList) {
                        if (child == null) continue;

                        var cList = child.GetComponents<Component>();

                        foreach (var c in cList) {
                            if (c == null) {
                                missingCount++;
                                continue;
                            }
                            var type = c.GetType();
                            var dict = c is MonoBehaviour ? listScript : listComponent;

                            if (!dict.ContainsKey(type)) {
                                dict.Add(type, 1);
                            } else {
                                dict[type] = dict[type] + 1;
                            }
                        }
                    }
                }
            }

            //do only once
            h2CheckIconMode(); 
        }

        static GameObject showingMenu;
        static int menuIndex;

        private static void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            //Debug.Log("----> instanceID " + instanceID + ":" + Event.current);
            if (!h2IsEnabled()) return;

            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (go == null) return;

            //Ignore (skip) Multiscene GameObjects
            if (go.IsMultiScene()) return;

            if (selectionRect.y < _lastH2IRect.y || _h2 == null) OnBeforeDrawH2();
            _lastH2IRect = selectionRect;

            // Support multiple phase rename - keep trying to rename until succeed
            if (HierarchyX.renameGO != null && instanceID == HierarchyX.renameGO.GetInstanceID()) go.Rename();

            // Support show / hide icons in Hiearchy view
            selectionRect = selectionRect.dw(- h2Settings.iconOffset);
            if (_enableIcons) h2DrawIcons(go, selectionRect);

            // Show parent indicator
            h2DrawParentIndicator(go, selectionRect);

            // Use shortcuts
            if (_useShortcut && Selection.activeGameObject == go && Event.current.type == EventType.keyDown) {
                h2CheckShortcut(go);
            }

            // Allow rename locked GO
            if (_canRenameLocked && Selection.activeGameObject == go) h2RenameLockedGO(go);

            // DisAllow drag locked GO
            if (_skipDragLocked && go.xIsLock() && selectionRect.l(0).xLMB_isDown().noModifier) {
                if (!Selection.instanceIDs.Contains(go.GetInstanceID())) Selection.activeGameObject = go;
            }

            //block all events
            /*if (showingMenu != null)
            {
                Event.current.Use(); //block all events

                if (showingMenu == Selection.activeGameObject && showingMenu == go) {
                    if (menuIndex == 1) {
                        var menu = new GenericMenu();
                        menu.xAdd(" _ISOLATE", null);
                        Context_Isolate(menu, go, "");

                        Context_Components(menu, go);
                        menu.xAddSep("");
                        Context_LayerTag(menu, go);
                        menu.ShowAsContext();

                    } else {
                        var menu = new GenericMenu();
                        Context_BuiltIn(menu, go);
                        Context_Special(menu, go, "");
                        menu.xAddSep("");

                        Context_Basic(menu, go);
                        //menu.xAddSep("");
                        //menu.xAdd("Isolate ... ", null);

                        menu.xAddSep("");
                        Context_Create(menu, go);
                        menu.ShowAsContext();
                    }
                    showingMenu = null;
                    /*EditorX.xDelayCall(()=> {
                        showingMenu = null;    
                    });#1#
                }

                return;
            }*/

            if (selectionRect.xRMB_isDown().with_Ctrl) {
                listCSDirty = true;
                Event.current.Use();
                
                /*Selection.activeGameObject = go;
                showingMenu = go;
                menuIndex = 1;*/
                //WindowX.Hierarchy.Repaint();

                var menu = new GenericMenu();
                menu.xAdd(" _ISOLATE", null);
                Context_Isolate(menu, go, "");

                Context_Components(menu, go);
                menu.xAddSep("");
                Context_LayerTag(menu, go);
                menu.ShowAsContext();
            }

            /*if (go == Selection.activeGameObject && Event.current.type != EventType.layout && Event.current.type != EventType.repaint && Event.current.type != EventType.used) {
                Debug.Log(Event.current + "----> " + selectionRect + ":" + selectionRect.xRMB_isDown().noModifier + ":" + Event.current.mousePosition);
            }*/

            if (selectionRect.xRMB_isDown().noModifier) {
                //DefaultContext(new GenericMenu(), go).ShowAsContext(); 
                Event.current.Use();

                /*Selection.activeGameObject = go;
                showingMenu = go;
                menuIndex = 0;   */

                var menu = new GenericMenu();
                Context_BuiltIn(menu, go);
                Context_Special(menu, go, "");
                menu.xAddSep("");

                Context_Basic(menu, go);
                //menu.xAddSep("");
                //menu.xAdd("Isolate ... ", null);

                menu.xAddSep("");
                Context_Create(menu, go);
                menu.ShowAsContext();
            }
        }
    }
}
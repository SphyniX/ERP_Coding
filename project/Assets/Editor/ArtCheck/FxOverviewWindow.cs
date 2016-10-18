using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZFrame.Asset;
//using Battle.FX;

namespace Artwork 
{
    public class FxOverviewWindow : EditorWindow 
    {
        private const int WINDOW_W = 800;
        private const int WINDOW_H = 600;
        private const int SCR_W = 200;
        private const int SCR_OFF_X = 10;
        private const int SCR_OFF_Y = 10;

        [MenuItem("Custom/资源检查/战斗特效...")]
        private static void OpenWindow()
        {
            var wnd = EditorWindow.GetWindowWithRect(
                typeof(FxOverviewWindow), 
                new Rect(Vector2.zero, new Vector2(WINDOW_W, WINDOW_H)),
                true,
                "战斗特效-总览") as FxOverviewWindow;;

            var dir = new DirectoryInfo("Assets/" + AssetBundleLoader.DIR_ASSETS + "/FX");
            wnd.m_FxGroups.Clear();
            foreach (var d in dir.GetDirectories()) {
                wnd.m_FxGroups.Add(new Entry(d));
            }
        }

        private class Entry
        {
            public DirectoryInfo dir;
            public bool isOn;
            public Entry(DirectoryInfo dir)
            {
                this.dir = dir;
                isOn = false;
            }
        }

        private List<Entry> m_FxGroups = new List<Entry>();
        private Vector2 m_ScrList, m_ScrOutput;
        private string m_Output;

        private void DrawFxGroupOp(Entry fxGroup)
        {
            fxGroup.isOn = GUILayout.Toggle(fxGroup.isOn, fxGroup.dir.Name);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(new Vector2(SCR_OFF_X, SCR_OFF_Y), new Vector2(SCR_W, WINDOW_H - SCR_OFF_Y * 2)));
            GUILayout.Label("特效组列表", CustomEditorStyles.titleStyle);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("全选", GUILayout.Height(30))) {
                foreach (var e in m_FxGroups) {
                    e.isOn = true;
                }
            }
            if (GUILayout.Button("清空", GUILayout.Height(30))) {
                foreach (var e in m_FxGroups) {
                    e.isOn = false;
                }
            }
            GUILayout.EndHorizontal();
            m_ScrList = GUILayout.BeginScrollView(m_ScrList);
            for (int i = 0; i < m_FxGroups.Count; ++i) {
                DrawFxGroupOp(m_FxGroups[i]);
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(new Vector2(SCR_W + SCR_OFF_X * 2, SCR_OFF_Y), new Vector2(WINDOW_W - SCR_W - SCR_OFF_X * 3, WINDOW_H - SCR_OFF_Y * 2)));

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("检查依赖", GUILayout.Height(30))) {
                ChkDependencies();
            }
            if (GUILayout.Button("检查脚本", GUILayout.Height(30))) {
                ChkComponent();
            }
            if (GUILayout.Button("检查资源", GUILayout.Height(30))) {
                ChkNeedAssets();
            }
            GUILayout.EndHorizontal();

            m_ScrOutput = GUILayout.BeginScrollView(m_ScrOutput);
            GUILayout.Label(m_Output);
            GUILayout.EndScrollView();

            GUILayout.EndArea();
        }

        private int TraverseFX(System.Action<string, string> onAction)
        {
            int index = Application.dataPath.Length + 1;
            int nGroup = 0;
            foreach (var e in m_FxGroups) {
                if (e.isOn) {
                    nGroup += 1;

                    var files = e.dir.GetFiles("*.prefab");
                    var assetRoot = Path.Combine("Assets", e.dir.FullName.Substring(index)).Replace("\\", "/");
                    foreach (var f in files) {
                        var assetPath = Path.Combine("Assets", f.FullName.Substring(index)).Replace("\\", "/");
                        onAction.Invoke(assetRoot, assetPath);
                    }
                }
            }
            return nGroup;
        }

        private void ChkDependencies()
        {
            var strbld = new StringBuilder();
            var n = TraverseFX((assetRoot, assetPath) => {
                var assetGroup = Path.GetFileName(assetRoot);
                var assetName = Path.GetFileNameWithoutExtension(assetPath);
                var dependencies = AssetDatabase.GetDependencies(assetPath);

                var liDepend = new List<string>();
                foreach (var path in dependencies) {
                    if (path.EndsWith(".cs", System.StringComparison.OrdinalIgnoreCase) 
                        || path.EndsWith(".shader", System.StringComparison.OrdinalIgnoreCase)) continue;

                    if (!path.StartsWith(assetRoot)) {
                        liDepend.Add(path);
                    }
                }
                if (liDepend.Count > 0) {
                    strbld.AppendFormat("[{0}/{1}]存在错误的依赖项：", assetGroup, assetName).AppendLine();
                    foreach (var p in liDepend) {
                        strbld.Append('\t').AppendLine(p);
                    }
                    strbld.AppendLine();
                }
            });
            if (n > 0) {
                m_Output = strbld.ToString();
            } else {
                m_Output = "请先勾选特效组";
            }
        }

        private void ChkComponent()
        {
            //var strbld = new StringBuilder();
            //var n = TraverseFX((assetRoot, assetPath) => {
            //    var assetGroup = Path.GetFileName(assetRoot);
            //    var assetName = Path.GetFileNameWithoutExtension(assetPath);

            //    var liErrors = new List<string>();
            //    var go = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            //    if (go) {
            //        var fxCfg = go.GetComponent(typeof(IFxCfg)) as IFxCfg;
            //        if (fxCfg != null) {
            //            for (int i = 0; ; i++) {
            //                var fxCtrl = fxCfg[i];
            //                if (fxCtrl == null) break;
                                
            //                if (fxCtrl.prefab == null) {
            //                    liErrors.Add(string.Format("第{0}个特效预设为空！", i));
            //                    continue;
            //                }

            //                // 检查是否出现多个FxCtrl
            //                var fxCtrls = fxCtrl.prefab.GetComponentsInChildren(typeof(IFxCtrl), true);
            //                if (fxCtrls.Length > 1) {
            //                    liErrors.Add(string.Format("第{0}个特效预设上存在多个控制组件<IFxCtrl>！", i));
            //                }

            //                /*
            //                var fxAnchor = fxCtrl.prefab.GetComponent(typeof(IFxAnchor));
            //                if (fxAnchor == null) {
            //                    liErrors.Add(string.Format("第{0}个特效预设上缺少挂载组件({1}或{2})！", i, typeof(FxBoneType), typeof(FxBoneName)));
            //                }
            //                //*/
            //            }
            //        }
            //    } else {
            //        liErrors.Add(string.Format("预设不存在！"));
            //    }
            //    if (liErrors.Count > 0) {
            //        strbld.AppendFormat("[{0}/{1}]存在错误的组件配置：", assetGroup, assetName).AppendLine();
            //        foreach (var p in liErrors) {
            //            strbld.Append('\t').AppendLine(p);
            //        }
            //        strbld.AppendLine();
            //    }
            //});
            //if (n > 0) {
            //    m_Output = strbld.ToString();
            //} else {
            //    m_Output = "请先勾选特效组";
            //}
        }

        private void ChkNeedAssets()
        {
            
        }
    }
}

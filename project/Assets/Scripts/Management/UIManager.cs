using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ZFrame
{
	using UGUI;

    public class UIManager : MonoSingleton<UIManager>
    {

        public Canvas[] canvases;
        public KeyCode[] Keys;

        /// <summary>
        /// 创建一个UI窗口
        /// </summary>
        /// <param name="prefabName">窗口预设路径</param>
        /// <param name="depth">深度：高的出现在前面；同样深度，后创建的出现在前面；0表示自动最前面</param>
        /// <returns></returns>
        public GameObject CreateWindow(string prefabName, int depth = 0)
        {
            var siblingIndex = 0;
            var cachedTransform = canvases[0].transform;

            // 计算Sibling
            var count = cachedTransform.childCount;
            for (int i = count - 1; i >= 0; --i) {
                var top = cachedTransform.GetChild(i).GetComponent<LuaComponent>();
                if (top && top.depth < 100) {
                    if (depth == 0) {
                        depth = top.depth + 1;
                    }
                    if (top.depth <= depth) {
                        siblingIndex = i + 1;
                        break;
                    }
                }
            }

            GameObject ret = null;
            // 界面是否已经是可见
            LuaComponent lc;
            var wndName = Path.GetFileName(prefabName);
            if (LuaComponent.dictLuaComs.TryGetValue(wndName, out lc)) {
                lc.depth = depth;
                var rt = lc.GetComponent<RectTransform>();
                rt.SetSiblingIndex(siblingIndex);
                ret = lc.gameObject;
                ret.SendMessage("Start");
            } else {
                // 新初始化界面
                var prefab = AssetsMgr.A.Load<GameObject>(prefabName);
                if (prefab) {
                    var go = ObjectPoolManager.AddChild(cachedTransform.gameObject, prefab, siblingIndex);
                    lc = go.GetComponent<LuaComponent>();
                    if (lc) {
                        lc.enabled = true;
                        lc.depth = depth;
                    }

                    ret = go;
                } else {
                    if (AssetsMgr.A.useLuaAssetBundle) {
                        LogMgr.W("未找到要创建的UI窗口：<{0}>，是否未完成打包？", prefabName);
                    } else {
                        LogMgr.W("未找到要创建的UI窗口：<{0}>，是否忘记了标志为[AssetBundle]？", prefabName);
                    }
                }
            }
            if (ret) {
                if (LogViewer.Instance) LogViewer.Instance.cachedTransform.SetAsLastSibling();
                if (!ret.activeSelf) ret.SetActive(true);
            }
            return ret;
        }

        // Use this for initialization
        private void Start()
        {
			DG.Tweening.DOTween.Init();

            CLZF2.Decrypt(null, AssetsMgr.VER + AssetsMgr.Instance.resHeight);
            LuaScriptMgr.OnScriptsFinish();
#if UNITY_EDITOR || UNITY_STANDALONE
            //编辑器工具管理类
            LogMgr.D("加载编辑器工具...");
            gameObject.AddComponent<GETools.GETBoot>();
#endif
        }

        public static void CleanLevel()
        {
            // Destroy all UI Elements
            var list = new List<GameObject>();
            for (int i = 0; i < Instance.canvases.Length; ++i) {
                var canvasTrans = Instance.canvases[i].transform;
                for (int j = 0; j < canvasTrans.childCount; ++j) {
                    var t = canvasTrans.GetChild(j);
                    if (ObjectPoolManager.IsPooled(t.gameObject)) {
                        list.Add(t.gameObject);
                    }
                }
            }
            for (int i = 0; i < list.Count; ++i) {
                var go = list[i];
                ObjectPoolManager.DestroyPooled(go);
            }

            ObjectPoolManager.ReleaseScenePool();
        }

    }
}

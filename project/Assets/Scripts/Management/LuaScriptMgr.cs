using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;

namespace ZFrame
{
    using Asset;
    public class LuaScriptMgr : MonoSingleton<LuaScriptMgr>
    {
        public const string SCRIPT_FILE = "framework/main.lua";
        public const string F_ON_LEVEL_LOADED = "on_level_loaded";
        public const string S_APP_PERSISTENTDATA_PATH = "app_persistentdata_path";
        public const string S_APP_STREAMINGASSETS_PATH = "app_streamingassets_Path";
        public const string B_USING_ASSETBUNDLE = "using_assetbundle";
        public const string B_REFLECTING_CSHARP = "reflecting_csharp";
        
        private LuaTable m_Tb;
        public LuaEnv Env { get; private set; }
        public ILuaState L {
            get {
                if (Env == null) {
                    Env = new LuaEnv();
                    var L = Env.L;

                    LibUnity.OpenLib(L);
                    LibAsset.OpenLib(L);
                    LibUGUI.OpenLib(L);
                    LibSystem.OpenLib(L);
                    LibNetwork.OpenLib(L);
                    LibTool.OpenLib(L);
                    LibCSharpIO.OpenLib(L);
                    LibBattle.OpenLib(L);

                }
                return Env.L;
            }
        }
        public bool IsLua { get { return Env != null; } }

        protected override void Awaking()
        {
			Debug.Log ("Starting Instance SDKMgrs Form Lua Files ___>> framwork/main.lua");
            int n = L.DoFile(SCRIPT_FILE);
            Assert.IsTrue(n == 1);

            m_Tb = L.ToLuaTable(-1);
            L.Pop(1);
            Assert.IsNotNull(m_Tb);
            
            m_Tb.CallFunc("awake", 0);            
        }

        private void OnUIClick(GameObject go)
        {
            m_Tb.CallFunc("on_ui_click", 0, go);
        }

        private void Start()
        {
            // 设定全局参数            
            L.GetGlobal("ENV");
            L.SetKeyValue(S_APP_PERSISTENTDATA_PATH, AssetBundleLoader.persistentDataPath);
            L.SetKeyValue(S_APP_STREAMINGASSETS_PATH, AssetBundleLoader.streamingAssetsPath);
            L.SetKeyValue(B_USING_ASSETBUNDLE, AssetBundleLoader.Instance ? "true" : "false");
            
            // Settings
            L.GetField(-1, "limit_frame_rate");
            int limitFrameRate = L.ToInteger(-1);
            L.Pop(2);

            if (limitFrameRate > 0) {
                Application.targetFrameRate = limitFrameRate;
            }
            
            m_Tb.CallFunc("start", 0, gameObject);

            UGUI.UIButton.onButtonClick = OnUIClick;
            UGUI.UIToggle.onToggleClick = OnUIClick;

            LuaComponent.OnStart = (lc) => {
                // TODO 做新手引导用
            };
        }

        private void Update()
        {
            for (int i = 0; i < UIManager.Instance.Keys.Length; ++i) {
                var key = UIManager.Instance.Keys[i];
                if (Input.GetKeyDown(key)) {
                    m_Tb.CallFunc("on_key", 0, key.ToString());
                    break;
                }
            }
        }

        /*************************************************
         * Prepare Lua Scripts. <IMPORTANT>
         *************************************************/

        /// <summary>
        /// Lua脚本全部准备就绪
        /// </summary>
        public static void OnScriptsFinish()
        {
			Debug.Log ("Finished All Lua Scripts!");
            var txt = Resources.Load<TextAsset>("luacoding");
            CLZF2.Decrypt(txt.bytes, txt.bytes.Length);

            UIManager.Instance.gameObject.AddComponent<LuaScriptMgr>();

            Application.runInBackground = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // 在使用资源的情况下加载此文件，并保存到自定义的目录
            AssetsMgr.A.Loader.BundleTask(AssetBundleLoader.FILE_LIST, true);

            // 执行预加载
            AssetsMgr.A.Loader.ExecutePreload(null, OnAssetBundleLoaded);
        }

        private static Coroutine OnAssetBundleLoaded(AbstractAssetBundleRef abf, string md5)
        {
			Debug.Log ("Starting Init Scence!");
            // Scene Init
            var L = Instance.L;
            L.GetGlobal(F_ON_LEVEL_LOADED);
            L.DoCall(0, SceneManager.GetActiveScene().name, true);
            return null;
        }
    }
}

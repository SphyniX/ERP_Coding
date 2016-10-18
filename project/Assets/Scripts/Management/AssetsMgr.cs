using UnityEngine;
using System.IO;
using System.Collections;
using ZFrame.Asset;
using ZFrame;

public class AssetsMgr : MonoSingleton<AssetsMgr> 
{
    public const int VER = 0x3F7A0;

    public static AssetsMgr A { get { return Instance; } }

#if UNITY_EDITOR
    [SerializeField, HideInInspector]
    private bool m_PrintLoadedLuaStack = false;
    public bool printLoadedLuaStack { get { return m_PrintLoadedLuaStack; } }
    [SerializeField, HideInInspector]
    private bool m_UseLuaAssetBundle = false;
    public bool useLuaAssetBundle { get { return m_UseLuaAssetBundle; } }
    [SerializeField, HideInInspector]
    private bool m_UseAssetBundleLoader = false;
    public bool useAssetBundleLoader { get { return m_UseLuaAssetBundle || m_UseAssetBundleLoader; } }
#elif UNITY_STANDALONE
	public bool printLoadedLuaStack { get { return false; } }
    public bool useLuaAssetBundle { get { return !System.IO.File.Exists("debug.txt"); } }
	public bool useAssetBundleLoader { get { return true; } }    
#else
    public bool printLoadedLuaStack { get { return false; } }
    public bool useLuaAssetBundle { get { return true; } }
	public bool useAssetBundleLoader { get { return true; } }
#endif
    public int resHeight = 720;
    [HideInInspector]
    public bool apiReflection = true;

	[System.NonSerialized]
    public AssetLoader Loader;
    private bool launchingGame = true;    

    private void SetQuality()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        const int quality = 5;
#else
		const int quality = 2;
#endif
        string[] names = QualitySettings.names;
        Debug.Log("Quality set to " + names[quality]);
        QualitySettings.SetQualityLevel(quality);
    }

    private void SetResolution()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
#else
        Screen.SetResolution((int)(resHeight * (float)Screen.width / Screen.height), resHeight, true);
#endif
    }
    /// <summary>
    /// 在此函数中加载UIRoot
    /// </summary>
    /// <param name="abRef"></param>
    /// <param name="md5"></param>
    /// <returns></returns>
    private Coroutine OnAssetsLaunched(AbstractAssetBundleRef abRef, string md5)
    {
        if (UIManager.Instance == null) {
            LoadAsync(typeof(GameObject), "Launch/UIROOT", false, (o, p) => {
                GoTools.AddForever(o as GameObject);
            });
        }
        return null;
    }

    /*************************************************
     * 启动后加载Lua脚本
     *************************************************/
    public const string LUA_SCRIPT = "lua/script.unity3d";
    public const string LUA_CONFIG = "lua/config.unity3d";
    public const string KEY_MD5_STREAMING_LUA = "Streaming-Lua";
    public const string KEY_DATE_STREAMING_LUA = "Streaming-Lua-Date";
    public const string KEY_MD5_USING_LUA = "Using-Lua";
    public const string KEY_DATE_USING_LUA = "Using-Lua-Date";

    // 初始化Lua脚本
    private void InitScriptsFromAssetBunles()
    {
        if (useLuaAssetBundle) {			
			// PathHook is Meaningless when using AssetBundle.
			// LuaFile.SetPathHook((s) => Path.Combine(Path.Combine(AssetBundleLoader.bundleRootPath, "LuaRoot"), s));
            // 先从streamingRootPath加载
			Debug.Log ("Starting Instace Lua AssetBundle ! Using AssetBundleLoader! ");
            AssetBundleLoader.Instance.StreamingTask(LUA_SCRIPT, false, OnStreamingLuaLoaded, true);
        } else {
            // LuaFile.SetPathHook((s) => Application.dataPath + "/../Essets/LuaRoot/" + s);
            // 加载 UIRoot, 开启游戏
            if (UIManager.Instance == null) {
				Debug.Log ("Starting Instace Lua AssetBundle ! Using OnAssetsLaunched! ");
                OnAssetsLaunched(null, null);
            }
        }
    }

    // streamingRootPath中的lua脚本加载完成
    private Coroutine OnStreamingLuaLoaded(AbstractAssetBundleRef abf, string md5)
    {
        // 更新streamingRootPath中lua脚本的md5
        string streamingMD5 = PlayerPrefs.GetString(KEY_MD5_STREAMING_LUA);
        string streamingDate = PlayerPrefs.GetString(KEY_DATE_STREAMING_LUA);
        if (md5 != streamingMD5) {
            streamingMD5 = md5;
            streamingDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            PlayerPrefs.SetString(KEY_MD5_STREAMING_LUA, streamingMD5);
            PlayerPrefs.SetString(KEY_DATE_STREAMING_LUA, streamingDate);
			LogMgr.I("Update streaming lua to {0} at {1}", streamingMD5, streamingDate);
        }

        string bundlePath = Path.Combine(AssetBundleLoader.bundleRootPath, LUA_SCRIPT);
        string bundleMD5 = PlayerPrefs.GetString(KEY_MD5_USING_LUA);
        string bundleDate = PlayerPrefs.GetString(KEY_DATE_USING_LUA);
        if (File.Exists(bundlePath) && !string.IsNullOrEmpty(bundleMD5)) {
            if (streamingMD5 != bundleMD5) {
                // 已存在lua脚本在bundleRootPath, 比较时间           
                var dtStreaming = System.DateTime.Parse(streamingDate);
                var dtUsing = System.DateTime.Parse(bundleDate);
                if (dtUsing < dtStreaming) {
                    // Streaming 的Lua脚本比较新
                    File.Delete(bundlePath);
                    // 所有资源需要更新
                    if (Directory.Exists(AssetBundleLoader.bundleRootPath)) {
                        Directory.Delete(AssetBundleLoader.bundleRootPath, true);
                        Directory.CreateDirectory(AssetBundleLoader.bundleRootPath);
                    }
                    PlayerPrefs.SetString(KEY_MD5_USING_LUA, streamingMD5);
                    PlayerPrefs.SetString(KEY_DATE_USING_LUA, streamingDate);
                    LogMgr.I("Update using lua to {0} at {1}", streamingMD5, streamingDate);
                } else {
                    // 移除Streaming中的旧脚本，加载新脚本
                    abf.Unload(true);
                    AssetBundleLoader.Instance.BundleTask(LUA_SCRIPT, false);
                }
            }
        } else {
            // 首次启动游戏
            PlayerPrefs.SetString(KEY_MD5_USING_LUA, streamingMD5);
            PlayerPrefs.SetString(KEY_DATE_USING_LUA, streamingDate);
            LogMgr.I("First using lua to {0} at {1}", streamingMD5, streamingDate);
        }

        //加载UIRoot
        AssetBundleLoader.Instance.BundleTask(LUA_CONFIG, false, OnAssetsLaunched);
        return null;
    }

    protected override void Awaking()
    {
        DontDestroyOnLoad(gameObject);

        UnityEngine.Assertions.Assert.raiseExceptions = true;

        LogMgr.D("[Lua] {0}", useLuaAssetBundle ? "AssetBundle" : "Source Code");
        LogMgr.D("[Assets] {0}", useAssetBundleLoader ? "AssetBundle" : "Assets Folder");

        if (useAssetBundleLoader) {
            Loader = gameObject.AddComponent<AssetBundleLoader>();
        } else {
#if UNITY_EDITOR
            Loader = gameObject.AddComponent<AssetsSimulate>();            
#else
            LogMgr.E("非编辑器模式不支持模拟使用AssetBundle。");
#endif
        }

        AssetLoader.CollectGarbage = GC;
    }

    private void Start()
    {
        //SetQuality();
        SetResolution();
        if (launchingGame) {
	Debug.Log ("Start Launching AssetBunles!");
            InitScriptsFromAssetBunles();
        }
    }

#if UNITY_EDITOR || UNITY_STANDALONE || RY_DEBUG
    private void OnGUI()
    {
        var fps = (1.0f / Time.unscaledDeltaTime);
        var color = Color.white;
        if (fps < 15) color = Color.red;
        if (fps < 20) color = Color.yellow;
        GUI.color = color;
        string text = string.Format("FPS: {0}", (int)fps);
        GUILayout.Label(text);
        GUI.color = Color.white;
        
    }
#endif
		
	/// <summary>
	/// 同步加载资源
	/// </summary>
	/// <param name="type">资源类型</param>
	/// <param name="path">路径：AssetBundle/ObjectName</param>    
	/// <param name="warnIfMissing">找不到时是否需要警告</param>
	/// <returns>加载到的资源</returns>
	public Object Load(System.Type type, string path, bool warnIfMissing = true)
	{
		return Loader.Load(type, path, warnIfMissing);
	}
	public T Load<T>(string path, bool warnIfMissing = true) where T : Object
	{
        if (string.IsNullOrEmpty(path)) return default(T);

		return Load(typeof(T), path, warnIfMissing) as T;
	}
    
    /// <summary>
    /// 异步加载一个资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="path">路径：AssetBundle/ObjectName</param>
    /// <param name="onLoaded">加载结束后做啥</param>
	public void LoadAsync(System.Type type, string path, bool allowUnload, DelegateObjectLoaded onLoaded = null, System.Object param = null)
    {
		Loader.LoadAsync(type, path, allowUnload, onLoaded, param);
    }

    /// <summary>
    /// 卸载一个资源
    /// </summary>
    /// <param name="assetName">资源名称</param>
    public void Unload(string assetName)
    {
        Loader.Unload(assetName);
    }

    #region 版本数据

    #endregion

    public static void GC()
    {
        LuaEnv.Instance.L.GC(LuaInterface.LuaGCOptions.LUA_GCCOLLECT, 0);
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}

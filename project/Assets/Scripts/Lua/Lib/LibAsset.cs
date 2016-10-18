using UnityEngine;
using System.Collections;
using ZFrame.Asset;
using ZFrame;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;
using ZFrame.UGUI;

public static class LibAsset {
	public const string LIB_NAME = "libasset.cs";


	public static void OpenLib(ILuaState lua)
    {
        var define = new NameFuncPair[]
        {
			new NameFuncPair("GetVersion", GetVersion),
			new NameFuncPair("UpdateLua", UpdateLua),
			new NameFuncPair("DoLua", DoLua),
            new NameFuncPair("Get", Get),
			new NameFuncPair("ProgressLoading", ProgressLoading),
				
            // Asset Methods
            new NameFuncPair("Load", Load),
			new NameFuncPair("LoadAsync", LoadAsync),
			new NameFuncPair("Unload", Unload),        
            new NameFuncPair("StopLoading", StopLoading),                
            new NameFuncPair("LimitAssetBundle", LimitAssetBundle),

			new NameFuncPair("PrepareAssets", PrepareAssets),
            new NameFuncPair("PersistentDataPath",PersistentDataPath),
        };

        lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
    }
    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int PersistentDataPath(ILuaState lua)
    {
        string p = SDKMgr.persistentDataPath;
        LogMgr.D("persistentDataPath {0}", p);
        lua.PushString(p);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int GetVersion(ILuaState lua)
	{
        lua.PushVariant(VersionMgr.LoadAppVersion().ToVariant());
        lua.PushVariant(VersionMgr.LoadAssetVersion().ToVariant());
		return 2;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int UpdateLua(ILuaState lua)
	{
		string usingMD5 = lua.ChkString(1);
		string usingDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		PlayerPrefs.SetString("Using-Lua", usingMD5);
		PlayerPrefs.SetString("Using-Lua-Date", usingDate);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int DoLua(ILuaState lua)
	{
        return lua.DoFile(lua.ChkString(1));
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int Get(ILuaState lua)
    {
        string path = lua.ChkString(1);
        string name = lua.ChkString(2);
        object type = lua.ToUserData(3);
        GameObject go = AssetsMgr.A.Load<GameObject>(path);
        var lib = go.GetComponent<ObjectLibrary>();
        if (lib) {
			var obj = lib.Get(type as System.Type, name);
            if (obj) {
                lua.PushLightUserData(obj);
            } else {
                lua.PushNil();
            }
        }

        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int ProgressLoading(ILuaState lua)
	{
        UnityEngine.UI.Slider bar = lua.ToComponent<UnityEngine.UI.Slider>(1);
		if (AssetBundleLoader.Instance) {
            if (bar) bar.value = 0;
			AssetBundleLoader.Instance.progressView = bar;
		}
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int Load(ILuaState lua)
    {
        var type = lua.ChkUserData(1, typeof(System.Type)) as System.Type;
        if (type != null) {
            string path = lua.ChkString(2);
            lua.PushLightUserData(AssetsMgr.A.Load(type, path));
        } else {
            lua.PushNil();
        }
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int LoadAsync(ILuaState lua)
    {
        object obj = lua.ToUserData(1);
        string path = lua.ChkString(2);
        bool allowUnload = lua.ToBoolean(3);
        var funcRef = lua.ToLuaFunction(4);
        object param = lua.ToAnyObject(5);
		DelegateObjectLoaded onLoaded = null;
		if (funcRef != null) {
			onLoaded = (o, p) => {
                funcRef.Invoke(o, p);
                funcRef.Dispose();
				var disposer = p as System.IDisposable;
				if (disposer != null) disposer.Dispose();
			};
		}
		System.Type type = obj as System.Type;
        AssetsMgr.A.LoadAsync(type, path, allowUnload, onLoaded, param);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int Unload(ILuaState lua)
	{
		string assetbundleName = lua.ChkString(1);
		AssetsMgr.A.Loader.Unload(assetbundleName);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int StopLoading(ILuaState lua)
    {
        AssetsMgr.A.Loader.StopLoading();
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int LimitAssetBundle(ILuaState lua)
    {
        string group = lua.ChkString(1);
        int limit = lua.ChkInteger(2);
        AssetsMgr.A.Loader.LimitAssetBundle(group, limit);        
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]	
	static int PrepareAssets(ILuaState lua)
	{
		var abLoader = AssetsMgr.A.Loader;
		abLoader.ClearPreload();
		TinyJSON.Variant jObj = lua.ToJsonObj(1);
		var assets = jObj as TinyJSON.ProxyArray;
		if (assets != null) {
			for (int i = 0; i < assets.Count; ++i) {
				string abName = assets[i]["name"];
				bool allowUnload = assets[i]["unload"];
				abLoader.CachedPreload(abName, allowUnload);
			}                
		}
		return 0;
	}
}

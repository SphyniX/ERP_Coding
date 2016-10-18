using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_ApplicationWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Quit", Quit),
			new LuaMethod("CancelQuit", CancelQuit),
			new LuaMethod("GetStreamProgressForLevel", GetStreamProgressForLevel),
			new LuaMethod("CanStreamedLevelBeLoaded", CanStreamedLevelBeLoaded),
			new LuaMethod("CaptureScreenshot", CaptureScreenshot),
			new LuaMethod("HasProLicense", HasProLicense),
			new LuaMethod("ExternalCall", ExternalCall),
			new LuaMethod("OpenURL", OpenURL),
			new LuaMethod("RequestUserAuthorization", RequestUserAuthorization),
			new LuaMethod("HasUserAuthorization", HasUserAuthorization),
			new LuaMethod("new", _CreateApplication),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("streamedBytes", get_streamedBytes, null),
			new LuaField("isPlaying", get_isPlaying, null),
			new LuaField("isEditor", get_isEditor, null),
			new LuaField("isWebPlayer", get_isWebPlayer, null),
			new LuaField("platform", get_platform, null),
			new LuaField("isMobilePlatform", get_isMobilePlatform, null),
			new LuaField("isConsolePlatform", get_isConsolePlatform, null),
			new LuaField("runInBackground", get_runInBackground, set_runInBackground),
			new LuaField("dataPath", get_dataPath, null),
			new LuaField("streamingAssetsPath", get_streamingAssetsPath, null),
			new LuaField("persistentDataPath", get_persistentDataPath, null),
			new LuaField("temporaryCachePath", get_temporaryCachePath, null),
			new LuaField("srcValue", get_srcValue, null),
			new LuaField("absoluteURL", get_absoluteURL, null),
			new LuaField("unityVersion", get_unityVersion, null),
			new LuaField("version", get_version, null),
			new LuaField("bundleIdentifier", get_bundleIdentifier, null),
			new LuaField("installMode", get_installMode, null),
			new LuaField("sandboxType", get_sandboxType, null),
			new LuaField("productName", get_productName, null),
			new LuaField("companyName", get_companyName, null),
			new LuaField("cloudProjectId", get_cloudProjectId, null),
			new LuaField("webSecurityEnabled", get_webSecurityEnabled, null),
			new LuaField("webSecurityHostUrl", get_webSecurityHostUrl, null),
			new LuaField("targetFrameRate", get_targetFrameRate, set_targetFrameRate),
			new LuaField("systemLanguage", get_systemLanguage, null),
			new LuaField("stackTraceLogType", get_stackTraceLogType, set_stackTraceLogType),
			new LuaField("backgroundLoadingPriority", get_backgroundLoadingPriority, set_backgroundLoadingPriority),
			new LuaField("internetReachability", get_internetReachability, null),
			new LuaField("genuine", get_genuine, null),
			new LuaField("genuineCheckAvailable", get_genuineCheckAvailable, null),
			new LuaField("isShowingSplashScreen", get_isShowingSplashScreen, null),
		};

		var type = typeof(Application);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateApplication(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Application obj = new Application();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Application.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Application));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_streamedBytes(IntPtr L)
	{
		L.PushInteger(Application.streamedBytes);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPlaying(IntPtr L)
	{
		L.PushBoolean(Application.isPlaying);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isEditor(IntPtr L)
	{
		L.PushBoolean(Application.isEditor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isWebPlayer(IntPtr L)
	{
		L.PushBoolean(Application.isWebPlayer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_platform(IntPtr L)
	{
		L.PushUData(Application.platform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isMobilePlatform(IntPtr L)
	{
		L.PushBoolean(Application.isMobilePlatform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isConsolePlatform(IntPtr L)
	{
		L.PushBoolean(Application.isConsolePlatform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_runInBackground(IntPtr L)
	{
		L.PushBoolean(Application.runInBackground);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dataPath(IntPtr L)
	{
		L.PushString(Application.dataPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_streamingAssetsPath(IntPtr L)
	{
		L.PushString(Application.streamingAssetsPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_persistentDataPath(IntPtr L)
	{
		L.PushString(Application.persistentDataPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_temporaryCachePath(IntPtr L)
	{
		L.PushString(Application.temporaryCachePath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_srcValue(IntPtr L)
	{
		L.PushString(Application.srcValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_absoluteURL(IntPtr L)
	{
		L.PushString(Application.absoluteURL);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unityVersion(IntPtr L)
	{
		L.PushString(Application.unityVersion);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_version(IntPtr L)
	{
		L.PushString(Application.version);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bundleIdentifier(IntPtr L)
	{
		L.PushString(Application.bundleIdentifier);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_installMode(IntPtr L)
	{
		L.PushUData(Application.installMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sandboxType(IntPtr L)
	{
		L.PushUData(Application.sandboxType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_productName(IntPtr L)
	{
		L.PushString(Application.productName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_companyName(IntPtr L)
	{
		L.PushString(Application.companyName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cloudProjectId(IntPtr L)
	{
		L.PushString(Application.cloudProjectId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_webSecurityEnabled(IntPtr L)
	{
		L.PushBoolean(Application.webSecurityEnabled);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_webSecurityHostUrl(IntPtr L)
	{
		L.PushString(Application.webSecurityHostUrl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetFrameRate(IntPtr L)
	{
		L.PushInteger(Application.targetFrameRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_systemLanguage(IntPtr L)
	{
		L.PushUData(Application.systemLanguage);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stackTraceLogType(IntPtr L)
	{
		L.PushUData(Application.stackTraceLogType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_backgroundLoadingPriority(IntPtr L)
	{
		L.PushUData(Application.backgroundLoadingPriority);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_internetReachability(IntPtr L)
	{
		L.PushUData(Application.internetReachability);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_genuine(IntPtr L)
	{
		L.PushBoolean(Application.genuine);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_genuineCheckAvailable(IntPtr L)
	{
		L.PushBoolean(Application.genuineCheckAvailable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isShowingSplashScreen(IntPtr L)
	{
		L.PushBoolean(Application.isShowingSplashScreen);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_runInBackground(IntPtr L)
	{
		Application.runInBackground = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetFrameRate(IntPtr L)
	{
		Application.targetFrameRate = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stackTraceLogType(IntPtr L)
	{
		Application.stackTraceLogType = (StackTraceLogType)L.ChkEnumValue(3, typeof(StackTraceLogType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_backgroundLoadingPriority(IntPtr L)
	{
		Application.backgroundLoadingPriority = (ThreadPriority)L.ChkEnumValue(3, typeof(ThreadPriority));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Quit(IntPtr L)
	{
		L.ChkArgsCount(0);
		Application.Quit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CancelQuit(IntPtr L)
	{
		L.ChkArgsCount(0);
		Application.CancelQuit();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetStreamProgressForLevel(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1 && L.CheckTypes(1, typeof(string)))
		{
			var arg0 = L.ChkLuaString(1);
			float o = Application.GetStreamProgressForLevel(arg0);
			L.PushNumber(o);
			return 1;
		}
		else if (count == 1 && L.CheckTypes(1, typeof(int)))
		{
			var arg0 = (int)L.ToNumber(1);
			float o = Application.GetStreamProgressForLevel(arg0);
			L.PushNumber(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Application.GetStreamProgressForLevel");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CanStreamedLevelBeLoaded(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1 && L.CheckTypes(1, typeof(string)))
		{
			var arg0 = L.ChkLuaString(1);
			bool o = Application.CanStreamedLevelBeLoaded(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 1 && L.CheckTypes(1, typeof(int)))
		{
			var arg0 = (int)L.ToNumber(1);
			bool o = Application.CanStreamedLevelBeLoaded(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Application.CanStreamedLevelBeLoaded");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CaptureScreenshot(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			var arg0 = L.ToLuaString(1);
			Application.CaptureScreenshot(arg0);
			return 0;
		}
		else if (count == 2)
		{
			var arg0 = L.ToLuaString(1);
			var arg1 = (int)L.ChkNumber(2);
			Application.CaptureScreenshot(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Application.CaptureScreenshot");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasProLicense(IntPtr L)
	{
		L.ChkArgsCount(0);
		bool o = Application.HasProLicense();
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ExternalCall(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		var arg0 = L.ToLuaString(1);
		object[] objs1 = L.ToParamsObject(2, count - 1);
		Application.ExternalCall(arg0,objs1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OpenURL(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		Application.OpenURL(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RequestUserAuthorization(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = (UserAuthorization)L.ChkEnumValue(1, typeof(UserAuthorization));
		AsyncOperation o = Application.RequestUserAuthorization(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasUserAuthorization(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = (UserAuthorization)L.ChkEnumValue(1, typeof(UserAuthorization));
		bool o = Application.HasUserAuthorization(arg0);
		L.PushBoolean(o);
		return 1;
	}
}


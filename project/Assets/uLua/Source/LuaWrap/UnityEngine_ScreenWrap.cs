using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_ScreenWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetResolution", SetResolution),
			new LuaMethod("new", _CreateScreen),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("resolutions", get_resolutions, null),
			new LuaField("currentResolution", get_currentResolution, null),
			new LuaField("width", get_width, null),
			new LuaField("height", get_height, null),
			new LuaField("dpi", get_dpi, null),
			new LuaField("fullScreen", get_fullScreen, set_fullScreen),
			new LuaField("autorotateToPortrait", get_autorotateToPortrait, set_autorotateToPortrait),
			new LuaField("autorotateToPortraitUpsideDown", get_autorotateToPortraitUpsideDown, set_autorotateToPortraitUpsideDown),
			new LuaField("autorotateToLandscapeLeft", get_autorotateToLandscapeLeft, set_autorotateToLandscapeLeft),
			new LuaField("autorotateToLandscapeRight", get_autorotateToLandscapeRight, set_autorotateToLandscapeRight),
			new LuaField("orientation", get_orientation, set_orientation),
			new LuaField("sleepTimeout", get_sleepTimeout, set_sleepTimeout),
		};

		var type = typeof(Screen);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateScreen(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Screen obj = new Screen();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Screen.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Screen));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_resolutions(IntPtr L)
	{
		L.PushUData(Screen.resolutions);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currentResolution(IntPtr L)
	{
		L.PushLightUserData(Screen.currentResolution);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_width(IntPtr L)
	{
		L.PushInteger(Screen.width);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_height(IntPtr L)
	{
		L.PushInteger(Screen.height);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dpi(IntPtr L)
	{
		L.PushNumber(Screen.dpi);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fullScreen(IntPtr L)
	{
		L.PushBoolean(Screen.fullScreen);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autorotateToPortrait(IntPtr L)
	{
		L.PushBoolean(Screen.autorotateToPortrait);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autorotateToPortraitUpsideDown(IntPtr L)
	{
		L.PushBoolean(Screen.autorotateToPortraitUpsideDown);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autorotateToLandscapeLeft(IntPtr L)
	{
		L.PushBoolean(Screen.autorotateToLandscapeLeft);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autorotateToLandscapeRight(IntPtr L)
	{
		L.PushBoolean(Screen.autorotateToLandscapeRight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_orientation(IntPtr L)
	{
		L.PushUData(Screen.orientation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sleepTimeout(IntPtr L)
	{
		L.PushInteger(Screen.sleepTimeout);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fullScreen(IntPtr L)
	{
		Screen.fullScreen = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autorotateToPortrait(IntPtr L)
	{
		Screen.autorotateToPortrait = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autorotateToPortraitUpsideDown(IntPtr L)
	{
		Screen.autorotateToPortraitUpsideDown = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autorotateToLandscapeLeft(IntPtr L)
	{
		Screen.autorotateToLandscapeLeft = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autorotateToLandscapeRight(IntPtr L)
	{
		Screen.autorotateToLandscapeRight = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_orientation(IntPtr L)
	{
		Screen.orientation = (ScreenOrientation)L.ChkEnumValue(3, typeof(ScreenOrientation));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sleepTimeout(IntPtr L)
	{
		Screen.sleepTimeout = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetResolution(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			var arg0 = (int)L.ChkNumber(1);
			var arg1 = (int)L.ChkNumber(2);
			var arg2 = L.ChkBoolean(3);
			Screen.SetResolution(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4)
		{
			var arg0 = (int)L.ChkNumber(1);
			var arg1 = (int)L.ChkNumber(2);
			var arg2 = L.ChkBoolean(3);
			var arg3 = (int)L.ChkNumber(4);
			Screen.SetResolution(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Screen.SetResolution");
		}

		return 0;
	}
}


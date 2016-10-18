using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_TimeWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateTime),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("time", get_time, null),
			new LuaField("timeSinceLevelLoad", get_timeSinceLevelLoad, null),
			new LuaField("deltaTime", get_deltaTime, null),
			new LuaField("fixedTime", get_fixedTime, null),
			new LuaField("unscaledTime", get_unscaledTime, null),
			new LuaField("unscaledDeltaTime", get_unscaledDeltaTime, null),
			new LuaField("fixedDeltaTime", get_fixedDeltaTime, set_fixedDeltaTime),
			new LuaField("maximumDeltaTime", get_maximumDeltaTime, set_maximumDeltaTime),
			new LuaField("smoothDeltaTime", get_smoothDeltaTime, null),
			new LuaField("timeScale", get_timeScale, set_timeScale),
			new LuaField("frameCount", get_frameCount, null),
			new LuaField("renderedFrameCount", get_renderedFrameCount, null),
			new LuaField("realtimeSinceStartup", get_realtimeSinceStartup, null),
			new LuaField("captureFramerate", get_captureFramerate, set_captureFramerate),
		};

		var type = typeof(Time);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTime(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Time obj = new Time();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Time.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Time));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_time(IntPtr L)
	{
		L.PushNumber(Time.time);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_timeSinceLevelLoad(IntPtr L)
	{
		L.PushNumber(Time.timeSinceLevelLoad);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deltaTime(IntPtr L)
	{
		L.PushNumber(Time.deltaTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fixedTime(IntPtr L)
	{
		L.PushNumber(Time.fixedTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unscaledTime(IntPtr L)
	{
		L.PushNumber(Time.unscaledTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_unscaledDeltaTime(IntPtr L)
	{
		L.PushNumber(Time.unscaledDeltaTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fixedDeltaTime(IntPtr L)
	{
		L.PushNumber(Time.fixedDeltaTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maximumDeltaTime(IntPtr L)
	{
		L.PushNumber(Time.maximumDeltaTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_smoothDeltaTime(IntPtr L)
	{
		L.PushNumber(Time.smoothDeltaTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_timeScale(IntPtr L)
	{
		L.PushNumber(Time.timeScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_frameCount(IntPtr L)
	{
		L.PushInteger(Time.frameCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_renderedFrameCount(IntPtr L)
	{
		L.PushInteger(Time.renderedFrameCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_realtimeSinceStartup(IntPtr L)
	{
		L.PushNumber(Time.realtimeSinceStartup);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_captureFramerate(IntPtr L)
	{
		L.PushInteger(Time.captureFramerate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fixedDeltaTime(IntPtr L)
	{
		Time.fixedDeltaTime = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maximumDeltaTime(IntPtr L)
	{
		Time.maximumDeltaTime = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_timeScale(IntPtr L)
	{
		Time.timeScale = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_captureFramerate(IntPtr L)
	{
		Time.captureFramerate = (int)L.ChkNumber(3);
		return 0;
	}
}


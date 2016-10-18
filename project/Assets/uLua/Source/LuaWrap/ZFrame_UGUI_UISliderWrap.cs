using System;
using LuaInterface;

public class ZFrame_UGUI_UISliderWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetProgress", SetProgress),
			new LuaMethod("Tween", Tween),
			new LuaMethod("new", _CreateZFrame_UGUI_UISlider),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("minLmt", get_minLmt, set_minLmt),
			new LuaField("maxLmt", get_maxLmt, set_maxLmt),
			new LuaField("onValue", get_onValue, set_onValue),
		};

		var type = typeof(ZFrame.UGUI.UISlider);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UISlider(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UISlider class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UISlider));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minLmt(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minLmt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minLmt on a nil value");
			}
		}

		L.PushNumber(obj.minLmt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxLmt(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxLmt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxLmt on a nil value");
			}
		}

		L.PushNumber(obj.maxLmt);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValue on a nil value");
			}
		}

		L.PushUData(obj.onValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minLmt(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minLmt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minLmt on a nil value");
			}
		}

		obj.minLmt = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxLmt(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxLmt");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxLmt on a nil value");
			}
		}

		obj.maxLmt = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValue on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onValue = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UISlider>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UISlider>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onValue = (param0) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetProgress(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UISlider");
		var arg0 = (float)L.ChkNumber(2);
		obj.SetProgress(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Tween(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.UGUI.UISlider obj = (ZFrame.UGUI.UISlider)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UISlider");
		var arg0 = L.ToAnyObject(2);
		var arg1 = L.ToAnyObject(3);
		var arg2 = (float)L.ChkNumber(4);
		ZFrame.Tween.ZTweener o = obj.Tween(arg0,arg1,arg2);
		L.PushLightUserData(o);
		return 1;
	}
}


using System;
using LuaInterface;

public class ZFrame_UGUI_UILongpressWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnPointerDown", OnPointerDown),
			new LuaMethod("OnPointerUp", OnPointerUp),
			new LuaMethod("new", _CreateZFrame_UGUI_UILongpress),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("threshold", get_threshold, set_threshold),
			new LuaField("interval", get_interval, set_interval),
			new LuaField("onAction", get_onAction, set_onAction),
		};

		var type = typeof(ZFrame.UGUI.UILongpress);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UILongpress(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UILongpress class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UILongpress));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UILongpress.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_threshold(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name threshold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index threshold on a nil value");
			}
		}

		L.PushNumber(obj.threshold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_interval(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interval on a nil value");
			}
		}

		L.PushNumber(obj.interval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAction on a nil value");
			}
		}

		L.PushUData(obj.onAction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		ZFrame.UGUI.UILongpress.current = L.ToComponent(3, typeof(ZFrame.UGUI.UILongpress)) as ZFrame.UGUI.UILongpress;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_threshold(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name threshold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index threshold on a nil value");
			}
		}

		obj.threshold = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_interval(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interval on a nil value");
			}
		}

		obj.interval = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onAction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAction on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onAction = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UILongpress>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UILongpress>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onAction = (param0) =>
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
	static int OnPointerDown(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UILongpress");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerUp(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UILongpress obj = (ZFrame.UGUI.UILongpress)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UILongpress");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerUp(arg0);
		return 0;
	}
}


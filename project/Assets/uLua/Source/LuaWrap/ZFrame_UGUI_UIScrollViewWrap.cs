using System;
using LuaInterface;

public class ZFrame_UGUI_UIScrollViewWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnBeginDrag", OnBeginDrag),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("OnEndDrag", OnEndDrag),
			new LuaMethod("OnScroll", OnScroll),
			new LuaMethod("new", _CreateZFrame_UGUI_UIScrollView),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("onBeginDrag", get_onBeginDrag, set_onBeginDrag),
			new LuaField("onDrag", get_onDrag, set_onDrag),
			new LuaField("onEndDrag", get_onEndDrag, set_onEndDrag),
			new LuaField("onScroll", get_onScroll, set_onScroll),
		};

		var type = typeof(ZFrame.UGUI.UIScrollView);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIScrollView(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIScrollView class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIScrollView));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIScrollView.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onBeginDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBeginDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBeginDrag on a nil value");
			}
		}

		L.PushUData(obj.onBeginDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrag on a nil value");
			}
		}

		L.PushUData(obj.onDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onEndDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEndDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEndDrag on a nil value");
			}
		}

		L.PushUData(obj.onEndDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onScroll(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onScroll");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onScroll on a nil value");
			}
		}

		L.PushUData(obj.onScroll);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		ZFrame.UGUI.UIScrollView.current = L.ToComponent(3, typeof(ZFrame.UGUI.UIScrollView)) as ZFrame.UGUI.UIScrollView;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onBeginDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onBeginDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onBeginDrag on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onBeginDrag = (UnityEngine.Events.UnityAction)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onBeginDrag = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrag on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDrag = (UnityEngine.Events.UnityAction)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onDrag = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onEndDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onEndDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onEndDrag on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onEndDrag = (UnityEngine.Events.UnityAction)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onEndDrag = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onScroll(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onScroll");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onScroll on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onScroll = (UnityEngine.Events.UnityAction)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onScroll = () =>
			{
				func.Call();
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIScrollView");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnBeginDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIScrollView");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIScrollView");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnEndDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnScroll(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIScrollView obj = (ZFrame.UGUI.UIScrollView)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIScrollView");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnScroll(arg0);
		return 0;
	}
}


using System;
using LuaInterface;

public class ZFrame_UGUI_UIDraggedWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnBeginDrag", OnBeginDrag),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("OnEndDrag", OnEndDrag),
			new LuaMethod("new", _CreateZFrame_UGUI_UIDragged),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("eventData", get_eventData, set_eventData),
			new LuaField("cloneOnDrag", get_cloneOnDrag, set_cloneOnDrag),
			new LuaField("target", get_target, set_target),
			new LuaField("onBeginDrag", get_onBeginDrag, set_onBeginDrag),
			new LuaField("onDragging", get_onDragging, set_onDragging),
			new LuaField("onEndDrag", get_onEndDrag, set_onEndDrag),
			new LuaField("DraggingObject", get_DraggingObject, null),
		};

		var type = typeof(ZFrame.UGUI.UIDragged);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIDragged(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIDragged class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIDragged));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIDragged.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eventData(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIDragged.eventData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cloneOnDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cloneOnDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cloneOnDrag on a nil value");
			}
		}

		L.PushBoolean(obj.cloneOnDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_target(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		L.PushLightUserData(obj.target);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onBeginDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

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
	static int get_onDragging(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragging on a nil value");
			}
		}

		L.PushUData(obj.onDragging);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onEndDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

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
	static int get_DraggingObject(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name DraggingObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index DraggingObject on a nil value");
			}
		}

		L.PushLightUserData(obj.DraggingObject);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		ZFrame.UGUI.UIDragged.current = L.ToComponent(3, typeof(ZFrame.UGUI.UIDragged)) as ZFrame.UGUI.UIDragged;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eventData(IntPtr L)
	{
		ZFrame.UGUI.UIDragged.eventData = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(3, typeof(UnityEngine.EventSystems.BaseEventData));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cloneOnDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cloneOnDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cloneOnDrag on a nil value");
			}
		}

		obj.cloneOnDrag = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_target(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		obj.target = L.ToGameObject(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onBeginDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

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
	static int set_onDragging(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDragging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDragging on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDragging = (UnityEngine.Events.UnityAction)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onDragging = () =>
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
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)o;

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
	static int OnBeginDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIDragged");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnBeginDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIDragged");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIDragged obj = (ZFrame.UGUI.UIDragged)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIDragged");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnEndDrag(arg0);
		return 0;
	}
}


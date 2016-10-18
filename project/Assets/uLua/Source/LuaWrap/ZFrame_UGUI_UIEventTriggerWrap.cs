using System;
using LuaInterface;

public class ZFrame_UGUI_UIEventTriggerWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnPointerEnter", OnPointerEnter),
			new LuaMethod("OnPointerExit", OnPointerExit),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("OnDrop", OnDrop),
			new LuaMethod("OnPointerDown", OnPointerDown),
			new LuaMethod("OnPointerUp", OnPointerUp),
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("OnSelect", OnSelect),
			new LuaMethod("OnDeselect", OnDeselect),
			new LuaMethod("OnScroll", OnScroll),
			new LuaMethod("OnMove", OnMove),
			new LuaMethod("OnUpdateSelected", OnUpdateSelected),
			new LuaMethod("OnInitializePotentialDrag", OnInitializePotentialDrag),
			new LuaMethod("OnBeginDrag", OnBeginDrag),
			new LuaMethod("OnEndDrag", OnEndDrag),
			new LuaMethod("OnSubmit", OnSubmit),
			new LuaMethod("OnCancel", OnCancel),
			new LuaMethod("new", _CreateZFrame_UGUI_UIEventTrigger),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("eventData", get_eventData, set_eventData),
			new LuaField("onPointerEnter", get_onPointerEnter, set_onPointerEnter),
			new LuaField("onPointerExit", get_onPointerExit, set_onPointerExit),
			new LuaField("onDrag", get_onDrag, set_onDrag),
			new LuaField("onDrop", get_onDrop, set_onDrop),
			new LuaField("onPointerDown", get_onPointerDown, set_onPointerDown),
			new LuaField("onPointerUp", get_onPointerUp, set_onPointerUp),
			new LuaField("onPointerClick", get_onPointerClick, set_onPointerClick),
			new LuaField("onSelect", get_onSelect, set_onSelect),
			new LuaField("onDeselect", get_onDeselect, set_onDeselect),
			new LuaField("onScroll", get_onScroll, set_onScroll),
			new LuaField("onMove", get_onMove, set_onMove),
			new LuaField("onUpdateSelected", get_onUpdateSelected, set_onUpdateSelected),
			new LuaField("onInitializePotentialDrag", get_onInitializePotentialDrag, set_onInitializePotentialDrag),
			new LuaField("onBeginDrag", get_onBeginDrag, set_onBeginDrag),
			new LuaField("onEndDrag", get_onEndDrag, set_onEndDrag),
			new LuaField("onSubmit", get_onSubmit, set_onSubmit),
			new LuaField("onCancel", get_onCancel, set_onCancel),
		};

		var type = typeof(ZFrame.UGUI.UIEventTrigger);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIEventTrigger(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIEventTrigger class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIEventTrigger));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIEventTrigger.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eventData(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIEventTrigger.eventData);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerEnter(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerEnter on a nil value");
			}
		}

		L.PushUData(obj.onPointerEnter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerExit(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerExit on a nil value");
			}
		}

		L.PushUData(obj.onPointerExit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
	static int get_onDrop(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrop on a nil value");
			}
		}

		L.PushUData(obj.onDrop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerDown(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerDown");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerDown on a nil value");
			}
		}

		L.PushUData(obj.onPointerDown);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerUp(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerUp on a nil value");
			}
		}

		L.PushUData(obj.onPointerUp);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPointerClick(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerClick on a nil value");
			}
		}

		L.PushUData(obj.onPointerClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onSelect(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSelect on a nil value");
			}
		}

		L.PushUData(obj.onSelect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDeselect(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDeselect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDeselect on a nil value");
			}
		}

		L.PushUData(obj.onDeselect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onScroll(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
	static int get_onMove(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onMove on a nil value");
			}
		}

		L.PushUData(obj.onMove);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onUpdateSelected(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onUpdateSelected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onUpdateSelected on a nil value");
			}
		}

		L.PushUData(obj.onUpdateSelected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onInitializePotentialDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onInitializePotentialDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onInitializePotentialDrag on a nil value");
			}
		}

		L.PushUData(obj.onInitializePotentialDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onBeginDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
	static int get_onEndDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
	static int get_onSubmit(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSubmit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSubmit on a nil value");
			}
		}

		L.PushUData(obj.onSubmit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onCancel(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCancel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCancel on a nil value");
			}
		}

		L.PushUData(obj.onCancel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		ZFrame.UGUI.UIEventTrigger.current = L.ToComponent(3, typeof(ZFrame.UGUI.UIEventTrigger)) as ZFrame.UGUI.UIEventTrigger;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eventData(IntPtr L)
	{
		ZFrame.UGUI.UIEventTrigger.eventData = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(3, typeof(UnityEngine.EventSystems.BaseEventData));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPointerEnter(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerEnter on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onPointerEnter = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onPointerEnter = (param0) =>
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
	static int set_onPointerExit(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerExit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerExit on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onPointerExit = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onPointerExit = (param0) =>
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
	static int set_onDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
			obj.onDrag = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onDrag = (param0) =>
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
	static int set_onDrop(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDrop");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDrop on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDrop = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onDrop = (param0) =>
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
	static int set_onPointerDown(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerDown");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerDown on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onPointerDown = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onPointerDown = (param0) =>
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
	static int set_onPointerUp(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerUp");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerUp on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onPointerUp = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onPointerUp = (param0) =>
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
	static int set_onPointerClick(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onPointerClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onPointerClick on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onPointerClick = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onPointerClick = (param0) =>
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
	static int set_onSelect(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSelect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSelect on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onSelect = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onSelect = (param0) =>
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
	static int set_onDeselect(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDeselect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDeselect on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDeselect = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onDeselect = (param0) =>
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
	static int set_onScroll(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
			obj.onScroll = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onScroll = (param0) =>
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
	static int set_onMove(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onMove");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onMove on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onMove = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onMove = (param0) =>
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
	static int set_onUpdateSelected(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onUpdateSelected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onUpdateSelected on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onUpdateSelected = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onUpdateSelected = (param0) =>
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
	static int set_onInitializePotentialDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onInitializePotentialDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onInitializePotentialDrag on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onInitializePotentialDrag = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onInitializePotentialDrag = (param0) =>
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
	static int set_onBeginDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
			obj.onBeginDrag = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onBeginDrag = (param0) =>
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
	static int set_onEndDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

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
			obj.onEndDrag = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onEndDrag = (param0) =>
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
	static int set_onSubmit(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSubmit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSubmit on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onSubmit = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onSubmit = (param0) =>
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
	static int set_onCancel(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCancel");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCancel on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onCancel = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onCancel = (param0) =>
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
	static int OnPointerEnter(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerExit(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerExit(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrop(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnDrop(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerDown(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerUp(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerUp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSelect(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnSelect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeselect(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnDeselect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnScroll(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnScroll(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMove(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.AxisEventData arg0 = (UnityEngine.EventSystems.AxisEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.AxisEventData));
		obj.OnMove(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnUpdateSelected(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnUpdateSelected(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnInitializePotentialDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnInitializePotentialDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnBeginDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnEndDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSubmit(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnSubmit(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnCancel(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIEventTrigger obj = (ZFrame.UGUI.UIEventTrigger)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIEventTrigger");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnCancel(arg0);
		return 0;
	}
}


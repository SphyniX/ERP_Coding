using System;
using UnityEngine;
using LuaInterface;

public class ZFrame_UGUI_UIToggleWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("SetInteractable", SetInteractable),
			new LuaMethod("new", _CreateZFrame_UGUI_UIToggle),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("onToggleClick", get_onToggleClick, set_onToggleClick),
			new LuaField("onAction", get_onAction, set_onAction),
			new LuaField("checkedTrans", get_checkedTrans, set_checkedTrans),
			new LuaField("disabled", get_disabled, set_disabled),
			new LuaField("value", get_value, set_value),
		};

		var type = typeof(ZFrame.UGUI.UIToggle);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIToggle(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIToggle class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIToggle));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIToggle.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onToggleClick(IntPtr L)
	{
		L.PushUData(ZFrame.UGUI.UIToggle.onToggleClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

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
	static int get_checkedTrans(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name checkedTrans");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index checkedTrans on a nil value");
			}
		}

		L.PushLightUserData(obj.checkedTrans);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_disabled(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disabled on a nil value");
			}
		}

		L.PushBoolean(obj.disabled);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_value(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index value on a nil value");
			}
		}

		L.PushBoolean(obj.value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		ZFrame.UGUI.UIToggle.current = L.ToComponent(3, typeof(ZFrame.UGUI.UIToggle)) as ZFrame.UGUI.UIToggle;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onToggleClick(IntPtr L)
	{
		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			ZFrame.UGUI.UIToggle.onToggleClick = (UnityEngine.Events.UnityAction<GameObject>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<GameObject>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			ZFrame.UGUI.UIToggle.onToggleClick = (param0) =>
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
	static int set_onAction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

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
			obj.onAction = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIToggle>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIToggle>));
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
	static int set_checkedTrans(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name checkedTrans");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index checkedTrans on a nil value");
			}
		}

		obj.checkedTrans = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_disabled(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disabled on a nil value");
			}
		}

		obj.disabled = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_value(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name value");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index value on a nil value");
			}
		}

		obj.value = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIToggle");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInteractable(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIToggle obj = (ZFrame.UGUI.UIToggle)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIToggle");
		var arg0 = L.ChkBoolean(2);
		obj.SetInteractable(arg0);
		return 0;
	}
}


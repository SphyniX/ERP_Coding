using System;
using UnityEngine;
using LuaInterface;

public class ZFrame_UGUI_UIButtonWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("OnClick", OnClick),
			new LuaMethod("SetInteractable", SetInteractable),
			new LuaMethod("new", _CreateZFrame_UGUI_UIButton),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("onButtonClick", get_onButtonClick, set_onButtonClick),
			new LuaField("onAction", get_onAction, set_onAction),
		};

		var type = typeof(ZFrame.UGUI.UIButton);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIButton(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIButton class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIButton));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIButton.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onButtonClick(IntPtr L)
	{
		L.PushUData(ZFrame.UGUI.UIButton.onButtonClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIButton obj = (ZFrame.UGUI.UIButton)o;

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
		ZFrame.UGUI.UIButton.current = L.ToComponent(3, typeof(ZFrame.UGUI.UIButton)) as ZFrame.UGUI.UIButton;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onButtonClick(IntPtr L)
	{
		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			ZFrame.UGUI.UIButton.onButtonClick = (UnityEngine.Events.UnityAction<GameObject>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<GameObject>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			ZFrame.UGUI.UIButton.onButtonClick = (param0) =>
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
		ZFrame.UGUI.UIButton obj = (ZFrame.UGUI.UIButton)o;

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
			obj.onAction = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIButton>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIButton>));
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
	static int OnPointerClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIButton obj = (ZFrame.UGUI.UIButton)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIButton");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIButton obj = (ZFrame.UGUI.UIButton)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIButton");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInteractable(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIButton obj = (ZFrame.UGUI.UIButton)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIButton");
		var arg0 = L.ChkBoolean(2);
		obj.SetInteractable(arg0);
		return 0;
	}
}


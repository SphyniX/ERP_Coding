using System;
using LuaInterface;

public class UnityEngine_UI_ToggleWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Rebuild", Rebuild),
			new LuaMethod("LayoutComplete", LayoutComplete),
			new LuaMethod("GraphicUpdateComplete", GraphicUpdateComplete),
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("OnSubmit", OnSubmit),
			new LuaMethod("new", _CreateUnityEngine_UI_Toggle),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("toggleTransition", get_toggleTransition, set_toggleTransition),
			new LuaField("graphic", get_graphic, set_graphic),
			new LuaField("onValueChanged", get_onValueChanged, set_onValueChanged),
			new LuaField("group", get_group, set_group),
			new LuaField("isOn", get_isOn, set_isOn),
		};

		var type = typeof(UnityEngine.UI.Toggle);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_Toggle(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.Toggle class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.Toggle));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_toggleTransition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name toggleTransition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index toggleTransition on a nil value");
			}
		}

		L.PushUData(obj.toggleTransition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphic(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name graphic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index graphic on a nil value");
			}
		}

		L.PushLightUserData(obj.graphic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		L.PushLightUserData(obj.onValueChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_group(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name group");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index group on a nil value");
			}
		}

		L.PushLightUserData(obj.group);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isOn(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isOn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isOn on a nil value");
			}
		}

		L.PushBoolean(obj.isOn);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_toggleTransition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name toggleTransition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index toggleTransition on a nil value");
			}
		}

		obj.toggleTransition = (UnityEngine.UI.Toggle.ToggleTransition)L.ChkEnumValue(3, typeof(UnityEngine.UI.Toggle.ToggleTransition));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_graphic(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name graphic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index graphic on a nil value");
			}
		}

		obj.graphic = L.ToComponent(3, typeof(UnityEngine.UI.Graphic)) as UnityEngine.UI.Graphic;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		obj.onValueChanged = (UnityEngine.UI.Toggle.ToggleEvent)L.ChkUserData(3, typeof(UnityEngine.UI.Toggle.ToggleEvent));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_group(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name group");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index group on a nil value");
			}
		}

		obj.group = L.ToComponent(3, typeof(UnityEngine.UI.ToggleGroup)) as UnityEngine.UI.ToggleGroup;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isOn(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isOn");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isOn on a nil value");
			}
		}

		obj.isOn = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rebuild(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Toggle");
		var arg0 = (UnityEngine.UI.CanvasUpdate)L.ChkEnumValue(2, typeof(UnityEngine.UI.CanvasUpdate));
		obj.Rebuild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LayoutComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Toggle");
		obj.LayoutComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GraphicUpdateComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Toggle");
		obj.GraphicUpdateComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Toggle");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSubmit(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Toggle obj = (UnityEngine.UI.Toggle)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Toggle");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnSubmit(arg0);
		return 0;
	}
}


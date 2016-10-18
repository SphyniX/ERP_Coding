using System;
using LuaInterface;

public class UnityEngine_UI_SliderWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Rebuild", Rebuild),
			new LuaMethod("LayoutComplete", LayoutComplete),
			new LuaMethod("GraphicUpdateComplete", GraphicUpdateComplete),
			new LuaMethod("OnPointerDown", OnPointerDown),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("OnMove", OnMove),
			new LuaMethod("FindSelectableOnLeft", FindSelectableOnLeft),
			new LuaMethod("FindSelectableOnRight", FindSelectableOnRight),
			new LuaMethod("FindSelectableOnUp", FindSelectableOnUp),
			new LuaMethod("FindSelectableOnDown", FindSelectableOnDown),
			new LuaMethod("OnInitializePotentialDrag", OnInitializePotentialDrag),
			new LuaMethod("SetDirection", SetDirection),
			new LuaMethod("new", _CreateUnityEngine_UI_Slider),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("fillRect", get_fillRect, set_fillRect),
			new LuaField("handleRect", get_handleRect, set_handleRect),
			new LuaField("direction", get_direction, set_direction),
			new LuaField("minValue", get_minValue, set_minValue),
			new LuaField("maxValue", get_maxValue, set_maxValue),
			new LuaField("wholeNumbers", get_wholeNumbers, set_wholeNumbers),
			new LuaField("value", get_value, set_value),
			new LuaField("normalizedValue", get_normalizedValue, set_normalizedValue),
			new LuaField("onValueChanged", get_onValueChanged, set_onValueChanged),
		};

		var type = typeof(UnityEngine.UI.Slider);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_Slider(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.Slider class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.Slider));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillRect on a nil value");
			}
		}

		L.PushLightUserData(obj.fillRect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_handleRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name handleRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index handleRect on a nil value");
			}
		}

		L.PushLightUserData(obj.handleRect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_direction(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name direction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index direction on a nil value");
			}
		}

		L.PushUData(obj.direction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minValue on a nil value");
			}
		}

		L.PushNumber(obj.minValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxValue on a nil value");
			}
		}

		L.PushNumber(obj.maxValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_wholeNumbers(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wholeNumbers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wholeNumbers on a nil value");
			}
		}

		L.PushBoolean(obj.wholeNumbers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_value(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

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

		L.PushNumber(obj.value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_normalizedValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalizedValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalizedValue on a nil value");
			}
		}

		L.PushNumber(obj.normalizedValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

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
	static int set_fillRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillRect on a nil value");
			}
		}

		obj.fillRect = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_handleRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name handleRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index handleRect on a nil value");
			}
		}

		obj.handleRect = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_direction(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name direction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index direction on a nil value");
			}
		}

		obj.direction = (UnityEngine.UI.Slider.Direction)L.ChkEnumValue(3, typeof(UnityEngine.UI.Slider.Direction));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minValue on a nil value");
			}
		}

		obj.minValue = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxValue on a nil value");
			}
		}

		obj.maxValue = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_wholeNumbers(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wholeNumbers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wholeNumbers on a nil value");
			}
		}

		obj.wholeNumbers = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_value(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

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

		obj.value = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_normalizedValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalizedValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalizedValue on a nil value");
			}
		}

		obj.normalizedValue = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)o;

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

		obj.onValueChanged = (UnityEngine.UI.Slider.SliderEvent)L.ChkUserData(3, typeof(UnityEngine.UI.Slider.SliderEvent));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rebuild(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		var arg0 = (UnityEngine.UI.CanvasUpdate)L.ChkEnumValue(2, typeof(UnityEngine.UI.CanvasUpdate));
		obj.Rebuild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LayoutComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		obj.LayoutComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GraphicUpdateComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		obj.GraphicUpdateComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerDown(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMove(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.EventSystems.AxisEventData arg0 = (UnityEngine.EventSystems.AxisEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.AxisEventData));
		obj.OnMove(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnLeft(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnLeft();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnRight(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnRight();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnUp(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnUp();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnDown(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnDown();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnInitializePotentialDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnInitializePotentialDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetDirection(IntPtr L)
	{
		L.ChkArgsCount(3);
		UnityEngine.UI.Slider obj = (UnityEngine.UI.Slider)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Slider");
		var arg0 = (UnityEngine.UI.Slider.Direction)L.ChkEnumValue(2, typeof(UnityEngine.UI.Slider.Direction));
		var arg1 = L.ChkBoolean(3);
		obj.SetDirection(arg0,arg1);
		return 0;
	}
}


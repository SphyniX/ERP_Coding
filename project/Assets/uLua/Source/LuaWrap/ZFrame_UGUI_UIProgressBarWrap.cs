using System;
using LuaInterface;

public class ZFrame_UGUI_UIProgressBarWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("InitValue", InitValue),
			new LuaMethod("Tween", Tween),
			new LuaMethod("new", _CreateZFrame_UGUI_UIProgressBar),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("minValue", get_minValue, null),
			new LuaField("maxValue", get_maxValue, null),
			new LuaField("maxLayer", get_maxLayer, set_maxLayer),
			new LuaField("m_PrevBar", get_m_PrevBar, set_m_PrevBar),
			new LuaField("m_FadeBar", get_m_FadeBar, set_m_FadeBar),
			new LuaField("m_CurrBar", get_m_CurrBar, set_m_CurrBar),
			new LuaField("m_Thumb", get_m_Thumb, set_m_Thumb),
			new LuaField("direction", get_direction, set_direction),
			new LuaField("onValueChanged", get_onValueChanged, set_onValueChanged),
			new LuaField("value", get_value, set_value),
			new LuaField("rectTransform", get_rectTransform, null),
		};

		var type = typeof(ZFrame.UGUI.UIProgressBar);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIProgressBar(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIProgressBar class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIProgressBar));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
	static int get_maxLayer(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxLayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxLayer on a nil value");
			}
		}

		L.PushInteger(obj.maxLayer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_PrevBar(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_PrevBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_PrevBar on a nil value");
			}
		}

		L.PushLightUserData(obj.m_PrevBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_FadeBar(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_FadeBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_FadeBar on a nil value");
			}
		}

		L.PushLightUserData(obj.m_FadeBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_CurrBar(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_CurrBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_CurrBar on a nil value");
			}
		}

		L.PushLightUserData(obj.m_CurrBar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_m_Thumb(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_Thumb");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_Thumb on a nil value");
			}
		}

		L.PushLightUserData(obj.m_Thumb);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_direction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
	static int get_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
	static int get_value(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
	static int get_rectTransform(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rectTransform");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rectTransform on a nil value");
			}
		}

		L.PushLightUserData(obj.rectTransform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxLayer(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maxLayer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maxLayer on a nil value");
			}
		}

		obj.maxLayer = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_PrevBar(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_PrevBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_PrevBar on a nil value");
			}
		}

		obj.m_PrevBar = L.ToComponent(3, typeof(UnityEngine.UI.Image)) as UnityEngine.UI.Image;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_FadeBar(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_FadeBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_FadeBar on a nil value");
			}
		}

		obj.m_FadeBar = L.ToComponent(3, typeof(UnityEngine.UI.Image)) as UnityEngine.UI.Image;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_CurrBar(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_CurrBar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_CurrBar on a nil value");
			}
		}

		obj.m_CurrBar = L.ToComponent(3, typeof(UnityEngine.UI.Image)) as UnityEngine.UI.Image;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_m_Thumb(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name m_Thumb");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index m_Thumb on a nil value");
			}
		}

		obj.m_Thumb = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_direction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
	static int set_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
	static int set_value(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)o;

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
	static int InitValue(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIProgressBar");
		var arg0 = (float)L.ChkNumber(2);
		obj.InitValue(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Tween(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.UGUI.UIProgressBar obj = (ZFrame.UGUI.UIProgressBar)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UIProgressBar");
		var arg0 = L.ToAnyObject(2);
		var arg1 = L.ToAnyObject(3);
		var arg2 = (float)L.ChkNumber(4);
		ZFrame.Tween.ZTweener o = obj.Tween(arg0,arg1,arg2);
		L.PushLightUserData(o);
		return 1;
	}
}


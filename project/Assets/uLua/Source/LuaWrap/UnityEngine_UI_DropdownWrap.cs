using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_DropdownWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("RefreshShownValue", RefreshShownValue),
			new LuaMethod("AddOptions", AddOptions),
			new LuaMethod("ClearOptions", ClearOptions),
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("OnSubmit", OnSubmit),
			new LuaMethod("OnCancel", OnCancel),
			new LuaMethod("Show", Show),
			new LuaMethod("Hide", Hide),
			new LuaMethod("new", _CreateUnityEngine_UI_Dropdown),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("template", get_template, set_template),
			new LuaField("captionText", get_captionText, set_captionText),
			new LuaField("captionImage", get_captionImage, set_captionImage),
			new LuaField("itemText", get_itemText, set_itemText),
			new LuaField("itemImage", get_itemImage, set_itemImage),
			new LuaField("options", get_options, set_options),
			new LuaField("onValueChanged", get_onValueChanged, set_onValueChanged),
			new LuaField("value", get_value, set_value),
		};

		var type = typeof(UnityEngine.UI.Dropdown);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_Dropdown(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.Dropdown class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.Dropdown));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_template(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name template");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index template on a nil value");
			}
		}

		L.PushLightUserData(obj.template);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_captionText(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name captionText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index captionText on a nil value");
			}
		}

		L.PushLightUserData(obj.captionText);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_captionImage(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name captionImage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index captionImage on a nil value");
			}
		}

		L.PushLightUserData(obj.captionImage);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_itemText(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name itemText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index itemText on a nil value");
			}
		}

		L.PushLightUserData(obj.itemText);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_itemImage(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name itemImage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index itemImage on a nil value");
			}
		}

		L.PushLightUserData(obj.itemImage);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_options(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name options");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index options on a nil value");
			}
		}

		L.PushUData(obj.options);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

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
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

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

		L.PushInteger(obj.value);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_template(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name template");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index template on a nil value");
			}
		}

		obj.template = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_captionText(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name captionText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index captionText on a nil value");
			}
		}

		obj.captionText = L.ToComponent(3, typeof(UnityEngine.UI.Text)) as UnityEngine.UI.Text;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_captionImage(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name captionImage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index captionImage on a nil value");
			}
		}

		obj.captionImage = L.ToComponent(3, typeof(UnityEngine.UI.Image)) as UnityEngine.UI.Image;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_itemText(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name itemText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index itemText on a nil value");
			}
		}

		obj.itemText = L.ToComponent(3, typeof(UnityEngine.UI.Text)) as UnityEngine.UI.Text;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_itemImage(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name itemImage");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index itemImage on a nil value");
			}
		}

		obj.itemImage = L.ToComponent(3, typeof(UnityEngine.UI.Image)) as UnityEngine.UI.Image;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_options(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name options");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index options on a nil value");
			}
		}

		obj.options = (List<UnityEngine.UI.Dropdown.OptionData>)L.ChkUserData(3, typeof(List<UnityEngine.UI.Dropdown.OptionData>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

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

		obj.onValueChanged = (UnityEngine.UI.Dropdown.DropdownEvent)L.ChkUserData(3, typeof(UnityEngine.UI.Dropdown.DropdownEvent));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_value(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)o;

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

		obj.value = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RefreshShownValue(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
		obj.RefreshShownValue();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddOptions(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(UnityEngine.UI.Dropdown), typeof(List<Sprite>)))
		{
			UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
			List<Sprite> arg0 = (List<Sprite>)L.ToUserData(2);
			obj.AddOptions(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(UnityEngine.UI.Dropdown), typeof(List<string>)))
		{
			UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
			List<string> arg0 = (List<string>)L.ToUserData(2);
			obj.AddOptions(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(UnityEngine.UI.Dropdown), typeof(List<UnityEngine.UI.Dropdown.OptionData>)))
		{
			UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
			List<UnityEngine.UI.Dropdown.OptionData> arg0 = (List<UnityEngine.UI.Dropdown.OptionData>)L.ToUserData(2);
			obj.AddOptions(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.UI.Dropdown.AddOptions");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearOptions(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
		obj.ClearOptions();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSubmit(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnSubmit(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnCancel(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnCancel(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Show(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
		obj.Show();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Hide(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Dropdown obj = (UnityEngine.UI.Dropdown)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Dropdown");
		obj.Hide();
		return 0;
	}
}


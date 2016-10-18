using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_Dropdown_OptionDataWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateUnityEngine_UI_Dropdown_OptionData),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("text", get_text, set_text),
			new LuaField("image", get_image, set_image),
		};

		var type = typeof(UnityEngine.UI.Dropdown.OptionData);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_Dropdown_OptionData(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			UnityEngine.UI.Dropdown.OptionData obj = new UnityEngine.UI.Dropdown.OptionData();
			L.PushLightUserData(obj);
			return 1;
		}
		else if (count == 1 && L.CheckTypes(1, typeof(Sprite)))
		{
			Sprite arg0 = (Sprite)L.ChkUnityObject(1, typeof(Sprite));
			UnityEngine.UI.Dropdown.OptionData obj = new UnityEngine.UI.Dropdown.OptionData(arg0);
			L.PushLightUserData(obj);
			return 1;
		}
		else if (count == 1 && L.CheckTypes(1, typeof(string)))
		{
			var arg0 = L.ChkLuaString(1);
			UnityEngine.UI.Dropdown.OptionData obj = new UnityEngine.UI.Dropdown.OptionData(arg0);
			L.PushLightUserData(obj);
			return 1;
		}
		else if (count == 2)
		{
			var arg0 = L.ChkLuaString(1);
			Sprite arg1 = (Sprite)L.ChkUnityObject(2, typeof(Sprite));
			UnityEngine.UI.Dropdown.OptionData obj = new UnityEngine.UI.Dropdown.OptionData(arg0,arg1);
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.UI.Dropdown.OptionData.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.Dropdown.OptionData));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_text(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown.OptionData obj = (UnityEngine.UI.Dropdown.OptionData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name text");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index text on a nil value");
			}
		}

		L.PushString(obj.text);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_image(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown.OptionData obj = (UnityEngine.UI.Dropdown.OptionData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name image");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index image on a nil value");
			}
		}

		L.PushLightUserData(obj.image);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_text(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown.OptionData obj = (UnityEngine.UI.Dropdown.OptionData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name text");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index text on a nil value");
			}
		}

		obj.text = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_image(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Dropdown.OptionData obj = (UnityEngine.UI.Dropdown.OptionData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name image");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index image on a nil value");
			}
		}

		obj.image = (Sprite)L.ChkUnityObject(3, typeof(Sprite));
		return 0;
	}
}


using System;
using LuaInterface;

public class ZFrame_UGUI_UILabelWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetFormatArgs", SetFormatArgs),
			new LuaMethod("UpdateNumber", UpdateNumber),
			new LuaMethod("Tween", Tween),
			new LuaMethod("new", _CreateZFrame_UGUI_UILabel),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("localize", get_localize, set_localize),
			new LuaField("textFormat", get_textFormat, set_textFormat),
			new LuaField("text", get_text, set_text),
			new LuaField("rawText", get_rawText, null),
		};

		var type = typeof(ZFrame.UGUI.UILabel);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UILabel(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UILabel class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UILabel));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localize(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localize on a nil value");
			}
		}

		L.PushBoolean(obj.localize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_textFormat(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name textFormat");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index textFormat on a nil value");
			}
		}

		L.PushString(obj.textFormat);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_text(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)o;

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
	static int get_rawText(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rawText");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rawText on a nil value");
			}
		}

		L.PushString(obj.rawText);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localize(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localize on a nil value");
			}
		}

		obj.localize = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_textFormat(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name textFormat");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index textFormat on a nil value");
			}
		}

		obj.textFormat = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_text(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)o;

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
	static int SetFormatArgs(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UILabel");
		object[] objs0 = L.ToParamsObject(2, count - 1);
		obj.SetFormatArgs(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateNumber(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UILabel");
		var arg0 = (float)L.ChkNumber(2);
		obj.UpdateNumber(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Tween(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.UGUI.UILabel obj = (ZFrame.UGUI.UILabel)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UILabel");
		var arg0 = L.ToAnyObject(2);
		var arg1 = L.ToAnyObject(3);
		var arg2 = (float)L.ChkNumber(4);
		ZFrame.Tween.ZTweener o = obj.Tween(arg0,arg1,arg2);
		L.PushLightUserData(o);
		return 1;
	}
}


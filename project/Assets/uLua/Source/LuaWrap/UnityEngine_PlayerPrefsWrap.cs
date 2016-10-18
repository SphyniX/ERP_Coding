using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_PlayerPrefsWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetInt", SetInt),
			new LuaMethod("GetInt", GetInt),
			new LuaMethod("SetFloat", SetFloat),
			new LuaMethod("GetFloat", GetFloat),
			new LuaMethod("SetString", SetString),
			new LuaMethod("GetString", GetString),
			new LuaMethod("HasKey", HasKey),
			new LuaMethod("DeleteKey", DeleteKey),
			new LuaMethod("DeleteAll", DeleteAll),
			new LuaMethod("Save", Save),
			new LuaMethod("new", _CreatePlayerPrefs),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		var type = typeof(PlayerPrefs);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePlayerPrefs(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			PlayerPrefs obj = new PlayerPrefs();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerPrefs.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(PlayerPrefs));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInt(IntPtr L)
	{
		L.ChkArgsCount(2);
		var arg0 = L.ToLuaString(1);
		var arg1 = (int)L.ChkNumber(2);
		PlayerPrefs.SetInt(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInt(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			var arg0 = L.ToLuaString(1);
			int o = PlayerPrefs.GetInt(arg0);
			L.PushInteger(o);
			return 1;
		}
		else if (count == 2)
		{
			var arg0 = L.ToLuaString(1);
			var arg1 = (int)L.ChkNumber(2);
			int o = PlayerPrefs.GetInt(arg0,arg1);
			L.PushInteger(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerPrefs.GetInt");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetFloat(IntPtr L)
	{
		L.ChkArgsCount(2);
		var arg0 = L.ToLuaString(1);
		var arg1 = (float)L.ChkNumber(2);
		PlayerPrefs.SetFloat(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFloat(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			var arg0 = L.ToLuaString(1);
			float o = PlayerPrefs.GetFloat(arg0);
			L.PushNumber(o);
			return 1;
		}
		else if (count == 2)
		{
			var arg0 = L.ToLuaString(1);
			var arg1 = (float)L.ChkNumber(2);
			float o = PlayerPrefs.GetFloat(arg0,arg1);
			L.PushNumber(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerPrefs.GetFloat");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetString(IntPtr L)
	{
		L.ChkArgsCount(2);
		var arg0 = L.ToLuaString(1);
		var arg1 = L.ToLuaString(2);
		PlayerPrefs.SetString(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetString(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			var arg0 = L.ToLuaString(1);
			string o = PlayerPrefs.GetString(arg0);
			L.PushString(o);
			return 1;
		}
		else if (count == 2)
		{
			var arg0 = L.ToLuaString(1);
			var arg1 = L.ToLuaString(2);
			string o = PlayerPrefs.GetString(arg0,arg1);
			L.PushString(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: PlayerPrefs.GetString");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasKey(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		bool o = PlayerPrefs.HasKey(arg0);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DeleteKey(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		PlayerPrefs.DeleteKey(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DeleteAll(IntPtr L)
	{
		L.ChkArgsCount(0);
		PlayerPrefs.DeleteAll();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Save(IntPtr L)
	{
		L.ChkArgsCount(0);
		PlayerPrefs.Save();
		return 0;
	}
}


using System;
using LuaInterface;

public class CMD5Wrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("MD5File", MD5File),
			new LuaMethod("MD5String", MD5String),
			new LuaMethod("MD5Data", MD5Data),
			new LuaMethod("HashFile", HashFile),
			new LuaMethod("HashData", HashData),
			new LuaMethod("ByteArrayToHexString", ByteArrayToHexString),
			new LuaMethod("new", _CreateCMD5),
			new LuaMethod("GetType", GetClassType),
		};

		var type = typeof(CMD5);
		L.WrapCSharpObject(type, regs);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCMD5(IntPtr L)
	{
		LuaDLL.luaL_error(L, "CMD5 class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(CMD5));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MD5File(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		string o = CMD5.MD5File(arg0);
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MD5String(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		string o = CMD5.MD5String(arg0);
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MD5Data(IntPtr L)
	{
		L.ChkArgsCount(1);
		byte[] objs0 = L.ToArrayNumber<byte>(1);
		string o = CMD5.MD5Data(objs0);
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HashFile(IntPtr L)
	{
		L.ChkArgsCount(2);
		var arg0 = L.ToLuaString(1);
		var arg1 = L.ToLuaString(2);
		string o = CMD5.HashFile(arg0,arg1);
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HashData(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(byte[]), typeof(string)))
		{
			byte[] objs0 = L.ToArrayNumber<byte>(1);
			var arg1 = L.ChkLuaString(2);
			byte[] o = CMD5.HashData(objs0,arg1);
			L.PushUData(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(System.IO.Stream), typeof(string)))
		{
			System.IO.Stream arg0 = (System.IO.Stream)L.ToUserData(1);
			var arg1 = L.ChkLuaString(2);
			byte[] o = CMD5.HashData(arg0,arg1);
			L.PushUData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CMD5.HashData");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ByteArrayToHexString(IntPtr L)
	{
		L.ChkArgsCount(1);
		byte[] objs0 = L.ToArrayNumber<byte>(1);
		string o = CMD5.ByteArrayToHexString(objs0);
		L.PushString(o);
		return 1;
	}
}


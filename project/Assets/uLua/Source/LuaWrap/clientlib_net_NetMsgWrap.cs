using System;
using LuaInterface;

public class clientlib_net_NetMsgWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("createReadMsg", createReadMsg),
			new LuaMethod("createMsg", createMsg),
			new LuaMethod("deserialization", deserialization),
			new LuaMethod("reset", reset),
			new LuaMethod("serialization", serialization),
			new LuaMethod("read", read),
			new LuaMethod("readU32", readU32),
			new LuaMethod("readU64", readU64),
			new LuaMethod("readDouble", readDouble),
			new LuaMethod("readFloat", readFloat),
			new LuaMethod("readString", readString),
			new LuaMethod("readInt", readInt),
			new LuaMethod("write", write),
			new LuaMethod("writeU32", writeU32),
			new LuaMethod("writeU64", writeU64),
			new LuaMethod("writeString", writeString),
			new LuaMethod("writeInt", writeInt),
			new LuaMethod("new", _Createclientlib_net_NetMsg),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("msgSize", get_msgSize, set_msgSize),
			new LuaField("readSize", get_readSize, null),
			new LuaField("writeSize", get_writeSize, null),
			new LuaField("type", get_type, set_type),
			new LuaField("buffer", get_buffer, null),
			new LuaField("limit", get_limit, null),
			new LuaField("posession", get_posession, null),
		};

		var type = typeof(clientlib.net.NetMsg);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createclientlib_net_NetMsg(IntPtr L)
	{
		LuaDLL.luaL_error(L, "clientlib.net.NetMsg class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(clientlib.net.NetMsg));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_msgSize(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name msgSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index msgSize on a nil value");
			}
		}

		L.PushInteger(obj.msgSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_readSize(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name readSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index readSize on a nil value");
			}
		}

		L.PushInteger(obj.readSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_writeSize(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name writeSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index writeSize on a nil value");
			}
		}

		L.PushInteger(obj.writeSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		L.PushInteger(obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_buffer(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name buffer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index buffer on a nil value");
			}
		}

		L.PushLightUserData(obj.buffer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_limit(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name limit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index limit on a nil value");
			}
		}

		L.PushInteger(obj.limit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_posession(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name posession");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index posession on a nil value");
			}
		}

		L.PushInteger(obj.posession);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_msgSize(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name msgSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index msgSize on a nil value");
			}
		}

		obj.msgSize = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = L.ToUserData(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int createReadMsg(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = (int)L.ChkNumber(1);
		clientlib.net.NetMsg o = clientlib.net.NetMsg.createReadMsg(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int createMsg(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			var arg0 = (int)L.ChkNumber(1);
			clientlib.net.NetMsg o = clientlib.net.NetMsg.createMsg(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 2)
		{
			var arg0 = (int)L.ChkNumber(1);
			var arg1 = (int)L.ChkNumber(2);
			clientlib.net.NetMsg o = clientlib.net.NetMsg.createMsg(arg0,arg1);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: clientlib.net.NetMsg.createMsg");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int deserialization(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		obj.deserialization();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int reset(IntPtr L)
	{
		L.ChkArgsCount(2);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		var arg0 = (int)L.ChkNumber(2);
		obj.reset(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int serialization(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		obj.serialization();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int read(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		byte o = obj.read();
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int readU32(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		int o = obj.readU32();
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int readU64(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		string o = obj.readU64();
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int readDouble(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		double o = obj.readDouble();
		L.PushNumber(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int readFloat(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		float o = obj.readFloat();
		L.PushNumber(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int readString(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		string o = obj.readString();
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int readInt(IntPtr L)
	{
		L.ChkArgsCount(1);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		int o = obj.readInt();
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int write(IntPtr L)
	{
		L.ChkArgsCount(2);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		var arg0 = (byte)L.ChkNumber(2);
		clientlib.net.INetMsg o = obj.write(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int writeU32(IntPtr L)
	{
		L.ChkArgsCount(2);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		var arg0 = (int)L.ChkNumber(2);
		clientlib.net.INetMsg o = obj.writeU32(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int writeU64(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(clientlib.net.NetMsg), typeof(string)))
		{
			clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
			var arg0 = L.ChkLuaString(2);
			clientlib.net.INetMsg o = obj.writeU64(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(clientlib.net.NetMsg), typeof(long)))
		{
			clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
			var arg0 = L.ToLong(2);
			clientlib.net.INetMsg o = obj.writeU64(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: clientlib.net.NetMsg.writeU64");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int writeString(IntPtr L)
	{
		L.ChkArgsCount(2);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		var arg0 = L.ToLuaString(2);
		clientlib.net.INetMsg o = obj.writeString(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int writeInt(IntPtr L)
	{
		L.ChkArgsCount(2);
		clientlib.net.NetMsg obj = (clientlib.net.NetMsg)L.ChkUserDataSelf(1, "clientlib.net.NetMsg");
		var arg0 = (int)L.ChkNumber(2);
		clientlib.net.INetMsg o = obj.writeInt(arg0);
		L.PushLightUserData(o);
		return 1;
	}
}


using System;
using LuaInterface;

public class ZFrame_NetEngine_TcpClientHandlerWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Connect", Connect),
			new LuaMethod("Disconnect", Disconnect),
			new LuaMethod("Send", Send),
			new LuaMethod("new", _CreateZFrame_NetEngine_TcpClientHandler),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("autoRecieve", get_autoRecieve, set_autoRecieve),
			new LuaField("doRecieving", get_doRecieving, set_doRecieving),
			new LuaField("onConnected", get_onConnected, set_onConnected),
			new LuaField("onDisconnected", get_onDisconnected, set_onDisconnected),
			new LuaField("IsConnected", get_IsConnected, null),
			new LuaField("Error", get_Error, null),
		};

		var type = typeof(ZFrame.NetEngine.TcpClientHandler);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_NetEngine_TcpClientHandler(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.NetEngine.TcpClientHandler class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.NetEngine.TcpClientHandler));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autoRecieve(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name autoRecieve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index autoRecieve on a nil value");
			}
		}

		L.PushBoolean(obj.autoRecieve);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_doRecieving(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name doRecieving");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index doRecieving on a nil value");
			}
		}

		L.PushUData(obj.doRecieving);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onConnected(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onConnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onConnected on a nil value");
			}
		}

		L.PushUData(obj.onConnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onDisconnected(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDisconnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDisconnected on a nil value");
			}
		}

		L.PushUData(obj.onDisconnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsConnected(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name IsConnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index IsConnected on a nil value");
			}
		}

		L.PushBoolean(obj.IsConnected);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Error(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Error");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Error on a nil value");
			}
		}

		L.PushString(obj.Error);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autoRecieve(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name autoRecieve");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index autoRecieve on a nil value");
			}
		}

		obj.autoRecieve = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_doRecieving(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name doRecieving");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index doRecieving on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.doRecieving = (UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler,clientlib.net.INetMsg>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler,clientlib.net.INetMsg>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.doRecieving = (param0, param1) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				L.PushLightUserData(param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onConnected(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onConnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onConnected on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onConnected = (UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onConnected = (param0) =>
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
	static int set_onDisconnected(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onDisconnected");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onDisconnected on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onDisconnected = (UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onDisconnected = (param0) =>
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
	static int Connect(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)L.ChkUnityObjectSelf(1, "ZFrame.NetEngine.TcpClientHandler");
		var arg0 = L.ToLuaString(2);
		var arg1 = (int)L.ChkNumber(3);
		var arg2 = (float)L.ChkNumber(4);
		obj.Connect(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Disconnect(IntPtr L)
	{
		L.ChkArgsCount(1);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)L.ChkUnityObjectSelf(1, "ZFrame.NetEngine.TcpClientHandler");
		obj.Disconnect();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Send(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.NetEngine.TcpClientHandler obj = (ZFrame.NetEngine.TcpClientHandler)L.ChkUnityObjectSelf(1, "ZFrame.NetEngine.TcpClientHandler");
		clientlib.net.INetMsg arg0 = (clientlib.net.INetMsg)L.ChkUserData(2, typeof(clientlib.net.INetMsg));
		obj.Send(arg0);
		return 0;
	}
}


using System;
using LuaInterface;

public class ZFrame_NetEngine_NetworkMgrWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetTcpHandler", GetTcpHandler),
			new LuaMethod("GetHttpHandler", GetHttpHandler),
			new LuaMethod("new", _CreateZFrame_NetEngine_NetworkMgr),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Inst", get_Inst, null),
		};

		var type = typeof(ZFrame.NetEngine.NetworkMgr);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_NetEngine_NetworkMgr(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.NetEngine.NetworkMgr class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.NetEngine.NetworkMgr));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Inst(IntPtr L)
	{
		L.PushLightUserData(ZFrame.NetEngine.NetworkMgr.Inst);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTcpHandler(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.NetEngine.NetworkMgr obj = (ZFrame.NetEngine.NetworkMgr)L.ChkUnityObjectSelf(1, "ZFrame.NetEngine.NetworkMgr");
		var arg0 = L.ToLuaString(2);
		ZFrame.NetEngine.TcpClientHandler o = obj.GetTcpHandler(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHttpHandler(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.NetEngine.NetworkMgr obj = (ZFrame.NetEngine.NetworkMgr)L.ChkUnityObjectSelf(1, "ZFrame.NetEngine.NetworkMgr");
		var arg0 = L.ToLuaString(2);
		ZFrame.NetEngine.HttpHandler o = obj.GetHttpHandler(arg0);
		L.PushLightUserData(o);
		return 1;
	}
}


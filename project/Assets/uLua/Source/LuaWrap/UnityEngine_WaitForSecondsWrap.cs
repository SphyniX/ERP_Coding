using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_WaitForSecondsWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateWaitForSeconds),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		var type = typeof(WaitForSeconds);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateWaitForSeconds(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 1)
		{
			var arg0 = (float)L.ChkNumber(1);
			WaitForSeconds obj = new WaitForSeconds(arg0);
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: WaitForSeconds.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(WaitForSeconds));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}
}


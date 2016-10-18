using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_BehaviourWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateBehaviour),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("enabled", get_enabled, set_enabled),
			new LuaField("isActiveAndEnabled", get_isActiveAndEnabled, null),
		};

		var type = typeof(Behaviour);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBehaviour(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Behaviour obj = new Behaviour();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Behaviour.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Behaviour));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enabled(IntPtr L)
	{
		object o = L.ToUserData(1);
		Behaviour obj = (Behaviour)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}

		L.PushBoolean(obj.enabled);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isActiveAndEnabled(IntPtr L)
	{
		object o = L.ToUserData(1);
		Behaviour obj = (Behaviour)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isActiveAndEnabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isActiveAndEnabled on a nil value");
			}
		}

		L.PushBoolean(obj.isActiveAndEnabled);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enabled(IntPtr L)
	{
		object o = L.ToUserData(1);
		Behaviour obj = (Behaviour)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enabled on a nil value");
			}
		}

		obj.enabled = L.ChkBoolean(3);
		return 0;
	}
}


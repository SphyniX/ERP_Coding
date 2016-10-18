using System;
using LuaInterface;

public class UnityEngine_EventSystems_UIBehaviourWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("IsActive", IsActive),
			new LuaMethod("IsDestroyed", IsDestroyed),
			new LuaMethod("new", _CreateUnityEngine_EventSystems_UIBehaviour),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		var type = typeof(UnityEngine.EventSystems.UIBehaviour);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_EventSystems_UIBehaviour(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.EventSystems.UIBehaviour class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.EventSystems.UIBehaviour));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsActive(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.EventSystems.UIBehaviour obj = (UnityEngine.EventSystems.UIBehaviour)L.ChkUnityObjectSelf(1, "UnityEngine.EventSystems.UIBehaviour");
		bool o = obj.IsActive();
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsDestroyed(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.EventSystems.UIBehaviour obj = (UnityEngine.EventSystems.UIBehaviour)L.ChkUnityObjectSelf(1, "UnityEngine.EventSystems.UIBehaviour");
		bool o = obj.IsDestroyed();
		L.PushBoolean(o);
		return 1;
	}
}


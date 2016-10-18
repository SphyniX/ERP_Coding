using System;
using LuaInterface;

public class UnityEngine_EventSystems_BaseEventDataWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateUnityEngine_EventSystems_BaseEventData),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("currentInputModule", get_currentInputModule, null),
			new LuaField("selectedObject", get_selectedObject, set_selectedObject),
		};

		var type = typeof(UnityEngine.EventSystems.BaseEventData);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_EventSystems_BaseEventData(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 1)
		{
			var arg0 = L.ToComponent(1, typeof(UnityEngine.EventSystems.EventSystem)) as UnityEngine.EventSystems.EventSystem;
			UnityEngine.EventSystems.BaseEventData obj = new UnityEngine.EventSystems.BaseEventData(arg0);
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.EventSystems.BaseEventData.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.EventSystems.BaseEventData));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currentInputModule(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.BaseEventData obj = (UnityEngine.EventSystems.BaseEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name currentInputModule");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index currentInputModule on a nil value");
			}
		}

		L.PushLightUserData(obj.currentInputModule);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_selectedObject(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.BaseEventData obj = (UnityEngine.EventSystems.BaseEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectedObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectedObject on a nil value");
			}
		}

		L.PushLightUserData(obj.selectedObject);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_selectedObject(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.BaseEventData obj = (UnityEngine.EventSystems.BaseEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectedObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectedObject on a nil value");
			}
		}

		obj.selectedObject = L.ToGameObject(3);
		return 0;
	}
}


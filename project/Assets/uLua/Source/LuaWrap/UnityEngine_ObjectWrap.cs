using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UnityEngine_ObjectWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("FindObjectsOfType", FindObjectsOfType),
			new LuaMethod("DontDestroyOnLoad", DontDestroyOnLoad),
			new LuaMethod("ToString", ToString),
			new LuaMethod("Equals", Equals),
			new LuaMethod("GetHashCode", GetHashCode),
			new LuaMethod("GetInstanceID", GetInstanceID),
			new LuaMethod("Instantiate", Instantiate),
			new LuaMethod("FindObjectOfType", FindObjectOfType),
			new LuaMethod("DestroyObject", DestroyObject),
			new LuaMethod("DestroyImmediate", DestroyImmediate),
			new LuaMethod("Destroy", Destroy),
			new LuaMethod("new", _CreateObject),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("name", get_name, set_name),
			new LuaField("hideFlags", get_hideFlags, set_hideFlags),
		};

		var type = typeof(Object);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateObject(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Object obj = new Object();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Object));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_name(IntPtr L)
	{
		object o = L.ToUserData(1);
		Object obj = (Object)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		L.PushString(obj.name);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hideFlags(IntPtr L)
	{
		object o = L.ToUserData(1);
		Object obj = (Object)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hideFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hideFlags on a nil value");
			}
		}

		L.PushUData(obj.hideFlags);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_name(IntPtr L)
	{
		object o = L.ToUserData(1);
		Object obj = (Object)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name name");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index name on a nil value");
			}
		}

		obj.name = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hideFlags(IntPtr L)
	{
		object o = L.ToUserData(1);
		Object obj = (Object)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hideFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hideFlags on a nil value");
			}
		}

		obj.hideFlags = (HideFlags)L.ChkEnumValue(3, typeof(HideFlags));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindObjectsOfType(IntPtr L)
	{
		L.ChkArgsCount(1);
		Type arg0 = L.ChkTypeObject(1);
		Object[] o = Object.FindObjectsOfType(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DontDestroyOnLoad(IntPtr L)
	{
		L.ChkArgsCount(1);
		Object arg0 = (Object)L.ChkUnityObject(1, typeof(Object));
		Object.DontDestroyOnLoad(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		L.ChkArgsCount(1);
		Object obj = (Object)L.ChkUnityObjectSelf(1, "Object");
		string o = obj.ToString();
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Equals(IntPtr L)
	{
		L.ChkArgsCount(2);
		Object obj = L.ToAnyObject(1) as Object;
		var arg0 = L.ToAnyObject(2);
		bool o = obj != null ? obj.Equals(arg0) : arg0 == null;
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetHashCode(IntPtr L)
	{
		L.ChkArgsCount(1);
		Object obj = (Object)L.ChkUnityObjectSelf(1, "Object");
		int o = obj.GetHashCode();
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInstanceID(IntPtr L)
	{
		L.ChkArgsCount(1);
		Object obj = (Object)L.ChkUnityObjectSelf(1, "Object");
		int o = obj.GetInstanceID();
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Instantiate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Object arg0 = (Object)L.ChkUnityObject(1, typeof(Object));
			Object o = Object.Instantiate(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 3)
		{
			Object arg0 = (Object)L.ChkUnityObject(1, typeof(Object));
			var arg1 = L.ToVector3(2);
			var arg2 = L.ToQuaternion(3);
			Object o = Object.Instantiate(arg0,arg1,arg2);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.Instantiate");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindObjectOfType(IntPtr L)
	{
		L.ChkArgsCount(1);
		Type arg0 = L.ChkTypeObject(1);
		Object o = Object.FindObjectOfType(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		L.ChkArgsCount(2);
		Object arg0 = L.ToUserData(1) as Object;
		Object arg1 = L.ToUserData(2) as Object;
		bool o = arg0 == arg1;
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyObject(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Object arg0 = (Object)L.ToUserData(1);
			MetaMethods.__gc(L);
			Object.DestroyObject(arg0);
			return 0;
		}
		else if (count == 2)
		{
			Object arg0 = (Object)L.ToUserData(1);
			float arg1 = (float)L.ChkNumber(2);
			Object.DestroyObject(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.DestroyObject");
		}

		return 0;

	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DestroyImmediate(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Object arg0 = (Object)L.ToUserData(1);
			MetaMethods.__gc(L);
			Object.DestroyImmediate(arg0);
			return 0;
		}
		else if (count == 2)
		{
			Object arg0 = (Object)L.ToUserData(1);
			bool arg1 = L.ChkBoolean(2);
			Object.DestroyImmediate(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.DestroyImmediate");
		}

		return 0;

	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Destroy(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Object arg0 = (Object)L.ToUserData(1);
			MetaMethods.__gc(L);
			Object.Destroy(arg0);
			return 0;
		}
		else if (count == 2)
		{
			Object arg0 = (Object)L.ToUserData(1);
			float arg1 = (float)L.ChkNumber(2);
			Object.Destroy(arg0, arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Object.Destroy");
		}

		return 0;

	}
}


using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;

public class UnityEngine_ComponentWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetComponent", GetComponent),
			new LuaMethod("GetComponentInChildren", GetComponentInChildren),
			new LuaMethod("GetComponentsInChildren", GetComponentsInChildren),
			new LuaMethod("GetComponentInParent", GetComponentInParent),
			new LuaMethod("GetComponentsInParent", GetComponentsInParent),
			new LuaMethod("GetComponents", GetComponents),
			new LuaMethod("CompareTag", CompareTag),
			new LuaMethod("SendMessageUpwards", SendMessageUpwards),
			new LuaMethod("SendMessage", SendMessage),
			new LuaMethod("BroadcastMessage", BroadcastMessage),
			new LuaMethod("new", _CreateComponent),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("transform", get_transform, null),
			new LuaField("gameObject", get_gameObject, null),
			new LuaField("tag", get_tag, set_tag),
		};

		var type = typeof(Component);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateComponent(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Component obj = new Component();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Component));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transform(IntPtr L)
	{
		object o = L.ToUserData(1);
		Component obj = (Component)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transform");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transform on a nil value");
			}
		}

		L.PushLightUserData(obj.transform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameObject(IntPtr L)
	{
		object o = L.ToUserData(1);
		Component obj = (Component)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameObject");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameObject on a nil value");
			}
		}

		L.PushLightUserData(obj.gameObject);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tag(IntPtr L)
	{
		object o = L.ToUserData(1);
		Component obj = (Component)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tag on a nil value");
			}
		}

		L.PushString(obj.tag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tag(IntPtr L)
	{
		object o = L.ToUserData(1);
		Component obj = (Component)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tag on a nil value");
			}
		}

		obj.tag = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Component), typeof(string)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ChkLuaString(2);
			Component o = obj.GetComponent(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Component), typeof(Type)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			Component o = obj.GetComponent(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentInChildren(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			Component o = obj.GetComponentInChildren(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 3)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			var arg1 = L.ChkBoolean(3);
			Component o = obj.GetComponentInChildren(arg0,arg1);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponentInChildren");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentsInChildren(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			Component[] o = obj.GetComponentsInChildren(arg0);
			L.PushUData(o);
			return 1;
		}
		else if (count == 3)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			var arg1 = L.ChkBoolean(3);
			Component[] o = obj.GetComponentsInChildren(arg0,arg1);
			L.PushUData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponentsInChildren");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentInParent(IntPtr L)
	{
		L.ChkArgsCount(2);
		Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
		Type arg0 = L.ChkTypeObject(2);
		Component o = obj.GetComponentInParent(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentsInParent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			Component[] o = obj.GetComponentsInParent(arg0);
			L.PushUData(o);
			return 1;
		}
		else if (count == 3)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			var arg1 = L.ChkBoolean(3);
			Component[] o = obj.GetComponentsInParent(arg0,arg1);
			L.PushUData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponentsInParent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponents(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			Component[] o = obj.GetComponents(arg0);
			L.PushUData(o);
			return 1;
		}
		else if (count == 3)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			Type arg0 = L.ChkTypeObject(2);
			List<Component> arg1 = (List<Component>)L.ChkUserData(3, typeof(List<Component>));
			obj.GetComponents(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.GetComponents");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompareTag(IntPtr L)
	{
		L.ChkArgsCount(2);
		Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
		var arg0 = L.ToLuaString(2);
		bool o = obj.CompareTag(arg0);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMessageUpwards(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ToLuaString(2);
			obj.SendMessageUpwards(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (SendMessageOptions)L.ChkEnumValue(3, typeof(SendMessageOptions));
			obj.SendMessageUpwards(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Component), typeof(string), typeof(object)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ChkLuaString(2);
			var arg1 = L.ToAnyObject(3);
			obj.SendMessageUpwards(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ToLuaString(2);
			var arg1 = L.ToAnyObject(3);
			var arg2 = (SendMessageOptions)L.ChkEnumValue(4, typeof(SendMessageOptions));
			obj.SendMessageUpwards(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.SendMessageUpwards");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMessage(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ToLuaString(2);
			obj.SendMessage(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (SendMessageOptions)L.ChkEnumValue(3, typeof(SendMessageOptions));
			obj.SendMessage(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Component), typeof(string), typeof(object)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ChkLuaString(2);
			var arg1 = L.ToAnyObject(3);
			obj.SendMessage(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ToLuaString(2);
			var arg1 = L.ToAnyObject(3);
			var arg2 = (SendMessageOptions)L.ChkEnumValue(4, typeof(SendMessageOptions));
			obj.SendMessage(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.SendMessage");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BroadcastMessage(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ToLuaString(2);
			obj.BroadcastMessage(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Component), typeof(string), typeof(SendMessageOptions)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (SendMessageOptions)L.ChkEnumValue(3, typeof(SendMessageOptions));
			obj.BroadcastMessage(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Component), typeof(string), typeof(object)))
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ChkLuaString(2);
			var arg1 = L.ToAnyObject(3);
			obj.BroadcastMessage(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			Component obj = (Component)L.ChkUnityObjectSelf(1, "Component");
			var arg0 = L.ToLuaString(2);
			var arg1 = L.ToAnyObject(3);
			var arg2 = (SendMessageOptions)L.ChkEnumValue(4, typeof(SendMessageOptions));
			obj.BroadcastMessage(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Component.BroadcastMessage");
		}

		return 0;
	}
}


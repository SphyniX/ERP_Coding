using System;
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;

public class UnityEngine_GameObjectWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("CreatePrimitive", CreatePrimitive),
			new LuaMethod("GetComponent", GetComponent),
			new LuaMethod("GetComponentInChildren", GetComponentInChildren),
			new LuaMethod("GetComponentInParent", GetComponentInParent),
			new LuaMethod("GetComponents", GetComponents),
			new LuaMethod("GetComponentsInChildren", GetComponentsInChildren),
			new LuaMethod("GetComponentsInParent", GetComponentsInParent),
			new LuaMethod("SetActive", SetActive),
			new LuaMethod("CompareTag", CompareTag),
			new LuaMethod("FindGameObjectWithTag", FindGameObjectWithTag),
			new LuaMethod("FindWithTag", FindWithTag),
			new LuaMethod("FindGameObjectsWithTag", FindGameObjectsWithTag),
			new LuaMethod("SendMessageUpwards", SendMessageUpwards),
			new LuaMethod("SendMessage", SendMessage),
			new LuaMethod("BroadcastMessage", BroadcastMessage),
			new LuaMethod("AddComponent", AddComponent),
			new LuaMethod("Find", Find),
			new LuaMethod("new", _CreateGameObject),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("transform", get_transform, null),
			new LuaField("layer", get_layer, set_layer),
			new LuaField("activeSelf", get_activeSelf, null),
			new LuaField("activeInHierarchy", get_activeInHierarchy, null),
			new LuaField("isStatic", get_isStatic, set_isStatic),
			new LuaField("tag", get_tag, set_tag),
			new LuaField("scene", get_scene, null),
			new LuaField("gameObject", get_gameObject, null),
		};

		var type = typeof(GameObject);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGameObject(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			GameObject obj = new GameObject();
			L.PushLightUserData(obj);
			return 1;
		}
		else if (count == 1)
		{
			var arg0 = L.ChkLuaString(1);
			GameObject obj = new GameObject(arg0);
			L.PushLightUserData(obj);
			return 1;
		}
		else if (L.CheckTypes(1, typeof(string)) && L.CheckParamsType(typeof(Type), 2, count - 1))
		{
			var arg0 = L.ChkLuaString(1);
			Type[] objs1 = L.ToParamsObject<Type>(2, count - 1);
			GameObject obj = new GameObject(arg0,objs1);
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(GameObject));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transform(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

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
	static int get_layer(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layer on a nil value");
			}
		}

		L.PushInteger(obj.layer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activeSelf(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activeSelf");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activeSelf on a nil value");
			}
		}

		L.PushBoolean(obj.activeSelf);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activeInHierarchy(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name activeInHierarchy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index activeInHierarchy on a nil value");
			}
		}

		L.PushBoolean(obj.activeInHierarchy);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isStatic(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isStatic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isStatic on a nil value");
			}
		}

		L.PushBoolean(obj.isStatic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tag(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

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
	static int get_scene(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scene");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scene on a nil value");
			}
		}

		L.PushLightUserData(obj.scene);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameObject(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

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
	static int set_layer(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layer on a nil value");
			}
		}

		obj.layer = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isStatic(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isStatic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isStatic on a nil value");
			}
		}

		obj.isStatic = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tag(IntPtr L)
	{
		object o = L.ToUserData(1);
		GameObject obj = (GameObject)o;

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
	static int CreatePrimitive(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = (PrimitiveType)L.ChkEnumValue(1, typeof(PrimitiveType));
		GameObject o = GameObject.CreatePrimitive(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(GameObject), typeof(string)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ChkLuaString(2);
			Component o = obj.GetComponent(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(GameObject), typeof(Type)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			Component o = obj.GetComponent(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentInChildren(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			Component o = obj.GetComponentInChildren(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 3)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			var arg1 = L.ChkBoolean(3);
			Component o = obj.GetComponentInChildren(arg0,arg1);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponentInChildren");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentInParent(IntPtr L)
	{
		L.ChkArgsCount(2);
		GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
		Type arg0 = L.ChkTypeObject(2);
		Component o = obj.GetComponentInParent(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponents(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			Component[] o = obj.GetComponents(arg0);
			L.PushUData(o);
			return 1;
		}
		else if (count == 3)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			List<Component> arg1 = (List<Component>)L.ChkUserData(3, typeof(List<Component>));
			obj.GetComponents(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponents");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentsInChildren(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			Component[] o = obj.GetComponentsInChildren(arg0);
			L.PushUData(o);
			return 1;
		}
		else if (count == 3)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			var arg1 = L.ChkBoolean(3);
			Component[] o = obj.GetComponentsInChildren(arg0,arg1);
			L.PushUData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponentsInChildren");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetComponentsInParent(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			Component[] o = obj.GetComponentsInParent(arg0);
			L.PushUData(o);
			return 1;
		}
		else if (count == 3)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			Type arg0 = L.ChkTypeObject(2);
			var arg1 = L.ChkBoolean(3);
			Component[] o = obj.GetComponentsInParent(arg0,arg1);
			L.PushUData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.GetComponentsInParent");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetActive(IntPtr L)
	{
		L.ChkArgsCount(2);
		GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
		var arg0 = L.ChkBoolean(2);
		obj.SetActive(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CompareTag(IntPtr L)
	{
		L.ChkArgsCount(2);
		GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
		var arg0 = L.ToLuaString(2);
		bool o = obj.CompareTag(arg0);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindGameObjectWithTag(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		GameObject o = GameObject.FindGameObjectWithTag(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindWithTag(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		GameObject o = GameObject.FindWithTag(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindGameObjectsWithTag(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		GameObject[] o = GameObject.FindGameObjectsWithTag(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMessageUpwards(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ToLuaString(2);
			obj.SendMessageUpwards(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (SendMessageOptions)L.ChkEnumValue(3, typeof(SendMessageOptions));
			obj.SendMessageUpwards(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(GameObject), typeof(string), typeof(object)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ChkLuaString(2);
			var arg1 = L.ToAnyObject(3);
			obj.SendMessageUpwards(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ToLuaString(2);
			var arg1 = L.ToAnyObject(3);
			var arg2 = (SendMessageOptions)L.ChkEnumValue(4, typeof(SendMessageOptions));
			obj.SendMessageUpwards(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.SendMessageUpwards");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMessage(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ToLuaString(2);
			obj.SendMessage(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (SendMessageOptions)L.ChkEnumValue(3, typeof(SendMessageOptions));
			obj.SendMessage(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(GameObject), typeof(string), typeof(object)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ChkLuaString(2);
			var arg1 = L.ToAnyObject(3);
			obj.SendMessage(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ToLuaString(2);
			var arg1 = L.ToAnyObject(3);
			var arg2 = (SendMessageOptions)L.ChkEnumValue(4, typeof(SendMessageOptions));
			obj.SendMessage(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.SendMessage");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BroadcastMessage(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ToLuaString(2);
			obj.BroadcastMessage(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(GameObject), typeof(string), typeof(SendMessageOptions)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (SendMessageOptions)L.ChkEnumValue(3, typeof(SendMessageOptions));
			obj.BroadcastMessage(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(GameObject), typeof(string), typeof(object)))
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ChkLuaString(2);
			var arg1 = L.ToAnyObject(3);
			obj.BroadcastMessage(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
			var arg0 = L.ToLuaString(2);
			var arg1 = L.ToAnyObject(3);
			var arg2 = (SendMessageOptions)L.ChkEnumValue(4, typeof(SendMessageOptions));
			obj.BroadcastMessage(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: GameObject.BroadcastMessage");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddComponent(IntPtr L)
	{
		L.ChkArgsCount(2);
		GameObject obj = (GameObject)L.ChkUnityObjectSelf(1, "GameObject");
		Type arg0 = L.ChkTypeObject(2);
		Component o = obj.AddComponent(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Find(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		GameObject o = GameObject.Find(arg0);
		L.PushLightUserData(o);
		return 1;
	}
}


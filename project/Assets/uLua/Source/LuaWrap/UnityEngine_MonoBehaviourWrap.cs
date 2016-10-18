using System;
using UnityEngine;
using System.Collections;
using LuaInterface;

public class UnityEngine_MonoBehaviourWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Invoke", Invoke),
			new LuaMethod("InvokeRepeating", InvokeRepeating),
			new LuaMethod("CancelInvoke", CancelInvoke),
			new LuaMethod("IsInvoking", IsInvoking),
			new LuaMethod("StartCoroutine", StartCoroutine),
			new LuaMethod("StartCoroutine_Auto", StartCoroutine_Auto),
			new LuaMethod("StopCoroutine", StopCoroutine),
			new LuaMethod("StopAllCoroutines", StopAllCoroutines),
			new LuaMethod("print", print),
			new LuaMethod("new", _CreateMonoBehaviour),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("useGUILayout", get_useGUILayout, set_useGUILayout),
		};

		var type = typeof(MonoBehaviour);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateMonoBehaviour(IntPtr L)
	{
		LuaDLL.luaL_error(L, "MonoBehaviour class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(MonoBehaviour));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useGUILayout(IntPtr L)
	{
		object o = L.ToUserData(1);
		MonoBehaviour obj = (MonoBehaviour)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useGUILayout");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useGUILayout on a nil value");
			}
		}

		L.PushBoolean(obj.useGUILayout);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useGUILayout(IntPtr L)
	{
		object o = L.ToUserData(1);
		MonoBehaviour obj = (MonoBehaviour)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useGUILayout");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useGUILayout on a nil value");
			}
		}

		obj.useGUILayout = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Invoke(IntPtr L)
	{
		L.ChkArgsCount(3);
		MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
		var arg0 = L.ToLuaString(2);
		var arg1 = (float)L.ChkNumber(3);
		obj.Invoke(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InvokeRepeating(IntPtr L)
	{
		L.ChkArgsCount(4);
		MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
		var arg0 = L.ToLuaString(2);
		var arg1 = (float)L.ChkNumber(3);
		var arg2 = (float)L.ChkNumber(4);
		obj.InvokeRepeating(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CancelInvoke(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			obj.CancelInvoke();
			return 0;
		}
		else if (count == 2)
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			var arg0 = L.ToLuaString(2);
			obj.CancelInvoke(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.CancelInvoke");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInvoking(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			bool o = obj.IsInvoking();
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 2)
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			var arg0 = L.ToLuaString(2);
			bool o = obj.IsInvoking(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.IsInvoking");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartCoroutine(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(MonoBehaviour), typeof(string)))
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			var arg0 = L.ChkLuaString(2);
			Coroutine o = obj.StartCoroutine(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(MonoBehaviour), typeof(IEnumerator)))
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			IEnumerator arg0 = (IEnumerator)L.ToUserData(2);
			Coroutine o = obj.StartCoroutine(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 3)
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			var arg0 = L.ToLuaString(2);
			var arg1 = L.ToAnyObject(3);
			Coroutine o = obj.StartCoroutine(arg0,arg1);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.StartCoroutine");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartCoroutine_Auto(IntPtr L)
	{
		L.ChkArgsCount(2);
		MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
		IEnumerator arg0 = (IEnumerator)L.ChkUserData(2, typeof(IEnumerator));
		Coroutine o = obj.StartCoroutine_Auto(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopCoroutine(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(MonoBehaviour), typeof(Coroutine)))
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			Coroutine arg0 = (Coroutine)L.ToUserData(2);
			obj.StopCoroutine(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(MonoBehaviour), typeof(IEnumerator)))
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			IEnumerator arg0 = (IEnumerator)L.ToUserData(2);
			obj.StopCoroutine(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(MonoBehaviour), typeof(string)))
		{
			MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
			var arg0 = L.ChkLuaString(2);
			obj.StopCoroutine(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: MonoBehaviour.StopCoroutine");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopAllCoroutines(IntPtr L)
	{
		L.ChkArgsCount(1);
		MonoBehaviour obj = (MonoBehaviour)L.ChkUnityObjectSelf(1, "MonoBehaviour");
		obj.StopAllCoroutines();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int print(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToAnyObject(1);
		MonoBehaviour.print(arg0);
		return 0;
	}
}


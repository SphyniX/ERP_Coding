using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_CanvasGroupWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("IsRaycastLocationValid", IsRaycastLocationValid),
			new LuaMethod("new", _CreateCanvasGroup),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("alpha", get_alpha, set_alpha),
			new LuaField("interactable", get_interactable, set_interactable),
			new LuaField("blocksRaycasts", get_blocksRaycasts, set_blocksRaycasts),
			new LuaField("ignoreParentGroups", get_ignoreParentGroups, set_ignoreParentGroups),
		};

		var type = typeof(CanvasGroup);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCanvasGroup(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			CanvasGroup obj = new CanvasGroup();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: CanvasGroup.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(CanvasGroup));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alpha(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alpha");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alpha on a nil value");
			}
		}

		L.PushNumber(obj.alpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_interactable(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interactable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interactable on a nil value");
			}
		}

		L.PushBoolean(obj.interactable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_blocksRaycasts(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name blocksRaycasts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index blocksRaycasts on a nil value");
			}
		}

		L.PushBoolean(obj.blocksRaycasts);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ignoreParentGroups(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreParentGroups");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreParentGroups on a nil value");
			}
		}

		L.PushBoolean(obj.ignoreParentGroups);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_alpha(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alpha");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alpha on a nil value");
			}
		}

		obj.alpha = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_interactable(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interactable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interactable on a nil value");
			}
		}

		obj.interactable = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_blocksRaycasts(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name blocksRaycasts");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index blocksRaycasts on a nil value");
			}
		}

		obj.blocksRaycasts = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ignoreParentGroups(IntPtr L)
	{
		object o = L.ToUserData(1);
		CanvasGroup obj = (CanvasGroup)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name ignoreParentGroups");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index ignoreParentGroups on a nil value");
			}
		}

		obj.ignoreParentGroups = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsRaycastLocationValid(IntPtr L)
	{
		L.ChkArgsCount(3);
		CanvasGroup obj = (CanvasGroup)L.ChkUnityObjectSelf(1, "CanvasGroup");
		var arg0 = L.ToVector2(2);
		var arg1 = L.ToComponent(3, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		bool o = obj.IsRaycastLocationValid(arg0,arg1);
		L.PushBoolean(o);
		return 1;
	}
}


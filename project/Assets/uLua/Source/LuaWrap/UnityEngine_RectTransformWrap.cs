using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_RectTransformWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetLocalCorners", GetLocalCorners),
			new LuaMethod("GetWorldCorners", GetWorldCorners),
			new LuaMethod("SetInsetAndSizeFromParentEdge", SetInsetAndSizeFromParentEdge),
			new LuaMethod("SetSizeWithCurrentAnchors", SetSizeWithCurrentAnchors),
			new LuaMethod("new", _CreateRectTransform),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("rect", get_rect, null),
			new LuaField("anchorMin", get_anchorMin, set_anchorMin),
			new LuaField("anchorMax", get_anchorMax, set_anchorMax),
			new LuaField("anchoredPosition3D", get_anchoredPosition3D, set_anchoredPosition3D),
			new LuaField("anchoredPosition", get_anchoredPosition, set_anchoredPosition),
			new LuaField("sizeDelta", get_sizeDelta, set_sizeDelta),
			new LuaField("pivot", get_pivot, set_pivot),
			new LuaField("offsetMin", get_offsetMin, set_offsetMin),
			new LuaField("offsetMax", get_offsetMax, set_offsetMax),
		};

		var type = typeof(RectTransform);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateRectTransform(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			RectTransform obj = new RectTransform();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: RectTransform.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(RectTransform));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rect(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rect on a nil value");
			}
		}

		L.PushLightUserData(obj.rect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_anchorMin(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMin on a nil value");
			}
		}

		L.PushUData(obj.anchorMin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_anchorMax(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMax on a nil value");
			}
		}

		L.PushUData(obj.anchorMax);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_anchoredPosition3D(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition3D");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition3D on a nil value");
			}
		}

		L.PushUData(obj.anchoredPosition3D);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_anchoredPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition on a nil value");
			}
		}

		L.PushUData(obj.anchoredPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sizeDelta(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sizeDelta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sizeDelta on a nil value");
			}
		}

		L.PushUData(obj.sizeDelta);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pivot(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pivot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pivot on a nil value");
			}
		}

		L.PushUData(obj.pivot);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_offsetMin(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMin on a nil value");
			}
		}

		L.PushUData(obj.offsetMin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_offsetMax(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMax on a nil value");
			}
		}

		L.PushUData(obj.offsetMax);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_anchorMin(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMin on a nil value");
			}
		}

		obj.anchorMin = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_anchorMax(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchorMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchorMax on a nil value");
			}
		}

		obj.anchorMax = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_anchoredPosition3D(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition3D");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition3D on a nil value");
			}
		}

		obj.anchoredPosition3D = L.ToVector3(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_anchoredPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name anchoredPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index anchoredPosition on a nil value");
			}
		}

		obj.anchoredPosition = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sizeDelta(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sizeDelta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sizeDelta on a nil value");
			}
		}

		obj.sizeDelta = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pivot(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pivot");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pivot on a nil value");
			}
		}

		obj.pivot = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_offsetMin(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMin on a nil value");
			}
		}

		obj.offsetMin = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_offsetMax(IntPtr L)
	{
		object o = L.ToUserData(1);
		RectTransform obj = (RectTransform)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name offsetMax");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index offsetMax on a nil value");
			}
		}

		obj.offsetMax = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLocalCorners(IntPtr L)
	{
		L.ChkArgsCount(2);
		RectTransform obj = (RectTransform)L.ChkUnityObjectSelf(1, "RectTransform");
		Vector3[] objs0 = L.ToArrayObject<Vector3>(2);
		obj.GetLocalCorners(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetWorldCorners(IntPtr L)
	{
		L.ChkArgsCount(2);
		RectTransform obj = (RectTransform)L.ChkUnityObjectSelf(1, "RectTransform");
		Vector3[] objs0 = L.ToArrayObject<Vector3>(2);
		obj.GetWorldCorners(objs0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInsetAndSizeFromParentEdge(IntPtr L)
	{
		L.ChkArgsCount(4);
		RectTransform obj = (RectTransform)L.ChkUnityObjectSelf(1, "RectTransform");
		var arg0 = (RectTransform.Edge)L.ChkEnumValue(2, typeof(RectTransform.Edge));
		var arg1 = (float)L.ChkNumber(3);
		var arg2 = (float)L.ChkNumber(4);
		obj.SetInsetAndSizeFromParentEdge(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSizeWithCurrentAnchors(IntPtr L)
	{
		L.ChkArgsCount(3);
		RectTransform obj = (RectTransform)L.ChkUnityObjectSelf(1, "RectTransform");
		var arg0 = (RectTransform.Axis)L.ChkEnumValue(2, typeof(RectTransform.Axis));
		var arg1 = (float)L.ChkNumber(3);
		obj.SetSizeWithCurrentAnchors(arg0,arg1);
		return 0;
	}
}


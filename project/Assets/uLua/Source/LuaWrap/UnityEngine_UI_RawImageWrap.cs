using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_RawImageWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetNativeSize", SetNativeSize),
			new LuaMethod("new", _CreateUnityEngine_UI_RawImage),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("mainTexture", get_mainTexture, null),
			new LuaField("texture", get_texture, set_texture),
			new LuaField("uvRect", get_uvRect, set_uvRect),
		};

		var type = typeof(UnityEngine.UI.RawImage);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_RawImage(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.RawImage class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.RawImage));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mainTexture(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.RawImage obj = (UnityEngine.UI.RawImage)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTexture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTexture on a nil value");
			}
		}

		L.PushLightUserData(obj.mainTexture);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_texture(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.RawImage obj = (UnityEngine.UI.RawImage)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name texture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index texture on a nil value");
			}
		}

		L.PushLightUserData(obj.texture);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_uvRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.RawImage obj = (UnityEngine.UI.RawImage)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvRect on a nil value");
			}
		}

		L.PushLightUserData(obj.uvRect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_texture(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.RawImage obj = (UnityEngine.UI.RawImage)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name texture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index texture on a nil value");
			}
		}

		obj.texture = (Texture)L.ChkUnityObject(3, typeof(Texture));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_uvRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.RawImage obj = (UnityEngine.UI.RawImage)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uvRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uvRect on a nil value");
			}
		}

		obj.uvRect = (Rect)L.ChkUserData(3, typeof(Rect));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetNativeSize(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.RawImage obj = (UnityEngine.UI.RawImage)L.ChkUnityObjectSelf(1, "UnityEngine.UI.RawImage");
		obj.SetNativeSize();
		return 0;
	}
}


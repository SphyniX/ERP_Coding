using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_MaskableGraphicWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetModifiedMaterial", GetModifiedMaterial),
			new LuaMethod("Cull", Cull),
			new LuaMethod("SetClipRect", SetClipRect),
			new LuaMethod("RecalculateClipping", RecalculateClipping),
			new LuaMethod("RecalculateMasking", RecalculateMasking),
			new LuaMethod("new", _CreateUnityEngine_UI_MaskableGraphic),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onCullStateChanged", get_onCullStateChanged, set_onCullStateChanged),
			new LuaField("maskable", get_maskable, set_maskable),
		};

		var type = typeof(UnityEngine.UI.MaskableGraphic);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_MaskableGraphic(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.MaskableGraphic class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.MaskableGraphic));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onCullStateChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCullStateChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCullStateChanged on a nil value");
			}
		}

		L.PushLightUserData(obj.onCullStateChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maskable(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maskable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maskable on a nil value");
			}
		}

		L.PushBoolean(obj.maskable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onCullStateChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onCullStateChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onCullStateChanged on a nil value");
			}
		}

		obj.onCullStateChanged = (UnityEngine.UI.MaskableGraphic.CullStateChangedEvent)L.ChkUserData(3, typeof(UnityEngine.UI.MaskableGraphic.CullStateChangedEvent));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maskable(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name maskable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index maskable on a nil value");
			}
		}

		obj.maskable = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetModifiedMaterial(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.MaskableGraphic");
		Material arg0 = (Material)L.ChkUnityObject(2, typeof(Material));
		Material o = obj.GetModifiedMaterial(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Cull(IntPtr L)
	{
		L.ChkArgsCount(3);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.MaskableGraphic");
		Rect arg0 = (Rect)L.ChkUserData(2, typeof(Rect));
		var arg1 = L.ChkBoolean(3);
		obj.Cull(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetClipRect(IntPtr L)
	{
		L.ChkArgsCount(3);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.MaskableGraphic");
		Rect arg0 = (Rect)L.ChkUserData(2, typeof(Rect));
		var arg1 = L.ChkBoolean(3);
		obj.SetClipRect(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RecalculateClipping(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.MaskableGraphic");
		obj.RecalculateClipping();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RecalculateMasking(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.MaskableGraphic obj = (UnityEngine.UI.MaskableGraphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.MaskableGraphic");
		obj.RecalculateMasking();
		return 0;
	}
}


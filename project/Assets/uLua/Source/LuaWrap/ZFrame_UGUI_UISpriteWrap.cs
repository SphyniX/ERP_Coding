using System;
using UnityEngine;
using LuaInterface;

public class ZFrame_UGUI_UISpriteWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Load", Load),
			new LuaMethod("SetNativeSize", SetNativeSize),
			new LuaMethod("Tween", Tween),
			new LuaMethod("IsRaycastLocationValid", IsRaycastLocationValid),
			new LuaMethod("new", _CreateZFrame_UGUI_UISprite),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("spritePath", null, set_spritePath),
			new LuaField("spriteName", null, set_spriteName),
			new LuaField("grayscale", get_grayscale, set_grayscale),
		};

		var type = typeof(ZFrame.UGUI.UISprite);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UISprite(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UISprite class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UISprite));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grayscale(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grayscale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grayscale on a nil value");
			}
		}

		L.PushBoolean(obj.grayscale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spritePath(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spritePath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spritePath on a nil value");
			}
		}

		obj.spritePath = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spriteName(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spriteName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spriteName on a nil value");
			}
		}

		obj.spriteName = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grayscale(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name grayscale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index grayscale on a nil value");
			}
		}

		obj.grayscale = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Load(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UISprite");
		var arg0 = L.ToLuaString(2);
		ZFrame.Asset.DelegateObjectLoaded arg1 = null;
		LuaTypes funcType3 = L.Type(3);

		if (funcType3 != LuaTypes.LUA_TFUNCTION)
		{
			 arg1 = (ZFrame.Asset.DelegateObjectLoaded)L.ChkUserData(3, typeof(ZFrame.Asset.DelegateObjectLoaded));
		}
		else
		{
			LuaFunction func = L.ChkLuaFunction(3);
			arg1 = (param0, param1) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				L.PushAnyObject(param1);
				func.PCall(top, 2);
				func.EndPCall(top);
			};
		}

		var arg2 = L.ToAnyObject(4);
		obj.Load(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetNativeSize(IntPtr L)
	{
		L.ChkArgsCount(1);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UISprite");
		obj.SetNativeSize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Tween(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UISprite");
		var arg0 = L.ToAnyObject(2);
		var arg1 = L.ToAnyObject(3);
		var arg2 = (float)L.ChkNumber(4);
		ZFrame.Tween.ZTweener o = obj.Tween(arg0,arg1,arg2);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsRaycastLocationValid(IntPtr L)
	{
		L.ChkArgsCount(3);
		ZFrame.UGUI.UISprite obj = (ZFrame.UGUI.UISprite)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UISprite");
		var arg0 = L.ToVector2(2);
		var arg1 = L.ToComponent(3, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		bool o = obj.IsRaycastLocationValid(arg0,arg1);
		L.PushBoolean(o);
		return 1;
	}
}


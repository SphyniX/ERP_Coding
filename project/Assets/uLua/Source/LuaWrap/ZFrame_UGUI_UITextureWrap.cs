using System;
using LuaInterface;

public class ZFrame_UGUI_UITextureWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Load", Load),
			new LuaMethod("Tween", Tween),
			new LuaMethod("new", _CreateZFrame_UGUI_UITexture),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("type", get_type, set_type),
			new LuaField("texturePath", null, set_texturePath),
			new LuaField("grayscale", get_grayscale, set_grayscale),
		};

		var type = typeof(ZFrame.UGUI.UITexture);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UITexture(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UITexture class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UITexture));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UITexture obj = (ZFrame.UGUI.UITexture)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		L.PushUData(obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_grayscale(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UITexture obj = (ZFrame.UGUI.UITexture)o;

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
	static int set_type(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UITexture obj = (ZFrame.UGUI.UITexture)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (UnityEngine.UI.Image.Type)L.ChkEnumValue(3, typeof(UnityEngine.UI.Image.Type));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_texturePath(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UITexture obj = (ZFrame.UGUI.UITexture)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name texturePath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index texturePath on a nil value");
			}
		}

		obj.texturePath = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_grayscale(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UITexture obj = (ZFrame.UGUI.UITexture)o;

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
		L.ChkArgsCount(5);
		ZFrame.UGUI.UITexture obj = (ZFrame.UGUI.UITexture)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UITexture");
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
		var arg3 = L.ChkBoolean(5);
		obj.Load(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Tween(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.UGUI.UITexture obj = (ZFrame.UGUI.UITexture)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UITexture");
		var arg0 = L.ToAnyObject(2);
		var arg1 = L.ToAnyObject(3);
		var arg2 = (float)L.ChkNumber(4);
		ZFrame.Tween.ZTweener o = obj.Tween(arg0,arg1,arg2);
		L.PushLightUserData(o);
		return 1;
	}
}


using System;
using UnityEngine;
using LuaInterface;

public class ThreeDButtonWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("TweenColor", TweenColor),
			new LuaMethod("SetColor", SetColor),
			new LuaMethod("new", _CreateThreeDButton),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onClick", get_onClick, set_onClick),
			new LuaField("color", get_color, set_color),
		};

		var type = typeof(ThreeDButton);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateThreeDButton(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ThreeDButton class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ThreeDButton));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onClick(IntPtr L)
	{
		object o = L.ToUserData(1);
		ThreeDButton obj = (ThreeDButton)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}

		L.PushUData(obj.onClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_color(IntPtr L)
	{
		object o = L.ToUserData(1);
		ThreeDButton obj = (ThreeDButton)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}

		L.PushUData(obj.color);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onClick(IntPtr L)
	{
		object o = L.ToUserData(1);
		ThreeDButton obj = (ThreeDButton)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onClick on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onClick = (UnityEngine.Events.UnityAction<GameObject>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<GameObject>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onClick = (param0) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_color(IntPtr L)
	{
		object o = L.ToUserData(1);
		ThreeDButton obj = (ThreeDButton)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}

		obj.color = L.ToColor(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int TweenColor(IntPtr L)
	{
		L.ChkArgsCount(2);
		ThreeDButton obj = (ThreeDButton)L.ChkUnityObjectSelf(1, "ThreeDButton");
		var arg0 = L.ToColor(2);
		obj.TweenColor(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetColor(IntPtr L)
	{
		L.ChkArgsCount(2);
		ThreeDButton obj = (ThreeDButton)L.ChkUnityObjectSelf(1, "ThreeDButton");
		var arg0 = L.ToColor(2);
		obj.SetColor(arg0);
		return 0;
	}
}


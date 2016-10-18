using System;
using LuaInterface;

public class ZFrame_UGUI_UIInputWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateZFrame_UGUI_UIInput),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("onSubmit", get_onSubmit, set_onSubmit),
		};

		var type = typeof(ZFrame.UGUI.UIInput);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIInput(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIInput class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIInput));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIInput.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onSubmit(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIInput obj = (ZFrame.UGUI.UIInput)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSubmit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSubmit on a nil value");
			}
		}

		L.PushUData(obj.onSubmit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		ZFrame.UGUI.UIInput.current = L.ToComponent(3, typeof(ZFrame.UGUI.UIInput)) as ZFrame.UGUI.UIInput;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onSubmit(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIInput obj = (ZFrame.UGUI.UIInput)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onSubmit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onSubmit on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onSubmit = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIInput>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIInput>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onSubmit = (param0) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}
}


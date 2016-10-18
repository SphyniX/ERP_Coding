using System;
using LuaInterface;

public class ZFrame_UGUI_UIDropdownWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateZFrame_UGUI_UIDropdown),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("current", get_current, set_current),
			new LuaField("onAction", get_onAction, set_onAction),
		};

		var type = typeof(ZFrame.UGUI.UIDropdown);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UIDropdown(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UIDropdown class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UIDropdown));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(ZFrame.UGUI.UIDropdown.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onAction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDropdown obj = (ZFrame.UGUI.UIDropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAction on a nil value");
			}
		}

		L.PushUData(obj.onAction);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_current(IntPtr L)
	{
		ZFrame.UGUI.UIDropdown.current = L.ToComponent(3, typeof(ZFrame.UGUI.UIDropdown)) as ZFrame.UGUI.UIDropdown;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onAction(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UIDropdown obj = (ZFrame.UGUI.UIDropdown)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onAction");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onAction on a nil value");
			}
		}

		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			obj.onAction = (UnityEngine.Events.UnityAction<ZFrame.UGUI.UIDropdown>)L.ChkUserData(3, typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIDropdown>));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			obj.onAction = (param0) =>
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


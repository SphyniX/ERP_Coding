using System;
using LuaInterface;

public class ZFrame_UGUI_UISelectableWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("IsInteractable", IsInteractable),
			new LuaMethod("new", _CreateZFrame_UGUI_UISelectable),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("selectable", get_selectable, set_selectable),
		};

		var type = typeof(ZFrame.UGUI.UISelectable);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UISelectable(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UISelectable class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UISelectable));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_selectable(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISelectable obj = (ZFrame.UGUI.UISelectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectable on a nil value");
			}
		}

		L.PushLightUserData(obj.selectable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_selectable(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.UISelectable obj = (ZFrame.UGUI.UISelectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name selectable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index selectable on a nil value");
			}
		}

		obj.selectable = L.ToComponent(3, typeof(UnityEngine.UI.Selectable)) as UnityEngine.UI.Selectable;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInteractable(IntPtr L)
	{
		L.ChkArgsCount(1);
		ZFrame.UGUI.UISelectable obj = (ZFrame.UGUI.UISelectable)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UISelectable");
		bool o = obj.IsInteractable();
		L.PushBoolean(o);
		return 1;
	}
}


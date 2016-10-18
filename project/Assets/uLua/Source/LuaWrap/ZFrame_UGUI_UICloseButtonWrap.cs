using System;
using LuaInterface;

public class ZFrame_UGUI_UICloseButtonWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnPointerClick", OnPointerClick),
			new LuaMethod("new", _CreateZFrame_UGUI_UICloseButton),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		var type = typeof(ZFrame.UGUI.UICloseButton);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_UICloseButton(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.UICloseButton class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.UICloseButton));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerClick(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.UGUI.UICloseButton obj = (ZFrame.UGUI.UICloseButton)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.UICloseButton");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerClick(arg0);
		return 0;
	}
}


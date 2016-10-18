using System;
using LuaInterface;

public class ZFrame_Tween_BaseTweenerWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Tween", Tween),
			new LuaMethod("new", _CreateZFrame_Tween_BaseTweener),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		var type = typeof(ZFrame.Tween.BaseTweener);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_Tween_BaseTweener(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.Tween.BaseTweener class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.Tween.BaseTweener));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Tween(IntPtr L)
	{
		L.ChkArgsCount(4);
		ZFrame.Tween.BaseTweener obj = (ZFrame.Tween.BaseTweener)L.ChkUnityObjectSelf(1, "ZFrame.Tween.BaseTweener");
		var arg0 = L.ToAnyObject(2);
		var arg1 = L.ToAnyObject(3);
		var arg2 = (float)L.ChkNumber(4);
		ZFrame.Tween.ZTweener o = obj.Tween(arg0,arg1,arg2);
		L.PushLightUserData(o);
		return 1;
	}
}


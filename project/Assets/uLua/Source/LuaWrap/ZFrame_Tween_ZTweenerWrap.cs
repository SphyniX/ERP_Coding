using System;
using LuaInterface;

public class ZFrame_Tween_ZTweenerWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("new", _CreateZFrame_Tween_ZTweener),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("tweener", get_tweener, set_tweener),
		};

		var type = typeof(ZFrame.Tween.ZTweener);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_Tween_ZTweener(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 1)
		{
			DG.Tweening.Tweener arg0 = (DG.Tweening.Tweener)L.ChkUserData(1, typeof(DG.Tweening.Tweener));
			ZFrame.Tween.ZTweener obj = new ZFrame.Tween.ZTweener(arg0);
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ZFrame.Tween.ZTweener.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.Tween.ZTweener));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_tweener(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.Tween.ZTweener obj = (ZFrame.Tween.ZTweener)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tweener");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tweener on a nil value");
			}
		}

		L.PushLightUserData(obj.tweener);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_tweener(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.Tween.ZTweener obj = (ZFrame.Tween.ZTweener)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name tweener");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index tweener on a nil value");
			}
		}

		obj.tweener = (DG.Tweening.Tweener)L.ChkUserData(3, typeof(DG.Tweening.Tweener));
		return 0;
	}
}


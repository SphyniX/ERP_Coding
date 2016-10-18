using System;
using LuaInterface;

public class ZFrame_UGUI_FollowUITargetWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("new", _CreateZFrame_UGUI_FollowUITarget),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("target", get_target, set_target),
			new LuaField("depthOfView", get_depthOfView, set_depthOfView),
			new LuaField("gameCamera", get_gameCamera, set_gameCamera),
			new LuaField("uiCamera", get_uiCamera, set_uiCamera),
			new LuaField("disableIfInvisible", get_disableIfInvisible, set_disableIfInvisible),
			new LuaField("alwaysSmooth", get_alwaysSmooth, set_alwaysSmooth),
			new LuaField("smoothTime", get_smoothTime, set_smoothTime),
			new LuaField("followTarget", get_followTarget, set_followTarget),
		};

		var type = typeof(ZFrame.UGUI.FollowUITarget);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_UGUI_FollowUITarget(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.UGUI.FollowUITarget class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.UGUI.FollowUITarget));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_target(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		L.PushLightUserData(obj.target);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_depthOfView(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depthOfView");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depthOfView on a nil value");
			}
		}

		L.PushNumber(obj.depthOfView);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameCamera on a nil value");
			}
		}

		L.PushLightUserData(obj.gameCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_uiCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uiCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uiCamera on a nil value");
			}
		}

		L.PushLightUserData(obj.uiCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_disableIfInvisible(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disableIfInvisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disableIfInvisible on a nil value");
			}
		}

		L.PushBoolean(obj.disableIfInvisible);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_alwaysSmooth(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alwaysSmooth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alwaysSmooth on a nil value");
			}
		}

		L.PushBoolean(obj.alwaysSmooth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_smoothTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name smoothTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index smoothTime on a nil value");
			}
		}

		L.PushNumber(obj.smoothTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_followTarget(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name followTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index followTarget on a nil value");
			}
		}

		L.PushLightUserData(obj.followTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_target(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name target");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index target on a nil value");
			}
		}

		obj.target = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_depthOfView(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depthOfView");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depthOfView on a nil value");
			}
		}

		obj.depthOfView = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_gameCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gameCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gameCamera on a nil value");
			}
		}

		obj.gameCamera = L.ToComponent(3, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_uiCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name uiCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index uiCamera on a nil value");
			}
		}

		obj.uiCamera = L.ToComponent(3, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_disableIfInvisible(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name disableIfInvisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index disableIfInvisible on a nil value");
			}
		}

		obj.disableIfInvisible = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_alwaysSmooth(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name alwaysSmooth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index alwaysSmooth on a nil value");
			}
		}

		obj.alwaysSmooth = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_smoothTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name smoothTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index smoothTime on a nil value");
			}
		}

		obj.smoothTime = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_followTarget(IntPtr L)
	{
		object o = L.ToUserData(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name followTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index followTarget on a nil value");
			}
		}

		obj.followTarget = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		L.ChkArgsCount(1);
		ZFrame.UGUI.FollowUITarget obj = (ZFrame.UGUI.FollowUITarget)L.ChkUnityObjectSelf(1, "ZFrame.UGUI.FollowUITarget");
		obj.Init();
		return 0;
	}
}


using System;
using UnityEngine;
using System.Collections;
using LuaInterface;

public class UnityEngine_AnimationWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Stop", Stop),
			new LuaMethod("Rewind", Rewind),
			new LuaMethod("Sample", Sample),
			new LuaMethod("IsPlaying", IsPlaying),
			new LuaMethod("get_Item", get_Item),
			new LuaMethod("Play", Play),
			new LuaMethod("CrossFade", CrossFade),
			new LuaMethod("Blend", Blend),
			new LuaMethod("CrossFadeQueued", CrossFadeQueued),
			new LuaMethod("PlayQueued", PlayQueued),
			new LuaMethod("AddClip", AddClip),
			new LuaMethod("RemoveClip", RemoveClip),
			new LuaMethod("GetClipCount", GetClipCount),
			new LuaMethod("SyncLayer", SyncLayer),
			new LuaMethod("GetEnumerator", GetEnumerator),
			new LuaMethod("GetClip", GetClip),
			new LuaMethod("new", _CreateAnimation),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("clip", get_clip, set_clip),
			new LuaField("playAutomatically", get_playAutomatically, set_playAutomatically),
			new LuaField("wrapMode", get_wrapMode, set_wrapMode),
			new LuaField("isPlaying", get_isPlaying, null),
			new LuaField("animatePhysics", get_animatePhysics, set_animatePhysics),
			new LuaField("cullingType", get_cullingType, set_cullingType),
			new LuaField("localBounds", get_localBounds, set_localBounds),
		};

		var type = typeof(Animation);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAnimation(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Animation obj = new Animation();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Animation));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_clip(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clip on a nil value");
			}
		}

		L.PushLightUserData(obj.clip);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playAutomatically(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playAutomatically");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playAutomatically on a nil value");
			}
		}

		L.PushBoolean(obj.playAutomatically);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_wrapMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wrapMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wrapMode on a nil value");
			}
		}

		L.PushUData(obj.wrapMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isPlaying(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isPlaying");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isPlaying on a nil value");
			}
		}

		L.PushBoolean(obj.isPlaying);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animatePhysics(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animatePhysics");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animatePhysics on a nil value");
			}
		}

		L.PushBoolean(obj.animatePhysics);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cullingType(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingType on a nil value");
			}
		}

		L.PushUData(obj.cullingType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_localBounds(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localBounds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localBounds on a nil value");
			}
		}

		L.PushLightUserData(obj.localBounds);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_clip(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clip");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clip on a nil value");
			}
		}

		obj.clip = (AnimationClip)L.ChkUnityObject(3, typeof(AnimationClip));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_playAutomatically(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playAutomatically");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playAutomatically on a nil value");
			}
		}

		obj.playAutomatically = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_wrapMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name wrapMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index wrapMode on a nil value");
			}
		}

		obj.wrapMode = (WrapMode)L.ChkEnumValue(3, typeof(WrapMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_animatePhysics(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animatePhysics");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animatePhysics on a nil value");
			}
		}

		obj.animatePhysics = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cullingType(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingType on a nil value");
			}
		}

		obj.cullingType = (AnimationCullingType)L.ChkEnumValue(3, typeof(AnimationCullingType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_localBounds(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animation obj = (Animation)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name localBounds");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index localBounds on a nil value");
			}
		}

		obj.localBounds = L.ToBounds(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Stop(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			obj.Stop();
			return 0;
		}
		else if (count == 2)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			obj.Stop(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Stop");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rewind(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			obj.Rewind();
			return 0;
		}
		else if (count == 2)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			obj.Rewind(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Rewind");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Sample(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
		obj.Sample();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsPlaying(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
		var arg0 = L.ToLuaString(2);
		bool o = obj.IsPlaying(arg0);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
		var arg0 = L.ToLuaString(2);
		AnimationState o = obj[arg0];
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			bool o = obj.Play();
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animation), typeof(string)))
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ChkLuaString(2);
			bool o = obj.Play(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animation), typeof(PlayMode)))
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = (PlayMode)L.ChkEnumValue(2, typeof(PlayMode));
			bool o = obj.Play(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 3)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (PlayMode)L.ChkEnumValue(3, typeof(PlayMode));
			bool o = obj.Play(arg0,arg1);
			L.PushBoolean(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Play");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CrossFade(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			obj.CrossFade(arg0);
			return 0;
		}
		else if (count == 3)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (float)L.ChkNumber(3);
			obj.CrossFade(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (float)L.ChkNumber(3);
			var arg2 = (PlayMode)L.ChkEnumValue(4, typeof(PlayMode));
			obj.CrossFade(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.CrossFade");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Blend(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			obj.Blend(arg0);
			return 0;
		}
		else if (count == 3)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (float)L.ChkNumber(3);
			obj.Blend(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (float)L.ChkNumber(3);
			var arg2 = (float)L.ChkNumber(4);
			obj.Blend(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.Blend");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CrossFadeQueued(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			AnimationState o = obj.CrossFadeQueued(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 3)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (float)L.ChkNumber(3);
			AnimationState o = obj.CrossFadeQueued(arg0,arg1);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 4)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (float)L.ChkNumber(3);
			var arg2 = (QueueMode)L.ChkEnumValue(4, typeof(QueueMode));
			AnimationState o = obj.CrossFadeQueued(arg0,arg1,arg2);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 5)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (float)L.ChkNumber(3);
			var arg2 = (QueueMode)L.ChkEnumValue(4, typeof(QueueMode));
			var arg3 = (PlayMode)L.ChkEnumValue(5, typeof(PlayMode));
			AnimationState o = obj.CrossFadeQueued(arg0,arg1,arg2,arg3);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.CrossFadeQueued");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayQueued(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			AnimationState o = obj.PlayQueued(arg0);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 3)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (QueueMode)L.ChkEnumValue(3, typeof(QueueMode));
			AnimationState o = obj.PlayQueued(arg0,arg1);
			L.PushLightUserData(o);
			return 1;
		}
		else if (count == 4)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ToLuaString(2);
			var arg1 = (QueueMode)L.ChkEnumValue(3, typeof(QueueMode));
			var arg2 = (PlayMode)L.ChkEnumValue(4, typeof(PlayMode));
			AnimationState o = obj.PlayQueued(arg0,arg1,arg2);
			L.PushLightUserData(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.PlayQueued");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddClip(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			AnimationClip arg0 = (AnimationClip)L.ChkUnityObject(2, typeof(AnimationClip));
			var arg1 = L.ToLuaString(3);
			obj.AddClip(arg0,arg1);
			return 0;
		}
		else if (count == 5)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			AnimationClip arg0 = (AnimationClip)L.ChkUnityObject(2, typeof(AnimationClip));
			var arg1 = L.ToLuaString(3);
			var arg2 = (int)L.ChkNumber(4);
			var arg3 = (int)L.ChkNumber(5);
			obj.AddClip(arg0,arg1,arg2,arg3);
			return 0;
		}
		else if (count == 6)
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			AnimationClip arg0 = (AnimationClip)L.ChkUnityObject(2, typeof(AnimationClip));
			var arg1 = L.ToLuaString(3);
			var arg2 = (int)L.ChkNumber(4);
			var arg3 = (int)L.ChkNumber(5);
			var arg4 = L.ChkBoolean(6);
			obj.AddClip(arg0,arg1,arg2,arg3,arg4);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.AddClip");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveClip(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animation), typeof(string)))
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			var arg0 = L.ChkLuaString(2);
			obj.RemoveClip(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animation), typeof(AnimationClip)))
		{
			Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
			AnimationClip arg0 = (AnimationClip)L.ToUserData(2);
			obj.RemoveClip(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animation.RemoveClip");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClipCount(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
		int o = obj.GetClipCount();
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SyncLayer(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
		var arg0 = (int)L.ChkNumber(2);
		obj.SyncLayer(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
		IEnumerator o = obj.GetEnumerator();
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClip(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animation obj = (Animation)L.ChkUnityObjectSelf(1, "Animation");
		var arg0 = L.ToLuaString(2);
		AnimationClip o = obj.GetClip(arg0);
		L.PushLightUserData(o);
		return 1;
	}
}


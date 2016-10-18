using System;
using UnityEngine;
using LuaInterface;

public class ZFrame_AudioManagerWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetTemplate", GetTemplate),
			new LuaMethod("GetSource", GetSource),
			new LuaMethod("FindSource", FindSource),
			new LuaMethod("Play", Play),
			new LuaMethod("Replay", Replay),
			new LuaMethod("Stop", Stop),
			new LuaMethod("new", _CreateZFrame_AudioManager),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("Inst", get_Inst, null),
		};

		var type = typeof(ZFrame.AudioManager);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateZFrame_AudioManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "ZFrame.AudioManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(ZFrame.AudioManager));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Inst(IntPtr L)
	{
		L.PushLightUserData(ZFrame.AudioManager.Inst);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTemplate(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.AudioManager obj = (ZFrame.AudioManager)L.ChkUnityObjectSelf(1, "ZFrame.AudioManager");
		var arg0 = L.ToLuaString(2);
		GameObject o = obj.GetTemplate(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSource(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.AudioManager obj = (ZFrame.AudioManager)L.ChkUnityObjectSelf(1, "ZFrame.AudioManager");
		var arg0 = L.ToLuaString(2);
		AudioSource o = obj.GetSource(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSource(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.AudioManager obj = (ZFrame.AudioManager)L.ChkUnityObjectSelf(1, "ZFrame.AudioManager");
		var arg0 = L.ToLuaString(2);
		AudioSource o = obj.FindSource(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		L.ChkArgsCount(3);
		ZFrame.AudioManager obj = (ZFrame.AudioManager)L.ChkUnityObjectSelf(1, "ZFrame.AudioManager");
		var arg0 = L.ToLuaString(2);
		var arg1 = L.ToLuaString(3);
		obj.Play(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Replay(IntPtr L)
	{
		L.ChkArgsCount(3);
		ZFrame.AudioManager obj = (ZFrame.AudioManager)L.ChkUnityObjectSelf(1, "ZFrame.AudioManager");
		var arg0 = L.ToLuaString(2);
		var arg1 = L.ToLuaString(3);
		obj.Replay(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Stop(IntPtr L)
	{
		L.ChkArgsCount(2);
		ZFrame.AudioManager obj = (ZFrame.AudioManager)L.ChkUnityObjectSelf(1, "ZFrame.AudioManager");
		var arg0 = L.ToLuaString(2);
		obj.Stop(arg0);
		return 0;
	}
}


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;

public static class LibSystem {

    public const string LIB_NAME = "libsystem.cs";

    public static void OpenLib(ILuaState lua)
    {
        var define = new NameFuncPair[]
        {
            new NameFuncPair("GetHashCode", GetHashCode),

            // 位运算
            new NameFuncPair("BitOr", BitOr),
            new NameFuncPair("BitAnd", BitAnd),
            new NameFuncPair("BitXor", BitXor),

            new NameFuncPair("StringFmt", StringFmt),
            new NameFuncPair("GetMacAddr", GetMacAddr),

            new NameFuncPair("ScheduleNotification", ScheduleNotification),
            new NameFuncPair("SubmitGameData", SubmitGameData),
            new NameFuncPair("DateTime", DateTime),
            new NameFuncPair("StartGps", StartGps),
            new NameFuncPair("GetGps", GetGps),
            new NameFuncPair("StopGps", StopGps),
            new NameFuncPair("CallApiReturn", CallApiReturn),
            new NameFuncPair("GetOS", GetOS),
        };

        lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetOS(ILuaState lua)
    {
#if UNITY_IOS
        lua.PushString("IOS");
#elif UNITY_ANDROID
        lua.PushString("Android");
#else
        lua.PushString("PC");
#endif
        return 1;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetHashCode(ILuaState lua)
    {
        object o = lua.ToAnyObject(1);
        lua.PushInteger(o.GetHashCode());
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int BitOr(ILuaState lua)
    {
        var a = lua.ChkInteger(1);
        var b = lua.ChkInteger(2);
        lua.PushInteger(a | b);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int BitAnd(ILuaState lua)
    {
        var a = lua.ChkInteger(1);
        var b = lua.ChkInteger(2);
        lua.PushInteger(a & b);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int BitXor(ILuaState lua)
    {
        var a = lua.ChkInteger(1);
        var b = lua.ChkInteger(2);
        lua.PushInteger(a ^ b);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int StringFmt(ILuaState lua)
    {
        string format;
        object[] Args;
        lua.ToStringFromatArgs(1, out format, out Args);
        lua.PushString(string.Format(format, Args));
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int GetMacAddr(ILuaState lua)
    {
        string macAddr = null;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS
        var nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();        
		for (int i = 0; i < nics.Length; ++i) {
			var ni = nics[i];
			if (ni.Description == "en0") {
				macAddr = ni.GetPhysicalAddress().ToString();
				break;
			} else {
				macAddr = ni.GetPhysicalAddress().ToString();
				if (string.IsNullOrEmpty(macAddr)) continue;
				break;
			}
		}

#elif UNITY_ANDROID
        try {
		    macAddr = ZFrame.SDKManager.CallApiReturn<string>("com.rongygame.util", "GetMacAddress");
        } catch { }
#endif

        if (macAddr == null) macAddr = "00:00:00:00";
        lua.PushString(macAddr);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int ScheduleNotification(ILuaState lua)
    {
        var jo = lua.ToJsonObj(1);
        ZFrame.Notification.NotifyMgr.Instance.ScheduleNotification(jo);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SubmitGameData(ILuaState lua)
    {
        var klass = lua.ChkString(1);
        var method = lua.ChkString(2);
        var json = lua.ToJsonObj(3).ToJSONString();
        lua.PushString(ZFrame.SDKManager.SubmitGameData(klass, method, json));
        return 1;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int CallApiReturn(ILuaState lua)
    {
        var klass = lua.ChkString(1);
        var method = lua.ChkString(2);
        var param = lua.ChkString(3);
        lua.PushString(ZFrame.SDKManager.CallApiReturn<string>(klass, method, param));
        return 1;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DateTime(ILuaState lua) {
        System.DateTime second = System.DateTime.Now;
        string time = second.ToString();
        lua.PushString(time);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int StartGps(ILuaState lua) {
        GPSMgr.Instance.StartGps();
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetGps(ILuaState lua)
    {
        lua.PushString(GPSMgr.Instance.GetGps());
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int StopGps(ILuaState lua)
    {
        GPSMgr.Instance.StopGps();
        return 0;
    }
}

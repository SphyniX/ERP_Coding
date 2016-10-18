using UnityEngine;
using System.Collections;
using TinyJSON;
using DG.Tweening;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;

public static class LibTool {

	public const string LIB_NAME = "libtool.cs";
	
	public static void OpenLib(ILuaState lua)
	{
		var define = new NameFuncPair[]
		{
			new NameFuncPair("JSONToTable", JSONToTable),
			new NameFuncPair("TableToJSON", TableToJSON),
		};
		
		lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int JSONToTable(ILuaState lua)
	{
		string jsonStr = lua.ChkString(1);
        if (string.IsNullOrEmpty(jsonStr)) {
            lua.PushNil();
        } else {
			lua.PushVariant(JSON.Load(jsonStr));
        }
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int TableToJSON(ILuaState lua)
    {
        Variant jsonObj = lua.ToJsonObj(1);
        if (jsonObj != null) {
            bool prettyPrinted = lua.OptBoolean(2, false);
            Variant.s_GlobalIndent = 0;
            lua.PushString(jsonObj.ToJSONString(prettyPrinted));
        } else lua.PushString("");
		return 1;
	}

}

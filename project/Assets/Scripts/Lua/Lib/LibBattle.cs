using UnityEngine;
using System.Collections;
using LuaInterface;
using TinyJSON;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;

public static class LibBattle {

    public const string LIB_NAME = "libbattle.cs";

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static void OpenLib(ILuaState lua)
    {
        var define = new NameFuncPair[]
        {

        };

        lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
    }
}

using UnityEngine;
using System.Collections;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;

public static class LibCSharpIO
{

    public const string LIB_NAME = "libcsharpio.cs";

    public static void OpenLib(ILuaState lua) {
        var define = new NameFuncPair[]
        {
			new NameFuncPair("ReadAllText", ReadAllText),
			new NameFuncPair("WriteAllText", WriteAllText),
			new NameFuncPair("DeleteFile", DeleteFile),
            new NameFuncPair("MoveFile", MoveFile),
            new NameFuncPair("CreateDir", CreateDir),


        };

        lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int ReadAllText(ILuaState lua)
    {
        string path = lua.ChkString(1);
        try {
            string text = System.IO.File.ReadAllText(path);
			lua.PushString(text);
        } catch (System.Exception e) {
            LogMgr.E(e.Message + ": " + path);
        }
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int WriteAllText(ILuaState lua)
	{
		string path = lua.ChkString(1);
		string text = lua.ChkString(2);
		try {
			System.IO.File.WriteAllText(path, text);
		} catch (System.Exception e) {
			LogMgr.E(e.Message + ": " + path);
		}
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int DeleteFile(ILuaState lua)
	{
		string path = lua.ChkString(1);
		System.IO.File.Delete(path);
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int MoveFile(ILuaState lua)
    {
        string src = lua.ChkString(1);
        string dst = lua.ChkString(2);
        bool overWrite = lua.OptBoolean(3, false);
        if (System.IO.File.Exists(dst)) {
            if (overWrite) {
                System.IO.File.Delete(dst);
            } else {
                //return 1;
            }            
        }
        if (System.IO.File.Exists(src))
        {
            System.IO.File.Move(src, dst);
            if (System.IO.File.Exists(dst))
            {
                lua.PushBoolean(true);
            }
            else
            {
                lua.PushBoolean(false);
            }
        }
        else
        {
            lua.PushBoolean(false);
        }
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int CreateDir(ILuaState lua)
    {
        string path = lua.ChkString(1);
        if (string.IsNullOrEmpty(path))
        {
            lua.PushNil();
        }
        else
        {
            SystemTools.NeedDirectory(path);
        }
        return 1;
    }

}

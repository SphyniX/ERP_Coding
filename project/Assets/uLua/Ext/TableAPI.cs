using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

public static class TableAPI
{
    public static void SetKeyValue(this ILuaState self, object key, object value)
    {
        self.PushAnyObject(key);
        self.PushAnyObject(value);
        self.RawSet(-3);
    }

    public static void SetRegistryIndex(this ILuaState self, string key, string regKey)
    {
        self.PushString(key);
        self.PushString(regKey);
        self.RawGet(LuaIndexes.LUA_REGISTRYINDEX);
        self.RawSet(-3);
    }

    public static void SetArray(this ILuaState self, int n, TinyJSON.Variant json)
    {
        self.PushInteger(n);
        self.PushVariant(json);
        self.RawSet(-3);
    }

    public static void SetDict(this ILuaState self, string key)
    {
        self.PushString(key);
        self.PushNil();
        self.RawSet(-3);
    }

    public static void SetDict(this ILuaState self, string key, float value)
    {
        self.PushString(key);
        self.PushNumber(value);
        self.RawSet(-3);
    }

    public static void SetDict(this ILuaState self, string key, string value)
    {
        self.PushString(key);
        self.PushString(value);
        self.RawSet(-3);
    }

    public static void SetDict(this ILuaState self, string key, TinyJSON.Variant json)
    {
        self.PushString(key);
        self.PushVariant(json);
        self.RawSet(-3);
    }

    public static void SetDict(this ILuaState self, string key, LuaCSFunction func)
    {
        self.PushString(key);
        self.PushCSharpFunction(func);
        self.RawSet(-3);
    }

    public static void SetDict(this ILuaState self, string key, Object uObj)
    {
        self.PushString(key);
        self.PushLightUserData(uObj);
        self.RawSet(-3);
    }

    public static float GetFloatValue(this ILuaState self, int index, string key)
    {
        self.PushString(key);
        self.GetTable(index);
        float ret = self.ToSingle(-1);
        self.Pop(1);
        return ret;
    }

    public static void SetFloatValue(this ILuaState self, int index, string key, float value)
    {
        self.PushString(key);
        self.PushNumber(value);
        self.RawSet(index);
    }
}

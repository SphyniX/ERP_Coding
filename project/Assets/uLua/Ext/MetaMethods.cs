using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

public static class MetaMethods
{
    public const string ToLua_TableCall = "ToLua_TableCall";
    public static void RegTableCall(ILuaState L)
    {
        string luaTableCall = @"
            local rawget = rawget
            local getmetatable = getmetatable     

            local function call(obj, ...)
                local meta = getmetatable(obj)
                local fun = rawget(meta, 'new')
                    
                if fun ~= nil then
                    return fun(...)
                else
                    error('unknow function __call',2)
                end
            end

            return call
        ";
        
        L.RefChunk(ToLua_TableCall, luaTableCall);
    }

    public const string ToLua_EnumIndex = "ToLua_EnumIndex";
    public static void RegEnumIndex(ILuaState L)
    {
        string luaEnumIndex = @"
            local rawget = rawget                
            local getmetatable = getmetatable         

            local function indexEnum(obj,name)
                local v = rawget(obj, name)
                
                if v ~= nil then
                    return v
                end

                local meta = getmetatable(obj)  
                local func = rawget(meta, name)            
                
                if func ~= nil then
                    v = func()
                    rawset(obj, name, v)
                    return v
                else
                    error('field '..name..' does not exist', 2)
                end
            end

            return indexEnum
        ";

        L.PushString(ToLua_EnumIndex);
        L.L_DoString(luaEnumIndex);
        L.RawSet(LuaIndexes.LUA_REGISTRYINDEX);
    }
    
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public static int GetType(ILuaState L)
    {
        var o = L.ToAnyObject(1);
        L.PushLightUserData(o is System.Type ? o : o.GetType());
        return 1;
    }
    
    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static int __tostring(ILuaState L)
    {
        if (L.Type(1) == LuaTypes.LUA_TUSERDATA) {
            L.PushString(L.ToUserData(1).ToString());
        } else {
            L.PushString("csharp meta table");
        }
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public static int __eq(ILuaState L)
    {
        var op1 = L.ToUserData(1);
        var op2 = L.ToUserData(2);

        if (op1 == null) {
            L.PushBoolean(op2 == null ? true : false);    
        } else {
            L.PushBoolean(op1.Equals(op2));
        }
        return 1;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static int __gc(ILuaState L)
    {
        int udata = LuaDLL.luanet_rawnetobj(L, 1);

        if (udata != -1) {
            var ls = LuaEnv.Get(L).ls;
            ls.translator.collectObject(udata);
        }

        return 0;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static int __index(ILuaState L)
    {
        // 从元表取出field的值
        string field = L.ToString(2);
        L.GetMetaTable(1);
        L.PushString(field);
        L.RawGet(-2);
        L.Remove(-2);

        var luaT = L.Type(-1);
        if (luaT == LuaTypes.LUA_TTABLE) {
            // 该field是个属性
            L.RawGetI(-1, 1);
            L.PushValue(1);
            L.Call(1, 1);
            L.Remove(-2);
        } else if (luaT == LuaTypes.LUA_TFUNCTION) {
            // 函数直接返回
        } else {
            L.L_Error(string.Format("field or property {0} does not exist, get {1}", field, luaT));
            L.Pop(1);
            return 0;
        }

        return 1;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static int __newindex(ILuaState L)
    {
        // 从元表取出field的值
        string field = L.ToString(2);
        L.GetMetaTable(1);
        L.PushString(field);
        L.RawGet(-2);
        L.Remove(-2);

        var luaT = L.Type(-1);
        if (luaT == LuaTypes.LUA_TTABLE) {
            // 该field只能是个属性
            L.RawGetI(-1, 2);
            L.PushValue(1);
            L.Call(1, 1);
            L.Remove(-2);
        } else {
            L.L_Error(string.Format("field or property {0} does not exist, get {1}", field, luaT));
            L.Pop(1);
            return 0;
        }

        return 1;
    }
 
}

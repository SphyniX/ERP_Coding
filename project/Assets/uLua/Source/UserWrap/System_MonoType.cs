using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

public static class System_MonoType
{
    public static void Wrap(ILuaState L)
    {
        // 没有类表，只有元表
        L.L_NewMetaTable(WrapToLua.GetMetaName("System.MonoType"));

        // 继承自System.Object
        L.L_GetMetaTable("System.Object");
        L.SetMetaTable(-2);

        LuaDLL.tolua_setindex(L);
        LuaDLL.tolua_setnewindex(L);
        L.SetDict("__gc", new LuaCSFunction(MetaMethods.__gc));
        L.SetDict("__tostring", new LuaCSFunction(MetaMethods.__tostring));

        L.RegistMembers(new LuaMethod[] {
            new LuaMethod("IsSubclassOf", IsSubclassOf),
        }, null);

        L.Pop(1);
    }
    
    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static int IsSubclassOf(ILuaState L)
    {
        var type = L.ChkTypeObject(1);
        var baseType = L.ChkTypeObject(2);
        L.PushBoolean(type.IsSubclassOf(baseType));
        return 1;
    }
}

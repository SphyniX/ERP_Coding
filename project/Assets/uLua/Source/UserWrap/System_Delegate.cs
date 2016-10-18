using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using ILuaState = System.IntPtr;

using DelegateCreate = System.Func<System.IntPtr, System.Delegate>;

public static class System_Delegate
{    
    private const string CLASS = "System.Deletage";

    public static void PushUData(this ILuaState self, System.Delegate value)
    {
        var ls = LuaEnv.Get(self).ls;
        ls.translator.pushObject(self, value, CLASS);
    }

    public static void Wrap(ILuaState L)
    {
        L.L_NewMetaTable(CLASS);

        L.L_GetMetaTable("System.Object");
        L.SetMetaTable(-2);

        L.SetDict("__gc", new LuaCSFunction(MetaMethods.__gc));
        L.SetDict("__tostring", new LuaCSFunction(MetaMethods.__tostring));
        L.SetDict("__call", new LuaCSFunction(__call));
        L.SetDict("Create", new LuaCSFunction(Create));

        L.Pop(1);
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public static int __call(ILuaState L)
    {
        var Delegate = L.ChkUserDataSelf(1, "System.Delegate") as System.Delegate;
        if (Delegate != null) {
            var top = L.GetTop();
            var args = L.ToParamsObject(2, top - 2);
            Delegate.DynamicInvoke(args);
        }
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    public static int Create(ILuaState L)
    {
        var key = L.ChkString(1);
        DelegateCreate creator;
        if (s_DelegateFactory.TryGetValue(key, out creator)) {
            L.PushUData(creator(L));
            return 1;
        }
        return 0;
    }

    private static Dictionary<string, DelegateCreate> s_DelegateFactory = new Dictionary<string, DelegateCreate>() {
        {"System.Action", New_System_Action },
        //{"UnityAction<IBehaviorObj>", New_UnityAction_IBehaviorObj },
        //{"UnityAction<IValueProcessor,IValue>", New_UnityAction_IValueProcessor_IValue },
    };
    
    public static System.Delegate New_System_Action(ILuaState L)
    {
        var func = L.ChkLuaFunction(2);
        return (System.Action)(() => {
            int top = func.BeginPCall();
            func.PCall(top, 0);
            func.EndPCall(top);
        });
    }
        
    //public static System.Delegate New_UnityAction_IBehaviorObj(ILuaState L)
    //{
    //    var func = L.ChkLuaFunction(2);
    //    return (UnityAction<Battle.Define.IBehaviorObj>)((p) => {
    //        var lua = func.GetLuaState();
    //        int top = func.BeginPCall();
    //        lua.PushLightUserData(p);
    //        func.PCall(top, 1);
    //        func.EndPCall(top);
    //    });
    //}

    //public static System.Delegate New_UnityAction_IValueProcessor_IValue(ILuaState L)
    //{
    //    var func = L.ChkLuaFunction(2);
    //    return (UnityAction<Battle.Define.IValueProcessor, Battle.Define.IValue>)((p, v) => {
    //        var lua = func.GetLuaState();
    //        int top = func.BeginPCall();
    //        lua.PushLightUserData(p);
    //        lua.PushLightUserData(v);
    //        func.PCall(top, 2);
    //        func.EndPCall(top);
    //    });
    //}
}

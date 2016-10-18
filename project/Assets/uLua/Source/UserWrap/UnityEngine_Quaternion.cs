using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

//namespace ToLua.Wrap
//{
public static class UnityEngine_Quaternion
{
    public const string CLASS = "UnityEngine.Quaternion";

    public static void PushUData(this ILuaState self, Quaternion value)
    {
        self.CreateTable(0, 4);
        self.SetDict("x", value.x);
        self.SetDict("y", value.y);
        self.SetDict("z", value.z);
        self.SetDict("w", value.z);
        self.L_GetMetaTable(CLASS);
        self.SetMetaTable(-2);
    }

    public static Quaternion ToQuaternion(this ILuaState self, int index)
    {
        float x = self.GetFloatValue(index, "x");
        float y = self.GetFloatValue(index, "y");
        float z = self.GetFloatValue(index, "z");
        float w = self.GetFloatValue(index, "w");
        return new Quaternion(x, y, z, w);
    }

    private static void SetQuaternion(ILuaState L, int index, float x, float y, float z, float w)
    {
        L.SetFloatValue(index, "x", x);
        L.SetFloatValue(index, "y", y);
        L.SetFloatValue(index, "z", z);
        L.SetFloatValue(index, "w", w);
    }

    public static void Wrap(ILuaState L)
    {
        L.RegistType(typeof(Quaternion));
        L.L_NewMetaTable(CLASS);
       
        L.L_GetMetaTable("System.Object");
        L.SetMetaTable(-2);

        L.SetDict("class", CLASS);
        L.SetDict("__tostring", new LuaCSFunction(__tostring));
        L.SetRegistryIndex("__call", MetaMethods.ToLua_TableCall);

        L.RegistMembers(new LuaMethod[] {
            new LuaMethod("Set", Set),
            new LuaMethod("Dot", Dot),
            new LuaMethod("AngleAxis", AngleAxis),
            new LuaMethod("ToAngleAxis", ToAngleAxis),
            new LuaMethod("FromToRotation", FromToRotation),
            new LuaMethod("SetFromToRotation", SetFromToRotation),
            new LuaMethod("LookRotation", LookRotation),
            new LuaMethod("SetLookRotation", SetLookRotation),
            new LuaMethod("Slerp", Slerp),
            new LuaMethod("SlerpUnclamped", SlerpUnclamped),
            new LuaMethod("Lerp", Lerp),
            new LuaMethod("LerpUnclamped", LerpUnclamped),
            new LuaMethod("RotateTowards", RotateTowards),
            new LuaMethod("Inverse", Inverse),
            new LuaMethod("Angle", Angle),
            new LuaMethod("Euler", Euler),
            new LuaMethod("new", _CreateQuaternion),
            new LuaMethod("GetType", GetClassType),
            new LuaMethod("__mul", Lua_Mul),
        },
        new LuaField[] {
            new LuaField("kEpsilon", get_kEpsilon, null),
            new LuaField("identity", get_identity, null),
            new LuaField("eulerAngles", get_eulerAngles, set_eulerAngles),
        });

        LuaDLL.tolua_setindex(L);
        LuaDLL.tolua_setnewindex(L);
        L.SetMetaTable(-2);
        L.Pop(1);
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int __tostring(ILuaState L)
    {
        L.PushString(L.ToQuaternion(1).ToString());
        return 1;
    }


    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateQuaternion(ILuaState L)
    {
        int count = L.GetTop();

        if (count == 4) {
            var arg0 = (float)L.ChkNumber(1);
            var arg1 = (float)L.ChkNumber(2);
            var arg2 = (float)L.ChkNumber(3);
            var arg3 = (float)L.ChkNumber(4);
            Quaternion obj = new Quaternion(arg0, arg1, arg2, arg3);
            L.PushUData(obj);
            return 1;
        } else if (count == 0) {
            Quaternion obj = new Quaternion();
            L.PushUData(obj);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Quaternion.New");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetClassType(ILuaState L)
    {
        int count = L.GetTop();
        if (count == 0) {
            L.PushLightUserData(typeof(Quaternion));
            return 1;
        } else {
            return MetaMethods.GetType(L);
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_kEpsilon(ILuaState L)
    {
        L.PushNumber(Quaternion.kEpsilon);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_identity(ILuaState L)
    {
        L.PushUData(Quaternion.identity);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_eulerAngles(ILuaState L)
    {
        L.PushUData(L.ToQuaternion(1).eulerAngles);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int set_eulerAngles(ILuaState L)
    {
        Quaternion obj = L.ToQuaternion(1);
        obj.eulerAngles = L.ToVector3(3);
        SetQuaternion(L, 1, obj.x, obj.y, obj.z, obj.w);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Set(ILuaState L)
    {
        L.ChkArgsCount(5);
        SetQuaternion(L, 1, L.ToSingle(2), L.ToSingle(3), L.ToSingle(4), L.ToSingle(5));
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Dot(ILuaState L)
    {
        L.ChkArgsCount(2);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        float o = Quaternion.Dot(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int AngleAxis(ILuaState L)
    {
        L.ChkArgsCount(2);
        var arg0 = (float)L.ChkNumber(1);
        Vector3 arg1 = L.ToVector3(2);
        Quaternion o = Quaternion.AngleAxis(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ToAngleAxis(ILuaState L)
    {
        L.ChkArgsCount(3);
        Quaternion obj = (Quaternion)L.ChkUserDataSelf(1, "Quaternion");
        float arg0;
        Vector3 arg1;
        obj.ToAngleAxis(out arg0, out arg1);
        L.SetValue(1, obj);
        L.PushNumber(arg0);
        L.PushUData(arg1);
        return 2;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int FromToRotation(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Quaternion o = Quaternion.FromToRotation(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SetFromToRotation(ILuaState L)
    {
        L.ChkArgsCount(3);
        Quaternion obj = L.ToQuaternion(1);
        Vector3 arg0 = L.ToVector3(2);
        Vector3 arg1 = L.ToVector3(3);
        obj.SetFromToRotation(arg0, arg1);
        SetQuaternion(L, 1, obj.x, obj.y, obj.z, obj.w);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LookRotation(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 1) {
            Vector3 arg0 = L.ToVector3(1);
            Quaternion o = Quaternion.LookRotation(arg0);
            L.PushUData(o);
            return 1;
        } else if (count == 2) {
            Vector3 arg0 = L.ToVector3(1);
            Vector3 arg1 = L.ToVector3(2);
            Quaternion o = Quaternion.LookRotation(arg0, arg1);
            L.PushUData(o);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Quaternion.LookRotation");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SetLookRotation(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 2) {
            Quaternion obj = L.ToQuaternion(1);
            Vector3 arg0 = L.ToVector3(2);
            obj.SetLookRotation(arg0);
            SetQuaternion(L, 1, obj.x, obj.y, obj.z, obj.w);
            return 0;
        } else if (count == 3) {
            Quaternion obj = L.ToQuaternion(1);
            Vector3 arg0 = L.ToVector3(2);
            Vector3 arg1 = L.ToVector3(3);
            obj.SetLookRotation(arg0, arg1);
            SetQuaternion(L, 1, obj.x, obj.y, obj.z, obj.w);
            return 0;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Quaternion.SetLookRotation");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Slerp(ILuaState L)
    {
        L.ChkArgsCount(3);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        var arg2 = (float)L.ChkNumber(3);
        Quaternion o = Quaternion.Slerp(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SlerpUnclamped(ILuaState L)
    {
        L.ChkArgsCount(3);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        var arg2 = (float)L.ChkNumber(3);
        Quaternion o = Quaternion.SlerpUnclamped(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lerp(ILuaState L)
    {
        L.ChkArgsCount(3);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        var arg2 = (float)L.ChkNumber(3);
        Quaternion o = Quaternion.Lerp(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LerpUnclamped(ILuaState L)
    {
        L.ChkArgsCount(3);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        var arg2 = (float)L.ChkNumber(3);
        Quaternion o = Quaternion.LerpUnclamped(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int RotateTowards(ILuaState L)
    {
        L.ChkArgsCount(3);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        var arg2 = (float)L.ChkNumber(3);
        Quaternion o = Quaternion.RotateTowards(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Inverse(ILuaState L)
    {
        L.ChkArgsCount(1);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion o = Quaternion.Inverse(arg0);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Angle(ILuaState L)
    {
        L.ChkArgsCount(2);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        float o = Quaternion.Angle(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Euler(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 1) {
            Vector3 arg0 = L.ToVector3(1);
            Quaternion o = Quaternion.Euler(arg0);
            L.PushUData(o);
            return 1;
        } else if (count == 3) {
            var arg0 = (float)L.ChkNumber(1);
            var arg1 = (float)L.ChkNumber(2);
            var arg2 = (float)L.ChkNumber(3);
            Quaternion o = Quaternion.Euler(arg0, arg1, arg2);
            L.PushUData(o);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Quaternion.Euler");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Mul(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 2 && L.CheckTypes(1, typeof(LuaTable), typeof(LuaTable))) {
            Quaternion arg0 = L.ToQuaternion(1);
            Vector3 arg1 = L.ToVector3(2);
            Vector3 o = arg0 * arg1;
            L.PushUData(o);
            return 1;
        } else if (count == 2 && L.CheckTypes(1, typeof(LuaTable), typeof(LuaTable))) {
            Quaternion arg0 = L.ToQuaternion(1);
            Quaternion arg1 = L.ToQuaternion(2);
            Quaternion o = arg0 * arg1;
            L.PushUData(o);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Quaternion.op_Multiply");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Eq(ILuaState L)
    {
        L.ChkArgsCount(2);
        Quaternion arg0 = L.ToQuaternion(1);
        Quaternion arg1 = L.ToQuaternion(2);
        bool o = arg0 == arg1;
        L.PushBoolean(o);
        return 1;
    }
}
//}

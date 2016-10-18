using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

//namespace ToLua.Wrap
//{
public static class UnityEngine_Vector3
{
    public const string CLASS = "UnityEngine.Vector3";

    public static void PushUData(this ILuaState self, Vector3 value)
    {
        self.CreateTable(0, 3);
        self.SetDict("x", value.x);
        self.SetDict("y", value.y);
        self.SetDict("z", value.z);
        self.L_GetMetaTable(CLASS);
        self.SetMetaTable(-2);
    }

    public static Vector3 ToVector3(this ILuaState self, int index)
    {
        float x = self.GetFloatValue(index, "x");
        float y = self.GetFloatValue(index, "y");
        float z = self.GetFloatValue(index, "z");
        return new Vector3(x, y, z);
    }

    private static void SetVector3(ILuaState L, int index, float x, float y, float z)
    {
        L.SetFloatValue(index, "x", x);
        L.SetFloatValue(index, "y", y);
        L.SetFloatValue(index, "z", z);
    }

    public static void Wrap(ILuaState L)
    {
        L.RegistType(typeof(Vector3));
        L.L_NewMetaTable(CLASS);
        
        L.L_GetMetaTable("System.Object");
        L.SetMetaTable(-2);

        L.SetDict("class", CLASS);
        L.SetDict("__tostring", new LuaCSFunction(__tostring));
        L.SetRegistryIndex("__call", MetaMethods.ToLua_TableCall);

        L.RegistMembers(new LuaMethod[] {
            new LuaMethod("Lerp", Lerp),
            new LuaMethod("LerpUnclamped", LerpUnclamped),
            new LuaMethod("Slerp", Slerp),
            new LuaMethod("SlerpUnclamped", SlerpUnclamped),
            new LuaMethod("OrthoNormalize", OrthoNormalize),
            new LuaMethod("MoveTowards", MoveTowards),
            new LuaMethod("RotateTowards", RotateTowards),
            new LuaMethod("SmoothDamp", SmoothDamp),
            new LuaMethod("Set", Set),
            new LuaMethod("Scale", Scale),
            new LuaMethod("Cross", Cross),
            new LuaMethod("Reflect", Reflect),
            new LuaMethod("Normalize", Normalize),
            new LuaMethod("Dot", Dot),
            new LuaMethod("Project", Project),
            new LuaMethod("ProjectOnPlane", ProjectOnPlane),
            new LuaMethod("Angle", Angle),
            new LuaMethod("Distance", Distance),
            new LuaMethod("ClampMagnitude", ClampMagnitude),
            new LuaMethod("Magnitude", Magnitude),
            new LuaMethod("SqrMagnitude", SqrMagnitude),
            new LuaMethod("Min", Min),
            new LuaMethod("Max", Max),
            new LuaMethod("new", _CreateVector3),
            new LuaMethod("GetType", GetClassType),
            new LuaMethod("__add", Lua_Add),
            new LuaMethod("__sub", Lua_Sub),
            new LuaMethod("__mul", Lua_Mul),
            new LuaMethod("__div", Lua_Div),
            new LuaMethod("__unm", Lua_Neg),
            new LuaMethod("__eq", Lua_Eq),
        },
        new LuaField[] {
            new LuaField("kEpsilon", get_kEpsilon, null),
            new LuaField("normalized", get_normalized, null),
            new LuaField("magnitude", get_magnitude, null),
            new LuaField("sqrMagnitude", get_sqrMagnitude, null),
            new LuaField("zero", get_zero, null),
            new LuaField("one", get_one, null),
            new LuaField("forward", get_forward, null),
            new LuaField("back", get_back, null),
            new LuaField("up", get_up, null),
            new LuaField("down", get_down, null),
            new LuaField("left", get_left, null),
            new LuaField("right", get_right, null),
        });

        LuaDLL.tolua_setindex(L);
        LuaDLL.tolua_setnewindex(L);
        L.SetMetaTable(-2);
        L.Pop(1);
    }
    
    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int __tostring(ILuaState L)
    {
        L.PushString(L.ToVector3(1).ToString());
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int _CreateVector3(ILuaState L)
    {
        int count = L.GetTop();

        if (count == 2) {
            var arg0 = (float)L.ChkNumber(1);
            var arg1 = (float)L.ChkNumber(2);
            Vector3 obj = new Vector3(arg0, arg1);
            L.PushUData(obj);
            return 1;
        } else if (count == 3) {
            var arg0 = (float)L.ChkNumber(1);
            var arg1 = (float)L.ChkNumber(2);
            var arg2 = (float)L.ChkNumber(3);
            Vector3 obj = new Vector3(arg0, arg1, arg2);
            L.PushUData(obj);
            return 1;
        } else if (count == 0) {
            Vector3 obj = new Vector3();
            L.PushUData(obj);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Vector3.New");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetClassType(ILuaState L)
    {
        int count = L.GetTop();
        if (count == 0) {
            L.PushLightUserData(typeof(Vector3));
            return 1;
        } else {
            return MetaMethods.GetType(L);
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_kEpsilon(ILuaState L)
    {
        L.PushNumber(Vector3.kEpsilon);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_normalized(ILuaState L)
    {
        L.PushUData(L.ToVector3(1).normalized);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_magnitude(ILuaState L)
    {
        L.PushNumber(L.ToVector3(1).magnitude);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_sqrMagnitude(ILuaState L)
    {
        L.PushNumber(L.ToVector3(1).sqrMagnitude);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_zero(ILuaState L)
    {
        L.PushUData(Vector3.zero);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_one(ILuaState L)
    {
        L.PushUData(Vector3.one);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_forward(ILuaState L)
    {
        L.PushUData(Vector3.forward);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_back(ILuaState L)
    {
        L.PushUData(Vector3.back);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_up(ILuaState L)
    {
        L.PushUData(Vector3.up);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_down(ILuaState L)
    {
        L.PushUData(Vector3.down);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_left(ILuaState L)
    {
        L.PushUData(Vector3.left);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_right(ILuaState L)
    {
        L.PushUData(Vector3.right);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lerp(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector3 o = Vector3.Lerp(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LerpUnclamped(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector3 o = Vector3.LerpUnclamped(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Slerp(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector3 o = Vector3.Slerp(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SlerpUnclamped(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector3 o = Vector3.SlerpUnclamped(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int OrthoNormalize(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 2) {
            Vector3 arg0 = L.ToVector3(1);
            Vector3 arg1 = L.ToVector3(2);
            Vector3.OrthoNormalize(ref arg0, ref arg1);
            L.PushUData(arg0);
            L.PushUData(arg1);
            return 2;
        } else if (count == 3) {
            Vector3 arg0 = L.ToVector3(1);
            Vector3 arg1 = L.ToVector3(2);
            Vector3 arg2 = L.ToVector3(3);
            Vector3.OrthoNormalize(ref arg0, ref arg1, ref arg2);
            L.PushUData(arg0);
            L.PushUData(arg1);
            L.PushUData(arg2);
            return 3;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Vector3.OrthoNormalize");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int MoveTowards(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector3 o = Vector3.MoveTowards(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int RotateTowards(ILuaState L)
    {
        L.ChkArgsCount(4);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        var arg2 = (float)L.ChkNumber(3);
        var arg3 = (float)L.ChkNumber(4);
        Vector3 o = Vector3.RotateTowards(arg0, arg1, arg2, arg3);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SmoothDamp(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 4) {
            Vector3 arg0 = L.ToVector3(1);
            Vector3 arg1 = L.ToVector3(2);
            Vector3 arg2 = L.ToVector3(3);
            var arg3 = (float)L.ChkNumber(4);
            Vector3 o = Vector3.SmoothDamp(arg0, arg1, ref arg2, arg3);
            L.PushUData(o);
            L.PushUData(arg2);
            return 2;
        } else if (count == 5) {
            Vector3 arg0 = L.ToVector3(1);
            Vector3 arg1 = L.ToVector3(2);
            Vector3 arg2 = L.ToVector3(3);
            var arg3 = (float)L.ChkNumber(4);
            var arg4 = (float)L.ChkNumber(5);
            Vector3 o = Vector3.SmoothDamp(arg0, arg1, ref arg2, arg3, arg4);
            L.PushUData(o);
            L.PushUData(arg2);
            return 2;
        } else if (count == 6) {
            Vector3 arg0 = L.ToVector3(1);
            Vector3 arg1 = L.ToVector3(2);
            Vector3 arg2 = L.ToVector3(3);
            var arg3 = (float)L.ChkNumber(4);
            var arg4 = (float)L.ChkNumber(5);
            var arg5 = (float)L.ChkNumber(6);
            Vector3 o = Vector3.SmoothDamp(arg0, arg1, ref arg2, arg3, arg4, arg5);
            L.PushUData(o);
            L.PushUData(arg2);
            return 2;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Vector3.SmoothDamp");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Set(ILuaState L)
    {
        L.ChkArgsCount(4);
        SetVector3(L, 1, L.ToSingle(2), L.ToSingle(3), L.ToSingle(4));
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Scale(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 obj = L.ToVector3(1);
        Vector3 arg0 = L.ToVector3(2);
        obj.Scale(arg0);
        SetVector3(L, 1, obj.x, obj.y, obj.z);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Cross(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = Vector3.Cross(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Reflect(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = Vector3.Reflect(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Normalize(ILuaState L)
    {
        L.ChkArgsCount(1);
        Vector3 obj = (Vector3)L.ChkUserDataSelf(1, "Vector3");
        obj.Normalize();
        SetVector3(L, 1, obj.x, obj.y, obj.z);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Dot(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        float o = Vector3.Dot(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Project(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = Vector3.Project(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ProjectOnPlane(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = Vector3.ProjectOnPlane(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Angle(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        float o = Vector3.Angle(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Distance(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        float o = Vector3.Distance(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ClampMagnitude(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        var arg1 = (float)L.ChkNumber(2);
        Vector3 o = Vector3.ClampMagnitude(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Magnitude(ILuaState L)
    {
        L.ChkArgsCount(1);
        Vector3 arg0 = L.ToVector3(1);
        float o = Vector3.Magnitude(arg0);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SqrMagnitude(ILuaState L)
    {
        L.ChkArgsCount(1);
        Vector3 arg0 = L.ToVector3(1);
        float o = Vector3.SqrMagnitude(arg0);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Min(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = Vector3.Min(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Max(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = Vector3.Max(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Add(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = arg0 + arg1;
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Sub(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        Vector3 o = arg0 - arg1;
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Neg(ILuaState L)
    {
        L.ChkArgsCount(1);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 o = -arg0;
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Mul(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 2 && L.CheckTypes(1, typeof(float), typeof(LuaTable))) {
            var arg0 = (float)L.ToNumber(1);
            Vector3 arg1 = L.ToVector3(2);
            Vector3 o = arg0 * arg1;
            L.PushUData(o);
            return 1;
        } else if (count == 2 && L.CheckTypes(1, typeof(LuaTable), typeof(float))) {
            Vector3 arg0 = L.ToVector3(1);
            var arg1 = (float)L.ToNumber(2);
            Vector3 o = arg0 * arg1;
            L.PushUData(o);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Vector3.op_Multiply");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Div(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        var arg1 = (float)L.ChkNumber(2);
        Vector3 o = arg0 / arg1;
        L.PushLightUserData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Eq(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector3 arg0 = L.ToVector3(1);
        Vector3 arg1 = L.ToVector3(2);
        bool o = arg0 == arg1;
        L.PushBoolean(o);
        return 1;
    }
}
//}

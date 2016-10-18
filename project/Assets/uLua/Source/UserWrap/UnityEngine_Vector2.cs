using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

//namespace ToLua.Wrap
//{
public static class UnityEngine_Vector2
{
    public const string CLASS = "UnityEngine.Vector2";

    public static void PushUData(this ILuaState self, Vector2 value)
    {
        self.CreateTable(0, 2);
        self.SetDict("x", value.x);
        self.SetDict("y", value.y);
        self.L_GetMetaTable(CLASS);
        self.SetMetaTable(-2);
    }

    public static Vector2 ToVector2(this ILuaState self, int index)
    {
        float x = self.GetFloatValue(index, "x");
        float y = self.GetFloatValue(index, "y");
        return new Vector2(x, y);
    }

    private static void SetVector2(ILuaState L, int index, float x, float y)
    {
        L.SetFloatValue(index, "x", x);
        L.SetFloatValue(index, "y", y);
    }

    public static void Wrap(ILuaState L)
    {
        L.RegistType(typeof(Vector2));
        L.L_NewMetaTable(CLASS);
        
        // 元表的元表是System.Object的元表：继承自System.Object
        L.L_GetMetaTable("System.Object");
        L.SetMetaTable(-2);

        L.SetDict("class", CLASS);
        L.SetDict("__tostring", new LuaCSFunction(__tostring));
        L.SetRegistryIndex("__call", MetaMethods.ToLua_TableCall);

        L.RegistMembers(new LuaMethod[] {
            new LuaMethod("Set", Set),
            new LuaMethod("Lerp", Lerp),
            new LuaMethod("LerpUnclamped", LerpUnclamped),
            new LuaMethod("MoveTowards", MoveTowards),
            new LuaMethod("Scale", Scale),
            new LuaMethod("Normalize", Normalize),
            new LuaMethod("ToString", ToString),
            new LuaMethod("Reflect", Reflect),
            new LuaMethod("Dot", Dot),
            new LuaMethod("Angle", Angle),
            new LuaMethod("Distance", Distance),
            new LuaMethod("ClampMagnitude", ClampMagnitude),
            new LuaMethod("SqrMagnitude", SqrMagnitude),
            new LuaMethod("Min", Min),
            new LuaMethod("Max", Max),
            new LuaMethod("SmoothDamp", SmoothDamp),
            new LuaMethod("new", _CreateVector2),
            new LuaMethod("GetType", GetClassType),
            new LuaMethod("__add", Lua_Add),
            new LuaMethod("__sub", Lua_Sub),
            new LuaMethod("__mul", Lua_Mul),
            new LuaMethod("__div", Lua_Div),
            new LuaMethod("__unm", Lua_Neg),
        },
        new LuaField[] {
            new LuaField("kEpsilon", get_kEpsilon, null),
            new LuaField("normalized", get_normalized, null),
            new LuaField("magnitude", get_magnitude, null),
            new LuaField("sqrMagnitude", get_sqrMagnitude, null),
            new LuaField("zero", get_zero, null),
            new LuaField("one", get_one, null),
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
        L.PushString(L.ToVector2(1).ToString());
        return 1;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int _CreateVector2(ILuaState L)
    {
        float x = L.ToSingle(1);
        float y = L.ToSingle(2);
        L.PushUData(new Vector2(x, y));
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GetClassType(ILuaState L)
    {
        int count = L.GetTop();
        if (count == 0) {
            L.PushLightUserData(typeof(Vector2));
            return 1;
        } else {
            return MetaMethods.GetType(L);
        }
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_kEpsilon(ILuaState L)
    {
        L.PushNumber(Vector2.kEpsilon);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_normalized(ILuaState L)
    {
        L.PushUData(L.ToVector2(1).normalized);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_magnitude(ILuaState L)
    {
        L.PushNumber(L.ToVector2(1).magnitude);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_sqrMagnitude(ILuaState L)
    {
        L.PushNumber(L.ToVector2(1).sqrMagnitude);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_zero(ILuaState L)
    {
        L.PushUData(Vector2.zero);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_one(ILuaState L)
    {
        L.PushUData(Vector2.one);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_up(ILuaState L)
    {
        L.PushUData(Vector2.up);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_down(ILuaState L)
    {
        L.PushUData(Vector2.down);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_left(ILuaState L)
    {
        L.PushUData(Vector2.left);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int get_right(ILuaState L)
    {
        L.PushUData(Vector2.right);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Set(ILuaState L)
    {
        L.ChkArgsCount(3);
        SetVector2(L, 1, L.ToSingle(2), L.ToSingle(3));
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lerp(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector2 o = Vector2.Lerp(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LerpUnclamped(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector2 o = Vector2.LerpUnclamped(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int MoveTowards(ILuaState L)
    {
        L.ChkArgsCount(3);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        var arg2 = (float)L.ChkNumber(3);
        Vector2 o = Vector2.MoveTowards(arg0, arg1, arg2);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Scale(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 obj = L.ToVector2(2);
        Vector2 arg0 = L.ToVector2(2);
        obj.Scale(arg0);
        SetVector2(L, 1, obj.x, obj.y);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Normalize(ILuaState L)
    {
        L.ChkArgsCount(1);
        Vector2 obj = L.ToVector2(1);
        obj.Normalize();
        SetVector2(L, 1, obj.x, obj.y);
        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ToString(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 1) {
            Vector2 obj = L.ToVector2(2);
            string o = obj.ToString();
            L.PushString(o);
            return 1;
        } else if (count == 2) {
            Vector2 obj = L.ToVector2(2);
            var arg0 = L.ToLuaString(2);
            string o = obj.ToString(arg0);
            L.PushString(o);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Vector2.ToString");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Reflect(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        Vector2 o = Vector2.Reflect(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Dot(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        float o = Vector2.Dot(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Angle(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        float o = Vector2.Angle(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Distance(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        float o = Vector2.Distance(arg0, arg1);
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int ClampMagnitude(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        var arg1 = (float)L.ChkNumber(2);
        Vector2 o = Vector2.ClampMagnitude(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SqrMagnitude(ILuaState L)
    {
        L.ChkArgsCount(1);
        Vector2 obj = L.ToVector2(1);
        float o = obj.SqrMagnitude();
        L.PushNumber(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Min(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        Vector2 o = Vector2.Min(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Max(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        Vector2 o = Vector2.Max(arg0, arg1);
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int SmoothDamp(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 4) {
            Vector2 arg0 = L.ToVector2(1);
            Vector2 arg1 = L.ToVector2(2);
            Vector2 arg2 = L.ToVector2(3);
            var arg3 = (float)L.ChkNumber(4);
            Vector2 o = Vector2.SmoothDamp(arg0, arg1, ref arg2, arg3);
            L.PushUData(o);
            L.PushUData(arg2);
            return 2;
        } else if (count == 5) {
            Vector2 arg0 = L.ToVector2(1);
            Vector2 arg1 = L.ToVector2(2);
            Vector2 arg2 = L.ToVector2(3);
            var arg3 = (float)L.ChkNumber(4);
            var arg4 = (float)L.ChkNumber(5);
            Vector2 o = Vector2.SmoothDamp(arg0, arg1, ref arg2, arg3, arg4);
            L.PushUData(o);
            L.PushUData(arg2);
            return 2;
        } else if (count == 6) {
            Vector2 arg0 = L.ToVector2(1);
            Vector2 arg1 = L.ToVector2(2);
            Vector2 arg2 = L.ToVector2(3);
            var arg3 = (float)L.ChkNumber(4);
            var arg4 = (float)L.ChkNumber(5);
            var arg5 = (float)L.ChkNumber(6);
            Vector2 o = Vector2.SmoothDamp(arg0, arg1, ref arg2, arg3, arg4, arg5);
            L.PushUData(o);
            L.PushUData(arg2);
            return 2;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Vector2.SmoothDamp");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Add(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        Vector2 o = arg0 + arg1;
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Sub(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        Vector2 o = arg0 - arg1;
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Neg(ILuaState L)
    {
        L.ChkArgsCount(1);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 o = -arg0;
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Mul(ILuaState L)
    {
        int count = LuaDLL.lua_gettop(L);

        if (count == 2 && L.CheckTypes(1, typeof(float), typeof(LuaTable))) {
            var arg0 = (float)L.ToNumber(1);
            Vector2 arg1 = L.ToVector2(2);
            Vector2 o = arg0 * arg1;
            L.PushUData(o);
            return 1;
        } else if (count == 2 && L.CheckTypes(1, typeof(LuaTable), typeof(float))) {
            Vector2 arg0 = L.ToVector2(1);
            var arg1 = (float)L.ToNumber(2);
            Vector2 o = arg0 * arg1;
            L.PushUData(o);
            return 1;
        } else {
            LuaDLL.luaL_error(L, "invalid arguments to method: Vector2.op_Multiply");
        }

        return 0;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Div(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        var arg1 = (float)L.ChkNumber(2);
        Vector2 o = arg0 / arg1;
        L.PushUData(o);
        return 1;
    }

    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int Lua_Eq(ILuaState L)
    {
        L.ChkArgsCount(2);
        Vector2 arg0 = L.ToVector2(1);
        Vector2 arg1 = L.ToVector2(2);
        bool o = arg0 == arg1;
        L.PushBoolean(o);
        return 1;
    }
}
//}

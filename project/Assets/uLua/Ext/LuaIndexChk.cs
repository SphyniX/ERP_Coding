using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

public static class LuaIndexChk 
{
    public static double ChkNumber(this ILuaState self, int index)
    {
        if (self.IsNumber(index)) {
            return self.ToNumber(index);
        }
        self.TypeError(index, "number");
        return default(double);
    }

    public static string ChkString(this ILuaState self, int index)
    {
        if (self.IsString(index)) {
            return self.ToString(index);
        }
        self.TypeError(index, "string");
        return default(string);
    }

    public static string ChkLuaString(this ILuaState self, int index)
    {
        var ret = self.ToLuaString(index);
        if (ret == null) {
            self.TypeError(index, "string");
        }

        return ret;
    }

    public static bool ChkBoolean(this ILuaState self, int index)
    {
        if (self.IsBoolean(index)) {
            return self.ToBoolean(index);
        } else {
            return !self.IsNoneOrNil(index);
        }
    }

    public static int ChkInteger(this ILuaState self, int index)
    {
        if (self.IsNumber(index)) {
            return self.ToInteger(index);
        }
        self.TypeError(index, "int");
        return default(int);
    }

    public static long ChkLong(this ILuaState self, int index)
    {
        return self.ToLong(index);
    }

    public static ulong ChkULong(this ILuaState self, int index)
    {
        return self.ToULong(index);
    }

    public static LuaTable ChkLuaTable(this ILuaState self, int index)
    {
        if (self.IsTable(index)) {
            return self.ToLuaTable(index);
        }
        self.TypeError(index, "table");
        return default(LuaTable);
    }

    public static LuaFunction ChkLuaFunction(this ILuaState self, int index)
    {
        if (self.IsFunction(index)) {
            return self.ToLuaFunction(index);
        }
        self.TypeError(index, "function");
        return default(LuaFunction);
    }

    public static object ChkEnumValue(this ILuaState self, int index, System.Type type)
    {
        var obj = self.ToEnumValue(index, type);
        if (!System.Enum.IsDefined(type, obj)) {
            self.L_Error(string.Format("{0} expected, got {1}", type, obj));
        }
        return obj;
    }

    public static LuaStringBuffer ChkStringBuffer(ILuaState self, int index)
    {
        LuaTypes luatype = LuaDLL.lua_type(self, index);

        if (luatype == LuaTypes.LUA_TNIL) {
            return null;
        } else if (luatype != LuaTypes.LUA_TSTRING) {
            self.TypeError(index, "string");
            return null;
        }

        int len = 0;
        var buffer = self.ToLString(index, out len);
        return new LuaStringBuffer(buffer, (int)len);
    }

    public static object ChkUserData(this ILuaState self, int index, System.Type type)
    {
        if (self.IsNil(index)) return null;

        var luaT = self.Type(index);
        if (luaT != LuaTypes.LUA_TUSERDATA && luaT != LuaTypes.LUA_TLIGHTUSERDATA) {
            self.L_ArgError(index, string.Format("{0} expected, got {1}", type.FullName, luaT));
            return null;
        }

        object obj = self.ToUserData(index);

        if (obj == null) {
            self.L_ArgError(index, string.Format("{0} expected, got nil", type.FullName));
            return null;
        }

        System.Type objType = obj.GetType();

        if (type == objType || type.IsAssignableFrom(objType)) {
            return obj;
        }

        self.L_ArgError(index, string.Format("{0} expected, got {1}", type.FullName, objType.Name));
        return null;
    }

    /// <summary>
    /// 访问System.Object的成员时，检查自身是否有效
    /// </summary>
    public static object ChkUserDataSelf(this ILuaState self, int index, string typeName)
    {
        object obj = self.ToUserData(index);
        if (obj == null) self.L_ArgError(index, string.Format("{0} expected, got nil", typeName));
        return obj;
    }

    public static Object ChkUnityObject(this ILuaState self, int index, System.Type type)
    {
        return self.ChkOtherObject<Object>(index, type);
    }

    /// <summary>
    /// 访问UnityEngine.Object的成员时，检查自身是否有效
    /// </summary>
    public static Object ChkUnityObjectSelf(this ILuaState self, int index, string typeName)
    {
        Object uObj = self.ToUserData(index) as Object;
        if (uObj == null) self.L_ArgError(index, string.Format("{0} expected, got nil", typeName));
        return uObj;
    }

    public static TrackedReference ChkTrackedObject(this ILuaState self, int index, System.Type type)
    {
        return self.ChkOtherObject<TrackedReference>(index, type);
    }

    /// <summary>
    /// 访问TrackedReference的成员时，检查自身是否有效
    /// </summary>
    public static object ChkTrackedObjectSelf(this ILuaState self, int index, string typeName)
    {
        TrackedReference tObj = self.ToUserData(index) as TrackedReference;
        if (tObj == null) self.L_ArgError(index, string.Format("{0} expected, got nil", typeName));
        return tObj;
    }

    public static System.Type ChkTypeObject(this ILuaState self, int index)
    {
        var obj = self.ToUserData(index);
        var monoType = typeof(System.Type).GetType();

        if (obj == null || obj.GetType() != monoType) {
            self.L_ArgError(index, string.Format("Type expected, got {0}", obj == null ? "nil" : obj.GetType().Name));
        }

        return (System.Type)obj;
    }

    public static object ChkAnyObject(this ILuaState self, int index, System.Type type)
    {
        object ret = null;
        if (type.IsEnum) {
            return self.ChkEnumValue(index, type);
        }

        if (type.IsSubclassOf(typeof(System.Delegate))) {
            // TODO
            return null;
        }

        switch (type.FullName) {
            case "System.Byte": ret = (byte)self.ToInteger(index); break;
            case "System.SByte": ret = (sbyte)self.ToInteger(index); break;
            case "System.Int16": ret = (short)self.ToInteger(index); break;
            case "System.UInt16": ret = (ushort)self.ToInteger(index); break;
            case "System.Int32": ret = self.ToInteger(index); break;
            case "System.UInt32": ret = (uint)self.ToInteger(index); break;
            case "System.Int64": ret = long.Parse(self.ToString(index)); break;
            case "System.UInt64": ret = ulong.Parse(self.ToString(index)); break;
            case "UnityEngine.Vector2" : ret = self.ToVector2(index); break;
            case "UnityEngine.Vector3" : ret = self.ToVector3(index); break;
            case "UnityEngine.Quaternion" : ret = self.ToQuaternion(index); break;
            case "UnityEngine.Color" : ret = self.ToColor(index); break;
            case "TinyJSON.Variant": ret = self.ToJsonObj(index); break;
            default: ret = self.ChkUserData(index, type); break;
        }
        return ret;
    }

    public static T ChkAnyObject<T>(this ILuaState self, int index)
    {
        return (T)self.ChkAnyObject(index, typeof(T));
    }
}

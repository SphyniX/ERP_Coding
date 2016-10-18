using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

public static class LuaIndexOpt
{
    public static T Opt<T>(this ILuaState self, System.Func<ILuaState, int, T> To, int index, T def)
    {
        return self.IsNoneOrNil(index) ? def : To(self, index);
    }

    public static double OptNumber(this ILuaState self, int index, double def)
    {
        return self.IsNoneOrNil(index) ? def : self.ToNumber(index);
    }

    public static float OptSingle(this ILuaState self, int index, float def)
    {
        return self.IsNoneOrNil(index) ? def : self.ToSingle(index);
    }

    public static string OptString(this ILuaState self, int index, string def)
    {
        return self.IsNoneOrNil(index) ? def : self.ToString(index);
    }

    public static bool OptBoolean(this ILuaState self, int index, bool def)
    {
        return self.IsNoneOrNil(index) ? def : self.ToBoolean(index);
    }

    public static int OptInteger(this ILuaState self, int index, int def)
    {
        return self.IsNoneOrNil(index) ? def : self.ToInteger(index);
    }

    public static long OptLong(this ILuaState self, int index, long def)
    {
        return self.IsNoneOrNil(index) ? def : self.ToLong(index);
    }

    public static object OptEnumValue(this ILuaState self, int index, System.Type type, System.Enum def)
    {
        return self.IsNoneOrNil(index) ? def : self.ToEnumValue(index, type);
    }

    public static object OptUserData(this ILuaState self, int index, System.Type type, System.Enum def)
    {
        return self.IsNoneOrNil(index) ? def : self.ChkUserData(index, type);
    }
}

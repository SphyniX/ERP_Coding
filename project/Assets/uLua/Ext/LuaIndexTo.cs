using UnityEngine;
using System.Collections;
using LuaInterface;
using TinyJSON;
using ILuaState = System.IntPtr;

public static class LuaIndexTo
{

    /// <summary>
    /// 把指定索引的值转成单精度浮点数
    /// </summary>
    /// <returns>The single.</returns>
    /// <param name="self">Self.</param>
    /// <param name="index">Index.</param>
    public static float ToSingle(this ILuaState self, int index)
    {
        return (float)self.ToNumber(index);
    }

    /// <summary>
    /// 把指定索引的值转成长整型，由于Lua没有长整型，这里统一用字符串来传递
    /// </summary>
    public static long ToLong(this ILuaState self, int index)
    {
        var luaT = self.Type(index);
        if (luaT == LuaTypes.LUA_TNUMBER) return self.ToInteger(index);

        return long.Parse(self.ToString(index));
    }

    public static ulong ToULong(this ILuaState self, int index)
    {
        var luaT = self.Type(index);
        if (luaT == LuaTypes.LUA_TNUMBER) return (ulong)self.ToInteger(index);

        return ulong.Parse(self.ToString(index));
    }

    /// <summary>
    /// 把指定索引的值强制转成字符串
    /// </summary>
    public static string ToLuaString(this ILuaState self, int index)
    {
        LuaTypes luaT = self.Type(index);

        switch (luaT) {
        case LuaTypes.LUA_TSTRING:
            return self.ToString (index);
        case LuaTypes.LUA_TNIL:
            return string.Empty;
        case LuaTypes.LUA_TNUMBER:
            return self.ToNumber(index).ToString();
        case LuaTypes.LUA_TBOOLEAN:
            return self.ToBoolean(index).ToString();
        case LuaTypes.LUA_TNONE:
            return null;
        default:
            self.GetGlobal("tostring");
            self.PushValue(index);
            self.Call(1, 1);
            var ret = self.ToString(-1);
            self.Pop(1);
            return ret;
        }
    }

    /// <summary>
    /// 把指定索引的值转成LuaTable类型
    /// </summary>
    public static LuaTable ToLuaTable(this ILuaState self, int index)
    {
        if (self.IsTable(index)) {
            self.PushValue(index);
            return new LuaTable(self.L_Ref(LuaIndexes.LUA_REGISTRYINDEX), LuaEnv.Get(self).ls);
        }
        return null;
    }

    /// <summary>
    /// 把指定索引的值转成LuaFunction类型
    /// </summary>
    public static LuaFunction ToLuaFunction(this ILuaState self, int index)
    {
        if (self.IsFunction(index)) {
            self.PushValue(index);
            return new LuaFunction(self.L_Ref(LuaIndexes.LUA_REGISTRYINDEX), LuaEnv.Get(self).ls);
        }
        return null;
    }


    /// <summary>
    /// 把指定索引的值转成一个UnityEngine.Object
    /// </summary>
    public static Object ToUnityObject(this ILuaState self, int index)
    {
        var luaT = self.Type(index);
        switch (luaT) {
            case LuaTypes.LUA_TSTRING: return self.ToGameObject(index);
            case LuaTypes.LUA_TUSERDATA: return self.ToUserData(index) as Object;
            default: break;
        }

        return null;
    }

    /// <summary>
    /// 把指定索引的值转成一个UnityEngine.GameObject
    /// 如果索引的值是一个字符串，将调用GameObject.Find来尝试找到一个GameObject
    /// </summary>
    public static GameObject ToGameObject(this ILuaState self, int index)
    {
        var luaT = self.Type(index);
        if (luaT == LuaTypes.LUA_TSTRING) {
            return GameObject.Find(self.ToString(index));
        }
        var obj = self.ToUnityObject(index);
        var go = obj as GameObject;
        if (go) {
            return go;
        } else {
            var com = obj as Component;
            if (com) return com.gameObject;
        }

        return null;
    }

    /// <summary>
    /// 把指定索引的值转成指定类型的UnityEngine.Component
    /// 如果类型不匹配，尝试获取其相关的GameObject，
    /// 再从GameoObject上获取其挂载的Component
    /// </summary>
    public static Component ToComponent(this ILuaState self, int index, System.Type type)
    {
        var obj = self.ToAnyObject(index);
        if (obj == null) return null;

        if (type.IsAssignableFrom(obj.GetType())) {
            return obj as Component;
        }

        var com = obj as Component;
        if (com) return com.gameObject.GetComponent(type);

        var go = obj as GameObject;
        if (go == null) go = self.ToGameObject(index);
        if (go) return go.GetComponent(type);

        return null;
    }
    /// <summary>
    /// 把指定索引的值转成指定类型的UnityEngine.Component。泛型版本
    /// </summary>
    public static T ToComponent<T>(this ILuaState self, int index) where T : Component
    {
        return self.ToComponent(index, typeof(T)) as T;
    }

    public static object ToYieldValue(this ILuaState self, int index)
    {
        var luaT = self.Type(index);
        switch (luaT) {
        case LuaTypes.LUA_TNIL:
        case LuaTypes.LUA_TNONE:
            return null;
        case LuaTypes.LUA_TNUMBER:
            return new WaitForSeconds(self.ToSingle(index));
        default:
            return self.ToAnyObject(3);
        }
    }

    /// <summary>
    /// 把栈顶的数值转为一个Variant表示的值
    /// </summary>
    public static Variant ToJsonValue(ILuaState L)
    {
        var luaT = L.Type(-1);
        Variant ret = null;
        switch (luaT) {
        case LuaTypes.LUA_TTABLE: ret = L.ToJsonObj(); break;
        case LuaTypes.LUA_TBOOLEAN: ret = new ProxyBoolean(L.ToBoolean(-1)); break;
        case LuaTypes.LUA_TSTRING: ret = new ProxyString(L.ToString(-1)); break;
        case LuaTypes.LUA_TNUMBER: ret = new ProxyNumber(L.ToNumber(-1)); break;
        case LuaTypes.LUA_TFUNCTION: ret = new ProxyUserdata(L.ToLuaFunction(-1)); break;
        case LuaTypes.LUA_TLIGHTUSERDATA:
        case LuaTypes.LUA_TUSERDATA: ret = new ProxyUserdata(L.ToUserData(-1)); break;
        case LuaTypes.LUA_TNIL:
        case LuaTypes.LUA_TNONE: break;

        default:
            LogMgr.W("ToJsonValue: Unsupport type {0}", luaT);
            break;
        }
        return ret;
    }

    /// <summary>
    /// 把栈顶的表转为一个ProxyObject或ProxyArray
    /// 转为ProxyArray时，其下标可能和Lua表不一致
    /// </summary>
    public static Variant ToJsonObj(this ILuaState self)
    {
        Variant ret = null;
        var top = self.GetTop();
        self.PushNil();
        if (self.Next(top)) {
            var key = self.ToAnyObject(-2) as string;
            if (key != null) {
                var obj = new ProxyObject();
                ret = obj;
                var value = ToJsonValue(self);
                obj[key] = value;
                self.Pop(1);
                while (self.Next(top)) {
                    key = self.ToString(-2);
                    value = ToJsonValue(self);
                    obj[key] = value;
                    self.Pop(1);
                }
            } else {
                var array = new ProxyArray();
                ret = array;
                array.Add(ToJsonValue(self));
                self.Pop(1);
                while (self.Next(top)) {
                    array.Add(ToJsonValue(self));
                    self.Pop(1);
                }
            }
        } else {
            return new ProxyArray();
        }
        return ret;
    }

    /// <summary>
    /// 把指定索引的Lua表转为一个TinyJson类
    /// </summary>
    public static Variant ToJsonObj(this ILuaState self, int index)
    {
        Variant ret = null;
        if (self.IsTable(index)) {
            self.PushValue(index);
            ret = self.ToJsonObj();
            self.Pop(1);
        }
        return ret;
    }


    /// <summary>
    /// Unity的结构体(Vector2等），也以Lua表形式保存，所以这里要判断一下
    /// </summary>
    public static object ToAnyTable(this ILuaState self, int index)
    {
        if (self.GetMetaTable(index)) {
            self.PushString("class");
            self.RawGet(-2);

            if (!self.IsNil(-1)) {
                var klass = self.ToLuaString(-1);
                self.Pop(2);

                switch (klass) {
                    case UnityEngine_Vector2.CLASS: return self.ToVector2(index);
                    case UnityEngine_Vector3.CLASS: return self.ToVector3(index);
                    case UnityEngine_Quaternion.CLASS: return self.ToQuaternion(index);
                    case UnityEngine_Color.CLASS: return self.ToColor(index);
                    case System_Enum.CLASS: return self.ToEnumValue(index, null);
                    default: break;
                }
            } else {
                self.Pop(2);
            }
        }

        return self.ToLuaTable(index);
    }

    /// <summary>
    /// 将指定栈位置的数据转为它确切的类型
    /// 即：自动判断其类型，然后做转换；包括Unity内置类型。
    /// </summary>
    public static object ToAnyObject(this ILuaState self, int index)
    {
        var luaT = self.Type(index);
        switch (luaT) {
        case LuaTypes.LUA_TNUMBER:
            return (float)self.ToNumber(index);
        case LuaTypes.LUA_TSTRING:
            return self.ToString(index);
        case LuaTypes.LUA_TUSERDATA:
            {
                var ls = LuaEnv.Get(self).ls;
                int udata = LuaDLL.luanet_rawnetobj(self, index);

                if (udata != -1) {
                    object obj = null;
                    ls.translator.objects.TryGetValue(udata, out obj);
                    return obj;
                } else {
                    return null;
                }
            }
        case LuaTypes.LUA_TBOOLEAN:
            return self.ToBoolean(index);
        case LuaTypes.LUA_TTABLE:
            return self.ToAnyTable(index);
        case LuaTypes.LUA_TFUNCTION:
            return self.ToLuaFunction(index);
        default:
            return null;
        }
    }

    #region 转换多个索引
    
    public static T1[] ToArray<T1, T2>(this ILuaState self, System.Func<ILuaState, int, T2> To, int index)
    {
        LuaTypes luaT = self.Type(index);

        if (luaT == LuaTypes.LUA_TTABLE) {
            var type = typeof(T1);
            int n = self.ObjLen(index);
            var ret = new T1[n];
            for (int i = 0; i < n; ++i) {
                self.RawGetI(index, i + 1);
                ret[i] = (T1)System.Convert.ChangeType(To(self, -1), type);
                self.Pop(1);
            }
            return ret;
        } else if (luaT == LuaTypes.LUA_TUSERDATA) {
            return (T1[])self.ChkUserData(index, typeof(T1[]));
        }

        return null;
    }

    public static bool[] ToArrayBoolean(this ILuaState self, int index)
    {
        return self.ToArray<bool, bool>(LuaAPI.ToBoolean, index);
    }

    public static string[] ToArrayString(this ILuaState self, int index)
    {
        return self.ToArray<string, string>(LuaAPI.ToString, index);
    }

    public static T[] ToArrayNumber<T>(this ILuaState self, int index)
    {
        return self.ToArray<T, double>(LuaAPI.ToNumber, index);
    }

    public static T[] ToArrayObject<T>(this ILuaState self, int index)
    {
        return self.ToArray<T, object>(LuaIndexTo.ToAnyObject, index);
    }

    public static string[] ToParamsString(this ILuaState L, int index, int count)
    {
        var strs = new string[count];
        for (int i = 0; i < strs.Length; ++i) {
            strs[i] = L.ToLuaString(index + i);
        }
        return strs;
    }

    public static object[] ToParamsObject(this ILuaState L, int index, int count)
    {
        var objs = new object[count];
        for (int i = 0; i < objs.Length; ++i) {
            objs[i] = L.ToAnyObject(index + i);
        }
        return objs;
    }

    public static T[] ToParamsObject<T>(this ILuaState L, int index, int count)
    {
        var objs = new T[count];
        for (int i = 0; i < objs.Length; ++i) {
            objs[i] = (T)L.ToAnyObject(index + i);
        }
        return objs;
    }

    public static void ToStringFromatArgs(this ILuaState self, int index, out string format, out object[] args)
    {
        format = self.ChkString(index);
        int n = self.GetTop() - index;
        args = new object[n];
        index += 1;
        for (int i = 0; i < n; ++i) {
            var pos = index + i;
            var luaT = self.Type(pos);
            object arg = null;
            switch (luaT) {
                case LuaTypes.LUA_TBOOLEAN:
                    arg = self.ToBoolean(pos); break;
                case LuaTypes.LUA_TNUMBER:
                    arg = self.ToNumber(pos); break;
                case LuaTypes.LUA_TSTRING:
                    arg = self.ToString(pos); break;
                default:
                    arg = self.ToLuaString(pos); break;
            }
            args[i] = arg;
        }
    }

    #endregion
}

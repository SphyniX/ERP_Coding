using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TinyJSON;
using LuaInterface;
using ILuaState = System.IntPtr;

public static partial class ULUA
{

    #region Ext
    public static void GetGlobal(this ILuaState self, string key1, object key2)
    {
        self.GetGlobal(key1);
        if (self.IsTable(-1)) {
            self.PushAnyObject(key2);
            self.GetTable(-2);
            self.Replace(-2);
        }
    }

    public static void GetGlobal(this ILuaState self, string key1, object key2, object key3)
    {
        self.GetGlobal(key1, key2);
        if (self.IsTable(-1)) {
            self.PushAnyObject(key3);
            self.GetTable(-2);
            self.Replace(-2);
        }
    }

    public static void Get(this ILuaState self, string gKey, params object[] Keys)
    {
        self.GetGlobal(gKey);
        for (int i = 0; i < Keys.Length; ++i) {
            if (!self.IsTable(-1)) return;
            self.PushAnyObject(Keys[i]);
            self.GetTable(-2);
            self.Replace(-2);
        }
    }

    public static T Get<T>(this ILuaState self, string gKey, params object[] Keys)
    {
        self.Get(gKey, Keys);
        T ret = self.ChkAnyObject<T>(-1);
        self.Pop(1);
        return (T)ret;
    }


    public static bool CheckUserData(this ILuaState self, LuaTypes luaType, System.Type t, int pos)
    {
        object obj = self.ToUserData(pos);
        var type = obj.GetType();

        if (t == type) {
            return true;
        }

        var monoType = typeof(System.Type).GetType();
        if (t == typeof(System.Type)) {
            return type == monoType;
        } else {
            return t.IsAssignableFrom(type);            
        }
    }

    public static bool CheckTableType(this ILuaState L, System.Type t, int stackPos)
    {
        if (t.IsArray) {
            return true;
        } else if (t == typeof(LuaTable)) {
            return true;
        }

        return true;
    }

    public static bool CheckType(this ILuaState self, System.Type t, int pos)
    {
        if (t == typeof(object)) {
            return true;
        }

        LuaTypes luaType = LuaDLL.lua_type(self, pos);

        switch (luaType) {
            case LuaTypes.LUA_TNUMBER:
                return t.IsPrimitive;
            case LuaTypes.LUA_TSTRING:
                return t == typeof(string);
            case LuaTypes.LUA_TUSERDATA:
            return self.CheckUserData(luaType, t, pos);
            case LuaTypes.LUA_TBOOLEAN:
                return t == typeof(bool);
            case LuaTypes.LUA_TFUNCTION:
                return t == typeof(LuaFunction);
            case LuaTypes.LUA_TTABLE:
                return self.CheckTableType(t, pos);
            case LuaTypes.LUA_TNIL:
                return t == null;
            default:
                break;
        }

        return false;
    }

    // TODO 为了避免GC，这个应该重载多个参数来代替
    public static bool CheckTypes(this ILuaState self, int begin, params System.Type[] types)
    {
        for (int i = 0; i < types.Length; i++) {            
            if (!CheckType(self, types[i], i + begin)) {
                return false;
            }
        }

        return true;
    }

    public static bool CheckParamsType(this ILuaState self, System.Type t, int begin, int count)
    {
        if (t == typeof(object)) {
            return true;
        }

        for (int i = 0; i < count; i++) {            
            if (!self.CheckType(t, i + begin)) {
                return false;
            }
        }

        return true;
    }


    public static T ChkOtherObject<T>(this ILuaState self, int index, System.Type type) where T : class
    {
        if (self.IsNil(index)) return null;

        object obj = self.ToUserData(index);
        if (obj == null) {
            self.L_ArgError(index, string.Format("{0} expected, got nil", type.Name));
            return null;
        }

        var tObj = obj as T;
        if (tObj == null) {
            self.L_ArgError(index, string.Format("{0} expected, got nil", type.Name));
            return null;
        }

        System.Type objType = tObj.GetType();

        if (type == objType || type.IsAssignableFrom(objType)) {
            return tObj;
        }

        self.L_ArgError(index, string.Format("{0} expected, got {1}", type.Name, objType.Name));
        return null;
    }

    private static string GetErrorFunc(int skip)
    {
        StackFrame sf = null;
        string file = string.Empty;
        var st = new StackTrace(skip, true);
        int pos = 0;

        do {
            sf = st.GetFrame(pos++);
            file = sf.GetFileName();
            file = System.IO.Path.GetFileName(file);
        } while (!file.EndsWith("Wrap.cs"));

        int index1 = file.LastIndexOf('\\');
        int index2 = file.LastIndexOf("Wrap.");
        string className = file.Substring(index1 + 1, index2 - index1 - 1);
        return string.Format("{0}.{1}", className, sf.GetMethod().Name);
    }

    public static void ChkArgsCount(this ILuaState self, int count)
    {
        int c = self.GetTop();

        if (c != count) {
            string str = string.Format("no overload for method '{0}' takes '{1}' arguments", GetErrorFunc(1), c);
            self.L_Error(str);
        }
    }

    /// <summary>
    /// luanet: 值类型发生变化后，要重新映射
    /// </summary>
    public static void SetValue(this ILuaState self, int index, System.ValueType value)
    {
        var ls = LuaEnv.Get(self).ls;
        ls.translator.SetValueObject(self, index, value);
    }

    /// <summary>
    /// 扩展具体类型
    /// </summary>

    public static void PushLong(this ILuaState self, long value)
    {
        self.PushString(value.ToString());
    }
   

    public static void PushULong(this ILuaState self, ulong value)
    {
        self.PushString(value.ToString());
    }

   
    public static Vector4 ToVector4(this ILuaState self, int index)
    {
        return (Vector4)self.ToUserData(index);
    }

    public static Ray ToRay(this ILuaState self, int index)
    {
        return (Ray)self.ToUserData(index);
    }

    public static Bounds ToBounds(this ILuaState self, int index)
    {
        return (Bounds)self.ToUserData(index);
    }

    public static void PushVariant(this ILuaState self, Variant json)
    {
        if (json is ProxyObject) {
            var jsonObject = json as IEnumerable<KeyValuePair<string, Variant>>;
            self.NewTable();
            using (var itor = jsonObject.GetEnumerator()) {
                while (itor.MoveNext()) {
                    var kv = itor.Current;
                    self.SetDict(kv.Key, kv.Value);
                }
            }
        } else if (json is ProxyArray) {
            var array = json as ProxyArray;
            self.NewTable();
            for (int i = 0; i < array.Count; ++i) {
                self.SetArray(i + 1, array[i]);
            }
        } else if (json is ProxyNumber) {
            var value = json as ProxyNumber;
            self.PushNumber((float)value);
        } else if (json is ProxyString) {
            var value = json as ProxyString;
            self.PushString((string)value);
        } else if (json is ProxyBoolean) {
            var value = json as ProxyBoolean;
            self.PushBoolean((bool)value);
        } else if (json is ProxyUserdata) {
            self.PushAnyObject(json.ToType(null, null));
        }
    }

    public static void PushAnyObject(this ILuaState self, object value)
    {
        if (value == null) {
            self.PushNil();
            return;
        }

        var joValue = value as Variant;
        if (joValue != null) {
            self.PushVariant(joValue);
            return;
        }

        var list = value as IList;
        if (list != null) {
            self.PushUData(list);
            return;
        }

        var itor = value as IEnumerator;
        if (itor != null) {
            self.PushUData(itor);
            return;
        }

        var t = value.GetType();

        // 数组
        if (t.IsArray) {
            self.PushUData((System.Array)value);
            return;
        }

        if (t.IsEnum) {
            self.PushUData((System.Enum)value);
            return;
        }

        // 委托(不同于LuaInterface.LuaCSFunction这个签名的)
        //        if (t.IsSubclassOf(typeof(System.Delegate))) {
        //            self.PushUData((System.Delegate)value);
        //            return;
        //        }

        // 根据类型全名
        string typeName = t.FullName;
        switch (typeName) {
            case "System.Boolean": self.PushBoolean((System.Boolean)value); return;
            case "System.Byte": self.PushInteger((System.Byte)value); return;
            case "System.SByte": self.PushInteger((System.SByte)value); return;
            case "System.Int16": self.PushInteger((System.Int16)value); return;
            case "System.UInt16": self.PushInteger((System.UInt16)value); return;
            case "System.Int32": self.PushInteger((System.Int32)value); return;
            case "System.UInt32":
                uint uv = (System.UInt32)value;
                int iv = (System.Int32)uv;
                self.PushInteger(iv);
                //self.PushInteger((System.Int32)value);
                return;
            case "System.Int64": self.PushString(value.ToString()); return;
            case "System.UInt64": self.PushString(value.ToString()); return;
            case "System.Single": self.PushNumber((System.Single)value); return;
            case "System.Double": self.PushNumber((System.Double)value); return;
            case "System.Char": self.PushString(value.ToString()); return;
            case "System.String": self.PushString((System.String)value); return;
            case "UnityEngine.Vector2": self.PushUData((UnityEngine.Vector2)value); return;
            case "UnityEngine.Vector3": self.PushUData((UnityEngine.Vector3)value); return;
            case "UnityEngine.Quaternion": self.PushUData((UnityEngine.Quaternion)value); return;
            case "UnityEngine.Color": self.PushUData((UnityEngine.Color)value); return;
            case "LuaInterface.LuaTable": ((LuaTable)value).push(self); return;
            case "LuaInterface.LuaFunction": ((LuaFunction)value).push(self); return;
            case "LuaInterface.LuaCSFunction": self.PushCSharpFunction((LuaCSFunction)value); return;
            default: break;
        }

        // 其他类型
        self.PushLightUserData(value); 
    }

    public static int RefChunk(this ILuaState self, string chunk)
    {
        self.L_DoString(chunk);
        return self.L_Ref(LuaIndexes.LUA_REGISTRYINDEX);
    }

    public static void RefChunk(this ILuaState self, string key, string chunk)
    {
        self.PushString(key);
        self.L_DoString(chunk);
        self.RawSet(LuaIndexes.LUA_REGISTRYINDEX);
    }
    #endregion


    #region Doing Lua Script
    /// <summary>
    /// 抛出Lua脚本异常
    /// </summary>
    public static void ThrowExceptionFromError(ILuaState lua, int oldTop)
    {
        string err = LuaDLL.lua_tostring(lua, -1);
        LuaDLL.lua_settop(lua, oldTop);

        if (err == null) err = "Unknown Lua Error";
        //LogMgr.E("{0}", err);
        throw new LuaScriptException(err, "");
    }

    public static void DoCall(this ILuaState self, int nResult, params object[] Args)
    {
        var b = self.GetTop();
        self.PushCSharpFunction(LuaStatic.traceback);
        self.Insert(b);

        int nArg = 0;
        if (Args != null) {
            nArg = Args.Length;
            for (int i = 0; i < Args.Length; ++i) {
                self.PushAnyObject(Args[i]);
            }
        }

        if (self.PCall(nArg, nResult, b) == LuaThreadStatus.LUA_OK) {
            self.Remove(b);
        } else {
            ThrowExceptionFromError(self, b - 1);
        }
    }
    
    public static int DoBuffer(this ILuaState self, byte[] nbytes, string name)
    {
        int oldTop = self.GetTop();
        self.PushCSharpFunction(LuaEnv.Get(self).ls.tracebackFunction);
        var b = oldTop + 1;

        if (self.L_LoadBuffer(nbytes, name) == LuaThreadStatus.LUA_OK
            && self.PCall(0, -1, b) == LuaThreadStatus.LUA_OK) {
            self.Remove(b);
            return self.GetTop() - oldTop;
        }

        ThrowExceptionFromError(self, oldTop);
        return 0;
    }

    public static int DoFile(this ILuaState L, string file)
    {
        byte[] nbytes = LuaStatic.Load(file);
        if (nbytes == null) return 0;
        return L.DoBuffer(nbytes, file);
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static int dofile(ILuaState L)
    {
        return L.DoFile(L.ChkString(1));
    }
    
    #endregion

    #region LuaFunction

    public static int Invoke(this LuaFunction self)
    {
        var lua = self.GetLuaState();
        var oldTop = lua.GetTop();
        lua.PushCSharpFunction(LuaStatic.traceback);
        var b = oldTop + 1;
        self.push(lua);

        if (lua.PCall(0, LuaDLL.LUA_MULTRET, b) == LuaThreadStatus.LUA_OK) {
            lua.Remove(b);
            return lua.GetTop() - oldTop;
        } else {
            ThrowExceptionFromError(lua, oldTop);
            return 0;
        }
    }

    public static int Invoke(this LuaFunction self, object arg0)
    {
        var L = self.GetLuaState();
        var oldTop = L.GetTop();
        var b = oldTop + 1;
        L.PushCSharpFunction(LuaStatic.traceback);
        self.push(L);

        L.PushAnyObject(arg0);

        if (L.PCall(1, LuaDLL.LUA_MULTRET, b) == LuaThreadStatus.LUA_OK) {
            L.Remove(b);
            return L.GetTop() - oldTop;
        } else {
            ThrowExceptionFromError(L, oldTop);
            return 0;
        }
    }

    public static int Invoke(this LuaFunction self, object arg0, object arg1)
    {
        var L = self.GetLuaState();
        var oldTop = L.GetTop();
        var b = oldTop + 1;
        L.PushCSharpFunction(LuaStatic.traceback);        
        self.push(L);

        L.PushAnyObject(arg0);
        L.PushAnyObject(arg1);

        if (L.PCall(2, LuaDLL.LUA_MULTRET, b) == LuaThreadStatus.LUA_OK) {
            L.Remove(b);
            return L.GetTop() - oldTop;
        } else {
            ThrowExceptionFromError(L, oldTop);
            return 0;
        }
    }

    public static int Invoke(this LuaFunction self, object arg0, object arg1, object arg2)
    {
        var L = self.GetLuaState();
        var oldTop = L.GetTop();
        var b = oldTop + 1;
        L.PushCSharpFunction(LuaStatic.traceback);
        self.push(L);

        L.PushAnyObject(arg0);
        L.PushAnyObject(arg1);
        L.PushAnyObject(arg2);

        if (L.PCall(2, LuaDLL.LUA_MULTRET, b) == LuaThreadStatus.LUA_OK) {
            L.Remove(b);
            return L.GetTop() - oldTop;
        } else {
            ThrowExceptionFromError(L, oldTop);
            return 0;
        }
    }

    public static int Invoke(this LuaFunction self, object arg0, object arg1, object arg2, object arg3)
    {
        var L = self.GetLuaState();
        var oldTop = L.GetTop();
        var b = oldTop + 1;
        L.PushCSharpFunction(LuaStatic.traceback);
        self.push(L);

        L.PushAnyObject(arg0);
        L.PushAnyObject(arg1);
        L.PushAnyObject(arg2);
        L.PushAnyObject(arg3);

        if (L.PCall(2, LuaDLL.LUA_MULTRET, b) == LuaThreadStatus.LUA_OK) {
            L.Remove(b);
            return L.GetTop() - oldTop;
        } else {
            ThrowExceptionFromError(L, oldTop);
            return 0;
        }
    }


    public static object InvokeR(this LuaFunction self)
    {
        int n = self.Invoke();
        var lua = self.GetLuaState();
        var ret = n > 0 ? lua.ToAnyObject(-1) : null;
        lua.Pop(n);
        return ret;
    }

    public static object InvokeR(this LuaFunction self, object arg0)
    {
        int n = self.Invoke(arg0);
        var lua = self.GetLuaState();
        var ret = n > 0 ? lua.ToAnyObject(-1) : null;
        lua.Pop(n);
        return ret;
    }

    public static object InvokeR(this LuaFunction self, object arg0, object arg1)
    {
        int n = self.Invoke(arg0, arg1);
        var lua = self.GetLuaState();
        var ret = n > 0 ? lua.ToAnyObject(-1) : null;
        lua.Pop(n);
        return ret;
    }

    #endregion
}

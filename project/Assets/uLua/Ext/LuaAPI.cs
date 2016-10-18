using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Runtime.InteropServices;
using LuaInterface;
using ILuaState = System.IntPtr;



/// <summary>
/// Lua Version 5.1
/// </summary>
public static class LuaAPI
{
    #region Api

    /// <summary>
    /// Controls the garbage collector.
    ///
    /// This function performs several tasks, according to the value of the parameter what:
    ///
    /// LUA_GCSTOP: stops the garbage collector.
    /// LUA_GCRESTART: restarts the garbage collector.
    /// LUA_GCCOLLECT: performs a full garbage-collection cycle.
    /// LUA_GCCOUNT: returns the current amount of memory (in Kbytes) in use by Lua.
    /// LUA_GCCOUNTB: returns the remainder of dividing the current amount of bytes of memory in use by Lua by 1024.
    /// LUA_GCSTEP: performs an incremental step of garbage collection.The step "size" is controlled by data(larger values mean more steps) in a non-specified way.If you want to control the step size you must experimentally tune the value of data.The function returns 1 if the step finished a garbage-collection cycle.
    /// LUA_GCSETPAUSE: sets data as the new value for the pause of the collector(see §2.10). The function returns the previous value of the pause.
    /// LUA_GCSETSTEPMUL: sets data as the new value for the step multiplier of the collector(see §2.10). The function returns the previous value of the step multiplier.
    /// </summary>
    public static void GC(this ILuaState self, LuaGCOptions option, int data)
    {
        LuaDLL.lua_gc(self, option, data);
    }

    /// <summary>
    /// Creates a new empty table and pushes it onto the stack. It is equivalent
    /// to lua_createtable(L, 0, 0).
    /// </summary>
    public static void NewTable(this ILuaState self)
    {
        LuaDLL.lua_newtable(self);
    }

    /// <summary>
    /// Creates a new empty table and pushes it onto the stack. 
    /// The new table has space pre-allocated for narr array elements and nrec non-array elements. 
    /// This pre-allocation is useful when you know exactly how many elements the table will have. 
    /// Otherwise you can use the function lua_newtable.
    /// </summary>
    public static void CreateTable(this ILuaState self, int narr, int nrec)
    {
        LuaDLL.lua_createtable(self, narr, nrec);
    }

    /// <summary>
    /// Similar to lua_gettable, but does a raw access (i.e., without metamethods).
    /// </summary>
    public static void RawGet(this ILuaState self, int index)
    {
        LuaDLL.lua_rawget(self, index);
    }

    /// <summary>
    /// Similar to lua_settable, but does a raw assignment (i.e., without metamethods).
    /// </summary>
    public static void RawSet(this ILuaState self, int index)
    {
        LuaDLL.lua_rawset(self, index);
    }

    /// <summary>
    /// Pushes onto the stack the value t[n], where t is the value at the given valid index. 
    /// The access is raw; that is, it does not invoke metamethods.
    /// </summary>
    public static void RawGetI(this ILuaState self, int index, int n)
    {
        LuaDLL.lua_rawgeti(self, index, n);
    }

    /// <summary>
    /// Does the equivalent of t[n] = v, where t is the value at the given valid index and v is the value at the top of the stack.
    ///
    /// This function pops the value from the stack.The assignment is raw; that is, it does not invoke metamethods.
    /// </summary>
    public static void RawSetI(this ILuaState self, int index, int n)
    {
        LuaDLL.lua_rawseti(self, index, n);
    }
    
    /// <summary>
    /// Pushes onto the stack the metatable of the value at the given acceptable index.
    /// If the index is not valid, or if the value does not have a metatable, the
    /// function returns 0 and pushes nothing on the stack.   
    /// </summary>
    public static bool GetMetaTable(this ILuaState self, int index)
    {
        return LuaDLL.lua_getmetatable(self, index) != 0;
    }
    
    /// <summary>
    /// Pops a table from the stack and sets it as the new metatable for the value
    /// at the given acceptable index.
    /// </summary>
    public static void SetMetaTable(this ILuaState self, int index)
    {
        LuaDLL.lua_setmetatable(self, index);
    }

    /// <summary>
    /// Pushes onto the stack the value t[k], where t is the value at the given valid index.
    /// As in Lua, this function may trigger a metamethod for the "index" event (see §2.8).
    /// </summary>
    public static void GetField(this ILuaState self, int index, string name)
    {
        LuaDLL.lua_getfield(self, index, name);
    }

    /// <summary>
    /// Does the equivalent to t[k] = v, where t is the value at the given valid 
    /// index and v is the value at the top of the stack.
    ///
    /// This function pops the value from the stack.As in Lua, this function may 
    /// trigger a metamethod for the "newindex" event (see §2.8).
    /// </summary>
    public static void SetField(this ILuaState self, int index, string name)
    {
        LuaDLL.lua_setfield(self, index, name);
    }

    /// <summary>
    /// Pushes onto the stack the value t[k], where t is the value at the given 
    /// valid index and k is the value at the top of the stack.
    ///
    /// This function pops the key from the stack(putting the resulting value in
    /// its place). As in Lua, this function may trigger a metamethod for the "index" event (see §2.8).
    /// </summary>
    public static void GetTable(this ILuaState self, int index)
    {
        LuaDLL.lua_gettable(self, index);
    }
    
    /// <summary>
    /// Does the equivalent to t[k] = v, where t is the value at the given valid
    /// index, v is the value at the top of the stack, and k is the value just below the top.
    ///
    /// This function pops both the key and the value from the stack.As in Lua, 
    /// this function may trigger a metamethod for the "newindex" event (see §2.8).
    /// </summary>
    public static void SetTable(this ILuaState self, int index)
    {
        LuaDLL.lua_settable(self, index);
    }

    /// <summary>
    /// Returns the type of the value in the given acceptable index, 
    /// or LUA_TNONE for a non-valid index (that is, an index to an "empty" stack position). 
    /// The types returned by lua_type are coded by the following constants defined in lua.h: 
    /// LUA_TNIL, LUA_TNUMBER, LUA_TBOOLEAN, LUA_TSTRING, LUA_TTABLE, LUA_TFUNCTION, LUA_TUSERDATA, LUA_TTHREAD, and LUA_TLIGHTUSERDATA.
    /// </summary>
    public static LuaTypes Type(this ILuaState self, int index)
    {
        return LuaDLL.lua_type(self, index);
    }

    /// <summary>
    /// Returns the name of the type encoded by the value tp, which must be one the values returned by lua_type.
    /// </summary>
    public static string TypeName(this ILuaState self, LuaTypes type)
    {
        return type.ToString();
        /// Will cause unity crash
        //return LuaDLL.lua_typename(self, type);
    }

    public static void Pop(this ILuaState self, int n)
    {
        LuaDLL.lua_pop(self, n);
    }

    /// <summary>
    /// Moves the top element into the given valid index, shifting up the elements 
    /// above this index to open space. Cannot be called with a pseudo-index, 
    /// because a pseudo-index is not an actual stack position.
    /// </summary>
    public static void Insert(this ILuaState self, int index)
    {
        LuaDLL.lua_insert(self, index);
    }

    /// <summary>
    /// Removes the element at the given valid index, shifting down the elements 
    /// above this index to fill the gap. Cannot be called with a pseudo-index, 
    /// because a pseudo-index is not an actual stack position.
    /// </summary>
    public static void Remove(this ILuaState self, int index)
    {
        Assert.IsFalse(self.GetTop() < Mathf.Abs(index));
        LuaDLL.lua_remove(self, index);
    }

    public static int GetTop(this ILuaState self)
    {
        return LuaDLL.lua_gettop(self);
    }

    public static void SetTop(this ILuaState self, int newTop)
    {
        LuaDLL.lua_settop(self, newTop);
    }

    /// <summary>
    /// Pops a key from the stack, and pushes a key-value pair from the table at 
    /// the given index (the "next" pair after the given key). If there are no more 
    /// elements in the table, then lua_next returns 0 (and pushes nothing).
    /// 
    /// A typical traversal looks like this:
    /// 
    ///  /* table is in the stack at index 't' */
    ///  lua_pushnil(L);  /* first key */
    ///  while (lua_next(L, t) != 0) {
    ///     /* uses 'key' (at index -2) and 'value' (at index -1) */
    ///     printf("%s - %s\n",
    ///            lua_typename(L, lua_type(L, -2)),
    ///            lua_typename(L, lua_type(L, -1)));
    ///     /* removes 'value'; keeps 'key' for next iteration */
    ///     lua_pop(L, 1);
    /// }
    /// While traversing a table, do not call lua_tolstring directly on a key, unless 
    /// you know that the key is actually a string. Recall that lua_tolstring changes 
    /// the value at the given index; this confuses the next call to lua_next.
    /// </summary>
    public static bool Next(this ILuaState self, int index)
    {
        return LuaDLL.lua_next(self, index) != 0;
    }

    public static void GetGlobal(this ILuaState self, string name)
    {
        LuaDLL.lua_getglobal(self, name);
    }


    /// <summary>
    /// Pops a value from the stack and sets it as the new value of global name.
    /// It is defined as a macro:
    ///
    ///     #define lua_setglobal(L,s)   lua_setfield(L, LUA_GLOBALSINDEX, s)
    /// 
    /// </summary>
    public static void SetGlobal(this ILuaState self, string name)
    {
        LuaDLL.lua_setglobal(self, name);
    }
    
    /// <summary>
    /// Returns 1 if the value at the given acceptable index is nil, and 0 otherwise.
    /// </summary>
    public static bool IsNil(this ILuaState self, int index)
    {
        return LuaDLL.lua_isnil(self, index);
    }

    /// <summary>
    /// Returns 1 if the given acceptable index is not valid 
    /// (that is, it refers to an element outside the current stack), and 0 otherwise.
    /// </summary>
    public static bool IsNone(this ILuaState self, int index)
    {
        return self.Type(index) == LuaTypes.LUA_TNONE;
    }

    /// <summary>
    /// Returns 1 if the given acceptable index is not valid 
    /// (that is, it refers to an element outside the current stack) 
    /// or if the value at this index is nil, and 0 otherwise.
    /// </summary>
    public static bool IsNoneOrNil(this ILuaState self, int index)
    {
        var luaT = self.Type(index);
        return luaT == LuaTypes.LUA_TNIL || luaT == LuaTypes.LUA_TNONE;
    }

    /// <summary>
    /// Returns 1 if the value at the given acceptable index has type boolean, and 0 otherwise.
    /// </summary>
    public static bool IsBoolean(this ILuaState self, int index)
    {
        return LuaDLL.lua_isboolean(self, index);
    }
    
    /// <summary>
    /// Returns 1 if the value at the given acceptable index is a function (either C or Lua), and 0 otherwise.
    /// </summary>
    public static bool IsFunction(this ILuaState self, int index)
    {
        return LuaDLL.lua_isfunction(self, index) != 0;
    }

    /// <summary>
    /// Returns 1 if the value at the given acceptable index is a light userdata, and 0 otherwise.
    /// </summary>
    public static bool IsLightUserData(this ILuaState self, int index)
    {
        return LuaDLL.lua_islightuserdata(self, index) != 0;
    }

    /// <summary>
    /// Returns 1 if the value at the given acceptable index is a string or a number (which is always convertible to a string), and 0 otherwise.
    /// </summary>
    public static bool IsString(this ILuaState self, int index)
    {
        return LuaDLL.lua_isstring(self, index);
    }

    /// <summary>
    /// Returns 1 if the value at the given acceptable index is a number or a string convertible to a number, and 0 otherwise.
    /// </summary>
    /// <returns></returns>
    public static bool IsNumber(this ILuaState self, int index)
    {
        return LuaDLL.lua_isnumber(self, index);
    }

    /// <summary>
    /// Returns 1 if the value at the given acceptable index is a table, and 0 otherwise.
    /// </summary>
    public static bool IsTable(this ILuaState self, int index)
    {
        return LuaDLL.lua_istable(self, index) != 0;
    }

    /// <summary>
    /// Returns 1 if the value at the given acceptable index is a userdata (either full or light), and 0 otherwise.
    /// </summary>
    public static bool IsUserData(this ILuaState self, int index)
    {
        return LuaDLL.lua_isuserdata(self, index) != 0;
    }

    /// <summary>
    /// Generates an error with a message like the following:
    ///
    /// location: bad argument narg to 'func' (tname expected, got rt)
    /// where location is produced by luaL_where, func is the name of the current function, and rt is the type name of the actual argument.
    /// </summary>
    public static void TypeError(this ILuaState self, int index, string tname)
    {
        LuaDLL.luaL_typerror(self, index, tname);
    }
    
    public static void PushNil(this ILuaState self)
    {
        LuaDLL.lua_pushnil(self);
    }

    /// <summary>
    /// Pushes a copy of the element at the given valid index onto the stack.
    /// </summary>
    public static void PushValue(this ILuaState self, int index)
    {
        LuaDLL.lua_pushvalue(self, index);
    }

    public static void PushBoolean(this ILuaState self, bool value)
    {
        LuaDLL.lua_pushboolean(self, value);
    }

    public static void PushString(this ILuaState self, string value)
    {
        LuaDLL.lua_pushstring(self, value);
    }

    public static void PushNumber(this ILuaState self, double value)
    {
        LuaDLL.lua_pushnumber(self, value);
    }

    public static void PushInteger(this ILuaState self, int value)
    {
        LuaDLL.lua_pushinteger(self, value);
    }

    /// <summary>
    /// Pushes a light userdata onto the stack.
    ///
    /// Userdata represent C values in Lua.A light userdata represents a pointer.
    /// It is a value (like a number): you do not create it, it has no individual metatable, 
    /// and it is not collected(as it was never created). 
    /// A light userdata is equal to "any" light userdata with the same C address.
    /// 
    /// 这里只通过luanet来管理userdata
    /// ⚠部分Unity的值类型不是通过这个接口来压栈的！！
    /// </summary>
    public static void PushLightUserData(this ILuaState self, object value)
    {
        if (value != null) {
#if UNITY_ASSERTIONS
            if (value.GetType().IsValueType) {
                LogMgr.W("一个值类型被压栈：{0}", value);
            }
#endif
            var oldTop = self.GetTop();
            // 检查该类型是否已经Wrap，如果没有就自动Wrap
            var type = value.GetType();
            string metaName = self.BindRecursively(type);
            self.SetTop(oldTop);

            var ls = LuaEnv.Get(self).ls;
            ls.translator.pushObject(self, value, metaName);
        } else {
            self.PushNil();
        }
    }

    /// <summary>
    /// Pushes a C function onto the stack. This function receives a pointer to 
    /// a C function and pushes onto the stack a Lua value of type function that, 
    /// when called, invokes the corresponding C function.
    ///
    /// Any function to be registered in Lua must follow the correct protocol to 
    /// receive its parameters and return its results(see lua_CFunction).
    ///
    /// lua_pushcfunction is defined as a macro:
    ///
    ///     #define lua_pushcfunction(L,f)  lua_pushcclosure(L,f,0)
    /// </summary>
    public static void PushCSharpFunction(this ILuaState self, LuaCSFunction value)
    {
        LuaDLL.lua_pushstdcallcfunction(self, value);
    }

    /// <summary>
    /// Converts the Lua value at the given acceptable index to the C type lua_Number (see lua_Number). 
    /// The Lua value must be a number or a string convertible to a number (see §2.2.1); 
    /// otherwise, lua_tonumber returns 0.
    /// </summary>
    public static double ToNumber(this ILuaState self, int index)
    {
        return LuaDLL.lua_tonumber(self, index);
    }

    public static int ToInteger(this ILuaState self, int index)
    {
        return (int)LuaDLL.lua_tonumber(self, index);
    }

    public static bool ToBoolean(this ILuaState self, int index)
    {
        return LuaDLL.lua_toboolean(self, index);
    }

    public static string ToString(this ILuaState self, int index)
    {
        return LuaDLL.lua_tostring(self, index);
    }

    /// <summary>
    /// Converts the Lua value at the given acceptable index to a C string. 
    /// If len is not NULL, it also sets *len with the string length. 
    /// The Lua value must be a string or a number; otherwise, the function returns NULL. 
    /// If the value is a number, then lua_tolstring also changes the actual value in the stack to a string. 
    /// (This change confuses lua_next when lua_tolstring is applied to keys during a table traversal.)
    ///
    /// lua_tolstring returns a fully aligned pointer to a string inside the Lua state.
    /// This string always has a zero('\0') after its last character(as in C), but can contain other zeros in its body.
    /// Because Lua has garbage collection, there is no guarantee that the pointer 
    /// returned by lua_tolstring will be valid after the corresponding value is removed from the stack.
    /// </summary>
    public static System.IntPtr ToLString(this ILuaState self, int index, out int len)
    {
        return LuaDLL.lua_tolstring(self, index, out len);
    }

    /// <summary>
    /// If the value at the given acceptable index is a full userdata, returns its 
    /// block address. If the value is a light userdata, returns its pointer. 
    /// Otherwise, returns NULL.
    /// 
    /// 这里只通过luanet来管理userdata
    /// </summary>
    public static object ToUserData(this ILuaState self, int index)
    {
        if (self.IsUserData(index)) {
            int udata = LuaDLL.luanet_rawnetobj(self, index);

            if (udata != -1) {
                object obj = null;
                var ls = LuaEnv.Get(self).ls;
                ls.translator.objects.TryGetValue(udata, out obj);
                return obj;
            }
        } else if (!self.IsNoneOrNil(index)) {
            LogMgr.W("try to convert {0} to {1}", self.Type(index), LuaTypes.LUA_TUSERDATA);
        }
        return null;
    }

    public static void Replace(this ILuaState self, int index)
    {
        LuaDLL.lua_replace(self, index);
    }

    /// <summary>
    /// Returns the "length" of the value at the given acceptable index: 
    /// for strings, this is the string length; 
    /// for tables, this is the result of the length operator ('#'); 
    /// for userdata, this is the size of the block of memory allocated for the userdata; 
    /// for other values, it is 0.
    /// </summary>
    public static int ObjLen(this ILuaState self, int index)
    {
        return LuaDLL.lua_objlen(self, index);
    }

    /// <summary>
    /// Calls a function.
    /// To call a function you must use the following protocol: 
    /// first, the function to be called is pushed onto the stack; 
    /// then, the arguments to the function are pushed in direct order; 
    /// that is, the first argument is pushed first. 
    /// Finally you call lua_call; nargs is the number of arguments that you pushed onto the stack. 
    /// All arguments and the function value are popped from the stack when the function is called. 
    /// The function results are pushed onto the stack when the function returns. 
    /// The number of results is adjusted to nresults, unless nresults is LUA_MULTRET. 
    /// In this case, all results from the function are pushed. 
    /// Lua takes care that the returned values fit into the stack space. 
    /// The function results are pushed onto the stack in direct order (the first result is pushed first), 
    /// so that after the call the last result is on the top of the stack.
    /// </summary>
    public static void Call(this ILuaState self, int nArgs, int nResults)
    {
        LuaDLL.lua_call(self, nArgs, nResults);
    }

    /// <summary>
    /// Calls a function in protected mode.
    /// </summary>
    public static LuaThreadStatus PCall(this ILuaState self, int nArgs, int nResults, int errfunc)
    {
        return (LuaThreadStatus)LuaDLL.lua_pcall(self, nArgs, nResults, errfunc);
    }
    #endregion

    #region Aux

    /// <summary>
    /// Creates and returns a reference, in the table at index t, for the object 
    /// at the top of the stack (and pops the object).
    ///
    /// A reference is a unique integer key.As long as you do not manually add 
    /// integer keys into table t, luaL_ref ensures the uniqueness of the key it returns.
    /// You can retrieve an object referred by reference r by calling lua_rawgeti(L, t, r). 
    /// Function luaL_unref frees a reference and its associated object.
    ///
    /// If the object at the top of the stack is nil, luaL_ref returns the constant LUA_REFNIL.
    /// The constant LUA_NOREF is guaranteed to be different from any reference returned by luaL_ref.
    /// </summary>
    public static int L_Ref(this ILuaState self, int t)
    {
        // 检测异常的引用建立
#if UNITY_ASSERTIONS
        //Assert.IsTrue(self == LuaEnv.Instance.L);
        if (self != LuaEnv.Instance.L) {
            LogMgr.E("L_Ref异常");
        }
#endif
        return LuaDLL.luaL_ref(self, t);
    }

    /// <summary>
    /// Releases reference ref from the table at index t (see luaL_ref). The entry is removed from the table, 
    /// so that the referred object can be collected. The reference ref is also freed to be used again.
    ///
    /// If ref is LUA_NOREF or LUA_REFNIL, luaL_unref does nothing.
    /// </summary>
    public static void L_Unref(this ILuaState self, int t, int @ref)
    {
        LuaDLL.luaL_unref(self, t, @ref);
    }

    /// <summary>
    /// 把注册表中引用为reference的对象压栈
    /// </summary>
    public static void GetRef(this ILuaState self, int reference)
    {
        LuaDLL.lua_getref(self, reference);
    }

    /// <summary>
    /// Loads a buffer as a Lua chunk. This function uses lua_load to load the 
    /// chunk in the buffer pointed to by buff with size sz.
    ///
    /// This function returns the same results as lua_load.name is the chunk name, 
    /// used for debug information and error messages.
    /// </summary>
    public static LuaThreadStatus L_LoadBuffer(this ILuaState self, byte[] buff, string name)
    {
        return (LuaThreadStatus)LuaDLL.luaL_loadbuffer(self, buff, buff.Length, name);
    }

    /// <summary>
    /// Raises an error. The error message format is given by fmt plus any extra 
    /// arguments, following the same rules of lua_pushfstring. It also adds at 
    /// the beginning of the message the file name and the line number where the 
    /// error occurred, if this information is available.
    /// </summary>
    public static void L_Error(this ILuaState self, string message)
    {
        LuaDLL.luaL_error(self, message);
    }

    /// <summary>
    /// Raises an error with the following message, where func is retrieved from the call stack:
    ///
    /// bad argument #<narg> to <func> (<extramsg>)
    /// This function never returns, but it is an idiom to use it in C functions as return luaL_argerror(args).
    /// </summary>
    public static int L_ArgError(this ILuaState self, int index, string extramsg)
    {
        return LuaDLL.luaL_argerror(self, index, extramsg);
    }

    /// <summary>
    /// Loads and runs the given string. It is defined as the following macro:
    ///
    /// (luaL_loadstring(L, str) || lua_pcall(L, 0, LUA_MULTRET, 0))
    /// 
    /// It returns 0 if there are no errors or 1 in case of errors.
    /// </summary>
    public static LuaThreadStatus L_DoString(this ILuaState self, string chunk)
    {
        return (LuaThreadStatus)LuaDLL.luaL_dostring(self, chunk);
    }


    /// <summary>
    /// If the registry already has the key tname, returns 0. Otherwise, creates 
    /// a new table to be used as a metatable for userdata, adds it to the registry 
    /// with key tname, and returns 1.
    ///
    /// In both cases pushes onto the stack the final value associated with tname in the registry.
    /// </summary>
    public static int L_NewMetaTable(this ILuaState self, string tname)
    {
        return LuaDLL.luaL_newmetatable(self, tname);
    }

    /// <summary>
    /// Pushes onto the stack the metatable associated with name tname in the registry 
    /// (see luaL_newmetatable).
    /// </summary>
    public static void L_GetMetaTable(this ILuaState self, string tname)
    {
        LuaDLL.luaL_getmetatable(self, tname);
    }

    /// <summary>
    /// Opens a library.
    ///
    /// When called with libname equal to NULL, it simply registers all functions 
    /// in the list l(see luaL_Reg) into the table on the top of the stack.
    ///
    /// When called with a non-null libname, luaL_register creates a new table t, 
    /// sets it as the value of the global variable libname, sets it as the value 
    /// of package.loaded[libname], and registers on it all functions in the list l.
    /// If there is a table in package.loaded[libname] or in variable libname, reuses 
    /// this table instead of creating a new one.
    ///
    /// In any case the function leaves the table on the top of the stack.
    /// </summary>
//	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    public static void L_Register(this ILuaState self, string libname, LuaMethod[] methods)
    {
		Debug.Log ("Start Register Mothods!");
        if (!string.IsNullOrEmpty(libname)) {
            self.GetGlobal("package", "loaded");
            self.GetField(-1, libname);
			Debug.Log ("Rigister System.cs Files : " + libname);
            if (self.IsNil(-1)) {
                self.Pop(1);
                
                self.NewTable();
                self.SetField(-2, libname);

                self.GetField(-1, libname);
                self.Replace(-2);
            }
        }

        if (methods != null && methods.Length > 0) {
            for (int i = 0; i < methods.Length; i++) {
				Debug.Log ("_____>>>>>PushString Function : " + methods[i].name);
                self.PushString(methods[i].name);
				Debug.Log ("_____>>>>>PushCSharpFunction Function : " + methods[i].func.ToString());
                self.PushCSharpFunction(methods[i].func);
                self.RawSet(-3);
            }
        }
    }
#endregion
    
}

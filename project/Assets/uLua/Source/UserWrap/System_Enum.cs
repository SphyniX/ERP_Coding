using UnityEngine;
using System.Collections;
using LuaInterface;
using ILuaState = System.IntPtr;

public static class System_Enum
{
    public const string CLASS = "System.Enum";

    public static object ToEnumValue(this ILuaState self, int index, System.Type type)
    {
        var luaT = self.Type(index);
        switch (luaT) {
            case LuaTypes.LUA_TNUMBER:
                return System.Enum.ToObject(type, self.ToInteger(index));
            case LuaTypes.LUA_TSTRING:
                {
                    var enName = self.ToString(index);
                    if (System.Enum.IsDefined(type, enName)) {
                        return System.Enum.Parse(type, enName);
                    }
                }
                break;
            case LuaTypes.LUA_TTABLE:
                {
                    self.PushString("id");
                    self.RawGet(index);
                    int id = self.ToInteger(-1);
                    self.Pop(1);

                    if (type != null) {
                        if (System.Enum.IsDefined(type, id)) {
                            return System.Enum.ToObject(type, id);
                        }
                    } else return id;
                } break;
            default:
                break;
        }

        return null;
    }

    public static void PushUData(this ILuaState self, System.Enum value)
    {
        var type = value.GetType();

        // 自动绑定到Lua
        var typeName = type.FullName.Replace('+', '.');
        var metaName = WrapToLua.GetMetaName(typeName);        
        self.L_GetMetaTable(metaName);
        if (self.IsNil(-1)) {            
            Wrap(self, type);
        }
        
        var name = System.Enum.GetName(type, value);
        //var id = (int)System.Convert.ChangeType(value, typeof(int));

        self.GetGlobal("package", "loaded", type.FullName);
        self.GetField(-1, name);
        self.Remove(-2);
    }

    public static string Wrap(ILuaState L, System.Type enumType)
    {
        // 保存类型
        var metaName = L.RegistType(enumType);

        L.L_NewMetaTable(metaName);

        L.L_GetMetaTable("System.Object");
        L.SetMetaTable(-2);

        L.SetDict("class", CLASS);
        L.SetDict("__tostring", new LuaCSFunction(__tostring));
        L.SetDict("__index", new LuaCSFunction(__index));
        L.SetDict("__newindex", new LuaCSFunction(__newindex));

        // 保存所有枚举值
        var values = System.Enum.GetValues(enumType);
        for (int i = 0; i < values.Length; ++i) {
            var value = values.GetValue(i);
            var id = (int)value;
            var name = value.ToString();
            L.CreateTable(0, 2); {
                L.SetDict("id", id);
                L.SetDict("name", name);
            }
            L.PushValue(-2);
            L.SetMetaTable(-2);
            L.SetField(-3, name);
        }
                
        L.Pop(2);

        return metaName;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int __index(ILuaState L)
    {
        L.L_Error("trying to index an enum value");
        return 0;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int __newindex(ILuaState L)
    {
        L.L_Error("trying to newindex an enum value");
        return 0;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int __tostring(ILuaState L)
    {
        L.PushString("name");
        L.RawGet(1);
        return 1;
    }    
}

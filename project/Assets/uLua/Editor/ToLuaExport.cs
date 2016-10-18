/*    
 * Copyright (c) 2014.9 , 蒙占志 (Zhanzhi Meng) topameng@gmail.com
 * All rights reserved.
 * Use, modification and distribution are subject to the "New BSD License"
*/

using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using LuaInterface;

using Object = UnityEngine.Object;
using System.IO;
using System.Text.RegularExpressions;

public enum MetaOp
{
    None = 0,
    Add = 1,
    Sub = 2,
    Mul = 4,
    Div = 8,
    Eq = 16,
    Neg = 32,
    ALL = Add | Sub | Mul | Div | Eq | Neg,
}

public enum ObjAmbig
{
    None = 0, 
    U3dObj = 1,
    NetObj = 2,
    All = 3
}

public class DelegateType
{
    public string name;
    public Type type;

    public string strType = "";

    public DelegateType(Type t)
    {
        type = t;
        strType = ToLuaExport.GetTypeStr(t);

        if (t.IsGenericType)
        {
            name = ToLuaExport.GetGenericLibName(t);
        }
        else
        {
            name = ToLuaExport.GetTypeStr(t);
            name = name.Replace(".", "_");
        }
    }

    public DelegateType SetName(string str)
    {
        name = str;
        return this;
    }
}

public static class ToLuaExport
{
    public static string className = string.Empty;
    public static Type type = null;

    public static string baseClassName = null;
    public static bool isStaticClass = true;

    static HashSet<string> usingList = new HashSet<string>();
    static MetaOp op = MetaOp.None;
    static StringBuilder sb = null;
    static MethodInfo[] methods = null;
    static Dictionary<string, int> nameCounter = null;
    static FieldInfo[] fields = null;
    static PropertyInfo[] props = null;
    static List<PropertyInfo> propList = new List<PropertyInfo>();  //非静态属性

    static BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;

    static ObjAmbig ambig = ObjAmbig.NetObj;
    //wrapClaaName + "Wrap" = 导出文件名，导出类名
    public static string wrapClassName = "";

    public static string libClassName = "";
    public static string extendName = "";
    public static Type extendType = null;

    public static HashSet<Type> eventSet = new HashSet<Type>();

    public static List<string> memberFilter = new List<string>
    {
        // 5.3.1
        "Application.AdvertisingIdentifierCallback",
        "Application.RequestAdvertisingIdentifierAsync",

        "AnimationClip.averageDuration",
        "AnimationClip.averageAngularSpeed",
        "AnimationClip.averageSpeed",
        "AnimationClip.apparentSpeed",
        "AnimationClip.isLooping",
        "AnimationClip.isAnimatorMotion",
        "AnimationClip.isHumanMotion",
        "AnimatorOverrideController.PerformOverrideClipListCleanup",
        "Caching.SetNoBackupFlag",
        "Caching.ResetNoBackupFlag",
        "Light.areaSize",
        "Security.GetChainOfTrustValue",
        "Texture2D.alphaIsTransparency",
        "WWW.movie",
        "WebCamTexture.MarkNonReadable",
        "WebCamTexture.isReadable",
        "Graphic.OnRebuildRequested",
        "Text.OnRebuildRequested",       
        "Application.ExternalEval",
        "Resources.LoadAssetAtPath",
        "Input.IsJoystickPreconfigured",
        "String.Chars",
    };

    public static bool IsMemberFilter(MemberInfo mi)
    {
        return memberFilter.Contains(type.Name + "." + mi.Name);
    }

    static ToLuaExport()
    {

    }

    public static void Clear()
    {
        className = null;
        type = null;
        isStaticClass = false;
        baseClassName = null;
        usingList.Clear();
        op = MetaOp.None;
        sb = new StringBuilder();
        methods = null;
        fields = null;
        props = null;
        propList.Clear();
        ambig = ObjAmbig.NetObj;
        wrapClassName = "";
        libClassName = "";
    }

    private static MetaOp GetOp(string name)
    {
        if (name == "op_Addition") {
            return MetaOp.Add;
        } else if (name == "op_Subtraction") {
            return MetaOp.Sub;
        } else if (name == "op_Equality") {
            return MetaOp.Eq;
        } else if (name == "op_Multiply") {
            return MetaOp.Mul;
        } else if (name == "op_Division") {
            return MetaOp.Div;
        } else if (name == "op_UnaryNegation") {
            return MetaOp.Neg;
        }

        return MetaOp.None;
    }

    //操作符函数无法通过继承metatable实现
    static void GenBaseOpFunction(List<MethodInfo> list)
    {
        Type baseType = type.BaseType;

        while (baseType != null) {
            MethodInfo[] methods = baseType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);

            for (int i = 0; i < methods.Length; i++) {
                MetaOp baseOp = GetOp(methods[i].Name);

                // 不独立生成等于操作
                if (baseOp == MetaOp.None || baseOp == MetaOp.Eq) continue;

                if ((op & baseOp) == 0) {
                    list.Add(methods[i]);
                }
            }

            baseType = baseType.BaseType;
        }
    }

    public static void Generate(params string[] param)
    {
        Debugger.Log("Begin Generate lua Wrap for class {0}\r\n", className);
        sb = new StringBuilder();
        usingList.Add("System");
        GetTypeStr(type);       //看是否有命名空间加入

        var genName = type.FullName.Replace('.', '_').Replace('+', '_') + "Wrap";
        //if (type.Namespace != null && type.Namespace != string.Empty)
        //{
        //    usingList.Add(type.Namespace);
        //}

        if (wrapClassName == "") {
            wrapClassName = className;
        }

        if (libClassName == "") {
            libClassName = className;
        }

        if (type.IsEnum) {
            GenEnum(genName);
            SaveFile(AppConst.uLuaPath + "/Source/LuaWrap/" + genName + ".cs");
            return;
        }

        nameCounter = new Dictionary<string, int>();
        List<MethodInfo> list = new List<MethodInfo>();

//        if (baseClassName != null) {
//            binding |= BindingFlags.DeclaredOnly;
//        } else if (baseClassName == null && isStaticClass) {
//            binding |= BindingFlags.DeclaredOnly;
//        }
        binding |= BindingFlags.DeclaredOnly;

        if (type.IsInterface) {
            list.AddRange(type.GetMethods());
        } else {
            list.AddRange(type.GetMethods(BindingFlags.Instance | binding));

            for (int i = list.Count - 1; i >= 0; --i) {
                var mi = list[i];

//                if (mi.GetBaseDefinition() != null) {
//                    list.RemoveAt(i);
//                    continue;
//                }

                //先去掉操作符函数
                if (mi.Name.Contains("op_") || mi.Name.Contains("add_") || mi.Name.Contains("remove_")) {
                    if (!IsNeedOp(mi.Name)) {
                        list.RemoveAt(i);
                    }

                    continue;
                }

                //扔掉 unity3d 废弃的函数                
                if (IsObsolete(mi)) {
                    list.RemoveAt(i);
                }
            }
        }

        PropertyInfo[] ps = type.GetProperties();

        for (int i = 0; i < ps.Length; i++) {
            int index = list.FindIndex((m) => { return m.Name == "get_" + ps[i].Name; });

            if (index >= 0 && list[index].Name != "get_Item") {
                list.RemoveAt(index);
            }

            index = list.FindIndex((m) => { return m.Name == "set_" + ps[i].Name; });

            if (index >= 0 && list[index].Name != "set_Item") {
                list.RemoveAt(index);
            }
        }

        ProcessExtends(list);
        GenBaseOpFunction(list);

        methods = list.ToArray();

        sb.AppendFormat("public class {0}", genName).AppendLine();
        sb.AppendLine("{");

        GenRegFunc();
        GenConstruct();
        GenGetType();
        GenIndexFunc();
        GenNewIndexFunc();
        //GenToStringFunc();
        GenFunction();

        sb.AppendLine("}").AppendLine();
        //Debugger.Log(sb.ToString());       
        string path = AppConst.uLuaPath + "/Source/LuaWrap/";
        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        SaveFile(path + genName + ".cs");
    }

    static void SaveFile(string file)
    {
        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8)) {
            StringBuilder usb = new StringBuilder();

            foreach (string str in usingList) {
                usb.AppendFormat("using {0};\r\n", str);
            }

            usb.AppendLine("using LuaInterface;");

            if (ambig == ObjAmbig.All) {
                usb.AppendLine("using Object = UnityEngine.Object;");
            }

            usb.AppendLine();

            textWriter.Write(usb.ToString());
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
    }

    static void GenLuaFields()
    {
        fields = type.GetFields(BindingFlags.GetField | BindingFlags.SetField | BindingFlags.Instance | binding);
        props = type.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | binding);
        propList.AddRange(type.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase));

        List<FieldInfo> fieldList = new List<FieldInfo>();
        fieldList.AddRange(fields);

        for (int i = fieldList.Count - 1; i >= 0; i--) {
            if (IsObsolete(fieldList[i])) {
                fieldList.RemoveAt(i);
            }
        }

        fields = fieldList.ToArray();

        List<PropertyInfo> piList = new List<PropertyInfo>();
        piList.AddRange(props);

        for (int i = piList.Count - 1; i >= 0; i--) {
//            var pi = piList[i];
//            if (pi.GetGetMethod().GetBaseDefinition() != null 
//                && pi.GetSetMethod().GetBaseDefinition() != null) {
//                piList.RemoveAt(i);
//                continue;
//            }

            if (piList[i].Name == "Item" || IsObsolete(piList[i])) {
                piList.RemoveAt(i);
            }
        }

        props = piList.ToArray();

        for (int i = propList.Count - 1; i >= 0; i--) {
            if (propList[i].Name == "Item" || IsObsolete(propList[i])) {
                propList.RemoveAt(i);
            }
        }

        if (fields.Length == 0 && props.Length == 0 && isStaticClass && baseClassName == null) {
            return;
        }

        sb.AppendLine("\t\tLuaField[] fields = new LuaField[]");
        sb.AppendLine("\t\t{");

        for (int i = 0; i < fields.Length; i++) {
            if (fields[i].IsLiteral || fields[i].IsPrivate || fields[i].IsInitOnly) {
                sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, null),\r\n", fields[i].Name);
            } else {
                sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, set_{0}),\r\n", fields[i].Name);
            }
        }

        for (int i = 0; i < props.Length; i++) {
            if (props[i].CanRead && props[i].CanWrite && props[i].GetSetMethod(true).IsPublic) {
                sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, set_{0}),\r\n", props[i].Name);
            } else if (props[i].CanRead) {
                sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", get_{0}, null),\r\n", props[i].Name);
            } else if (props[i].CanWrite) {
                sb.AppendFormat("\t\t\tnew LuaField(\"{0}\", null, set_{0}),\r\n", props[i].Name);
            }
        }

        sb.AppendLine("\t\t};\r\n");
    }

    static void GenLuaMethods()
    {
        sb.AppendLine("\t\tLuaMethod[] regs = new LuaMethod[]");
        sb.AppendLine("\t\t{");

        //注册库函数
        for (int i = 0; i < methods.Length; i++) {
            MethodInfo m = methods[i];
            int count = 1;

            if (m.IsGenericMethod) {
                continue;
            }

            if (!nameCounter.TryGetValue(m.Name, out count)) {
                if (!m.Name.Contains("op_")) {
                    sb.AppendFormat("\t\t\tnew LuaMethod(\"{0}\", {0}),\r\n", m.Name);
                }

                nameCounter[m.Name] = 1;
            } else {
                nameCounter[m.Name] = count + 1;
            }
        }

        sb.AppendFormat("\t\t\tnew LuaMethod(\"new\", _Create{0}),\r\n", wrapClassName);
        sb.AppendLine("\t\t\tnew LuaMethod(\"GetType\", GetClassType),");

        // 不独立生成ToString，运行时指定同一个
        //int index = Array.FindIndex<MethodInfo>(methods, (p) => { return p.Name == "ToString"; });
        //if (index >= 0 && !isStaticClass) {
        //    sb.AppendLine("\t\t\tnew LuaMethod(\"__tostring\", Lua_ToString),");
        //}

        GenOperatorReg();
        sb.AppendLine("\t\t};\r\n");
    }

    static void GenOperatorReg()
    {
        if ((op & MetaOp.Add) != 0) {
            sb.AppendLine("\t\t\tnew LuaMethod(\"__add\", Lua_Add),");
        }

        if ((op & MetaOp.Sub) != 0) {
            sb.AppendLine("\t\t\tnew LuaMethod(\"__sub\", Lua_Sub),");
        }

        if ((op & MetaOp.Mul) != 0) {
            sb.AppendLine("\t\t\tnew LuaMethod(\"__mul\", Lua_Mul),");
        }

        if ((op & MetaOp.Div) != 0) {
            sb.AppendLine("\t\t\tnew LuaMethod(\"__div\", Lua_Div),");
        }

        // 不独立生成__eq，运行时指定同一个
//        if ((op & MetaOp.Eq) != 0) {
//            sb.AppendLine("\t\t\tnew LuaMethod(\"__eq\", Lua_Eq),");
//        }

        if ((op & MetaOp.Neg) != 0) {
            sb.AppendLine("\t\t\tnew LuaMethod(\"__unm\", Lua_Neg),");
        }
    }

    static void GenRegFunc()
    {
        sb.AppendLine("\tpublic static System.Type Register(IntPtr L)");
        sb.AppendLine("\t{");

        GenLuaMethods();
        GenLuaFields();

        sb.AppendFormat("\t\tvar type = typeof({0});", className).AppendLine();

        if (isStaticClass && fields.Length == 0 && props.Length == 0) {
            sb.AppendLine("\t\tL.WrapCSharpObject(type, regs);");
        } else {
            sb.AppendLine("\t\tL.WrapCSharpObject(type, regs, fields);");
        }

        sb.AppendLine("\t\treturn type;");

        sb.AppendLine("\t}");
    }

    static bool IsParams(ParameterInfo param)
    {
        return param.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0;
    }

    static void GenFunction()
    {
        HashSet<string> set = new HashSet<string>();

        for (int i = 0; i < methods.Length; i++) {
            MethodInfo m = methods[i];

            if (m.IsGenericMethod) {
                Debugger.Log("Generic Method {0} cannot be export to lua", m.Name);
                continue;
            }

            if (nameCounter[m.Name] > 1) {
                if (!set.Contains(m.Name)) {
                    MethodInfo mi = GenOverrideFunc(m.Name);

                    if (mi == null) {
                        set.Add(m.Name);
                        continue;
                    } else {
                        m = mi;
                    }
                } else {
                    continue;
                }
            }

            set.Add(m.Name);
            sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
            sb.AppendFormat("\tstatic int {0}(IntPtr L)\r\n", GetFuncName(m.Name));
            sb.AppendLine("\t{");

            if (HasAttribute(m, typeof(OnlyGCAttribute))) {
                sb.AppendLine("\t\tMetaMethods.__gc(L);");
                sb.AppendLine("\t\treturn 0;");
                sb.AppendLine("\t}");
                continue;
            }

            if (HasAttribute(m, typeof(UseDefinedAttribute))) {
                FieldInfo field = extendType.GetField(m.Name + "Defined");
                string strfun = field.GetValue(null) as string;
                sb.AppendLine(strfun);
                sb.AppendLine("\t}");
                continue;
            }

            ParameterInfo[] paramInfos = m.GetParameters();
            int offset = m.IsStatic ? 1 : 2;
            bool haveParams = HasOptionalParam(paramInfos);

            if (!haveParams) {
                int count = paramInfos.Length + offset - 1;
                sb.AppendFormat("\t\tL.ChkArgsCount({0});\r\n", count);
            } else {
                sb.AppendLine("\t\tint count = LuaDLL.lua_gettop(L);");
            }

            int rc = m.ReturnType == typeof(void) ? 0 : 1;
            rc += ProcessParams(m, 2, false, false);
            sb.AppendFormat("\t\treturn {0};\r\n", rc);
            sb.AppendLine("\t}");
        }
    }

    static void NoConsturct()
    {
        sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
        sb.AppendLine("\t{");
        sb.AppendFormat("\t\tLuaDLL.luaL_error(L, \"{0} class does not have a constructor function\");\r\n", className);
        sb.AppendLine("\t\treturn 0;");
        sb.AppendLine("\t}");
    }

    static string GetPushFunction(Type t)
    {
        if (t.IsArray) return "PushUData";
        if (t.IsEnum) return "PushUData";
        if (t == typeof(object)) return "PushAnyObject";
        if (t == typeof(bool)) return "PushBoolean";
        if (t == typeof(string)) return "PushString";
        if (t == typeof(float) || t == typeof(double)) return "PushNumber";
        if (t == typeof(int)
            || t == typeof(short) || t == typeof(ushort)
            || t == typeof(byte) || t == typeof(sbyte)) {
            return "PushInteger";
        }
        if (t == typeof(uint)) return "PushUnsigned";
        if (t == typeof(long)) return "PushLong";
        if (t == typeof(ulong)) return "PushULong";

        if (t == typeof(LuaCSFunction)) return "PushCSharpFunction";
        if (typeof(IList).IsAssignableFrom(t) 
            || typeof(IEnumerator).IsAssignableFrom(t) 
            || typeof(Delegate).IsAssignableFrom(t)) {
            return "PushUData";
        }

        if (t == typeof(Vector2) || t == typeof(Vector3) || t == typeof(Quaternion) || t == typeof(Color))
            return "PushUData";
        
        //if (t == typeof(Vector3) || t == typeof(Vector2) || t == typeof(Vector4) || t == typeof(Quaternion) || t == typeof(Color) || t == typeof(RaycastHit) ||
        //    t == typeof(Ray) || t == typeof(Touch) || t == typeof(Bounds)) {
        //    return "Push";
        //}
        //if (t == typeof(bool) || t.IsPrimitive || t == typeof(string) || t == typeof(LuaTable) || t == typeof(LuaCSFunction) || t == typeof(LuaFunction) ||
        //          typeof(UnityEngine.Object).IsAssignableFrom(t) || t == typeof(Type) || t == typeof(IntPtr) || typeof(Delegate).IsAssignableFrom(t) ||
        //          t == typeof(LuaStringBuffer) || typeof(UnityEngine.TrackedReference).IsAssignableFrom(t) || typeof(IEnumerator).IsAssignableFrom(t)) {
        //    return "Push";
        //}

        return "PushLightUserData";
    }

    static void DefaultConstruct()
    {
        sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tL.ChkArgsCount(0);");
        sb.AppendFormat("\t\t{0} obj = new {0}();\r\n", className);
        string str = GetPushFunction(type);
        sb.AppendFormat("\t\tL.{0}(obj);\r\n", str);
        sb.AppendLine("\t\treturn 1;");
        sb.AppendLine("\t}");
    }

    static string GetCountStr(int count)
    {
        if (count != 0) {
            return string.Format("count - {0}", count);
        }

        return "count";
    }

    static void GenGetType()
    {
        sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        sb.AppendFormat("\tstatic int {0}(IntPtr L)\r\n", "GetClassType");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tint count = L.GetTop();");
        sb.AppendLine("\t\tif (count == 0) {");
        sb.AppendLine(string.Format("\t\t\tL.PushLightUserData(typeof({0}));", className));
        sb.AppendLine("\t\t\treturn 1;");
        sb.AppendLine("\t\t} else {");
        sb.AppendLine("\t\t\treturn MetaMethods.GetType(L);");
        sb.AppendLine("\t\t}");        
        sb.AppendLine("\t}");
    }

    static void GenConstruct()
    {
        if (isStaticClass || typeof(MonoBehaviour).IsAssignableFrom(type)) {
            NoConsturct();
            return;
        }

        ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | binding);

        if (extendType != null) {
            ConstructorInfo[] ctorExtends = extendType.GetConstructors(BindingFlags.Instance | binding);

            if (ctorExtends != null && ctorExtends.Length > 0) {
                if (HasAttribute(ctorExtends[0], typeof(UseDefinedAttribute))) {
                    sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
                    sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
                    sb.AppendLine("\t{");

                    if (HasAttribute(ctorExtends[0], typeof(UseDefinedAttribute))) {
                        FieldInfo field = extendType.GetField(extendName + "Defined");
                        string strfun = field.GetValue(null) as string;
                        sb.AppendLine(strfun);
                        sb.AppendLine("\t}");
                        return;
                    }
                }
            }
        }


        if (constructors.Length == 0) {
            if (!type.IsValueType) {
                NoConsturct();
            } else {
                DefaultConstruct();
            }

            return;
        }

        List<ConstructorInfo> list = new List<ConstructorInfo>();

        for (int i = 0; i < constructors.Length; i++) {
            //c# decimal 参数类型扔掉了
            if (HasDecimal(constructors[i].GetParameters())) continue;

            if (IsObsolete(constructors[i])) {
                continue;
            }

            ConstructorInfo r = constructors[i];
            int index = list.FindIndex((p) => { return CompareMethod(p, r) >= 0; });

            if (index >= 0) {
                if (CompareMethod(list[index], r) == 2) {
                    list.RemoveAt(index);
                    list.Add(r);
                }
            } else {
                list.Add(r);
            }
        }

        if (list.Count == 0) {
            if (!type.IsValueType) {
                NoConsturct();
            } else {
                DefaultConstruct();
            }

            return;
        }

        list.Sort(Compare);

        sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        sb.AppendFormat("\tstatic int _Create{0}(IntPtr L)\r\n", wrapClassName);
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tint count = L.GetTop();");
        sb.AppendLine();

        List<ConstructorInfo> countList = new List<ConstructorInfo>();

        for (int i = 0; i < list.Count; i++) {
            int index = list.FindIndex((p) => { return p != list[i] && p.GetParameters().Length == list[i].GetParameters().Length; });

            if (index >= 0 || (HasOptionalParam(list[i].GetParameters()) && list[i].GetParameters().Length > 1)) {
                countList.Add(list[i]);
            }
        }

        //if (countList.Count > 1)
        //{
        //    sb.AppendLine();
        //}

        MethodBase md = list[0];
        bool hasEmptyCon = list[0].GetParameters().Length == 0 ? true : false;

        //处理重载构造函数
        if (HasOptionalParam(md.GetParameters())) {
            ParameterInfo[] paramInfos = md.GetParameters();
            ParameterInfo param = paramInfos[paramInfos.Length - 1];
            string str = GetTypeStr(param.ParameterType.GetElementType());

            if (paramInfos.Length > 1) {
                string strParams = GenParamTypes(paramInfos, true);
                sb.AppendFormat("\t\tif (L.CheckTypes(1, {0}) && L.CheckParamsType(typeof({1}), {2}, {3}))\r\n", strParams, str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
            } else {
                sb.AppendFormat("\t\tif (L.CheckParamsType(typeof({0}), {1}, {2}))\r\n", str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
            }
        } else {
            ParameterInfo[] paramInfos = md.GetParameters();

            if (list.Count == 1 || md.GetParameters().Length != list[1].GetParameters().Length) {
                sb.AppendFormat("\t\tif (count == {0})\r\n", paramInfos.Length);
            } else {
                string strParams = GenParamTypes(paramInfos, true);
                sb.AppendFormat("\t\tif (count == {0} && L.CheckTypes(1, {1}))\r\n", paramInfos.Length, strParams);
            }
        }

        sb.AppendLine("\t\t{");
        int rc = ProcessParams(md, 3, true, list.Count > 1);
        sb.AppendFormat("\t\t\treturn {0};\r\n", rc);
        sb.AppendLine("\t\t}");

        for (int i = 1; i < list.Count; i++) {
            hasEmptyCon = list[i].GetParameters().Length == 0 ? true : hasEmptyCon;
            md = list[i];
            ParameterInfo[] paramInfos = md.GetParameters();

            if (!HasOptionalParam(md.GetParameters())) {
                if (countList.Contains(list[i])) {
                    string strParams = GenParamTypes(paramInfos, true);
                    sb.AppendFormat("\t\telse if (count == {0} && L.CheckTypes(1, {1}))\r\n", paramInfos.Length, strParams);
                } else {
                    sb.AppendFormat("\t\telse if (count == {0})\r\n", paramInfos.Length);
                }
            } else {
                ParameterInfo param = paramInfos[paramInfos.Length - 1];
                string str = GetTypeStr(param.ParameterType.GetElementType());

                if (paramInfos.Length > 1) {
                    string strParams = GenParamTypes(paramInfos, true);
                    sb.AppendFormat("\t\telse if (L.CheckTypes(1, {0}) && L.CheckParamsType(typeof({1}), {2}, {3}))\r\n", strParams, str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
                } else {
                    sb.AppendFormat("\t\telse if (L.CheckParamsType(typeof({0}), {1}, {2}))\r\n", str, paramInfos.Length, GetCountStr(paramInfos.Length - 1));
                }
            }

            sb.AppendLine("\t\t{");
            rc = ProcessParams(md, 3, true, true);
            sb.AppendFormat("\t\t\treturn {0};\r\n", rc);
            sb.AppendLine("\t\t}");
        }

        if (type.IsValueType && !hasEmptyCon) {
            sb.AppendLine("\t\telse if (count == 0)");
            sb.AppendLine("\t\t{");
            sb.AppendFormat("\t\t\t{0} obj = new {0}();\r\n", className);
            string str = GetPushFunction(type);
            sb.AppendFormat("\t\t\tL.{0}(obj);\r\n", str);
            sb.AppendLine("\t\t\treturn 1;");
            sb.AppendLine("\t\t}");
        }

        sb.AppendLine("\t\telse");
        sb.AppendLine("\t\t{");
        sb.AppendFormat("\t\t\tLuaDLL.luaL_error(L, \"invalid arguments to method: {0}.New\");\r\n", className);
        sb.AppendLine("\t\t}");

        sb.AppendLine();
        sb.AppendLine("\t\treturn 0;");
        sb.AppendLine("\t}");
    }


    static int GetOptionalParamPos(ParameterInfo[] infos)
    {
        for (int i = 0; i < infos.Length; i++) {
            if (IsParams(infos[i])) {
                return i;
            }
        }

        return -1;
    }

    static int Compare(MethodBase lhs, MethodBase rhs)
    {
        int off1 = lhs.IsStatic ? 0 : 1;
        int off2 = rhs.IsStatic ? 0 : 1;

        ParameterInfo[] lp = lhs.GetParameters();
        ParameterInfo[] rp = rhs.GetParameters();

        int pos1 = GetOptionalParamPos(lp);
        int pos2 = GetOptionalParamPos(rp);

        if (pos1 >= 0 && pos2 < 0) {
            return 1;
        } else if (pos1 < 0 && pos2 >= 0) {
            return -1;
        } else if (pos1 >= 0 && pos2 >= 0) {
            pos1 += off1;
            pos2 += off2;

            if (pos1 != pos2) {
                return pos1 > pos2 ? -1 : 1;
            } else {
                pos1 -= off1;
                pos2 -= off2;

                if (lp[pos1].ParameterType.GetElementType() == typeof(object) && rp[pos2].ParameterType.GetElementType() != typeof(object)) {
                    return 1;
                } else if (lp[pos1].ParameterType.GetElementType() != typeof(object) && rp[pos2].ParameterType.GetElementType() == typeof(object)) {
                    return -1;
                }
            }
        }

        int c1 = off1 + lp.Length;
        int c2 = off2 + rp.Length;

        if (c1 > c2) {
            return 1;
        } else if (c1 == c2) {
            List<ParameterInfo> list1 = new List<ParameterInfo>(lp);
            List<ParameterInfo> list2 = new List<ParameterInfo>(rp);

            if (list1.Count > list2.Count) {
                if (list1[0].ParameterType == typeof(object)) {
                    return 1;
                }

                list1.RemoveAt(0);
            } else if (list2.Count > list1.Count) {
                if (list2[0].ParameterType == typeof(object)) {
                    return -1;
                }

                list2.RemoveAt(0);
            }

            for (int i = 0; i < list1.Count; i++) {
                if (list1[i].ParameterType == typeof(object) && list2[i].ParameterType != typeof(object)) {
                    return 1;
                } else if (list1[i].ParameterType != typeof(object) && list2[i].ParameterType == typeof(object)) {
                    return -1;
                }
            }

            return 0;
        } else {
            return -1;
        }
    }

    static bool HasOptionalParam(ParameterInfo[] infos)
    {
        for (int i = 0; i < infos.Length; i++) {
            if (IsParams(infos[i])) {
                return true;
            }
        }

        return false;
    }

    static Type GetRefBaseType(string str)
    {
        int index = str.IndexOf("&");
        string ss = index >= 0 ? str.Remove(index) : str;
        Type t = Type.GetType(ss);

        if (t == null) {
            t = Type.GetType(ss + ", UnityEngine");
        }

        if (t == null) {
            t = Type.GetType(ss + ", Assembly-CSharp-firstpass");
        }

        return t;
    }


    private static string ProcessParam(string luaType, string step, string argName, int stackPos, bool beCheckTypes, string castType = null)
    {
        if (castType != null) {
            castType = "(" + castType + ")";
        }
        if (beCheckTypes) {
            return string.Format("{0}var {1} = {4}L.To{2}({3});", step, argName, luaType, stackPos, castType);
        } else {
            return string.Format("{0}var {1} = {4}L.Chk{2}({3});", step, argName, luaType, stackPos, castType);
        }
    }

    static int ProcessParams(MethodBase md, int tab, bool beConstruct, bool beLuaString, bool beCheckTypes = false)
    {
        ParameterInfo[] paramInfos = md.GetParameters();
        int count = paramInfos.Length;
        string head = string.Empty;

        for (int i = 0; i < tab; i++) {
            head += "\t";
        }

        if (!md.IsStatic && !beConstruct) {
            if (md.Name == "Equals") {
                if (!type.IsValueType) {
                    sb.AppendFormat("{0}{1} obj = L.ToAnyObject(1) as {1};\r\n", head, className);
                } else {
                    sb.AppendFormat("{0}{1} obj = ({1})L.ToAnyObject(1);\r\n", head, className);
                }
            } else if (className != "Type" && className != "System.Type") {
                if (typeof(UnityEngine.Object).IsAssignableFrom(type)) {
                    sb.AppendFormat("{0}{1} obj = ({1})L.ChkUnityObjectSelf(1, \"{1}\");\r\n", head, className);
                } else if (typeof(UnityEngine.TrackedReference).IsAssignableFrom(type)) {
                    sb.AppendFormat("{0}{1} obj = ({1})L.ChkTrackedObjectSelf(1, \"{1}\");\r\n", head, className);
                } else {
                    sb.AppendFormat("{0}{1} obj = ({1})L.ChkUserDataSelf(1, \"{1}\");\r\n", head, className);
                }
            } else {
                sb.AppendFormat("{0}{1} obj = L.ChkTypeObject(1);\r\n", head, className);
            }
        }

        for (int j = 0; j < count; j++) {
            ParameterInfo param = paramInfos[j];
            string str = GetTypeStr(param.ParameterType);

            string arg = "arg" + j;
            int offset = (md.IsStatic || beConstruct) ? 1 : 2;

            if (param.Attributes == ParameterAttributes.Out) {
                Type outType = GetRefBaseType(param.ParameterType.ToString());

                if (outType.IsValueType) {
                    sb.AppendFormat("{0}{1} {2};\r\n", head, str, arg);
                } else {
                    sb.AppendFormat("{0}{1} {2} = null;\r\n", head, str, arg);
                }
            } else if (param.ParameterType == typeof(bool)) {
                sb.AppendLine(ProcessParam("Boolean", head, arg, j + offset, beCheckTypes));
            } else if (param.ParameterType == typeof(string)) {
                sb.AppendLine(ProcessParam("LuaString", head, arg, j + offset, !beLuaString));
            } else if (param.ParameterType.IsEnum) {
                sb.AppendFormat("{0}var {1} = ({3})L.ChkEnumValue({2}, typeof({3}));", head, arg, j + offset, str).AppendLine();
            } else if (param.ParameterType.IsPrimitive) {
                if (param.ParameterType == typeof(long)) {
                    sb.AppendLine(ProcessParam("Long", head, arg, j + offset, beCheckTypes));
                } else if (param.ParameterType == typeof(ulong)) {
                    sb.AppendLine(ProcessParam("ULong", head, arg, j + offset, beCheckTypes));
                } else {
                    sb.AppendLine(ProcessParam("Number", head, arg, j + offset, beCheckTypes, str));
                }
            } else if (param.ParameterType == typeof(LuaFunction)) {
                sb.AppendLine(ProcessParam("LuaFunction", head, arg, j + offset, beCheckTypes));
            } else if (param.ParameterType.IsSubclassOf(typeof(System.MulticastDelegate))) {
                sb.AppendFormat("{0}{1} {2} = null;\r\n", head, str, arg);
                sb.AppendFormat("{0}LuaTypes funcType{1} = L.Type({1});\r\n", head, j + offset);
                sb.AppendLine();
                sb.AppendFormat("{0}if (funcType{1} != LuaTypes.LUA_TFUNCTION)\r\n", head, j + offset);
                sb.AppendLine(head + "{");

                if (beCheckTypes) {
                    sb.AppendFormat("{3} {1} = ({0})L.ToUserData({2});\r\n", str, arg, j + offset, head + "\t");
                } else {
                    sb.AppendFormat("{3} {1} = ({0})L.ChkUserData({2}, typeof({0}));\r\n", str, arg, j + offset, head + "\t");
                }

                sb.AppendFormat("{0}}}\r\n{0}else\r\n{0}{{\r\n", head);
                sb.AppendFormat("{0}\tLuaFunction func = L.ChkLuaFunction({1});\r\n", head, j + offset);
                sb.AppendFormat("{0}\t{1} = ", head, arg);

                GenDelegateBody(param.ParameterType, head + "\t", true);
                sb.AppendLine(head + "}\r\n");
            } else if (param.ParameterType == typeof(LuaTable)) {
                sb.AppendLine(ProcessParam("LuaTable", head, arg, j + offset, beCheckTypes));
            } else if (param.ParameterType == typeof(Vector2) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Vector2)) {
                sb.AppendFormat("{2}var {0} = L.ToVector2({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(Vector3) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Vector3)) {
                sb.AppendFormat("{2}var {0} = L.ToVector3({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(Vector4) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Vector4)) {
                sb.AppendFormat("{2}var {0} = L.ToVector4({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(Quaternion) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Quaternion)) {
                sb.AppendFormat("{2}var {0} = L.ToQuaternion({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(Color) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Color)) {
                sb.AppendFormat("{2}var {0} = L.ToColor({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(Ray) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Ray)) {
                sb.AppendFormat("{2}var {0} = L.ToRay({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(Bounds) || GetRefBaseType(param.ParameterType.ToString()) == typeof(Bounds)) {
                sb.AppendFormat("{2}var {0} = L.ToBounds({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(object)) {
                sb.AppendFormat("{2}var {0} = L.ToAnyObject({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(Type)) {
                sb.AppendFormat("{0}{1} {2} = L.ChkTypeObject({3});\r\n", head, str, arg, j + offset);
            } else if (param.ParameterType == typeof(LuaStringBuffer)) {
                sb.AppendFormat("{2}var {0} = L.ChkStringBuffer({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(TinyJSON.Variant)) {
                sb.AppendFormat("{2}var {0} = L.ToJsonObj({1});\r\n", arg, j + offset, head);
            } else if (param.ParameterType == typeof(GameObject)) {
                sb.AppendFormat("{2}var {0} = L.ToGameObject({1});\r\n", arg, j + offset, head);
            } else if (typeof(Component).IsAssignableFrom(param.ParameterType)) {
                sb.AppendFormat("{2}var {0} = L.ToComponent({1}, typeof({3})) as {3};\r\n", arg, j + offset, head, param.ParameterType.FullName);
            } else if (param.ParameterType.IsArray) {
                Type et = param.ParameterType.GetElementType();
                string atstr = GetTypeStr(et);
                string fname = "GetArrayObject";
                bool flag = false;
                bool optional = false;
                bool isObject = false;

                if (et == typeof(bool)) {
                    fname = "ToArrayBoolean";
                } else if (et.IsPrimitive) {
                    flag = true;
                    fname = "ToArrayNumber";
                } else if (et == typeof(string)) {
                    optional = IsParams(param);
                    fname = optional ? "ToParamsString" : "ToArrayString";
                } else //if (et == typeof(object))
                  {
                    flag = true;
                    optional = IsParams(param);
                    fname = optional ? "ToParamsObject" : "ToArrayObject";

                    if (et == typeof(object)) {
                        isObject = true;
                    }

                    if (et == typeof(UnityEngine.Object)) {
                        ambig |= ObjAmbig.U3dObj;
                    }
                }

                if (flag) {
                    if (optional) {
                        if (!isObject) {
                            sb.AppendFormat("{5}{0}[] objs{2} = L.{4}<{0}>({1}, {3});\r\n", atstr, j + offset, j, GetCountStr(j + offset - 1), fname, head);
                        } else {
                            sb.AppendFormat("{4}object[] objs{1} = L.{3}({0}, {2});\r\n", j + offset, j, GetCountStr(j + offset - 1), fname, head);
                        }
                    } else {
                        sb.AppendFormat("{4}{0}[] objs{2} = L.{3}<{0}>({1});\r\n", atstr, j + offset, j, fname, head);
                    }
                } else {
                    if (optional) {
                        sb.AppendFormat("{5}{0}[] objs{2} = L.{4}({1}, {3});\r\n", atstr, j + offset, j, GetCountStr(j + offset - 1), fname, head);
                    } else {
                        sb.AppendFormat("{5}{0}[] objs{2} = L.{4}({1});\r\n", atstr, j + offset, j, j + offset - 1, fname, head);
                    }
                }
            } else //if (param.ParameterType == typeof(object))
              {
                if (md.Name == "op_Equality") {
                    if (!type.IsValueType) // && type.IsEnum)
                    {
                        sb.AppendFormat("{3}{0} {1} = L.ToUserData({2}) as {0};\r\n", str, arg, j + offset, head);
                    } else {
                        sb.AppendFormat("{3}{0} {1} = ({0})L.ToAnyObject({2});\r\n", str, arg, j + offset, head);
                    }
                } else {
                    if (beCheckTypes) {
                        sb.AppendFormat("{3}{0} {1} = ({0})L.ToUserData({2});\r\n", str, arg, j + offset, head);
                    } else if (typeof(UnityEngine.Object).IsAssignableFrom(param.ParameterType)) {
                        sb.AppendFormat("{3}{0} {1} = ({0})L.ChkUnityObject({2}, typeof({0}));\r\n", str, arg, j + offset, head);
                    } else if (typeof(UnityEngine.TrackedReference).IsAssignableFrom(param.ParameterType)) {
                        sb.AppendFormat("{3}{0} {1} = ({0})L.ChkTrackedObject({2}, typeof({0}));\r\n", str, arg, j + offset, head);
                    } else {
                        sb.AppendFormat("{3}{0} {1} = ({0})L.ChkUserData({2}, typeof({0}));\r\n", str, arg, j + offset, head);
                    }
                }
            }
        }

        StringBuilder sbArgs = new StringBuilder();
        List<string> refList = new List<string>();
        List<Type> refTypes = new List<Type>();

        for (int j = 0; j < count - 1; j++) {
            ParameterInfo param = paramInfos[j];

            if (!param.ParameterType.IsArray) {
                if (!param.ParameterType.ToString().Contains("&")) {
                    sbArgs.Append("arg");
                } else {
                    if (param.Attributes == ParameterAttributes.Out) {
                        sbArgs.Append("out arg");
                    } else {
                        sbArgs.Append("ref arg");
                    }

                    refList.Add("arg" + j);
                    refTypes.Add(GetRefBaseType(param.ParameterType.ToString()));
                }
            } else {
                sbArgs.Append("objs");
            }

            sbArgs.Append(j);
            sbArgs.Append(",");
        }

        if (count > 0) {
            ParameterInfo param = paramInfos[count - 1];

            if (!param.ParameterType.IsArray) {
                if (!param.ParameterType.ToString().Contains("&")) {
                    sbArgs.Append("arg");
                } else {
                    if (param.Attributes == ParameterAttributes.Out) {
                        sbArgs.Append("out arg");
                    } else {
                        sbArgs.Append("ref arg");
                    }

                    refList.Add("arg" + (count - 1));
                    refTypes.Add(GetRefBaseType(param.ParameterType.ToString()));
                }
            } else {
                sbArgs.Append("objs");
            }

            sbArgs.Append(count - 1);
        }

        if (beConstruct) {
            sb.AppendFormat("{2}{0} obj = new {0}({1});\r\n", className, sbArgs.ToString(), head);
            string str = GetPushFunction(type);
            sb.AppendFormat("{0}L.{1}(obj);\r\n", head, str);

            for (int i = 0; i < refList.Count; i++) {
                str = GetPushFunction(refTypes[i]);
                sb.AppendFormat("{1}L.{2}({0});\r\n", refList[i], head, str);
            }

            return refList.Count + 1;
        }

        string obj = md.IsStatic ? className : "obj";
        MethodInfo m = md as MethodInfo;

        if (m.ReturnType == typeof(void)) {
            if (md.Name == "set_Item") {
                if (count == 2) {
                    sb.AppendFormat("{0}{1}[arg0] = arg1;\r\n", head, obj);
                } else if (count == 3) {
                    sb.AppendFormat("{0}{1}[arg0, arg1] = arg2;\r\n", head, obj);
                }
            } else {
                sb.AppendFormat("{3}{0}.{1}({2});\r\n", obj, md.Name, sbArgs.ToString(), head);
            }

            if (!md.IsStatic && type.IsValueType) {
                sb.AppendFormat("{0}L.SetValue(1, obj);\r\n", head);
            }
        } else {
            string ret = GetTypeStr(m.ReturnType);

            if (md.Name.Contains("op_")) {
                CallOpFunction(md.Name, tab, ret);
            } else if (md.Name == "get_Item") {
                sb.AppendFormat("{4}{3} o = {0}[{2}];\r\n", obj, md.Name, sbArgs.ToString(), ret, head);
            } else if (md.Name == "Equals") {
                if (type.IsValueType) {
                    sb.AppendFormat("{0}bool o = obj.Equals(arg0);\r\n", head);
                } else {
                    sb.AppendFormat("{0}bool o = obj != null ? obj.Equals(arg0) : arg0 == null;\r\n", head);
                }
            } else {
                sb.AppendFormat("{4}{3} o = {0}.{1}({2});\r\n", obj, md.Name, sbArgs.ToString(), ret, head);
            }

            string str = GetPushFunction(m.ReturnType);
            sb.AppendFormat("{0}L.{1}(o);\r\n", head, str);
        }

        for (int i = 0; i < refList.Count; i++) {
            string str = GetPushFunction(refTypes[i]);
            sb.AppendFormat("{1}L.{2}({0});\r\n", refList[i], head, str);
        }

        return refList.Count;
    }

    static bool CompareParmsCount(MethodBase l, MethodBase r)
    {
        if (l == r) {
            return false;
        }

        int c1 = l.IsStatic ? 0 : 1;
        int c2 = r.IsStatic ? 0 : 1;

        c1 += l.GetParameters().Length;
        c2 += r.GetParameters().Length;

        return c1 == c2;
    }

    //decimal 类型扔掉了
    static Dictionary<Type, int> typeSize = new Dictionary<Type, int>()
    {
        {typeof(bool), 1},
        { typeof(char), 2},
        { typeof(byte), 3 },
        { typeof(sbyte), 4 },
        { typeof(ushort),5 },
        { typeof(short), 6 },
        { typeof(uint), 7 },
        {typeof(int), 8},
        {typeof(float), 9},
        { typeof(ulong), 10},
        { typeof(long), 11},
        { typeof(double), 12 },

    };

    //-1 不存在替换, 1 保留左面， 2 保留右面
    static int CompareMethod(MethodBase l, MethodBase r)
    {
        int s = 0;

        if (!CompareParmsCount(l, r)) {
            return -1;
        } else {
            ParameterInfo[] lp = l.GetParameters();
            ParameterInfo[] rp = r.GetParameters();

            List<Type> ll = new List<Type>();
            List<Type> lr = new List<Type>();

            if (!l.IsStatic) {
                ll.Add(type);
            }

            if (!r.IsStatic) {
                lr.Add(type);
            }

            for (int i = 0; i < lp.Length; i++) {
                ll.Add(lp[i].ParameterType);
            }

            for (int i = 0; i < rp.Length; i++) {
                lr.Add(rp[i].ParameterType);
            }

            for (int i = 0; i < ll.Count; i++) {
                if (!typeSize.ContainsKey(ll[i]) || !typeSize.ContainsKey(lr[i])) {
                    if (ll[i] == lr[i]) {
                        continue;
                    } else {
                        return -1;
                    }
                } else if (ll[i].IsPrimitive && lr[i].IsPrimitive && s == 0) {
                    s = typeSize[ll[i]] >= typeSize[lr[i]] ? 1 : 2;
                } else if (ll[i] != lr[i]) {
                    return -1;
                }
            }

            if (s == 0 && l.IsStatic) {
                s = 2;
            }
        }

        return s;
    }

    static void Push(List<MethodInfo> list, MethodInfo r)
    {
        int index = list.FindIndex((p) => { return p.Name == r.Name && CompareMethod(p, r) >= 0; });

        if (index >= 0) {
            if (CompareMethod(list[index], r) == 2) {
                list.RemoveAt(index);
                list.Add(r);
                return;
            } else {
                return;
            }
        }

        list.Add(r);
    }

    static bool HasDecimal(ParameterInfo[] pi)
    {
        for (int i = 0; i < pi.Length; i++) {
            if (pi[i].ParameterType == typeof(decimal)) {
                return true;
            }
        }

        return false;
    }

    public static MethodInfo GenOverrideFunc(string name)
    {
        List<MethodInfo> list = new List<MethodInfo>();

        for (int i = 0; i < methods.Length; i++) {
            if (methods[i].Name == name && !methods[i].IsGenericMethod && !HasDecimal(methods[i].GetParameters())) {
                Push(list, methods[i]);
            }
        }

        if (list.Count == 1) {
            return list[0];
        }

        list.Sort(Compare);

        sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        sb.AppendFormat("\tstatic int {0}(IntPtr L)\r\n", GetFuncName(name));
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tint count = LuaDLL.lua_gettop(L);");

        List<MethodInfo> countList = new List<MethodInfo>();

        for (int i = 0; i < list.Count; i++) {
            int index = list.FindIndex((p) => { return CompareParmsCount(p, list[i]); });

            if (index >= 0 || (HasOptionalParam(list[i].GetParameters()) && list[i].GetParameters().Length > 1)) {
                countList.Add(list[i]);
            }
        }

        //if (countList.Count >= 1)
        //{
        sb.AppendLine();
        //}

        MethodInfo md = list[0];
        int ret = md.ReturnType == typeof(void) ? 0 : 1;
        int offset = md.IsStatic ? 0 : 1;
        int beginPos = offset + 1;
        int c1 = md.GetParameters().Length + offset;
        int c2 = list[1].GetParameters().Length + (list[1].IsStatic ? 0 : 1);
        bool noLuaString = true;    //是否检测string转换成功
        bool beCheckTypes = true;

        if (HasOptionalParam(md.GetParameters())) {
            ParameterInfo[] paramInfos = md.GetParameters();
            ParameterInfo param = paramInfos[paramInfos.Length - 1];
            string str = GetTypeStr(param.ParameterType.GetElementType());

            if (paramInfos.Length > 1) {
                string strParams = GenParamTypes(paramInfos, md.IsStatic);
                sb.AppendFormat("\t\tif (L.CheckTypes(1, {1}) && L.CheckParamsType(typeof({2}), {3}, {4}))\r\n", beginPos, strParams, str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
            } else {
                sb.AppendFormat("\t\tif (L.CheckParamsType(typeof({0}), {1}, {2}))\r\n", str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
            }
        } else {
            if (c1 != c2) {
                sb.AppendFormat("\t\tif (count == {0})\r\n", md.GetParameters().Length + offset);
                noLuaString = false;
                beCheckTypes = false;
            } else {
                ParameterInfo[] paramInfos = md.GetParameters();

                if (paramInfos.Length > 0) {
                    string strParams = GenParamTypes(paramInfos, md.IsStatic);
                    sb.AppendFormat("\t\tif (count == {0} && L.CheckTypes(1, {2}))\r\n", paramInfos.Length + offset, beginPos, strParams);
                } else {
                    sb.AppendFormat("\t\tif (count == {0})\r\n", paramInfos.Length + offset);
                }
            }
        }

        sb.AppendLine("\t\t{");
        int count = ProcessParams(md, 3, false, (list.Count > 1) && noLuaString, beCheckTypes);
        sb.AppendFormat("\t\t\treturn {0};\r\n", ret + count);
        sb.AppendLine("\t\t}");
        //int offset = md.IsStatic ? 1 : 2;        

        for (int i = 1; i < list.Count; i++) {
            noLuaString = true;
            beCheckTypes = true;
            md = list[i];
            offset = md.IsStatic ? 0 : 1;
            beginPos = offset + 1;
            ret = md.ReturnType == typeof(void) ? 0 : 1;

            if (!HasOptionalParam(md.GetParameters())) {
                ParameterInfo[] paramInfos = md.GetParameters();

                if (countList.Contains(list[i])) {
                    string strParams = GenParamTypes(paramInfos, md.IsStatic);
                    sb.AppendFormat("\t\telse if (count == {0} && L.CheckTypes(1, {2}))\r\n", paramInfos.Length + offset, beginPos, strParams);
                } else {
                    sb.AppendFormat("\t\telse if (count == {0})\r\n", paramInfos.Length + offset);
                    noLuaString = false;
                    beCheckTypes = false;
                }
            } else {
                ParameterInfo[] paramInfos = md.GetParameters();
                ParameterInfo param = paramInfos[paramInfos.Length - 1];
                string str = GetTypeStr(param.ParameterType.GetElementType());

                if (paramInfos.Length > 1) {
                    string strParams = GenParamTypes(paramInfos, md.IsStatic);
                    sb.AppendFormat("\t\telse if (L.CheckTypes(1, {1}) && L.CheckParamsType(typeof({2}), {3}, {4}))\r\n", beginPos, strParams, str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
                } else {
                    sb.AppendFormat("\t\telse if (L.CheckParamsType(typeof({0}), {1}, {2}))\r\n", str, paramInfos.Length + offset, GetCountStr(paramInfos.Length + offset - 1));
                }
            }

            sb.AppendLine("\t\t{");
            count = ProcessParams(md, 3, false, noLuaString, beCheckTypes);
            sb.AppendFormat("\t\t\treturn {0};\r\n", ret + count);
            sb.AppendLine("\t\t}");
        }

        sb.AppendLine("\t\telse");
        sb.AppendLine("\t\t{");
        sb.AppendFormat("\t\t\tLuaDLL.luaL_error(L, \"invalid arguments to method: {0}.{1}\");\r\n", className, name);
        sb.AppendLine("\t\t}");

        sb.AppendLine();
        sb.AppendLine("\t\treturn 0;");
        sb.AppendLine("\t}");

        return null;
    }

    private static string[] GetGenericName(Type[] types)
    {
        string[] results = new string[types.Length];

        for (int i = 0; i < types.Length; i++) {
            if (types[i].IsGenericType) {
                results[i] = GetGenericName(types[i]);
            } else {
                results[i] = GetTypeStr(types[i]);
            }

        }

        return results;
    }
    
    /// <summary>
    /// 模版类型名字
    /// </summary>
    static string GetGenericName(Type t)
    {
        Type[] gArgs = t.GetGenericArguments();
        string typeName = t.FullName;
        string pureTypeName = typeName.Substring(0, typeName.IndexOf('`'));
        pureTypeName = _C(pureTypeName);

        if (typeName.Contains("+")) {
            int pos1 = typeName.IndexOf("+");
            int pos2 = typeName.IndexOf("[");

            if (pos2 > pos1) {
                string add = typeName.Substring(pos1 + 1, pos2 - pos1 - 1);
                return pureTypeName + "<" + string.Join(",", GetGenericName(gArgs)) + ">." + add;
            } else {
                return pureTypeName + "<" + string.Join(",", GetGenericName(gArgs)) + ">";
            }
        } else {
            return pureTypeName + "<" + string.Join(",", GetGenericName(gArgs)) + ">";
        }
    }

    /// <summary>
    /// 获取类型名字
    /// </summary>
    public static string GetTypeStr(Type t)
    {
        if (t.IsArray) {
            // 数组: TypeName[]
            t = t.GetElementType();
            string str = GetTypeStr(t);
            str += "[]";
            return str;
        } else if (t.IsGenericType) {
            // 模版
            return GetGenericName(t);
        } else {
            // 
            return _C(t.ToString());
        }
    }

    public static string _C(string str)
    {
        if (str.Length > 1 && str[str.Length - 1] == '&') {
            str = str.Remove(str.Length - 1);
        }

        // 分别获取命名空间和类型名
        var lastDot = str.LastIndexOf('.');
        string nsName, typeName;
        if (lastDot < 0) {
            nsName = "";
            typeName = str;
        } else {
            nsName = str.Substring(0, lastDot);
            typeName = str.Substring(lastDot + 1);
        }

        if (nsName == "System") {
            switch (typeName) {
                case "Single": return "float";
                case "String": return "string";
                case "Int32": return "int";
                case "UInt32": return "uint";
                case "Int64": return "long";
                case "UInt64": return "ulong";
                case "SByte": return "sbyte";
                case "Byte": return "byte";
                case "Int16": return "short";
                case "UInt16": return "ushort";
                case "Char": return "char";
                case "Decimal": return "decimal";
                case "Double": return "double";
                case "Boolean": return "bool";
                case "Object": return "object";
                default: str = typeName; break;
            }            
        } else if (nsName == "UnityEngine") {
            if (!usingList.Contains(nsName)) usingList.Add(nsName);
            if (typeName == "Object") {
                ambig |= ObjAmbig.U3dObj;
            }
            str = typeName;
        } else {
            if (nsName == "System.Collections" || nsName == "System.Collections.Generic") {
                if (!usingList.Contains(nsName)) usingList.Add(nsName);
                str = typeName;
            }
        }

        //if (str.Contains(".")) {
        //    int pos1 = str.LastIndexOf('.');
        //    string nameSpace = str.Substring(0, pos1);

        //    if (str.Length > 12 && str.Substring(0, 12) == "UnityEngine.") {
        //        if (nameSpace == "UnityEngine") {
        //            usingList.Add("UnityEngine");
        //        }

        //        if (str == "UnityEngine.Object") {
        //            ambig |= ObjAmbig.U3dObj;
        //        }
        //    } else if (str.Length > 7 && str.Substring(0, 7) == "System.") {
        //        if (nameSpace == "System.Collections") {
        //            usingList.Add(nameSpace);
        //        } else if (nameSpace == "System.Collections.Generic") {
        //            usingList.Add(nameSpace);
        //        } else if (nameSpace == "System") {
        //            usingList.Add(nameSpace);
        //        }                
        //    }
            
        //    if (usingList.Contains(nameSpace)) {
        //        str = str.Substring(pos1 + 1);
        //    }            
        //}


        if (str.Contains("+")) {
            return str.Replace('+', '.');
        }

        if (str == extendName) {
            return GetTypeStr(type);
        }

        return str;
    }

    static bool IsLuaTableType(Type t)
    {
        if (t.IsArray) {
            t = t.GetElementType();
        }

        return t == typeof(Vector3) || t == typeof(Vector2) || t == typeof(Vector4) || t == typeof(Quaternion) || t == typeof(Color) || t == typeof(Ray) || t == typeof(Bounds);
    }

    static string GetTypeOf(Type t, string sep)
    {
        string str;

        if (t == null) {
            str = string.Format("null{0}", sep);
        } else if (IsLuaTableType(t)) {
            str = string.Format("typeof(LuaTable{1}){0}", sep, t.IsArray ? "[]" : "");
        } else {
            str = string.Format("typeof({0}){1}", GetTypeStr(t), sep);
        }

        return str;
    }

    //生成 CheckTypes() 里面的参数列表
    static string GenParamTypes(ParameterInfo[] p, bool isStatic)
    {
        StringBuilder sb = new StringBuilder();
        List<Type> list = new List<Type>();

        if (!isStatic) {
            list.Add(type);
        }

        for (int i = 0; i < p.Length; i++) {
            if (IsParams(p[i])) {
                continue;
            }

            if (p[i].Attributes != ParameterAttributes.Out) {
                list.Add(p[i].ParameterType);
            } else {
                list.Add(null);
            }
        }

        for (int i = 0; i < list.Count - 1; i++) {
            sb.Append(GetTypeOf(list[i], ", "));
        }

        sb.Append(GetTypeOf(list[list.Count - 1], ""));
        return sb.ToString();
    }

    static void CheckObjectNull()
    {
        if (type.IsValueType) {
            sb.AppendLine("\t\tif (o == null)");
        } else {
            sb.AppendLine("\t\tif (obj == null)");
        }
    }

    static void GenIndexFunc()
    {
        for (int i = 0; i < fields.Length; i++) {
            sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
            sb.AppendFormat("\tstatic int get_{0}(IntPtr L)\r\n", fields[i].Name);
            sb.AppendLine("\t{");

            string str = GetPushFunction(fields[i].FieldType);

            if (fields[i].IsStatic) {
                sb.AppendFormat("\t\tL.{2}({0}.{1});\r\n", className, fields[i].Name, str);
            } else {
                sb.AppendFormat("\t\tobject o = L.ToUserData(1);\r\n");

                if (!type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }

                sb.AppendLine();
                CheckObjectNull();
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\tLuaTypes types = L.Type(1);");
                sb.AppendLine();
                sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", fields[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t\telse");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", fields[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t}");
                sb.AppendLine();

                if (type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }

                sb.AppendFormat("\t\tL.{1}(obj.{0});\r\n", fields[i].Name, str);
            }

            sb.AppendLine("\t\treturn 1;");
            sb.AppendLine("\t}");
        }

        for (int i = 0; i < props.Length; i++) {
            if (!props[i].CanRead) {
                continue;
            }

            bool isStatic = true;
            int index = propList.IndexOf(props[i]);

            if (index >= 0) {
                isStatic = false;
            }

            sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
            sb.AppendFormat("\tstatic int get_{0}(IntPtr L)\r\n", props[i].Name);
            sb.AppendLine("\t{");

            string str = GetPushFunction(props[i].PropertyType);

            if (isStatic) {
                sb.AppendFormat("\t\tL.{2}({0}.{1});\r\n", className, props[i].Name, str);
            } else {
                sb.AppendFormat("\t\tobject o = L.ToUserData(1);\r\n");

                if (!type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }

                sb.AppendLine();
                CheckObjectNull();
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\tLuaTypes types = L.Type(1);");
                sb.AppendLine();
                sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", props[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t\telse");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", props[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t}");
                sb.AppendLine();

                if (type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }

                sb.AppendFormat("\t\tL.{1}(obj.{0});\r\n", props[i].Name, str);
            }

            sb.AppendLine("\t\treturn 1;");
            sb.AppendLine("\t}");
        }
    }

    static void GenNewIndexFunc()
    {
        for (int i = 0; i < fields.Length; i++) {
            if (fields[i].IsLiteral || fields[i].IsInitOnly || fields[i].IsPrivate) {
                continue;
            }

            sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
            sb.AppendFormat("\tstatic int set_{0}(IntPtr L)\r\n", fields[i].Name);
            sb.AppendLine("\t{");
            string o = fields[i].IsStatic ? className : "obj";

            if (!fields[i].IsStatic) {
                sb.AppendFormat("\t\tobject o = L.ToUserData(1);\r\n");

                if (!type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }

                sb.AppendLine();
                CheckObjectNull();
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\tLuaTypes types = L.Type(1);");
                sb.AppendLine();
                sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", fields[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t\telse");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", fields[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t}");

                sb.AppendLine();

                if (type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }
            }

            NewIndexSetValue(fields[i].FieldType, o, fields[i].Name);

            if (!fields[i].IsStatic && type.IsValueType) {
                sb.AppendLine("\t\tL.SetValue(1, obj);");
            }

            sb.AppendLine("\t\treturn 0;");
            sb.AppendLine("\t}");
        }

        for (int i = 0; i < props.Length; i++) {
            if (!props[i].CanWrite || !props[i].GetSetMethod(true).IsPublic) {
                continue;
            }

            bool isStatic = true;
            int index = propList.IndexOf(props[i]);

            if (index >= 0) {
                isStatic = false;
            }

            sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
            sb.AppendFormat("\tstatic int set_{0}(IntPtr L)\r\n", props[i].Name);
            sb.AppendLine("\t{");
            string o = isStatic ? className : "obj";

            if (!isStatic) {
                sb.AppendFormat("\t\tobject o = L.ToUserData(1);\r\n");

                if (!type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }

                sb.AppendLine();
                CheckObjectNull();
                sb.AppendLine("\t\t{");
                sb.AppendLine("\t\t\tLuaTypes types = L.Type(1);");
                sb.AppendLine();
                sb.AppendLine("\t\t\tif (types == LuaTypes.LUA_TTABLE)");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"unknown member name {0}\");\r\n", props[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t\telse");
                sb.AppendLine("\t\t\t{");
                sb.AppendFormat("\t\t\t\tLuaDLL.luaL_error(L, \"attempt to index {0} on a nil value\");\r\n", props[i].Name);
                sb.AppendLine("\t\t\t}");
                sb.AppendLine("\t\t}");
                sb.AppendLine();

                if (type.IsValueType) {
                    sb.AppendFormat("\t\t{0} obj = ({0})o;\r\n", className);
                }
            }

            NewIndexSetValue(props[i].PropertyType, o, props[i].Name);

            if (!isStatic && type.IsValueType) {
                sb.AppendLine("\t\tL.SetValue(1, obj);");
            }

            sb.AppendLine("\t\treturn 0;");
            sb.AppendLine("\t}");
        }
    }

    static void GenDelegateBody(Type t, string head, bool haveState)
    {
        eventSet.Add(t);
        MethodInfo mi = t.GetMethod("Invoke");
        ParameterInfo[] pi = mi.GetParameters();
        int n = pi.Length;

        if (n == 0) {
            sb.AppendLine("() =>");

            if (mi.ReturnType == typeof(void)) {
                sb.AppendFormat("{0}{{\r\n{0}\tfunc.Call();\r\n{0}}};\r\n", head);
            } else {
                sb.AppendFormat("{0}{{\r\n{0}\tobject[] objs = func.Call();\r\n", head);
                sb.AppendFormat("{1}\treturn ({0})objs[0];\r\n", GetTypeStr(mi.ReturnType), head);
                sb.AppendFormat("{0}}};\r\n", head);
            }

            return;
        }

        sb.AppendFormat("(param0");

        for (int i = 1; i < n; i++) {
            sb.AppendFormat(", param{0}", i);
        }

        sb.AppendFormat(") =>\r\n{0}{{\r\n{0}", head);
        sb.AppendLine("\tint top = func.BeginPCall();");

        if (!haveState) {
            sb.AppendFormat("{0}\tIntPtr L = func.GetLuaState();\r\n", head);
        }

        for (int i = 0; i < n; i++) {
            string push = GetPushFunction(pi[i].ParameterType);
            sb.AppendFormat("{2}\tL.{0}(param{1});\r\n", push, i, head);
        }
        sb.AppendFormat("{1}\tfunc.PCall(top, {0});\r\n", n, head);

        if (mi.ReturnType == typeof(void)) {
            sb.AppendFormat("{0}\tfunc.EndPCall(top);\r\n", head);
        } else {
            sb.AppendFormat("{0}\tobject[] objs = func.PopValues(top);\r\n", head);
            sb.AppendFormat("{0}\tfunc.EndPCall(top);\r\n", head);
            sb.AppendFormat("{1}\treturn ({0})objs[0];\r\n", GetTypeStr(mi.ReturnType), head);
        }

        sb.AppendFormat("{0}}};\r\n", head);
    }


    static void NewIndexSetValue(Type t, string o, string name)
    {
        if (t.IsArray) {
            Type et = t.GetElementType();
            string atstr = GetTypeStr(et);

            if (et == typeof(bool)) {
                sb.AppendFormat("\t\t{0}.{1} = L.ToArrayBoolean(3);\r\n", o, name);
            } else if (et.IsPrimitive) {
                sb.AppendFormat("\t\t{0}.{1} = L.ToArrayNumber<{2}>(3);\r\n", o, name, atstr);
            } else if (et == typeof(string)) {
                sb.AppendFormat("\t\t{0}.{1} = L.ToArrayString(3);\r\n", o, name);
            } else {
                if (et == typeof(UnityEngine.Object)) {
                    ambig |= ObjAmbig.U3dObj;
                }

                sb.AppendFormat("\t\t{0}.{1} = L.ToArrayObject<{2}>(3);\r\n", o, name, atstr);
            }

            return;
        }

        if (t == typeof(bool)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ChkBoolean(3);\r\n", o, name);
        } else if (t == typeof(string)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ChkLuaString(3);\r\n", o, name);
        } else if (t == typeof(long)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ChkLong(3);\r\n", o, name);
        } else if (t.IsPrimitive) {
            sb.AppendFormat("\t\t{0}.{1} = ({2})L.ChkNumber(3);\r\n", o, name, _C(t.ToString()));
        } else if (t == typeof(LuaFunction)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ChkLuaFunction(3);\r\n", o, name);
        } else if (t == typeof(LuaTable)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ChkLuaTable(3);\r\n", o, name);
        } else if (t == typeof(object)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToAnyObject(3);\r\n", o, name);
        } else if (t == typeof(Vector3)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToVector3(3);\r\n", o, name);
        } else if (t == typeof(Quaternion)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToQuaternion(3);\r\n", o, name);
        } else if (t == typeof(Vector2)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToVector2(3);\r\n", o, name);
        } else if (t == typeof(Vector4)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToVector4(3);\r\n", o, name);
        } else if (t == typeof(Color)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToColor(3);\r\n", o, name);
        } else if (t == typeof(Ray)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToRay(3);\r\n", o, name);
        } else if (t == typeof(Bounds)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToBounds(3);\r\n", o, name);
        } else if (t == typeof(LuaStringBuffer)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ChkStringBuffer(3);\r\n", o, name);
        } else if (t == typeof(TinyJSON.Variant)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToJsonObj(3);\r\n", o, name);
        } else if (t == typeof(GameObject)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToGameObject(3);\r\n", o, name);
        } else if (typeof(Component).IsAssignableFrom(t)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ToComponent(3, typeof({2})) as {2};\r\n", o, name, t.FullName);
        } else if (typeof(UnityEngine.TrackedReference).IsAssignableFrom(t)) {
            sb.AppendFormat("\t\t{0}.{1} = ({2})L.ChkTrackedObject(3, typeof(2));\r\n", o, name, GetTypeStr(t));
        } else if (typeof(UnityEngine.Object).IsAssignableFrom(t)) {
            sb.AppendFormat("\t\t{0}.{1} = ({2})L.ChkUnityObject(3, typeof({2}));\r\n", o, name, GetTypeStr(t));
        } else if (typeof(System.Delegate).IsAssignableFrom(t)) {
            sb.AppendLine("\t\tLuaTypes funcType = L.Type(3);\r\n");
            sb.AppendLine("\t\tif (funcType != LuaTypes.LUA_TFUNCTION)");
            sb.AppendLine("\t\t{");
            sb.AppendFormat("\t\t\t{0}.{1} = ({2})L.ChkUserData(3, typeof({2}));\r\n", o, name, GetTypeStr(t));
            sb.AppendLine("\t\t}\r\n\t\telse");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\tLuaFunction func = L.ToLuaFunction(3);");
            sb.AppendFormat("\t\t\t{0}.{1} = ", o, name);
            GenDelegateBody(t, "\t\t\t", true);
            sb.AppendLine("\t\t}");
        } else if (t.IsEnum){
            sb.AppendFormat("\t\t{0}.{1} = ({2})L.ChkEnumValue(3, typeof({2}));\r\n", o, name, GetTypeStr(t));
        } else if (typeof(object).IsAssignableFrom(t)) {
            sb.AppendFormat("\t\t{0}.{1} = ({2})L.ChkUserData(3, typeof({2}));\r\n", o, name, GetTypeStr(t));
        } else if (t == typeof(Type)) {
            sb.AppendFormat("\t\t{0}.{1} = L.ChkTypeObject(3);\r\n", o, name);
        } else {
            Debugger.LogError("not defined type {0}", t);
        }
    }

    static void GenToStringFunc()
    {
        int index = Array.FindIndex<MethodInfo>(methods, (p) => { return p.Name == "ToString"; });
        if (index < 0 || isStaticClass) return;

        sb.AppendLine("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        sb.AppendLine("\tstatic int Lua_ToString(IntPtr L)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tobject obj = L.ToUserData(1);\r\n");

        sb.AppendLine("\t\tif (obj != null)");
        sb.AppendLine("\t\t{");
        sb.AppendLine("\t\t\tL.PushString(obj.ToString());");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t\telse");
        sb.AppendLine("\t\t{");
        sb.AppendFormat("\t\t\tL.PushString(\"Table: {0}\");\r\n", libClassName);
        sb.AppendLine("\t\t}");
        sb.AppendLine();
        sb.AppendLine("\t\treturn 1;");
        sb.AppendLine("\t}");
    }

    static bool IsNeedOp(string name)
    {
        if (name == "op_Addition") {
            op |= MetaOp.Add;
        } else if (name == "op_Subtraction") {
            op |= MetaOp.Sub;
        } else if (name == "op_Equality") {
            // 不独立生成
            // op |= MetaOp.Eq;
        } else if (name == "op_Multiply") {
            op |= MetaOp.Mul;
        } else if (name == "op_Division") {
            op |= MetaOp.Div;
        } else if (name == "op_UnaryNegation") {
            op |= MetaOp.Neg;
        } else {
            return false;
        }


        return true;
    }

    static void CallOpFunction(string name, int count, string ret)
    {
        string head = string.Empty;

        for (int i = 0; i < count; i++) {
            head += "\t";
        }

        if (name == "op_Addition") {
            sb.AppendFormat("{0}{1} o = arg0 + arg1;\r\n", head, ret);
        } else if (name == "op_Subtraction") {
            sb.AppendFormat("{0}{1} o = arg0 - arg1;\r\n", head, ret);
        } else if (name == "op_Equality") {
            sb.AppendFormat("{0}bool o = arg0 == arg1;\r\n", head);
        } else if (name == "op_Multiply") {
            sb.AppendFormat("{0}{1} o = arg0 * arg1;\r\n", head, ret);
        } else if (name == "op_Division") {
            sb.AppendFormat("{0}{1} o = arg0 / arg1;\r\n", head, ret);
        } else if (name == "op_UnaryNegation") {
            sb.AppendFormat("{0}{1} o = -arg0;\r\n", head, ret);
        }
    }

    static string GetFuncName(string name)
    {
        if (name == "op_Addition") {
            return "Lua_Add";
        } else if (name == "op_Subtraction") {
            return "Lua_Sub";
        } else if (name == "op_Equality") {
            return "Lua_Eq";
        } else if (name == "op_Multiply") {
            return "Lua_Mul";
        } else if (name == "op_Division") {
            return "Lua_Div";
        } else if (name == "op_UnaryNegation") {
            return "Lua_Neg";
        }

        return name;
    }

    public static bool IsObsolete(MemberInfo mb)
    {
        object[] attrs = mb.GetCustomAttributes(true);

        for (int j = 0; j < attrs.Length; j++) {
            Type t = attrs[j].GetType();

            if (t == typeof(System.ObsoleteAttribute) || t == typeof(NoToLuaAttribute)) // || t.ToString() == "UnityEngine.WrapperlessIcall")
            {
                return true;
            }
        }

        if (IsMemberFilter(mb)) {
            return true;
        }

        return false;
    }

    public static bool HasAttribute(MemberInfo mb, Type atrtype)
    {
        object[] attrs = mb.GetCustomAttributes(true);

        for (int j = 0; j < attrs.Length; j++) {
            Type t = attrs[j].GetType();

            if (t == atrtype) {
                return true;
            }
        }

        return false;
    }

    static void GenEnum(string genName)
    {
        fields = type.GetFields(BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static);
        List<FieldInfo> list = new List<FieldInfo>(fields);

        for (int i = list.Count - 1; i > 0; i--) {
            if (IsObsolete(list[i])) {
                list.RemoveAt(i);
            }
        }

        var gen = new CodeGenerator();
        fields = list.ToArray();
        gen.DoStatement("public class {0}", genName);
        gen.BeginBlock("{");
        gen.DoStatement("static LuaMethod[] enums = new LuaMethod[]");
        gen.BeginBlock("{");

        for (int i = 0; i < fields.Length; i++) {
            gen.DoStatement("new LuaMethod(\"{0}\", Get{0}),", fields[i].Name);
        }

        gen.DoStatement("new LuaMethod(\"IntToEnum\", IntToEnum)");
        gen.EndBlock("};");

        gen.DoStatement("");

        gen.DoStatement("public static void Register(IntPtr L)");
        gen.BeginBlock("{");
        gen.DoStatement("L.WrapCSharpObject(typeof({0}), enums);", libClassName);
        gen.EndBlock("}");

        for (int i = 0; i < fields.Length; i++) {
            gen.DoStatement("[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
            gen.DoStatement("static int Get{0}(IntPtr L)", fields[i].Name);
            gen.BeginBlock("{");
            gen.DoStatement("L.Push({0}.{1});", className, fields[i].Name);
            gen.DoStatement("return 1;");
            gen.EndBlock("}");
        }

        gen.DoStatement("[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        gen.DoStatement("static int IntToEnum(IntPtr L)");
        gen.BeginBlock("{");
        gen.DoStatement("int arg0 = (int)L.ToNumber(1);");
        gen.DoStatement("{0} o = ({0})arg0;\r\n", className);
        gen.DoStatement("L.Push(o);");
        gen.DoStatement("return 1;");
        gen.EndBlock("}");
        gen.EndBlock("}");

        sb.Append(gen);
    }

    public static void GenDelegates(DelegateType[] list)
    {
        usingList.Add("System");
        usingList.Add("System.Collections.Generic");

        for (int i = 0; i < list.Length; i++) {
            Type t = list[i].type;

            //if (t.Namespace != null && t.Namespace != string.Empty)
            //{
            //    usingList.Add(t.Namespace);
            //}

            if (!typeof(System.Delegate).IsAssignableFrom(t)) {
                Debug.LogError(t.FullName + " not a delegate type");
                return;
            }

            //MethodInfo mi = t.GetMethod("Invoke");
            //ParameterInfo[] pi = mi.GetParameters();
            //int n = pi.Length;

            //for (int j = 0; j < n; j++)
            //{
            //    ParameterInfo param = pi[j];
            //    t = param.ParameterType;

            //    if (!t.IsPrimitive && t.Namespace != null && t.Namespace != string.Empty)
            //    {
            //        usingList.Add(t.Namespace);                 
            //    }
            //}            
        }


        sb.AppendLine("public static class DelegateFactory");
        sb.AppendLine("{");
        sb.AppendLine("\tdelegate Delegate DelegateValue(LuaFunction func);");
        sb.AppendLine("\tstatic Dictionary<Type, DelegateValue> dict = new Dictionary<Type, DelegateValue>();");

        sb.AppendLine();
        sb.AppendLine("\t[NoToLuaAttribute]");
        sb.AppendLine("\tpublic static void Register(IntPtr L)");
        sb.AppendLine("\t{");

        for (int i = 0; i < list.Length; i++) {
            string type = list[i].strType;
            string name = list[i].name;
            sb.AppendFormat("\t\tdict.Add(typeof({0}), new DelegateValue({1}));\r\n", type, name);
        }

        sb.AppendLine("\t}\r\n");

        sb.AppendLine("\t[NoToLuaAttribute]");
        sb.AppendLine("\tpublic static Delegate CreateDelegate(Type t, LuaFunction func)");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tDelegateValue create = null;\r\n");
        sb.AppendLine("\t\tif (!dict.TryGetValue(t, out create))");
        sb.AppendLine("\t\t{");
        sb.AppendLine("\t\t\tDebugger.LogError(\"Delegate {0} not register\", t.FullName);");
        sb.AppendLine("\t\t\treturn null;");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t\treturn create(func);");
        sb.AppendLine("\t}\r\n");


        for (int i = 0; i < list.Length; i++) {
            Type t = list[i].type;
            string type = list[i].strType;
            string name = list[i].name;

            sb.AppendFormat("\tpublic static Delegate {0}(LuaFunction func)\r\n", name);
            sb.AppendLine("\t{");

            sb.AppendFormat("\t\t{0} d = ", type);
            GenDelegateBody(t, "\t\t", false);
            sb.AppendLine("\t\treturn d;");

            sb.AppendLine("\t}\r\n");
        }
        sb.AppendLine("\tpublic static void Clear()");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tdict.Clear();");
        sb.AppendLine("\t}\r\n");

        sb.AppendLine("}");
        SaveFile(AppConst.uLuaPath + "/Source/Base/DelegateFactory.cs");

        Clear();
    }

    private static string[] GetGenericLibName(Type[] types)
    {
        string[] results = new string[types.Length];

        for (int i = 0; i < types.Length; i++) {
            Type t = types[i];

            if (t.IsGenericType) {
                results[i] = GetGenericLibName(types[i]);
            } else {
                if (t.IsArray) {
                    t = t.GetElementType();
                    results[i] = _C(t.ToString()).Replace('.', '_') + "s";
                } else {
                    results[i] = _C(t.ToString()).Replace('.', '_');
                }
            }

        }

        return results;
    }

    public static string GetGenericLibName(Type type)
    {
        Type[] gArgs = type.GetGenericArguments();
        string typeName = type.Name;
        string pureTypeName = typeName.Substring(0, typeName.IndexOf('`'));
        pureTypeName = _C(pureTypeName);

        return pureTypeName + "_" + string.Join("_", GetGenericLibName(gArgs));
    }

    static void ProcessExtends(List<MethodInfo> list)
    {
        extendName = "ToLua_" + libClassName.Replace(".", "_");
        extendType = Type.GetType(extendName + ", Assembly-CSharp-Editor");

        if (extendType != null) {
            List<MethodInfo> list2 = new List<MethodInfo>();
            list2.AddRange(extendType.GetMethods(BindingFlags.Instance | binding | BindingFlags.DeclaredOnly));

            for (int i = list2.Count - 1; i >= 0; i--) {
                if (list2[i].Name.Contains("op_") || list2[i].Name.Contains("add_") || list2[i].Name.Contains("remove_")) {
                    if (!IsNeedOp(list2[i].Name)) {
                        //list2.RemoveAt(i);
                        continue;
                    }
                }

                list.RemoveAll((md) => { return md.Name == list2[i].Name; });

                if (!IsObsolete(list2[i])) {
                    list.Add(list2[i]);
                }
            }
        }
    }
}

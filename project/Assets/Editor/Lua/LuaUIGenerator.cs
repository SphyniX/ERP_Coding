
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZFrame.UGUI;

/// <summary>
/// 自动生成Lua脚本处理UI表现和逻辑
/// 1. 每次只能选择一个UI的预设
/// 2. 预设必须挂载一个<LuaComponent>组件
/// 3. LuaComponent组件上定义好目标lua脚本和所需调用的函数
/// 4. UI构建规则
///     以"_"结尾的对象会被忽略
///     前缀      说明
///     lb      该对象挂载有UILable组件  
///     sp      该对象挂载有UISprite组件
///     btn     该对象挂载有UIButton组件
///     tgl     该对象挂载有UIToggle组件
///     sld     该对象挂载有UISlider组件
///     ---     挂载在以上对象下的其他对象会被忽略
///     ent     只能挂载在Grp下面, 该对象下只能挂载以上基本对象
///     Grp     该对象下会必须挂载一个ent前缀，和以上其他对象
///     Sub     该对象下可以挂载所有对象，包括Sub自己
/// </summary>
public class LuaUIGenerator : EditorWindow
{

    [MenuItem("Custom/UI脚本生成（Lua)...")]
    public static void ShowWindow()
    {

        EditorWindow edtWnd = EditorWindow.GetWindow(typeof(LuaUIGenerator));
        edtWnd.minSize = new Vector2(1400, 1500);
        edtWnd.maxSize = new Vector2(1400, 1500);
    }

    const string AUTO_DEFINE = "--!*以下：自动生成的回调函数*--";
    const string AUTO_REGIST = "--!*以上：自动注册的回调函数*--";

    private struct InteractStruct
    {
        public string prefix;
        public string actionCall;
        public string actionName;
        public InteractStruct(string prefix, string actionCall, string actionName)
        {
            this.prefix = prefix;
            this.actionCall = actionCall;
            this.actionName = actionName;
        }
    }

    private static Dictionary<System.Type, InteractStruct> s_DStruct = new Dictionary<System.Type, InteractStruct>() {
        { typeof(UIButton), new InteractStruct("btn", "onAction", "click") },
        { typeof(UIToggle), new InteractStruct("tgl", "onAction", "change") },
        { typeof(UISlider), new InteractStruct("sld", null, null) },
        { typeof(UIDropdown), new InteractStruct("drp", "onAction", "select") },
    };

    private static InteractStruct GetStruct(Component com)
    {
        var sel = com as Selectable;
        if (sel != null && sel.interactable) {
            var type = sel.GetType();
            foreach (var kv in s_DStruct) {
                if (kv.Key.IsAssignableFrom(type)) {
                    return kv.Value;
                }
            }
        }
        return new InteractStruct();
    }

    string getShortName(Component com)
    {
        var st = GetStruct(com);
        return st.prefix;
    }

    // 根据控件类型得到回调函数名
    string genFuncName(InteractStruct st, string path)
    {
        path = path.Replace('/', '_').ToLower();
        
        if (string.IsNullOrEmpty(st.actionName)) return null;

        if (string.IsNullOrEmpty(path)) {
            return string.Format("on_{0}", st.actionName);
        } else {
            return string.Format("on_{0}_{1}", path, st.actionName);
        }
    }
    string genFuncName(Component com, string path)
    {
        return genFuncName(GetStruct(com), path);
    }

    // 根据控件类型得到默认的触发器名
    string genDelegateName(Component com)
    {
        return GetStruct(com).actionCall;
    }

    string opTips = "[---]";
    Vector2 scroll = Vector2.zero;
    string logicFile = "";
    string scriptLogic = "";
    LuaComponent selected;
    bool flagGenCode;
    //
    Dictionary<string, Component> dictRef = new Dictionary<string, Component>();
    Dictionary<string, Component> dictEntRef = new Dictionary<string, Component>();
    // *_logic.lua的结构
    string codeDefined = null;
    string codeInited = null;
    
    Dictionary<string, string> dictFunc = new Dictionary<string, string>();

    public void OnGUI()
    {

        if (GUI.Button(new Rect(0, 0, 200, 100), "生成脚本")) {
            generateWithSelected();
        }
        if (GUI.Button(new Rect(300, 0, 200, 100), "写入脚本")) {
            saveLogic();
        }
        if (GUI.Button(new Rect(600, 0, 200, 100), "生成并写入脚本")) {
            generateWithSelected();
            saveLogic();
        }
        GUI.color = Color.red;
        GUI.Label(new Rect(0, 150, 1400, 100), opTips);
        GUI.color = Color.white;

        GUI.BeginGroup(new Rect(0, 150, 1400, 1500));

        scroll = GUILayout.BeginScrollView(scroll, GUILayout.Width(1400), GUILayout.Height(1500));

        GUILayout.Label(logicFile);
        if (GUILayout.Button( "写入脚本" + logicFile,GUILayout.Height(40))) {
            saveLogic();
        }
        if (scriptLogic.Length > 3000)
        {
            string[] tempscriptLogic = new string[5];
            int templength = (scriptLogic.Length / 5);
            for (int i = 0; i < 5; i++)
            {
                tempscriptLogic[i] = scriptLogic.Substring(templength * i, templength);
                GUILayout.TextField(tempscriptLogic[i]);
            }
        }
        else
        {
            GUILayout.TextField(scriptLogic);
        }
     


        GUILayout.EndScrollView();
        GUI.EndGroup();
    }

    void ShowMessage(string str)
    {
        opTips = str;
        //EditorUtility.DisplayDialog("提示", str, "确定");
    }

    // 为选定的预设生成Lua脚本
    private void generateWithSelected()
    {
        GameObject[] goes = Selection.gameObjects;
        if (goes != null && goes.Length == 1) {
            selected = goes[0].GetComponent<LuaComponent>();
            flagGenCode = true;
            if (selected != null) {
                // 清空结构
                codeDefined = null;
                codeInited = null;
                dictRef.Clear();
                dictEntRef.Clear();
                dictFunc.Clear();
                // 生成文件名
                if (string.IsNullOrEmpty(selected.luaScript)) {
                    ShowMessage("脚本名称为空！");
                    return;
                }
                string fileExt = Path.GetExtension(selected.luaScript);
                string fileName = Path.GetFileNameWithoutExtension(selected.luaScript);
                if (string.IsNullOrEmpty(fileExt)) {
                    if (Directory.Exists(LuaEnv.GetFilePath("ui/" + fileName))) {
                        // 扩展名为空，名称是模块名
                        logicFile = "ui/" + fileName + "/lc_" + selected.name.ToLower() + ".lua";
                        selected.luaScript = logicFile;
                    } else {
                        ShowMessage("不存在的UI模块: " + fileName);
                        return;
                    }
                } else {
                    // 名称已存在
                    logicFile = selected.luaScript;
                }

                parseLogic(logicFile);
                GenerateLogic();
            } else {
                ShowMessage("预设体上需要挂载<LuaComponent>组件");
            }
        } else {
            ShowMessage("只能选择一个预设体来生成脚本");
        }
    }

    // 解析已有的UI Logic脚本
    void parseLogic(string path)
    {
        var filePath = LuaEnv.GetFilePath(path);
        if (!File.Exists(filePath)) {
            codeDefined = null;
            codeInited = null;
            dictFunc.Clear();
            return;
        }

        // 解析
        string text = File.ReadAllText(filePath);
        // 注释：文件名
        text = text.Substring(text.IndexOf(".lua") + 5);

        int codeStart = text.IndexOf(AUTO_DEFINE) - 1;
        if (codeStart > 0) {
            codeDefined = text.Substring(0, text.IndexOf(AUTO_DEFINE) - 1);
        } else {
            codeDefined = null;
        }
        codeStart = text.IndexOf(AUTO_DEFINE);
        if (codeStart >= 0) {
            text = text.Substring(codeStart);
        } else {
            return;
        }
        string funcName = null;
        string funcDefine = null;
        using (var reader = new StringReader(text)) {
            for (;;) {
                var line = reader.ReadLine();
                if (line == null) break;

                if (line.Contains(AUTO_REGIST)) {
                    if (funcName == "init_view") {
                        // 开始记录codeInited
                        codeInited = "";
                    }
                    continue;
                } else if (line.Contains("local P = {")) {
                    if (funcName != null) {
                        dictFunc.Add(funcName, funcDefine.Substring(0, funcDefine.Length - 1));
                    }
                    break;
                }
                // function inside function will be ignore...
                string[] segs = line.Split(new char[] { ' ', '\t', '\n' }, System.StringSplitOptions.None);
                if (segs != null && segs.Length > 2 && segs[0] == "local" && segs[1] == "function") {
                    if (funcName != null) {
                        if (funcName != "init_view") {
                            try {
                                dictFunc.Add(funcName, funcDefine.Substring(0, funcDefine.Length - 1));
                            } catch (System.Exception e) {
                                Debug.LogError(e.Message + ":" + funcName);
                            }
                        } else {
                            int endIndex = codeInited.LastIndexOf("end");
                            codeInited = codeInited.Substring(0, endIndex);
                        }
                    }
                    // 取函数名, 记录函数
                    funcName = segs[2].Substring(0, segs[2].IndexOf('(')).Trim();
                    funcDefine = '\n' + line + '\n';
                } else {
                    if (funcName != null) {
                        if (funcName == "init_view" && codeInited != null) {
                            codeInited += line + '\n';
                        }
                        funcDefine += line + '\n';
                    }
                }
            }
        }
    }

    // 生成UI Logic脚本 
    private void GenerateLogic()
    {
        StoreGrpEnts(selected.cachedTransform, selected.cachedTransform);
        
        StoreSelectable();

        // 文件头
        var strbld = new System.Text.StringBuilder();
        genFileHeader(strbld);

        // 独立的Triggler
        GenSingleTrigger(strbld);

        // 自定义的事件处理函数
        List<string> listOnMethods = new List<string>();
        foreach (KeyValuePair<string, string> kv in dictFunc) {
            if (selected.LocalMethods.Contains(kv.Key)) continue;

            if (kv.Key.StartsWith("on_")) {
                strbld.Append(kv.Value);
                listOnMethods.Add(kv.Key);
            }
        }
        foreach (string method in listOnMethods) {
            dictFunc.Remove(method);
        }
        listOnMethods.Clear();

        // 界面显示初始化
        functionBegin(strbld, true, "init_view");
        // 注册函数回调
        foreach (KeyValuePair<string, Component> kv in dictRef) {
            string path = kv.Key;
            var type = kv.Value;
            if (type == null) continue;

            string refPath = path;
            string funcPath = path;
            if (string.IsNullOrEmpty(path)) {
                // 空路径，直接使用短名称
                refPath = getShortName(type);
            } else {
                var fileName = Path.GetFileName(path);
                // 是个ent，修改结构
                var entIndex = path.IndexOf("/ent");
                if (entIndex > 0) {
                    var entName = path.Substring(entIndex + 1);
                    var slashIdx = entName.IndexOf('/');
                    if (slashIdx > 0) entName = entName.Substring(0, slashIdx);
                    refPath = refPath.Replace(entName, "Ent");
                    if (slashIdx < 0) {
                        refPath = refPath + "/" + getShortName(type);
                    }
                }

                if (fileName.StartsWith("Sub") || fileName.StartsWith("Grp")) {
                    // 是个容器，使用短名称
                    refPath = refPath + "/" + getShortName(type);
                }
            }

            if (type) {
                string funcName = genFuncName(type, funcPath);
                string delegateName = genDelegateName(type);
                if (!string.IsNullOrEmpty(funcName)) {
                    normal(strbld, string.Format("Ref.{0}.{1} = {2}", refPath.Replace('/', '.'), delegateName, funcName));
                }
            }
        }

        // Grp生成方法
        MakeGroupFunc(strbld);

        normal(strbld, AUTO_REGIST);
        strbld.Append(codeInited);
        functionEnd(strbld);

        // 界面逻辑初始化
        generateFunc(strbld, "init_logic");
        // 开始
        if (!dictFunc.ContainsKey("start")) {
            functionBegin(strbld, true, "start", "self");
            ifBegin(strbld, "Ref == nil or Ref.root ~= self");
            normal(strbld, "Ref = libugui.GenLuaTable(self, \"root\")");
            normal(strbld, "init_view()");
            ifEnd(strbld);
            normal(strbld, "init_logic()");
            functionEnd(strbld);
        }

        // 其他函数
        foreach (KeyValuePair<string, string> kv in dictFunc) {
            strbld.Append(kv.Value);
        }

        // LuaComponent的其他函数
        foreach (string method in selected.LocalMethods) {
            if (method != "start") {
                if (!dictFunc.ContainsKey(method)) {
                    generateFunc(strbld, method);
                }
            }
        }

        // return表
        strbld.Append('\n');
        tableBegin(strbld, "local P");
        normal(strbld, "start = start");
        foreach (string method in selected.LocalMethods) {
            if (method != "start") {
                normal(strbld, string.Format("{0} = {1}", method, method));
            }
        }
        tableEnd(strbld);
        normal(strbld, "return P\n");

        scriptLogic = strbld.ToString();
    }

    /// <summary>
    /// 记录所有Selectable
    /// </summary>
    private void StoreSelectable()
    {
        Selectable[] coms = selected.GetComponentsInChildren<Selectable>();
        foreach (var com in coms) {
            if (!com.interactable) continue;
            var c = com.name[com.name.Length - 1];
            if (c == '_' || c == '=') continue;
            var path = com.transform.GetHierarchy(selected.transform);
            dictRef.AddOrReplace(path, com);
        }
    }

    void genFileTmpl(StringBuilder strbld)
    {
        var data = System.DateTime.Now;
        strbld.AppendFormat("-- @authors {0}\n", System.Environment.UserName);
        strbld.AppendFormat("-- @date    {0}\n", data.ToString("yyyy-MM-dd HH:mm:ss"));
        strbld.AppendFormat("-- @desc    {0}\n", selected.name);
        strbld.AppendLine("--\n");
    }

    // 生成文件头
    void genFileHeader(StringBuilder strbld)
    {
        // 注释：文件名
        strbld.AppendFormat("--\n-- @file    {0}\n", logicFile);

        // 已有的本地变量定
        if (codeDefined != null) {
            codeDefined = codeDefined.TrimStart();
            if (!codeDefined.StartsWith("-- @authors")) {
                genFileTmpl(strbld);
            }
            normal(strbld, codeDefined);
        } else {
            genFileTmpl(strbld);

            normal(strbld, "local ipairs, pairs");
            normal(strbld, "    = ipairs, pairs");
            normal(strbld, "local libugui = require \"libugui.cs\"");
            normal(strbld, "local libunity = require \"libunity.cs\"");
            normal(strbld, string.Format("local UIMGR = MERequire \"{0}\"", "ui/uimgr"));
            normal(strbld, "local Ref");
        }

        normal(strbld, AUTO_DEFINE);
    }

    /// <summary>
    ///  独立的触发器，对于ent中的触发器不生成，仅记录下来
    /// </summary>
    void GenSingleTrigger(StringBuilder strbld)
    {
        foreach (KeyValuePair<string, Component> kv in dictRef) {
            string path = kv.Key;
            var type = kv.Value;
            if (type) {
                if (path.Contains("/ent")) {
                    path = renameEntFunc(path);
                    dictEntRef.Add(path, type);
                }
                var st = GetStruct(type);
                string funcName = genFuncName(st, path);
                if (!string.IsNullOrEmpty(funcName)) {
                    if (string.IsNullOrEmpty(st.prefix)) {
                        generateFunc(strbld, funcName);
                    } else {
                        generateFunc(strbld, funcName, st.prefix);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 产生Group生产的代码
    /// </summary>
    private void MakeGroupFunc(StringBuilder strbld)
    {
        foreach (KeyValuePair<string, Component> kv in dictRef) {
            string path = kv.Key;                        
            string entName = Path.GetFileNameWithoutExtension(path);
            if (!entName.StartsWith("ent")) continue;

            string grpName = Path.GetDirectoryName(path);
            string pointName = grpName.Replace('/', '.');
            List<string> listCode = new List<string>();

            // 自身Selectable
            Transform entTrans = selected.transform.Find(path);
            var com = entTrans.GetComponent<Selectable>();
            // 回调
            if (com) {
                string delegateName = genDelegateName(com);
                if (!string.IsNullOrEmpty(delegateName)) {
                    string cbName = genFuncName(com, path);
                    if (!string.IsNullOrEmpty(cbName)) {
                        string actionPath = string.Format("{0}.{1}", getShortName(com), delegateName);
                        listCode.Add(string.Format("New.{0} = Ent.{0}", actionPath));
                    }
                }
            }

            // ent内部的回调
            foreach (KeyValuePair<string, Component> kvEnt in dictEntRef) {
                if (!kvEnt.Key.Contains(path)) continue;

                string entPath = kvEnt.Key;
                var entType = kvEnt.Value;
                if (!entPath.EndsWith(entName)) {
                    string entFuncName = genFuncName(entType, entPath);
                    string entDelegateName = genDelegateName(entType);
                    if (!string.IsNullOrEmpty(entFuncName)) {
                        entPath = entPath.Substring(entPath.IndexOf(entName) + entName.Length + 1);
                        string actionPath = null;
                        if (entPath.StartsWith("Sub") && !entPath.Contains("/")) {
                            actionPath = string.Format("{0}.{1}.{2}", entPath.Replace('/', '.'), getShortName(entType), entDelegateName);
                        } else {
                            actionPath = string.Format("{0}.{1}", entPath.Replace('/', '.'), entDelegateName);
                        }
                        listCode.Add(string.Format("New.{0} = Ent.{0}", actionPath));
                    }
                }
            }

            if (listCode.Count > 0) {
                normal(strbld, string.Format("UIMGR.make_group(Ref.{0}, function (New, Ent)", pointName));
                step += 1;
                foreach (var code in listCode) {
                    normal(strbld, code);
                }
                step -= 1;
                normal(strbld, "end)");
            } else {
                normal(strbld, string.Format("UIMGR.make_group(Ref.{0})", pointName));
            }
        }
    }

    // 生成回调函数定义
    void generateFunc(StringBuilder strbld, string funcName, params string[] args)
    {
        string content;
        if (dictFunc.TryGetValue(funcName, out content)) {
            strbld.Append(content);
            dictFunc.Remove(funcName);
        } else {
            functionBegin(strbld, true, funcName, args);
            normal(strbld, "");
            functionEnd(strbld);
        }
    }

    string renameEntFunc(string path)
    {
        int entS = path.IndexOf("/ent");
        string entPath = path.Substring(entS + 1);
        int entE = entPath.IndexOf('/');
        string entName = entE < 0 ? entPath.Substring(0, entPath.Length - 1) : entPath.Substring(0, entE - 1);
        return path.Replace(entName + "1", entName);
    }
    string renameEntPath(string path)
    {
        int entS = path.IndexOf("/ent");
        string entPath = path.Substring(entS + 1);
        int entE = entPath.IndexOf('/');
        string entName = entE < 0 ? entPath.Substring(0, entPath.Length - 1) : entPath.Substring(0, entE - 1);
        return path.Replace(entName + "1", "Ents[1]");
    }

    void saveLogic()
    {
        if (!string.IsNullOrEmpty(scriptLogic)) {
            string path = LuaEnv.GetFilePath(logicFile);
            File.WriteAllText(path, scriptLogic);
            ShowMessage(string.Format("写入{0}成功！", path));
            string prefabPath = string.Format("Assets/{0}/UI/{1}.prefab",
                ZFrame.Asset.AssetBundleLoader.DIR_ASSETS, selected.name);
            var prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
            if (!prefab) {
                PrefabUtility.CreatePrefab(prefabPath, selected.gameObject, ReplacePrefabOptions.ConnectToPrefab);
                AssetDatabase.Refresh();
                var ai = AssetImporter.GetAtPath(prefabPath);
                ai.assetBundleName = "ui.unity3d";
            } else {
                //PrefabUtility.ReplacePrefab(selected.gameObject, prefab, ReplacePrefabOptions.ReplaceNameBased);
            }
        } else {
            ShowMessage(logicFile + "脚本为空!");
        }
    }

    /// <summary>
    /// 解析窗口结构，记录所有组
    /// </summary>
    private void StoreGrpEnts(Transform root, Transform curr)
    {
        string findPreffix = curr.GetHierarchy(root);

        for (int i = 0; i < curr.childCount; ++i) {
            Transform trans = curr.GetChild(i);
            if (!trans.gameObject.activeSelf) continue;

            string sName = trans.name;
            if (sName.EndsWith("_")) continue;

            if (sName.StartsWith("Sub")) {
                StoreGrpEnts(root, trans);
            } else if (sName.StartsWith("Grp")) {
                StoreGrpEnts(root, trans);
            } else if (sName.StartsWith("ent")) {
                dictRef.Add(findPreffix + "/" + sName, trans);
            }
        }
    }

    #region 以下为Lua代码组装

    int step = 0;
    int nt = 0;
    StringBuilder appendTabs(System.Text.StringBuilder strbld)
    {
        if (!flagGenCode) return strbld;

        for (int i = 0; i < step; ++i) {
            strbld.Append("\t");
        }
        return strbld;
    }

    void functionBegin(System.Text.StringBuilder strbld, bool blocal, string funcName, params string[] Params)
    {
        if (!flagGenCode) return;

        strbld.Append('\n');
        appendTabs(strbld);
        strbld.AppendFormat("{0}function {1}", blocal ? "local " : "", funcName);
        if (Params != null && Params.Length > 0) {
            strbld.Append('(');
            for (int i = 0; i < Params.Length; ++i) {
                strbld.Append(Params[i]);
                if (i < Params.Length - 1) {
                    strbld.Append(", ");
                } else if (i == Params.Length - 1) {
                    strbld.Append(")\n");
                }
            }
        } else {
            strbld.Append("()\n");
        }
        step += 1;
    }
    void functionEnd(System.Text.StringBuilder strbld)
    {
        if (!flagGenCode) return;

        step -= 1;
        appendTabs(strbld);
        strbld.Append("end\n");
    }

    void ifBegin(System.Text.StringBuilder strbld, string logic)
    {
        if (!flagGenCode) return;

        appendTabs(strbld);
        strbld.AppendFormat("if {0} then\n", logic);
        step += 1;
    }
    void ifEnd(System.Text.StringBuilder strbld)
    {
        if (!flagGenCode) return;

        step -= 1;
        appendTabs(strbld);
        strbld.Append("end\n");
    }
    void forBegin(StringBuilder strbld, string var, string from, string to)
    {
        if (!flagGenCode) return;

        appendTabs(strbld);
        strbld.AppendFormat("for {0} = {1}, {2} do\n", var, from, to);
        step += 1;
    }
    void forEnd(StringBuilder strbld)
    {
        if (!flagGenCode) return;

        step -= 1;
        appendTabs(strbld);
        strbld.Append("end\n");
    }

    void tableBegin(StringBuilder strbld, string tableName)
    {
        if (!flagGenCode) return;

        appendTabs(strbld);
        if (string.IsNullOrEmpty(tableName)) {
            strbld.Append("{\n");
        } else {
            strbld.AppendFormat("{0} = {1}\n", tableName, '{');
        }
        step += 1;
        nt += 1;
    }
    void tableEnd(StringBuilder strbld)
    {
        if (!flagGenCode) return;

        step -= 1;
        nt -= 1;
        appendTabs(strbld).Append('}');
        if (nt > 0) {
            strbld.Append(',');
        }
        strbld.Append("\n");
    }
    void normal(StringBuilder strbld, string logic)
    {
        if (!flagGenCode) return;

        if (logic != null) {
            appendTabs(strbld).Append(logic);
            if (nt > 0) {
                strbld.Append(',');
            }
            strbld.Append("\n");
        }
    }
    #endregion
}

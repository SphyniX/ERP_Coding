using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

public class LuaExportGenerator : EditorWindow
{
    //[MenuItem("Custom/导出csharp接口（Lua）...")]
    public static void ShowWindow()
    {
        EditorWindow edtWnd = EditorWindow.GetWindow(typeof(LuaExportGenerator));
        edtWnd.minSize = new Vector2(800, 650);
        edtWnd.maxSize = edtWnd.minSize;
    }

    private static readonly System.Type[] s_ExportTypes = new System.Type[] { 
        typeof(UnityEngine.Vector3),
    };

    public void OnGUI()
    {
        GUILayout.TextField("");
        GUILayout.TextField("");
        if (GUILayout.Button("Load")) {
            Load();
        }                
    }

    private void Load()
    {
        foreach (var t in s_ExportTypes) {
            var methods = t.GetMethods();
            foreach (var m in methods) {
                LogMgr.D("{0}:{1}", t, m);
            }
        }
    }
}

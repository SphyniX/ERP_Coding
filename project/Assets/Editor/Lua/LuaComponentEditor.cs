using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(LuaComponent))]
public class LuaComponentEditor : Editor
{
    private ReorderableList m_MethodList;
    private SerializedProperty LocalMethods;
    
    private void OnEnable()
    {
        LocalMethods = serializedObject.FindProperty("LocalMethods");
        m_MethodList = new ReorderableList(serializedObject, LocalMethods, true, true, true, true);
        m_MethodList.drawHeaderCallback = DrawHeader;
        m_MethodList.drawElementCallback = DrawElement;
    }

    private void DrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, "Lua脚本实现的函数列表");
    }

    private void DrawElement(Rect rect, int index, bool selected, bool focused)
    {
        SerializedProperty element = m_MethodList.serializedProperty.GetArrayElementAtIndex(index);

        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(rect, element, GUIContent.none);
    }

    public override void OnInspectorGUI()
	{
        base.OnInspectorGUI();

		var script = (LuaComponent)target;		
		script.luaScript = EditorGUILayout.TextField("Lua Script", script.luaScript);
        
        serializedObject.Update();
        m_MethodList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.LabelField("窗口层级 @ [" + script.depth + "]", EditorStyles.boldLabel);

        if (!Application.isPlaying) {
            EditorGUILayout.Separator();
            if (GUILayout.Button("生成脚本...", CustomEditorStyles.richTextBtn)) {
                LuaUIGenerator.ShowWindow();
            }
        }
	}
}


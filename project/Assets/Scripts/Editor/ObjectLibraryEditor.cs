using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;

[CustomEditor(typeof(ObjectLibrary))]
public class ObjectLibraryEditor : Editor
{
    private ReorderableList m_MethodList;
    private SerializedProperty m_Objects;

    private void OnEnable()
    {
        m_Objects = serializedObject.FindProperty("m_Objects");
        m_MethodList = new ReorderableList(serializedObject, m_Objects, true, true, false, true);
        m_MethodList.drawHeaderCallback = DrawHeader;
        m_MethodList.drawElementCallback = DrawElement;
    }

    private void DrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, "资源列表");
    }

    private void DrawElement(Rect rect, int index, bool selected, bool focused)
    {
        SerializedProperty element = m_MethodList.serializedProperty.GetArrayElementAtIndex(index);

        rect.y += 2;
        rect.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.BeginDisabledGroup(true);
        EditorGUI.PropertyField(rect, element, GUIContent.none);
        EditorGUI.EndDisabledGroup();
    }

    public override void OnInspectorGUI()
    {
        var self = target as ObjectLibrary;

        var addedObj = EditorGUILayout.ObjectField("拖拽对象来添加->", null, typeof(Object), false);
        var addedGameObj = EditorGUILayout.ObjectField("拖拽预设添加->", null, typeof(GameObject), false);
        if (addedObj) {
            var go = addedObj as GameObject;
            if (go) addedObj = go.GetComponent<Behaviour>();
        } else if (addedGameObj) {
            addedObj = addedGameObj;
        }

        if (addedObj) {
            if (!self.Objects.Contains(addedObj)) {
                for (int i = 0; i < self.Objects.Count; ++i) {
                    if (self.Objects[i] == null) {
                        self.Objects[i] = addedObj;
                        addedObj = null;
                        break;
                    }
                }
                if (addedObj) {
                    self.Objects.Add(addedObj);
                }
            } else {
                EditorUtility.DisplayDialog("警告",
                    string.Format("这个对象【{0}】已经保存在这儿了！", addedObj), "我知道了，不会再拖上来了");
            }
        }
        
        serializedObject.Update();        
        m_MethodList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

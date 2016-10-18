using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(UIToggle))]
    public class UIToggleEditor : ToggleEditor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("checkedTrans"));
            serializedObject.ApplyModifiedProperties();
            
        }
    }
}

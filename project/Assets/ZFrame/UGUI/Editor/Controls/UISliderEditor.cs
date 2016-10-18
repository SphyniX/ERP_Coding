using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.UI;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(UISlider))]
    public class UISliderEditor : SliderEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Separator();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("minLmt"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxLmt"));

            serializedObject.ApplyModifiedProperties();

        }
    }
}


using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(FadeBase), true)]
    [CanEditMultipleObjects()]
    public class FadeBaseEditor : Editor
    {
        private string[] AutoFadeNAME;

        private SerializedProperty m_Group;
        private SerializedProperty m_Target;
        private SerializedProperty m_Ease;
        private SerializedProperty m_Duration;
        private SerializedProperty m_Delay;
        private SerializedProperty m_Loops;
        private SerializedProperty loopType;
        private SerializedProperty autoFade;
        private SerializedProperty m_IgnoreTimescale;

        private void OnEnable()
        {
            var list = new List<string>();
            var type = typeof(AutoFade);
            var fields = type.GetFields();
            for (int i = 0; i < fields.Length; ++i) {
                var f = fields[i];
                if (f.FieldType == type) list.Add(f.Name);
            }
            AutoFadeNAME = list.ToArray();

            m_Group = serializedObject.FindProperty("m_Group");
            m_Target = serializedObject.FindProperty("m_Target");
            m_Ease = serializedObject.FindProperty("m_Ease");
            m_Duration = serializedObject.FindProperty("m_Duration");
            m_Delay = serializedObject.FindProperty("m_Delay");
            m_Loops = serializedObject.FindProperty("m_Loops");
            loopType = serializedObject.FindProperty("loopType");
            autoFade = serializedObject.FindProperty("autoFade");
            m_IgnoreTimescale = serializedObject.FindProperty("m_IgnoreTimescale");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Tween Settings", EditorStyles.boldLabel);
            ++EditorGUI.indentLevel;

            EditorGUILayout.PropertyField(m_Group);

            EditorGUILayout.PropertyField(m_Target);

            EditorGUILayout.PropertyField(m_Ease);

            EditorGUILayout.PropertyField(m_Duration);

            EditorGUILayout.PropertyField(m_Delay);

            EditorGUILayout.PropertyField(m_Loops);

            EditorGUILayout.PropertyField(loopType);

            autoFade.intValue = EditorGUILayout.MaskField("Auto Fade", autoFade.intValue, AutoFadeNAME);

            EditorGUILayout.PropertyField(m_IgnoreTimescale);

            --EditorGUI.indentLevel;

			if (Application.isPlaying) {
				if (GUILayout.Button("Animate")) {
					(target as FadeBase).DOFade(true);
				}
			} else {
                if (GUILayout.Button("Apply")) {
                    (target as FadeBase).Apply();
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

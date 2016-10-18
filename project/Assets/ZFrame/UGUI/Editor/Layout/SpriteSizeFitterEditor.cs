using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(SpriteSizeFitter))]
    [CanEditMultipleObjects]
    public class SpriteSizeFitterEditor : Editor
    {
        private SerializedProperty m_MinWidth, m_MinHeight;
        private GUIContent fitWidth = new GUIContent("Fit Width");
        private GUIContent fitHeight = new GUIContent("Fit Height");

        private void OnEnable()
        {
            m_MinWidth = serializedObject.FindProperty("m_MinWidth");
            m_MinHeight = serializedObject.FindProperty("m_MinHeight");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var self = target as SpriteSizeFitter;
            var group = self.transform.parent.GetComponent(typeof(ILayoutController));
            if (group == null) {
                EditorGUILayout.HelpBox("Missing <ILayoutController> on parent", MessageType.Error);
            }
            switch (self.aspectMode) {
                case AspectRatioFitter.AspectMode.None:
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(m_MinWidth, fitWidth);
                    EditorGUILayout.PropertyField(m_MinHeight, fitHeight);
                    EditorGUI.EndDisabledGroup();
                    break;
                case AspectRatioFitter.AspectMode.WidthControlsHeight:
                    EditorGUILayout.PropertyField(m_MinWidth, fitWidth);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(m_MinHeight, fitHeight);
                    EditorGUI.EndDisabledGroup();
                    break;
                case AspectRatioFitter.AspectMode.HeightControlsWidth:
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(m_MinWidth, fitWidth);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.PropertyField(m_MinHeight, fitHeight);
                    break;
                default:
                    EditorGUILayout.LabelField(string.Format("{0} mode has no effect.", self.aspectMode), EditorStyles.boldLabel);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

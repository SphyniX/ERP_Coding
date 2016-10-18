using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(UITexture)), CanEditMultipleObjects]
    public class UITextureEditor : RawImageEditor
    {
        public override void OnInspectorGUI()
        {
            var self = target as UITexture;
            var tex = self.texture;

            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Type"), new GUIContent("Image Type"));
            if (tex != self.texture) {
                self.enabled = self.texture;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}

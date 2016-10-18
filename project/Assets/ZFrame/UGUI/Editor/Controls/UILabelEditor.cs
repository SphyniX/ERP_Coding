using UnityEditor;
using UnityEditor.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(UILabel))]
    [CanEditMultipleObjects]
    public class UILabelEditor : TextEditor
    {
        SerializedProperty textFormat, localize;

        protected override void OnEnable()
        {
            base.OnEnable();
            textFormat = serializedObject.FindProperty("textFormat");
            localize = serializedObject.FindProperty("localize");
        }

        public override void OnInspectorGUI()
        {
            var self = target as UILabel;
            var cachedFont = self.font;
            
            base.OnInspectorGUI();

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Extern Properties", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(textFormat);
            EditorGUILayout.PropertyField(localize);
            serializedObject.ApplyModifiedProperties();

            if (cachedFont != self.font) {
                UGUITools.lastSelectedFont = self.font;
            }
        }
    }
}

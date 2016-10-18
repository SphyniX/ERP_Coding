using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(UISprite))]
    public class UISpriteEditor : ImageEditor
    {
        private bool m_Grayscale;

        public override void OnInspectorGUI()
        {
            var self = target as UISprite;
            var sp = self.sprite;

            base.OnInspectorGUI();

            if (Application.isPlaying) {
                var grayscale = self.material == UGUITools.grayScaleMat;
                m_Grayscale = EditorGUILayout.Toggle("Grayscale", grayscale);
                if (grayscale != m_Grayscale) {
                    m_Material.objectReferenceValue = m_Grayscale ? UGUITools.grayScaleMat : null;
                }
            }

            if (sp != self.sprite) {
                self.enabled = self.sprite;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

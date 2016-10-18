using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.UI;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(UIProgressBar))]
    public class UIProgressBarEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Separator();

            var self = target as UIProgressBar;
            var prevValue = self.value;
            var currValue = EditorGUILayout.Slider(prevValue, self.minValue, self.maxValue);
            if (currValue != prevValue) self.value = currValue;

            serializedObject.ApplyModifiedProperties();

        }
    }
}


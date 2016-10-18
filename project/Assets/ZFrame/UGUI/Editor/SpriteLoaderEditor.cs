using UnityEngine;
using UnityEditor;
using System.Collections;

namespace ZFrame.UGUI
{
    [CustomEditor(typeof(SpriteLoader))]
    [CanEditMultipleObjects]
    public class SpriteLoaderEditor : Editor
    {
        private Sprite cachedSprite;
        public override void OnInspectorGUI()
        {
            var self = target as SpriteLoader;
            if (cachedSprite == null && !string.IsNullOrEmpty(self.assetPath)) {
                string assetbundleName, assetName;
                Asset.AssetLoader.GetAssetpath(self.assetPath, out assetbundleName, out assetName);
                var paths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetbundleName, assetName);
                foreach (var p in paths) {
                    cachedSprite = AssetDatabase.LoadAssetAtPath(p, typeof(Sprite)) as Sprite;
                }
            }
            
            var currentSprite = EditorGUILayout.ObjectField("拖拽对象来添加->", cachedSprite, typeof(Sprite), false) as Sprite;
            if (currentSprite) {
                var path = AssetDatabase.GetAssetPath(currentSprite);
                var ai = AssetImporter.GetAtPath(path);
                if (string.IsNullOrEmpty(ai.assetBundleName)) {
                    LogMgr.W("{0}没有标志为一个AssetBundle。", currentSprite);
                    self.assetPath = "";
                    currentSprite = null;
                } else {
                    var lastPoint = ai.assetBundleName.LastIndexOf('.');
                    var abName = ai.assetBundleName.Substring(0, lastPoint);
                    self.assetPath = string.Format("{0}/{1}", abName, currentSprite.name);
                }
            } else {
                self.assetPath = "";
            }

            if (currentSprite != cachedSprite) {
                cachedSprite = currentSprite;
                var img = self.GetComponent<UnityEngine.UI.Image>();
                img.sprite = cachedSprite;
                if (self.nativeSizeOnLoaded) {
                    img.SetNativeSize();
                }
                img.SetAllDirty();
            }

            EditorGUILayout.Separator();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("nativeSizeOnLoaded"));
            EditorGUILayout.LabelField(string.Format("Asset Path: [{0}]", self.assetPath));
            serializedObject.ApplyModifiedProperties();
        }
             
    }
}

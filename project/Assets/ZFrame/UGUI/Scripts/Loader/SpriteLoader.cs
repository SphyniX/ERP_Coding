using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.UGUI
{
	public class SpriteLoader : MonoBehaviour
    {
        public bool nativeSizeOnLoaded;
        public string assetPath;
        
        private void OnEnable()
        {
            var img = GetComponent<Image>();
            if (img) {
                img.enabled = false;
                AssetsMgr.A.LoadAsync(typeof(Sprite), assetPath, true, OnSpriteLoaded);
                return;
            }

            var raw = GetComponent<RawImage>();
            if (raw) {
                raw.enabled = false;
                AssetsMgr.A.LoadAsync(typeof(Texture), assetPath, true, OnTextureLoaded);
                return;
            }
        }

        private void OnSpriteLoaded(Object o, object p)
        {
            var img = GetComponent<Image>();
            if (img) {
                img.enabled = true;
                img.sprite = o as Sprite;
                if (nativeSizeOnLoaded) img.SetNativeSize();
            }
        }

        private void OnTextureLoaded(Object o, object p)
        {
            var raw = GetComponent<RawImage>();
            if (raw) {
                raw.enabled = true;
                raw.texture = o as Texture;
                if (nativeSizeOnLoaded) raw.SetNativeSize();
            }
        }

        private void OnDestroy()
        {
            if (AssetsMgr.Instance) {
                AssetsMgr.Instance.Unload(assetPath);
            }
        }
    }
}

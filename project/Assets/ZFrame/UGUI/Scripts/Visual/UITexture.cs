using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.UGUI
{
    using Asset;
	using Tween;

    public class UITexture : RawImage, ITweenable
    {
        [SerializeField]
        private Image.Type m_Type;
        public Image.Type type {
            get { return m_Type; }
            set { 
                m_Type = value;
            }
        }

		public string texturePath { set { Load(value); } }

        public bool grayscale
        {
            get { return material == UGUITools.grayScaleMat; }
            set { material = value ? UGUITools.grayScaleMat : null; }
        }

		private void OnTextureLoaded(Object o, object param)
		{
            texture = o as Texture;
			enabled = texture != null;
		}

        public void Load(string path, DelegateObjectLoaded onLoaded = null, object param = null, bool fromAseetBundle = true)
        {
            var loadedCfg = OnTextureLoaded + onLoaded;
            if (fromAseetBundle) {
                if (string.IsNullOrEmpty(path) || path.EndsWith("/")) {
                    loadedCfg.Invoke(null, param);
                } else {
                    AssetsMgr.A.LoadAsync(typeof(Texture), path, true, loadedCfg, param);
                }
            } else {

            }
        }

        protected void ImageTypeChanged()
        {
            switch (type) {
            case Image.Type.Tiled: {
                    var uv = uvRect;
                    Vector2 size = rectTransform.rect.size;
                    uv.size = new Vector2(size.x / texture.width, size.y / texture.height);
                    uvRect = uv;
                } break;
            case Image.Type.Simple: 
                // 不会修改uvRect
                break;
            default :
                LogMgr.W("{0} is not support for UITexture.", type);
                break;
            }
        }

        protected override void Start()
        {
#if UNITY_EDITOR
			UGUITools.AutoUIRoot(this);
#endif
            base.Start();
            enabled = texture;
        }

        protected override void OnRectTransformDimensionsChange ()
        {
            base.OnRectTransformDimensionsChange();
            ImageTypeChanged();
        }

        private Vector2 GetUVOffset() { return uvRect.position; }
        private void SetUVOffset(Vector2 position) 
        { 
            var uv = uvRect;
            uv.position = position;
            uvRect = uv; 
        }

        public ZTweener Tween(object from, object to, float duration)
        {
			ZTweener tw = null;
            if (to is Color) {
                tw = this.TweenColor((Color)to, duration);
                if (from is Color) {
                    tw.StartFrom((Color)from);
                }
            } else if (to is float) {
                tw = this.TweenAlpha((float)to, duration);
                if (from is float) {
                    var fromColor = color;
                    fromColor.a = (float)from;
                    color = fromColor;
                    tw.StartFrom(color);
                }
            } else if (to is Vector2) {
                tw = ZTween.Tween(GetUVOffset, SetUVOffset, (Vector2)to, duration);
                if (from is Vector2) {
                    tw.StartFrom((Vector2)from);
                }
            } else if (to is Vector4) {
            }
            if (tw != null) tw.SetTag(this);
            return tw;
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            ImageTypeChanged();
        }
#endif

    }

}

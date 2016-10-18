using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ZFrame.UGUI
{
    using AspectMode = AspectRatioFitter.AspectMode;
    public class SpriteSizeFitter : UIBehaviour, ILayoutElement
    {
        [SerializeField]
        private AspectMode m_AspectMode = AspectMode.None;
        public AspectMode aspectMode { get { return m_AspectMode; } set { if (SetPropertyUtility.SetStruct(ref m_AspectMode, value)) SetDirty(); } }
        [SerializeField, HideInInspector]
        private float m_MinWidth = -1;
        [SerializeField, HideInInspector]
        private float m_MinHeight = -1;

        public virtual bool ignoreLayout { get { return false; } }

        public virtual float minWidth { get { return m_MinWidth; } }
        public virtual float minHeight { get { return m_MinHeight; } }
        public virtual float preferredWidth { get { return m_MinWidth; } }
        public virtual float preferredHeight { get { return m_MinHeight; } }
        public virtual float flexibleWidth { get { return -1; } }
        public virtual float flexibleHeight { get { return -1; } }
        public virtual int layoutPriority { get { return 1; } }
        
        private RectTransform m_Rect;
        public RectTransform rectTransform
        {
            get
            {
                if (m_Rect == null)
                    m_Rect = GetComponent<RectTransform>();
                return m_Rect;
            }
        }
        
        #region Unity Lifetime calls

        protected override void OnEnable()
        {
            base.OnEnable();
            SetDirty();
        }

        protected override void OnTransformParentChanged()
        {
            SetDirty();
        }

        protected override void OnDisable()
        {
            SetDirty();
            base.OnDisable();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            SetDirty();
        }

        protected override void OnBeforeTransformParentChanged()
        {
            SetDirty();
        }
        
        #endregion

        private Vector2 ? GetRawSize()
        {
            var graphic = GetComponent<Graphic>();

            var image = graphic as Image;
            if (image && image.sprite) return image.sprite.rect.size;

            var rawImg = graphic as RawImage;
            if (rawImg && rawImg.texture) return new Vector2(rawImg.texture.width, rawImg.texture.height);

            return null;
        }
               
        private void UpdateRect()
        {
            if (!IsActive())
                return;
            
            var size = GetRawSize();
            var layoutGroup = rectTransform.parent.GetComponent(typeof(ILayoutController));
            float nativeAspectRatio = 1f;
            if (layoutGroup && size != null) {
                var siz = (Vector2)size;
                nativeAspectRatio = siz.x / siz.y;
            }

            switch (m_AspectMode) {
                case AspectMode.None:
                    m_MinWidth = -1;
                    m_MinHeight = -1;
                    break;
                case AspectMode.HeightControlsWidth:
                    m_MinWidth = m_MinHeight * nativeAspectRatio;
                    break;
                case AspectMode.WidthControlsHeight:
                    m_MinHeight = m_MinWidth / nativeAspectRatio;
                    break;
                default:
                    break;
            }
        }
        
        protected void SetDirty()
        {
            if (!IsActive())
                return;

            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            UpdateRect();
            SetDirty();
        }

#endif

        public virtual void CalculateLayoutInputHorizontal()
        {
            UpdateRect();
        }

        public virtual void CalculateLayoutInputVertical()
        {
            UpdateRect();
        }
    }
}

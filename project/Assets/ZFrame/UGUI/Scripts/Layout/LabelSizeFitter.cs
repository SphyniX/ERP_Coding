using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ZFrame.UGUI
{
    [RequireComponent(typeof(Text))]
    public class LabelSizeFitter : UIBehaviour, ILayoutElement
    {
        public bool autoWidth = false;
        public bool autoHeight = false;
        [SerializeField] private float m_MinWidth = -1;
        [SerializeField] private float m_MinHeight = -1;

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

        protected void SetDirty()
        {
            if (!IsActive())
                return;
            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            SetDirty();
        }

#endif
        
        private void SetLayout(RectTransform.Axis axis)
        {
            rectTransform.SetSizeWithCurrentAnchors(axis, LayoutUtility.GetPreferredSize(m_Rect, (int)axis));
        }
        
        public virtual void CalculateLayoutInputHorizontal()
        {
            if (autoWidth) {
                m_MinWidth = -1;
                SetLayout(RectTransform.Axis.Horizontal);
                m_MinWidth = Mathf.Ceil(rectTransform.sizeDelta.x);
                SetDirty();
            }            
        }

        public virtual void CalculateLayoutInputVertical()
        {
            if (autoHeight) {
                m_MinHeight = -1;
                SetLayout(RectTransform.Axis.Vertical);
                m_MinHeight = Mathf.Ceil(rectTransform.sizeDelta.y);
                SetDirty();
            }            
        }

    }
}

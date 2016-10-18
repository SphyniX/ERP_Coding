using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using DG.Tweening;

namespace ZFrame.UGUI
{
    public class UIToggle : Toggle
    {
        public static UIToggle current;
        public static UnityAction<GameObject> onToggleClick;
        public UnityAction<UIToggle> onAction;

        public RectTransform checkedTrans;
        
        public bool value {
            get { return isOn; }
            set { isOn = value; }
        }

        public bool disabled;

        private bool m_PrevState;
        	
        private void SetVisible(RectTransform target, bool visible)
        {
            if (target) {
#if UNITY_EDITOR
                if (!Application.isPlaying) return;
#endif
                if (toggleTransition == ToggleTransition.None) {
                    target.gameObject.SetActive(visible);
                } else {
                    var cv = target.GetComponent<CanvasGroup>();
                    if (cv == null) {
                        cv = target.gameObject.AddComponent<CanvasGroup>();
                    } else {
                        DOTween.Kill(cv);
                    }
                    target.gameObject.SetActive(true);
                    var tw = cv.DOFade(visible ? 1 : 0, 0.1f).SetUpdate(true);
                    if (!visible) {
                        tw.OnComplete(() => target.gameObject.SetActive(false));
                    }
                }
            }
        }

        private void doValueChanged(bool currentState)
        {
            if (disabled) return;

            if (graphic) {
                SetVisible(graphic.rectTransform, currentState);
            }
            SetVisible(checkedTrans, currentState);

            current = this;
            if (m_PrevState != currentState) {
                if (onAction != null) onAction.Invoke(this);
            }
            m_PrevState = currentState;
        }

        protected override void Awake()
        {
            base.Awake();

            m_PrevState = isOn;

            if (graphic) {
                SetVisible(graphic.rectTransform, isOn);
            }
			SetVisible(checkedTrans, isOn);
            onValueChanged.AddListener(doValueChanged);
        }

        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (!disabled) {
                base.OnPointerClick(eventData);
            } else {
                if (onAction != null) onAction.Invoke(this);
            }
            if (IsActive() && IsInteractable()) {
                if (onToggleClick != null) onToggleClick.Invoke(gameObject);
            }
        }

        public void SetInteractable(bool interactable)
        {
            this.interactable = interactable;
			UGUITools.SetGrayscale(gameObject, !interactable);
        }
    }
}
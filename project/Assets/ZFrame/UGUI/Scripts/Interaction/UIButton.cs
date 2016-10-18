using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace ZFrame.UGUI
{
    public class UIButton : Button
    {
        public static UIButton current;
        public static UnityAction<GameObject> onButtonClick;

        // 事件回调
        public UnityAction<UIButton> onAction;
        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            current = this;
            base.OnPointerClick(eventData);
            if (IsActive() && IsInteractable()) {
                if (onAction != null) onAction.Invoke(this);
                if (onButtonClick != null) onButtonClick.Invoke(gameObject);
            }
        }

        public void OnClick(UnityEngine.EventSystems.BaseEventData eventData)
        {
            var pointerEventData = eventData as UnityEngine.EventSystems.PointerEventData;
            if (pointerEventData != null) {
                OnPointerClick(pointerEventData);
            }
        }
        
        public void SetInteractable(bool interactable)
        {
            this.interactable = interactable;
			UGUITools.SetGrayscale(gameObject, !interactable);
        }
    }
}
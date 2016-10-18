using UnityEngine;
using System.Collections;

namespace ZFrame.UGUI
{
    public class UICloseButton : UIButton
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            interactable = true;
        }

        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            interactable = false;
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace ZFrame.UGUI
{
    public class UIClickAnim : UIInteractAnim, IPointerDownHandler, IPointerUpHandler
    {
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            ExecuteAnim(true);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            ExecuteAnim(true);
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace ZFrame.UGUI
{
    public class UIDropAnim : UIInteractAnim, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {        
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            ExecuteAnim(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            ExecuteAnim(false);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            ExecuteAnim(false);
        }   
    }
}
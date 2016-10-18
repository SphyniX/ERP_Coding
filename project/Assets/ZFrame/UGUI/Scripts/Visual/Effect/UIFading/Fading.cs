using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

namespace ZFrame.UGUI
{
    public abstract class Fading<T> : FadeBase,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IDropHandler,
        IUpdateSelectedHandler,
        ISelectHandler,
        IDeselectHandler,
        IInitializePotentialDragHandler,
        IBeginDragHandler,
        IEndDragHandler,
        ISubmitHandler,
        ICancelHandler
    {
        [SerializeField]
        protected T m_Source;
        [SerializeField]
        protected T m_Destina;

		public override object source { get { return m_Source; } }
		public override object destina { get { return m_Destina; } }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.PointerEnter)) DOFade(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.PointerExit)) DOFade(true);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.PointerDown)) DOFade(true);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.PointerUp)) DOFade(true);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.Drop)) DOFade(true);
        }

        void IUpdateSelectedHandler.OnUpdateSelected(BaseEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.UpdateSelected)) DOFade(true);
        }

        void ISelectHandler.OnSelect(BaseEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.Select)) DOFade(true);
        }

        void IDeselectHandler.OnDeselect(BaseEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.Deselect)) DOFade(true);
        }

        void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.InitializePotentialDrag)) DOFade(true);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.BeginDrag)) DOFade(true);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.EndDrag)) DOFade(true);
        }

        void ISubmitHandler.OnSubmit(BaseEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.Submit)) DOFade(true);
        }

        void ICancelHandler.OnCancel(BaseEventData eventData)
        {
            if (IsInteractable() && ChkAutoFade(AutoFade.Cancle)) DOFade(true);
        }
    }    
}
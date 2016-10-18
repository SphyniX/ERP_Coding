using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

namespace ZFrame.UGUI
{
    using TriggerAction = UnityAction<UIEventTrigger>;
    public class UIEventTrigger : UISelectable,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerClickHandler,
        IInitializePotentialDragHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IDropHandler,
        IScrollHandler,
        IUpdateSelectedHandler,
        ISelectHandler,
        IDeselectHandler,
        IMoveHandler,
        ISubmitHandler,
        ICancelHandler
    {
        public static UIEventTrigger current;
        public static BaseEventData eventData;
        
        public TriggerAction onPointerEnter, onPointerExit;
        public TriggerAction onDrag, onDrop;
        public TriggerAction onPointerDown, onPointerUp;
        public TriggerAction onPointerClick;
        public TriggerAction onSelect, onDeselect;
        public TriggerAction onScroll, onMove;
        public TriggerAction onUpdateSelected;
        public TriggerAction onInitializePotentialDrag;
        public TriggerAction onBeginDrag, onEndDrag;
        public TriggerAction onSubmit, onCancel;
        
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onPointerEnter != null) onPointerEnter.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onPointerExit != null) onPointerExit.Invoke(this);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onDrag != null) onDrag.Invoke(this);
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;            
            UIEventTrigger.eventData = eventData;
            if (onDrop != null) onDrop.Invoke(this);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onPointerDown != null) onPointerDown.Invoke(this);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onPointerUp != null) onPointerUp.Invoke(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onPointerClick != null) onPointerClick.Invoke(this);
        }

        public virtual void OnSelect(BaseEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onSelect != null) onSelect.Invoke(this);
        }

        public virtual void OnDeselect(BaseEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onDeselect != null) onDeselect.Invoke(this);
        }

        public virtual void OnScroll(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onScroll != null) onScroll.Invoke(this);
        }

        public virtual void OnMove(AxisEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onMove != null) onMove.Invoke(this);
        }

        public virtual void OnUpdateSelected(BaseEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onUpdateSelected != null) onUpdateSelected.Invoke(this);
        }

        public virtual void OnInitializePotentialDrag(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onInitializePotentialDrag != null) onInitializePotentialDrag.Invoke(this);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onBeginDrag != null) onBeginDrag.Invoke(this);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onEndDrag != null) onEndDrag.Invoke(this);
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;
            if (onSubmit != null) onSubmit.Invoke(this);
        }

        public virtual void OnCancel(BaseEventData eventData)
        {
			if (!IsInteractable()) return;

            current = this;
            UIEventTrigger.eventData = eventData;            
            if (onCancel != null) onCancel.Invoke(this);
        }
    }
}

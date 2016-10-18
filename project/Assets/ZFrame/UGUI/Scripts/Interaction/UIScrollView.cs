using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace ZFrame.UGUI
{
    public class UIScrollView : UnityEngine.UI.ScrollRect
    {
        public static UIScrollView current;
        public UnityAction onBeginDrag, onDrag, onEndDrag, onScroll;

        public override void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            current = this;
            if (onBeginDrag != null) onBeginDrag.Invoke();
        }

        public override void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnDrag(eventData);

            current = this;
            if (onDrag != null) onDrag.Invoke();
        }

        public override void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            current = this;
            if (onEndDrag != null) onEndDrag.Invoke();
        }

        public override void OnScroll(UnityEngine.EventSystems.PointerEventData data)
        {
            base.OnScroll(data);

            current = this;
            if (onScroll != null) onScroll.Invoke();
        }
    }
}

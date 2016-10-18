using UnityEngine;
using System.Collections;

namespace ZFrame.Tween
{
    public class TweenSize : BaseTweener
    {
        public override ZTweener Tween(object from, object to, float duration)
        {
            var trans = transform as RectTransform;
            if (trans == null) return null;

            m_Tweener = trans.TweenSize((Vector2)to, duration);

            m_Tweener.SetTag(gameObject);
            if (from != null) {
                var v2 = (Vector2)from;
                trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, v2.x);
                trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, v2.y);
                m_Tweener.StartFrom(from);
            }
            return m_Tweener;
        }
    }
}

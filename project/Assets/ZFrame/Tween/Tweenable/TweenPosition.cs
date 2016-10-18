using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace ZFrame.Tween
{
	public class TweenPosition : BaseTweener
    {
        public override ZTweener Tween(object from, object to, float duration)
        {
            var trans = transform;
            var rect = trans as RectTransform;
			Vector3? v3From = null, v3To =  null;
			if (to is Vector3) {
				v3To = (Vector3)to;
			} else if (to is Vector2) {
				v3To = (Vector2)to;
			}

			if (v3To != null) {
				if (rect) {
					m_Tweener = rect.TweenAnchorPos((Vector3)v3To, duration);
				} else {
					m_Tweener = trans.TweenLocalPosition((Vector3)v3To, duration);
				}

				if (from is Vector3) {
					v3From = (Vector3)from;
				} else if (from is Vector2) {
					v3From = (Vector2)from;
				}
				if (v3From != null) {
					m_Tweener.StartFrom((Vector3)v3From);
				}
			}

            if (m_Tweener != null) m_Tweener.SetTag(this);
			return m_Tweener;
        }
    }
}

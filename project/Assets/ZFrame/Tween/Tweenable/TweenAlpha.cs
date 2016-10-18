using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.Tween
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TweenAlpha : BaseTweener
    {
        public override ZTweener Tween(object from, object to, float duration)
        {
            var cvGrp = gameObject.GetComponent<CanvasGroup>();
            var alpha = (float)to;
            cvGrp.blocksRaycasts = alpha > 0;
            m_Tweener = cvGrp.TweenAlpha(alpha, duration);
			m_Tweener.SetTag(this);
            if (from != null) {
                cvGrp.alpha = (float)from;
				m_Tweener.StartFrom(from);
            }
			return m_Tweener;
        }
    }
}

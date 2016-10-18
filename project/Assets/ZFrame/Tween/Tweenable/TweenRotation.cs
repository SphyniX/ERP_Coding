using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace ZFrame.Tween
{
	public class TweenRotation : BaseTweener 
	{
		public override ZTweener Tween (object from, object to, float duration)
		{
			var trans = transform;
			m_Tweener = trans.TweenLocalRotation((Vector3)to, duration);
			if (from != null) {
				trans.localRotation = Quaternion.Euler((Vector3)from);
				m_Tweener.StartFrom(from);
			}

            m_Tweener.SetTag(this);
            return m_Tweener;
		}
	}
}

using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace ZFrame.Tween 
{
	public class TweenScaling : BaseTweener 
	{
		public override ZTweener Tween (object from, object to, float duration)
		{
			var trans = transform;
			m_Tweener = trans.TweenScaling((Vector3)to, duration);
			
			m_Tweener.SetTag(gameObject);
			if (from != null) {
				trans.localScale = (Vector3)from;
				m_Tweener.StartFrom(from);
			}
			return m_Tweener;
		}
	}
}
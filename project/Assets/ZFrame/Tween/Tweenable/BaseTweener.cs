using UnityEngine;
using System.Collections;

namespace ZFrame.Tween 
{
	public abstract class BaseTweener : MonoBehaviour, ITweenable
	{
		protected ZTweener m_Tweener;
		public abstract ZTweener Tween(object from, object to, float duration);

		protected virtual void OnDisable()
		{
			if (m_Tweener != null && m_Tweener.IsTweening()) {
				m_Tweener.Stop();
			}
		}
	}
}

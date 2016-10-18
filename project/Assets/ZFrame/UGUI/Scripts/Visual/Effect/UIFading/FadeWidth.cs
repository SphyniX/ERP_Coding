using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.UGUI
{
	using Tween;

	public class FadeWidth : Fading<float>
    {
        public override object source
        {
            get
            {
                var rect = target.GetComponent<RectTransform>();
				return rect ? new Vector2(m_Source, rect.sizeDelta.y) : default(Vector2);
            }
        }

        public override object destina
        {
            get
            {
                var rect = target.GetComponent<RectTransform>();
				return rect ? new Vector2(m_Destina, rect.sizeDelta.y) : default(Vector2);
            }
        }

        public override void Apply()
        {
            var rect = target.GetComponent<RectTransform>();
            if (rect) {
                m_Source = rect.sizeDelta.x;
                m_Destina = m_Source;
            }

            LogMgr.W("FadeWidth失败：没有找到<RectTransform>");
        }

        protected override void Restart()
        {
            var rect = target.GetComponent<RectTransform>();
            if (rect) {
                rect.sizeDelta = (Vector2)source;
            }
        }

        protected override ZTweener AnimateFade(bool forward)
        {
            var tweenTar = forward ? destina : source;

            var rect = target.GetComponent<RectTransform>();
            if (rect) {
                return rect.TweenSize((Vector2)tweenTar, duration)
                    .EaseBy(easeType).DelayFor(delay);
            }

            LogMgr.W("FadeHeight失败：没有找到<RectTransform>");
            return null;
        }
    }
}
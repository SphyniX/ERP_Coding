using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

namespace ZFrame.UGUI
{
	using Tween;

    public class UISlider : Slider, ITweenable
    {
        public float minLmt = 0, maxLmt = 1;

        public UnityAction<UISlider> onValue;

        public void SetProgress(float progress)
        {
            var range = maxValue - minValue;
            value = (minLmt + progress * (maxLmt - minLmt)) * range;
        }

		private void Setter(float value)
		{
			Set(value, true);
		}
		
		private float Getter()
		{
			return value;
		}

        public ZTweener Tween(object from, object to, float duration)
        {
			ZTweener tw = null;
			
			if (to is float) {
				tw = ZTween.Tween(Getter, Setter, (float)to, duration);
				if (from != null) {
					value = (float)from;
					tw.StartFrom(value);
				}
			}
			
			if (tw != null) tw.SetTag(this);
			return tw;
        }

        private void DoValueChanged(float value)
        {
            if (onValue != null)
            {
                onValue.Invoke(this);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            onValueChanged.AddListener(DoValueChanged);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace ZFrame.UGUI
{
    public class UILongpress : Selectable
    {
        public static UILongpress current;

        public float threshold = 0.5f;
        public float interval = 0;
        public UnityAction<UILongpress> onAction;

        private float m_Time = -1;
        private float m_Last = 0;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            m_Time = 0;
            m_Last = 0;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            m_Time = -1;
        }

        private void Update()
        {
            if (m_Time < 0) return;

            float lasting = m_Time;
            m_Time += Time.unscaledDeltaTime;
            if (lasting < threshold) {
                // 第一次触发长按
                if (m_Time >= threshold) {
                    m_Last = threshold;
                    current = this;
                    if (onAction != null) onAction.Invoke(this);
                    // 是否支持反复触发
                    if (interval == 0) m_Time = -1;
                }
            } else {
                // 反复触发
                float dura = m_Time - m_Last;
                int n = Mathf.FloorToInt(dura / interval);
                if (n > 0) {
                    m_Last += interval * n;
                    current = this;
                    if (onAction != null) {
                        for (int i = 0; i < n; ++i) onAction.Invoke(this);
                    }
                }
            }
        }
    }
}

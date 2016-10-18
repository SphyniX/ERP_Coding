/**
 * 文  件  名：UISliderCtr 
 * 作       者：浪浪
 * 生成日期：16-07-25 17:31:28"
 * 功       能：
 * 修改日志：
 * 说明：
**/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace ZFrame.UGUI
{
    public class UISliderCtr : MonoBehaviour
    {
        public ScrollRect m_ScrollRect;
        public Scrollbar m_Scrollbar;
        public int count;
        private float mTargetValue;
        private bool mNeedMove = false;
        private const float MOVE_SPEED = 1F;
        private const float SMOOTH_TIME = 0.2F;
        private float mMoveSpeed = 0f;
        public void OnPointerDown()
        {
            mNeedMove = false;
        }
        public void OnPointerUp() {
            float len = 1.0f / (count - 1);
            float i = Mathf.FloorToInt(m_Scrollbar.value / len + 0.5f);
            mTargetValue = i * len;
            mNeedMove = true;
            mMoveSpeed = 0;
        }

        public void OnButtonClick(int value) {
            float len = 1.0f / (count - 1);
            mTargetValue = (value - 1) * len;
            mNeedMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (mNeedMove) {
                if (Mathf.Abs(m_Scrollbar.value - mTargetValue) < 0.01f) {
                    m_Scrollbar.value = mTargetValue;
                    mNeedMove = false;
                    return;
                }

                m_Scrollbar.value = Mathf.SmoothDamp(m_Scrollbar.value, mTargetValue, ref mMoveSpeed, SMOOTH_TIME);
            }
        }
    }
}

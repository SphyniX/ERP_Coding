/**
 * 文  件  名：UIScrollPage 
 * 作       者：浪浪
 * 生成日期：16-07-27 11:51:07"
 * 功       能：
 * 修改日志：
 * 说明：
**/
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace ZFrame.UGUI {
    //public class UIScrollPage : UIScrollView{
    [RequireComponent(typeof(UIScrollView))]
    public class UIScrollPage : MonoBehaviour , IEventSystemHandler, IEndDragHandler{
        public static UIScrollPage current;
        public UnityAction onEndDrag;
        public int nPage;
        public float MOVE_SPEED = 1F;
        public float SMOOTH_TIME = 0.2F;
        
        private int currentPage;
        private UIScrollView targetScrollView;
        private UIScrollView mScrollView;
        private float mTargetValue;
        private bool mNeedMove = false;
        private float mMoveSpeed = 0f;

        void Start() {
            targetScrollView = this.GetComponentInParent<UIScrollView>();
            mScrollView = this.GetComponent<UIScrollView>();
            currentPage = 0;
        }

        void Update() {
            if (mNeedMove) {
                if (mScrollView.horizontal) {
                    if (Mathf.Abs(mScrollView.horizontalNormalizedPosition - mTargetValue) < 0.0001f) {
                        mScrollView.horizontalNormalizedPosition = mTargetValue;
                        mNeedMove = false;
                        return;
                    }
                    mScrollView.horizontalNormalizedPosition = Mathf.SmoothDamp(mScrollView.horizontalNormalizedPosition, mTargetValue, ref mMoveSpeed, SMOOTH_TIME);
                } else {
                    if (Mathf.Abs(mScrollView.verticalNormalizedPosition - mTargetValue) < 0.0001f) {
                        mScrollView.verticalNormalizedPosition = mTargetValue;
                        mNeedMove = false;
                        return;
                    }
                    mScrollView.verticalNormalizedPosition = Mathf.SmoothDamp(mScrollView.verticalNormalizedPosition, mTargetValue, ref mMoveSpeed, SMOOTH_TIME);
                }
                
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnPage();
        }

        public void OnPage(int i)
        {
            float len = 1.0f / (nPage - 1);
            mTargetValue = (i - 1) * len;
            mNeedMove = true;
            currentPage = i;
        }

        private void OnPage()
        {
            float len = 1.0f / (nPage - 1);
            int i;
            if (mScrollView.horizontal) {
                i = Mathf.FloorToInt(mScrollView.horizontalNormalizedPosition / len + 0.5f);
                LogMgr.D("mScrollView.horizontalNormalizedPosition is {0}, len is {1}, i = {2} ", mScrollView.horizontalNormalizedPosition, len, i);
            } else {
                i = Mathf.FloorToInt(mScrollView.verticalNormalizedPosition / len + 0.5f);
                LogMgr.D("mScrollView.verticalNormalizedPosition is {0}, len is {1}, i = {2} ", mScrollView.verticalNormalizedPosition, len, i);
            }
            i = i < 0 ? 0 : i;
            i = i < nPage ? i : nPage;
            mTargetValue = i * len;
            mNeedMove = true;
            mMoveSpeed = 0;
            currentPage = i;
            LogMgr.D("Scroll Page is :{0}", currentPage);
        }
    }
}
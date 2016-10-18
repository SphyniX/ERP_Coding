using UnityEngine;
using System.Collections;
using ZFrame;
using System.Collections.Generic;

namespace GETools
{
    /// <summary>
    /// 游戏编辑工具 管理类
    /// </summary>
    public class GET_Mamager : MonoBehaviour, IGETHeartbeatable
    {
        private const int HEART_SEC = 1;

        public List<GameObject> Windows = new List<GameObject>();


        private int mCurrentWindow = 0;
        private GETTimeHeart mHeart;
        private IGETHeartbeatable mCurrentWHeart;

        // Use this for initialization
        private void Start()
        {
            mHeart = new GETTimeHeart(HEART_SEC);

            Reset();
        }

        // Update is called once per frame
        private void Update()
        {
            mHeart.DoRun(heartbeat);
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            foreach (GameObject win in Windows)
            {
                win.SetActive(false);
            }

            ChangeWindow(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">window to active</param>
        /// <param name="disable">disabled currents window?</param>
        public void ChangeWindow(int id) 
        {
            if (mCurrentWHeart!=null && mCurrentWindow == id)
                return;

            Windows[mCurrentWindow].SetActive(false);

            mCurrentWindow = id;
            Windows[id].SetActive(true);

            mCurrentWHeart = Windows[id].GetComponent<IGETHeartbeatable>();
        }

        public void heartbeat()
        {
            if (mCurrentWHeart == null) return;
            mCurrentWHeart.heartbeat();
        }
    }
}
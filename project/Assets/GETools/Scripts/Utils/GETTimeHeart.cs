using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GETools
{
    public delegate void ExecProcess();
    /// <summary>
    /// 心跳
    /// </summary>
    class GETTimeHeart
    {
        /// <summary>
        /// 间隔时间
        /// </summary>
        public int eachTime;

        private float nextTime;

        public GETTimeHeart(int eachTime = 1)
        {
            this.eachTime = eachTime;
        }

        public void DoRun(ExecProcess exec)
        {
            //LogMgr.D(string.Format("GETHeadbeat time:{0},next:{1}", Time.time, nextTime));
            if (Time.time < nextTime) return;
            nextTime = Time.time + eachTime;
            exec();
        }
    }
}

using UnityEngine;
using System.Collections;

namespace ZFrame.Util
{
    [System.Serializable]
    public class TimeCounter
    {
        [SerializeField]
        private float m_TimeLimit;
        private float m_TimeCount;
        public TimeCounter(float limit)
        {
            m_TimeLimit = limit;
            m_TimeCount = 0;
        }

        public bool Count(float time)
        {
            m_TimeCount += time;
            if (m_TimeCount >= m_TimeLimit) {
                m_TimeCount = 0;
                return true;
            }

            return false;
        }
    }

    public class WaitForRealtime : CustomYieldInstruction
    {
        private float m_Time;
        public WaitForRealtime(float seconds)
        {
            m_Time = Time.realtimeSinceStartup + seconds;
        }

        public override bool keepWaiting { get { return m_Time > Time.realtimeSinceStartup;  } }
    }
}
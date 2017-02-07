using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

namespace ZFrame
{
    public class AppController : MonoSingleton<AppController>
    {
        public const string APP_LAUNCH = "on_app_launch";
        public const string APP_PAUSE = "on_app_pause";
        public const string APP_FOCUS = "on_app_focus";
        public const string SYS_MESSAGE = "on_sys_message";
        
        [SerializeField]
        private string m_luaScript;
        public float gcInterval = 30;
        private float m_TimeOfLastGC = 0;
        
        private LuaTable m_Tb;
        
        protected override void Awaking()
        {
            if (!string.IsNullOrEmpty(m_luaScript)) {
                var L = LuaScriptMgr.Instance.L;
                int n = L.DoFile(m_luaScript);
                Assert.IsTrue(n == 1);

                m_Tb = L.ToLuaTable(-1);
                L.Pop(1);
                Assert.IsNotNull(m_Tb);

                m_Tb.CallFunc(APP_LAUNCH, 0);
            }

#if UNITY_EDITOR
#elif UNITY_IOS
		    SDKManager.OnGameLaunch();
#endif
        }

        private void OnReceiveMemoryWarning(string msg)
        {
            float currTime = Time.realtimeSinceStartup;
            if (currTime - m_TimeOfLastGC > gcInterval) {
                LogMgr.W("内存警告， 强制回收内存");
                AssetsMgr.GC();
                m_TimeOfLastGC = currTime;
            } else {
                LogMgr.W("过于频繁了，等会再回收");
            }
        }

        private void OnSystemMessage(string message)
        {
            LogMgr.D(" c#--xxx--AppController.OnSystemMessage()");
            if (m_Tb != null) m_Tb.CallFunc(SYS_MESSAGE, 0, message);
        }

        /// <summary>
        /// 强制暂停时，先 OnApplicationPause，后 OnApplicationFocus；
        /// 重新“启动”手机时，先OnApplicationFocus，后 OnApplicationPause；
        /// </summary>
        private void OnApplicationPause(bool paused)
        {
            LogMgr.D(" c#--xxx--AppController.OnApplicationPause()");
            if (m_Tb != null) m_Tb.CallFunc(APP_PAUSE, 0, paused);
        }

        private void OnApplicationFocus(bool focused)
        {
            LogMgr.D(" c#--xxx--AppController.OnApplicationFocus()");
            if (m_Tb != null) m_Tb.CallFunc(APP_FOCUS, 0, focused);
        }

        private void OnApplicationQuit()
        {
#if UNITY_EDITOR
            LogMgr.D("Application Quit.");
#endif
        }
    }
}

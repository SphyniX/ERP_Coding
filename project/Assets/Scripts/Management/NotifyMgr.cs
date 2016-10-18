using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TinyJSON;

namespace ZFrame.Notification
{
    public class Notice
    {
        public int id;
        public string title;
        public string icon;
        public string message;
        public float hour;
        public bool isDaily;
        public Notice(Variant joCfg)
        {
            id = joCfg["id"];
            title = joCfg["title"];
            icon = joCfg["icon"];
            message = joCfg["message"];
            hour = joCfg["hour"];
            isDaily = joCfg["daily"];
        }

        public override string ToString()
        {
            return string.Format("[本地推送@{0}, 每日:{1}, 消息:{2}]", hour, isDaily, message);
        }
    }

    public partial class NotifyMgr : MonoSingleton<NotifyMgr>
    {
        public const string fmtDate = "yyyy-MM-dd HH:mm:ss";

        public void ScheduleNotification(Variant joCfg)
        {
            Notice notice = new Notice(joCfg);
#if UNITY_EDITOR
#elif UNITY_STANDALONE
#elif UNITY_ANDROID
            AndroidNotification(notice);
#elif UNITY_IOS
            iOSNotification(notice);
#endif
            LogMgr.Log(notice);
        }

        public void CancelAllNotification()
        {
#if UNITY_EDITOR
#elif UNITY_STANDALONE
#elif UNITY_ANDROID
            AndroidCancelAll();
#elif UNITY_IOS
            iOSCancelAll();
#endif
        }

        public void CleanAllNotification()
        {
            LogMgr.D("CleanAllNotification");
#if UNITY_EDITOR
#elif UNITY_STANDALONE
#elif UNITY_ANDROID
            AndroidCleanAll();
#elif UNITY_IOS
            iOSCleanAll();
#endif
        }
    }
}
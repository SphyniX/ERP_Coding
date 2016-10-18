using UnityEngine;
using System.Collections;

namespace ZFrame.Notification {
    public partial class NotifyMgr : MonoSingleton<NotifyMgr> {
        
        private void AndroidNotification(Notice notice)
        {
            if (notice.isDaily) {
                SDKManager.callApi("com.shanggame.net.util.XNotification", "RegDailyNotification",
                    notice.id, notice.title, notice.icon, notice.message, notice.hour);
            } else {
                SDKManager.callApi("com.shanggame.net.util.XNotification", "RegOnceNotification",
                    notice.id, notice.title, notice.icon, notice.message, (int)(notice.hour * 3600));
            }
        }

        private void AndroidCancelAll()
        {
            SDKManager.callApi("com.shanggame.net.util.XNotification", "CancelAllNotifications");
        }

        private void AndroidCleanAll()
        {

        }

    }
}

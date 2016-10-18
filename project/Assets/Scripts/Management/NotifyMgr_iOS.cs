using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ZFrame.Notification
{
#if UNITY_EDITOR || UNITY_IOS
    using LocalNotification = UnityEngine.iOS.LocalNotification;
    using NotificationServices = UnityEngine.iOS.NotificationServices;
    using CalendarIdentifier = UnityEngine.iOS.CalendarIdentifier;
    using CalendarUnit = UnityEngine.iOS.CalendarUnit;
#endif

    public partial class NotifyMgr : MonoSingleton<NotifyMgr>
    {
#if UNITY_EDITOR || UNITY_IOS
        [DllImport("__Internal")]
        private static extern void setApplicationIconBadgeNumber(int n);
        List<LocalNotification> listNtfs = new List<LocalNotification>();

        void OnAppPause(bool paused)
        {
            if (!paused) {
                foreach (var ntf in listNtfs) {
                    NotificationServices.CancelLocalNotification(ntf);
                }
                listNtfs.Clear();
                setApplicationIconBadgeNumber(0);
            }
        }

        private void iOSNotification(Notice notice)
        {
            System.DateTime newDate = System.DateTime.Now;
            if (notice.isDaily) {
                int h = (int)notice.hour;
                var hour = notice.hour - h;
                int year = newDate.Year;
                int month = newDate.Month;
                int day = newDate.Day;
                newDate = new System.DateTime(year, month, day, h, 0, 0).AddHours(hour);
                if (newDate <= System.DateTime.Now) {
                    newDate = newDate.AddDays(1);
                }
            } else {
                newDate = newDate.AddHours(notice.hour);
            }

            if (newDate > System.DateTime.Now) {
                LocalNotification localNotification = new LocalNotification();
                localNotification.fireDate = newDate;
                localNotification.alertBody = notice.message;
                localNotification.applicationIconBadgeNumber = 1;
                localNotification.hasAction = true;
                localNotification.soundName = LocalNotification.defaultSoundName;
                if (notice.isDaily) {
                    localNotification.repeatCalendar = CalendarIdentifier.ChineseCalendar;
                    localNotification.repeatInterval = CalendarUnit.Day;
                } else {
                    listNtfs.Add(localNotification);
                }
                NotificationServices.ScheduleLocalNotification(localNotification);
            }
        }

        public void iOSCancelAll()
        {
            foreach (var ntf in listNtfs) {
                NotificationServices.CancelLocalNotification(ntf);
            }
            listNtfs.Clear();
            setApplicationIconBadgeNumber(0);
        }

        public void iOSCleanAll()
        {
            NotificationServices.CancelAllLocalNotifications();
            NotificationServices.ClearLocalNotifications();
        }
#endif
    }
}

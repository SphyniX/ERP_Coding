using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ZFrame
{
    public static class SDKManager
    {

        // UNITY_IOS
        [DllImport("__Internal")]
        public static extern string OnGameMessageRet(string param);
        [DllImport("__Internal")]
        public static extern void OnGameLaunch();

        static public void callApi(string className, string method, params object[] args)
        {
#if UNITY_EDITOR || UNITY_ANDROID
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (AndroidJavaClass jc_info = new AndroidJavaClass(className)) {
                        List<object> li = new List<object>();
                        li.Add(jo);
                        li.AddRange(args);
                        jc_info.CallStatic(method, li.ToArray());
                    }
                }
            }
#endif
        }

        static public T CallApiReturn<T>(string className, string method, params object[] args)
        {
#if UNITY_EDITOR || UNITY_ANDROID
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (AndroidJavaClass jc_info = new AndroidJavaClass(className)) {
                        List<object> li = new List<object>();
                        li.Add(jo);
                        li.AddRange(args);
                        return jc_info.CallStatic<T>(method, li.ToArray());

                    }
                }
            }
#else
			return default(T);
#endif
        }


        public static string SubmitGameData(string klass, string method, string param)
        {
#if UNITY_EDITOR
            return string.Empty;
#elif UNITY_STANDALONE
        return string.Empty;
#elif UNITY_ANDROID
        return CallApiReturn<string>(klass, method, param);
#elif UNITY_IOS
        return OnGameMessageRet(param);
#endif
        }
    }
}

using UnityEngine;
using System.Collections;
using System.IO;

namespace ZFrame.Asset
{    

    public static class VersionMgr
    {
        public struct VersionInfo
        {
            public string version;
            public string timeCreated;
            public string whoCreated;

            public override string ToString()
            {
                return string.Format("版本号：{0}，日期：{1}，生成：{2}", version, timeCreated, whoCreated);
            }

            public string ToFormatString(string fmt)
            {
                return string.Format(fmt, version, timeCreated, whoCreated);
            }
        }

        private static VersionInfo s_AppVer, s_AssetVer;

        public static VersionInfo AppVersion
        {
            get
            {
                if (s_AppVer.version == null) {
                    var txt = Resources.Load<TextAsset>("version");
                    if (txt) {
                        s_AppVer = ParseVersionInfo(txt.text);
                    } else {
                        s_AppVer.version = "";
                        s_AppVer.timeCreated = "";
                        s_AppVer.whoCreated = "";
                    }
                }
                return s_AppVer;
            }
        }

        public static VersionInfo AssetVersion
        {
            get
            {
#if UNITY_EDITOR
                 if (s_AssetVer.version == null)
                 {
                     string AssetVersionPath = string.Format("Assets/{0}/version.txt", AssetBundleLoader.DIR_ASSETS);
                     if (File.Exists(AssetVersionPath)) {
                         var text = File.ReadAllText(AssetVersionPath);
                         s_AssetVer = ParseVersionInfo(text);
                     } else {
                        s_AssetVer.version = "";
                        s_AssetVer.timeCreated = "";
                        s_AssetVer.whoCreated = "";
                    }
                 }
#endif
                return s_AssetVer;
            }
        }

        private static VersionInfo ParseVersionInfo(string text)
        {
            VersionInfo verInf = new VersionInfo();
            var Args = text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (Args != null) {
                if (Args.Length > 2) {
                    //int.TryParse(Args[0], out verInf.version);
                    verInf.version = Args[0];
					verInf.timeCreated = Args[1]; //System.Convert.ToDateTime(Args[1]);
					verInf.whoCreated = Args[2];
                }
            }
            return verInf;
        }

		public static VersionInfo LoadAppVersion()
        {			
#if UNITY_EDITOR
            if (!Application.isPlaying) {
                s_AppVer.version = null;
            }                
            return AppVersion;
#else
			return AppVersion;
#endif
        }

        public static void SaveAppVersion(string ver)
        {
#if UNITY_EDITOR
            string AppVersionPath = "Assets/Resources/version.txt";
            var dateTime = System.DateTime.Now;
            File.WriteAllText(AppVersionPath, string.Format(
				"{0}\n{1} {2}\n{3}", ver, 
				dateTime.ToShortDateString(), 
				dateTime.ToLongTimeString(),
				SystemInfo.deviceName));
            s_AppVer.version = ver;
#else
            LogMgr.W("非编辑器模式无法修改应用版本号");
#endif
        }

        public static VersionInfo LoadAssetVersion()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) {
                s_AssetVer.version = null;
            }
            return AssetVersion;
#else       
			if (s_AssetVer.version == null) {
				var text = File.ReadAllText(AssetBundleLoader.bundleRootPath + "/" + AssetBundleLoader.FILE_LIST);
				try {
					var json = TinyJSON.JSON.Load(text);
					s_AssetVer.version = json["version"];
					s_AssetVer.timeCreated = json["timeCreated"];
					s_AssetVer.whoCreated = json["whoCreated"];
				} catch (System.Exception e) {
					s_AssetVer.version = "unknow";
					LogMgr.E(e.Message);
				}
			}
			return s_AssetVer;
#endif
        }

        public static void SaveAssetVersion(string ver)
        {
#if UNITY_EDITOR
            string AssetVersionPath = string.Format("Assets/{0}/version.txt", AssetBundleLoader.DIR_ASSETS);
            var dateTime = System.DateTime.Now;
            File.WriteAllText(AssetVersionPath, string.Format(
				"{0}\n{1} {2}\n{3}", ver, 
				dateTime.ToShortDateString(), 
				dateTime.ToLongTimeString(),
				SystemInfo.deviceName));
            s_AssetVer.version = ver;
#else
            LogMgr.W("非编辑器模式无法修改资源版本号");
#endif
        }

    }
}

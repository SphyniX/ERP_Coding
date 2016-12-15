using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ZFrame.Asset;
using Assets.Editor.Utils;

public static class AssetPacker
{
	private static string FILE_LIST { get { return AssetBundleLoader.FILE_LIST; } }

    public static string EditorStreamingAssetsPath
    {
        get
        {
			string streamingPath = AssetBundleLoader.streamingRootPath;
            if (!Directory.Exists(streamingPath)) {
				SystemTools.NeedDirectory(streamingPath);
            }
            return streamingPath;
        }
    }

	public static string EditorPersistentAssetsPath
	{
		get
		{
			string persistentPath = AssetBundleLoader.bundleRootPath;
			if (!Directory.Exists(persistentPath)) {
				SystemTools.NeedDirectory(persistentPath);
			}
			return persistentPath;
		}
	}
    
	private static string m_StreamingAssetsPath 
	{
		get
		{
			return string.Format("{0}/{1}", 
				Application.streamingAssetsPath, AssetBundleLoader.ASSETBUNDLE_FOLDER);
		}
	}
    public static string StreamingAssetsPath
    {
        get
        {
			string path = m_StreamingAssetsPath;
            if (!Directory.Exists(path)) {
				SystemTools.NeedDirectory(path);
            }
            return path;
        }
    }

    static public BuildAssetBundleOptions options = 
        BuildAssetBundleOptions.DeterministicAssetBundle
        ;

#if UNITY_IOS
	static public BuildTarget buildTarget = BuildTarget.iOS;
#elif UNITY_ANDROID
	static public BuildTarget buildTarget = BuildTarget.Android;
#else
    static public BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
#endif

    static public string DIR_ASSETS { get { return AssetBundleLoader.DIR_ASSETS; } }

    [System.Runtime.InteropServices.DllImport("SYBuffer")]
    extern static void Activate();
    
	public static void Log(string fmt, params object[] Args)
	{
		Debug.LogFormat("[PACK] " + fmt, Args);
	}

    /// <summary>
    /// 压缩和打包Lua脚本/配置
    /// </summary>
    public static void EncryptLua()
    {
        CLZF2.Decrypt(null, 260769);
        CLZF2.Decrypt(new byte[1], 3);

        string CodeRoot = Path.Combine(Application.dataPath, "LuaCodes");
        string scriptDir = Path.Combine(CodeRoot, "Script");
        string configDir = Path.Combine(CodeRoot, "Config");
        if (!Directory.Exists(scriptDir)) {
            SystemTools.NeedDirectory(scriptDir);
            AssetDatabase.Refresh();
            var ai = AssetImporter.GetAtPath("Assets/LuaCodes/Script");
            ai.assetBundleName = AssetsMgr.LUA_SCRIPT;
        }
        if (!Directory.Exists(configDir)) {
            SystemTools.NeedDirectory(configDir);
            AssetDatabase.Refresh();
            var ai = AssetImporter.GetAtPath("Assets/LuaCodes/Config");
            ai.assetBundleName = AssetsMgr.LUA_CONFIG;
        }

		var scripts = new DirectoryInfo(scriptDir).GetFiles("*.bytes");
		var configs = new DirectoryInfo(configDir).GetFiles("*.bytes");
		var listExists = new List<string>();
		foreach (var f in scripts) listExists.Add("Script/" + f.Name);
		foreach (var f in configs) listExists.Add("Config/" + f.Name);
        
        DirectoryInfo dirLua = new DirectoryInfo(LuaEnv.GetFilePath(""));
        FileInfo[] files = dirLua.GetFiles("*.lua", SearchOption.AllDirectories);
        int startIndex = dirLua.FullName.Length + 1;
        foreach (FileInfo f in files) {
            string fullName = f.FullName.Substring(startIndex).Replace('/', '%').Replace('\\', '%');
            string fileName = fullName.Remove(fullName.Length - 4) + ".bytes";
            string[] lines = File.ReadAllLines(f.FullName);
            // 以"--"开头的注释以换行符代替
            List<string> liLine = new List<string>();
            foreach (var l in lines) {
                string ltim = l.Trim();
                if (ltim.StartsWith("--") && !ltim.StartsWith("--[[") && !ltim.StartsWith("--]]")) {
                    liLine.Add("\n");
                } else {
                    liLine.Add(l + "\n");
                }
            }
            string codes = string.Concat(liLine.ToArray());
            byte[] nbytes = System.Text.Encoding.UTF8.GetBytes(codes);
            if (nbytes.Length > 0) {
                nbytes = CLZF2.DllCompress(nbytes);
                CLZF2.Encrypt(nbytes, nbytes.Length);
            } else {
                Debug.LogWarning("Compress Lua: " + fileName + " is empty!");
            }

			string path;
			if (fileName.StartsWith("config")) {
				listExists.Remove("Config/" + fileName);
				path = Path.Combine(configDir, fileName);
			} else {
				listExists.Remove("Script/" + fileName);
				path = Path.Combine(scriptDir, fileName);
			}
            if (File.Exists(path)) {
                using (var file = File.OpenWrite(path)) {
                    file.Seek(0, SeekOrigin.Begin);
                    file.Write(nbytes, 0, nbytes.Length);
                    file.SetLength(nbytes.Length);
                }
            } else {
                File.WriteAllBytes(path, nbytes);
            }
        }
		foreach (var n in listExists) {
			var path = Path.Combine(CodeRoot, n);
			File.Delete(path);
			Log("Delete: {0}", n);
		}
		Log("Compress {0} files success.\n => {1}", files.Length, CodeRoot);
    }

    static public void PackAssets()
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
        Artwork.UIChecker.ClearIconReference();
        
        AssetDatabase.Refresh();
        
        BuildPipeline.BuildAssetBundles(EditorStreamingAssetsPath, options, buildTarget);

        // 更新资源版本号
        VersionMgr.SaveAssetVersion(GitTools.getVerInfo());
        AssetDatabase.Refresh();

        Log("BuildAssetBundles success.\n => {0}", EditorStreamingAssetsPath);
    }

    class AssetInf
    {
        public long siz;
        public string md5;
        public AssetInf(long _siz, string _md5)
        {
            siz = _siz;
            md5 = _md5;
        }
    }
    class ResInf
    {
        public string version;
        public string timeCreated;
		public string whoCreated;
        public Dictionary<string, AssetInf> Assets;
        public ResInf()
        {
            timeCreated = "";
            Assets = new Dictionary<string, AssetInf>();
        }
    }
    static public void GenFileList()
    {
        DirectoryInfo dir = new DirectoryInfo(AssetBundleLoader.streamingRootPath);
        FileInfo[] files = dir.GetFiles("*.unity3d", SearchOption.AllDirectories);
        ResInf resInf = new ResInf();
        var ver = VersionMgr.LoadAssetVersion();
#if UNITY_EDITOR
        resInf.version = GitTools.getVerInfo();
#else
        resInf.version = ver.version;
#endif
		resInf.timeCreated = ver.timeCreated;
		resInf.whoCreated = ver.whoCreated;

        int startIdx = dir.FullName.Length;
        for (int i = 0; i < files.Length; ++i) {
            var file = files[i];
            string md5 = CMD5.MD5File(file.FullName);
            long siz = file.Length;
            string path = file.FullName.Substring(startIdx).Replace("\\", "/").Substring(1);
            resInf.Assets.Add(path, new AssetInf(siz, md5));
        }

		string savedPath = EditorStreamingAssetsPath + "/" + FILE_LIST;
        File.WriteAllText(savedPath, TinyJSON.JSON.Dump(resInf, true));

		Log("Generate {0} success.\n => {1}", FILE_LIST, savedPath);
    }

    public static void UpdateFileList()
	{
		GenFileList();
        AssetDatabase.Refresh();

		var srcDir = AssetPacker.EditorStreamingAssetsPath;
		var dstDir = AssetPacker.StreamingAssetsPath;
		File.Copy(srcDir + "/" + FILE_LIST, dstDir + "/" + FILE_LIST, true);
		Log("Update {0} success.\n  => {1}/{2}", FILE_LIST, dstDir, FILE_LIST);
	}

	public static void ClearStreamingAssets()
	{
		var path = m_StreamingAssetsPath;
		if (Directory.Exists(path)) {
			Directory.Delete(path, true);
		}
	}
	
	public static void ClearEditorStreamingAssets()
	{
		var path = AssetBundleLoader.streamingRootPath;
		if (Directory.Exists(path)) {
			Directory.Delete(path, true);
		}
	}
	
	public static void ClearEditorPersistentAssets()
	{
		var path = AssetBundleLoader.bundleRootPath;
		if (Directory.Exists(path)) {
			Directory.Delete(path, true);
		}
	}
}

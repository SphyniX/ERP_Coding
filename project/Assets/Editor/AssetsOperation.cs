using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using ZFrame.Asset;

#pragma warning disable 0219,0414
public class AssetsOperation
{                               
	[MenuItem("Assets/测试")]
	static void TestSomething()
	{
        /*
		System.DateTime origin = new System.DateTime(1970, 1, 1, 8, 0, 0);
        var tick = System.DateTime.Now.Ticks - origin.Ticks;
        Debug.Log(tick);
        var span = new System.TimeSpan(tick);
        Debug.Log(span.TotalSeconds);
        //*/
		/*
        string data = System.IO.File.ReadAllText(UniLua.LuaRoot.Path + "/framework/clock.lua");
        byte[] encData = System.Text.Encoding.UTF8.GetBytes(data);
        CLZF2.Encrypt(encData, encData.Length);
        LogMgr.D(System.Text.Encoding.UTF8.GetString(encData));
        CLZF2.Decrypt(encData, encData.Length);
        LogMgr.D(System.Text.Encoding.UTF8.GetString(encData));
		//*/
	}

    /// <summary>
    /// 目录下的资源以各自的名称独立命名
    /// </summary>
    private static void markSingleAssetName(string rootDir, string pattern, string group)
    {
        var rootPath = "Assets/" + rootDir;
        var dir = new DirectoryInfo(rootPath);
        var files = dir.GetFiles(pattern);
        foreach (var f in files) {
            var assetPath = rootPath + "/" + f.Name;
            var ai = AssetImporter.GetAtPath(assetPath);
            if (ai) {
                string assetName = Path.GetFileNameWithoutExtension(f.Name);
                string abName = string.Format("{0}/{1}.unity3d", group, assetName).ToLower();
                ai.assetBundleName = abName;
				AssetPacker.Log("设置了资源名称: {0} -> {1}", ai.assetPath, abName);
            }
        }
    }

    /// <summary>
    /// 将目录下的资源以目录名命名
    /// </summary>
    private static void markPackedAssetName(
        DirectoryInfo dir, 
        string abName, 
        string pattern, 
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        int index = Application.dataPath.Length + 1;
        var files = dir.GetFiles(pattern, searchOption);
        int count = 0;
        foreach (var f in files) {
            var assetPath = "Assets/" + f.FullName.Substring(index);
            var ai = AssetImporter.GetAtPath(assetPath);
            if (ai) {
                ai.assetBundleName = abName;
                count += 1;
            }
        }
        AssetPacker.Log("设置了资源名称: Assets/{0} -> {1}。共{2}个资源", dir.FullName.Substring(index), abName, count);
    }

    /// <summary>
    /// 将目录下的资源以目录名命名
    /// </summary>
    private static void markPackedAssetName(
        string rootPath, 
        string abName, 
        string pattern, 
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        rootPath = "Assets/" + rootPath;
        var dir = new DirectoryInfo(rootPath);
        markPackedAssetName(dir, abName, pattern, searchOption);
    }

    /// <summary>
    /// 将根目录下子目录的资源以子目录名命名。
    /// </summary>
    /// <param name="rootDir">根目录</param>
    /// <param name="pattern">筛选规则</param>
    /// <param name="group">组名</param>
    private static void markMultipleAssetName(string rootDir, string pattern, string group)
    {
        var rootPath = "Assets/" + rootDir;
        var dir = new DirectoryInfo(rootPath);
        var dirs = dir.GetDirectories();
        foreach (var d in dirs) {
            var abName = string.Format("{0}/{1}.unity3d", group, d.Name).ToLower();
            markPackedAssetName(d, abName, pattern);
        }
    }

    [MenuItem("Assets/资源/自动标志资源(AssetBundle Name)")]
    static void AutoMarkAssetBundle()
    {
        // 启动
        markPackedAssetName(AssetBundleLoader.DIR_ASSETS + "/Launch", "launch.unity3d", "*");
        // UI-窗口预设体(搜索所有目录)
        markPackedAssetName(AssetBundleLoader.DIR_ASSETS + "/UI", "ui.unity3d", "*.prefab", SearchOption.AllDirectories);
        //// 独立的图片
        //markSingleAssetName(AssetBundleLoader.DIR_ASSETS + "/RawImage", "*", "RawImage");
        //// UI特效
        //markMultipleAssetName(AssetBundleLoader.DIR_ASSETS + "/UIFX", "*.prefab", "UIFX");
        // 场景
        markMultipleAssetName("Scenes", "*.unity", "Scenes");
        //// 背景音乐
        //markSingleAssetName(AssetBundleLoader.DIR_ASSETS + "/BGM", "*", "BGM");
    }
               
	[MenuItem("Assets/资源/删除废弃的资源包")]
	public static void RemoveUnusedAssest()
	{
		AssetDatabase.RemoveUnusedAssetBundleNames();
		var list = new List<string>(AssetDatabase.GetAllAssetBundleNames());
		DirectoryInfo dir = new DirectoryInfo(AssetBundleLoader.streamingRootPath);
		FileInfo[] files = dir.GetFiles("*.unity3d", SearchOption.AllDirectories);
		var index = AssetBundleLoader.streamingRootPath.Length + 1;
		int nCount = 0;
		foreach (var f in files) {
			var abName = f.FullName.Substring(index).Replace('\\', '/');
			if (list.Contains(abName)) continue;
			
			f.Delete();
			File.Delete(f.FullName + ".manifest");

			nCount += 1;
			AssetPacker.Log("删除废弃的资源包: {0}", abName);
		}

		// Remove empty directories
		var dirList = new List<string>();
		for (int i = 0; i < list.Count; ++i) {
			string root = Path.GetDirectoryName(list[i]);
			for (; !string.IsNullOrEmpty(root); root = Path.GetDirectoryName(root)) {
				if (!dirList.Contains(root)) {
					dirList.Add(root);
				}
			}
		}

		var subs = dir.GetDirectories("*", SearchOption.AllDirectories);
		foreach (var d in subs) {
			var abRoot = d.FullName.Substring(index).Replace('\\', '/');
            if (dirList.Contains(abRoot)) continue;

			d.Delete();
			AssetPacker.Log("删除空的资源目录: {0}", abRoot);
		}

		AssetPacker.Log("共删除{0}个废弃的资源包", nCount);
	}

    [MenuItem("Assets/资源/查看资源类型")]
	static void CheckAssetType()
	{
		Object obj = Selection.activeObject;
		LogMgr.Log(obj);
	}
    
}

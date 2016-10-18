using UnityEngine;
using System.IO;
using System.Collections;

public static class SystemTools {

    /// <summary>
    /// 创建一个目录，自动创建不存在的父级目录
    /// </summary>
    /// <param name="path"></param>
    public static void NeedDirectory(string path)
    {
		if (string.IsNullOrEmpty(path) || path == ".") return;

        var dir = Path.GetDirectoryName(path);        
        if (!Directory.Exists(dir)) {
            NeedDirectory(dir);
        } 
        Directory.CreateDirectory(path);
    }

    /// <summary>  
    /// 复制指定目录的所有文件  
    /// </summary>  
    /// <param name="sourceDir">原始目录</param>  
    /// <param name="targetDir">目标目录</param>   
    public static void CopyDirectory(string sourceDir, string targetDir, string pattern = "*")
    {
        if (!Directory.Exists(targetDir))
            Directory.CreateDirectory(targetDir);

        //复制当前目录文件
        foreach (string srcPath in Directory.GetFiles(sourceDir, pattern)) {            
            string dstPath = Path.Combine(targetDir,  Path.GetFileName(srcPath));
            File.Copy(srcPath, dstPath, true);
        }

        //复制子目录  
        foreach (string srcPath in Directory.GetDirectories(sourceDir)) {
            string dstPath = Path.Combine(targetDir, Path.GetFileName(srcPath));
            CopyDirectory(srcPath, dstPath, pattern);
        }
    }
}

using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class FileX {
    internal static byte[] xReadBytes(this string filePath) {
        var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var nBytes = (int) (new FileInfo(filePath).Length);
        return new BinaryReader(fs).ReadBytes(nBytes);
    }

    internal static void xWriteBytes(this byte[] bytes, string filePath) {
        var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        var bw = new BinaryWriter(fs);
        bw.Write(bytes);
        bw.Flush();
    }

    internal static void xWriteText(this string text, string filePath) {
        var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        var bw = new StreamWriter(fs);
        bw.Write(text);
        bw.Flush();
    }

    internal static string xReadText(this string filePath) {
        var fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
        var br = new StreamReader(fs);
        return br.ReadToEnd();
    }

    public static string xToAbsolutePath(this string path) { return new FileInfo(@path).FullName; }

    public static string xToRelativePath(this string path) {
        var fullPath = (new FileInfo(path)).FullName;
        var basePath = (new FileInfo("Assets")).FullName;
        return "Assets" + (fullPath.Replace(basePath, "")).Replace(@"\", "/");
    }

    public static string[] xGetPaths(this FileInfo[] fileList) {
        return fileList.ToList()
            .Select(file => file.FullName)
            .ToArray();
    }

    public static bool xIsFolder(this string path) {
        if (!string.IsNullOrEmpty(path)) return (File.GetAttributes(@path) & FileAttributes.Directory) == FileAttributes.Directory;
        Debug.LogWarning("vlbFile.IsFolder() Error - path should not be null or empty");
        return false;
    }

    public static bool xIsFile(this string path) {
        if (!string.IsNullOrEmpty(path)) return (File.GetAttributes(@path) & FileAttributes.Directory) != FileAttributes.Directory;
        Debug.LogWarning("vlbFile.IsFile() Error - path should not be null or empty");
        return false;
    }

    public static string xGetName(this string path) {
        return path.xIsFolder() ? new DirectoryInfo(@path).Name : new FileInfo(@path).Name;
    }

    public static string xGetExtension(this string path) {
        return path.xIsFolder() ? new DirectoryInfo(@path).Extension : new FileInfo(@path).Extension;
    }

    public static string xGetNameWithoutExtension(this string path) {
        //replace is not safe, what if the path contains a folder with exactly the same extension with the current file/folder ?
        //should replace the last one instead
        return path.xGetName()
            .Replace(path.xGetExtension(), "");
    }

    public static string xCreatePath(this string path) {
        var info = Directory.CreateDirectory(@path);
        AssetDatabase.Refresh();
        return info.FullName;
    }

    public static string xParentFolder(this string path) {
        var directoryInfo = new DirectoryInfo(@path).Parent;
        return directoryInfo != null ? (path.xIsFolder() ? directoryInfo.FullName : new FileInfo(@path).DirectoryName) : null;
    }

    public static string[] xGetFolders(this string path, string pattern = null, bool recursive = false) {
        return Directory.GetDirectories(
            path, pattern ?? "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    }

    public static string[] xGetFiles(this string path, string pattern = null, bool recursive = false) {
        return Directory.GetFiles(
            path, pattern ?? "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
    }
}
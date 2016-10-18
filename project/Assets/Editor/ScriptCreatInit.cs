using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;

public class ScriptCreatInit : UnityEditor.AssetModificationProcessor {

    private static void OnWillCreateAsset(string path)
    {
        path = path.Replace(".meta", "");
        if (path.EndsWith(".cs")) {
            string strContent = File.ReadAllText(path);
            strContent = strContent.Replace("#AuthorName#", "浪浪").Replace("#CreateTime#", DateTime.Now.ToString("yy-MM-dd HH:mm:ss")).Replace("initialization", "DSB");
            File.WriteAllText(path, strContent);
            AssetDatabase.Refresh();
        }
    }
}

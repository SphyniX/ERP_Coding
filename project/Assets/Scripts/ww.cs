using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class ww  {
    [MenuItem("test/te")]
    public static void te()
    {
        Debug.Log("start");
        var assetNames = AssetDatabase.GetAllAssetBundleNames();
        foreach (var ab in assetNames)
        {
            var assets = AssetDatabase.GetAssetPathsFromAssetBundle(ab);
            foreach (var abs in assets)
            {
                Debug.Log(abs);
            }
            Debug.Log(assets);
        }

    }

}

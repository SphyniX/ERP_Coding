using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class ww  {
    static  string titleName="newTitle/";


    //[ExecuteInEditMode]
    [MenuItem("newTitle/EditorStart  %h")]
    static void EditorStart()
    {
        //EditorApplication.update = EditorUpdate;
        EditorUpdate();
    }
    [MenuItem("newTitle/GameObjectHide %g")]
    static void GameObjectHide()
    {
        Transform obj = Selection.activeTransform;
        if (obj.name != null)
        {

            obj.gameObject.SetActive(!obj.gameObject.activeSelf);

        }
        else
        {
        }

    }
    static void EditorUpdate()
    {
        Transform[] objs = Selection.GetTransforms(SelectionMode.Unfiltered);
        foreach (Transform obj in objs)
        {
            if (obj.name == "UIROOT")
            {

                    obj.gameObject.SetActive(!obj.gameObject.activeSelf);

            }
            else
            {
            }
        }
        //if (EditorApplication.isPlaying)
        //{
        //    Debug.Log("sfdf");

        //}
        //else
        //{
        //}
    }
    [MenuItem("newTitle/EditorFinish")]
    static void EditorFinish()
    {
        EditorApplication.update = null;

    }
    [MenuItem("newTitle/resources")]
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
    [MenuItem("newTitle/textFun")]
    public static void textFun()
    {
        Debug.Log("Path"+Application.dataPath);
        Debug.Log("Path" + Application.persistentDataPath);
        Debug.Log("Path" + Application.streamingAssetsPath);
        Debug.Log("Path" + Application.temporaryCachePath);
    }

    [MenuItem("newTitle/GameObjectList")]
    static void GameObjectList()
    {
        EditorApplication.update = EditorUpdate;

    }
    static void GameObjectListUpdate()
    {
        if (EditorApplication.isPlaying)
        {
            Debug.Log("sfdf");

        }
        else
        {
            Transform[] objs = Selection.GetTransforms(SelectionMode.Unfiltered);
            foreach (Transform obj in objs)
            {
                if (obj.name == "UIROOT")
                {
                    if (obj.gameObject.activeSelf)
                    {
                        obj.gameObject.SetActive(false);
                    }
                }
                else
                {
                }
            }
        }
    }

}

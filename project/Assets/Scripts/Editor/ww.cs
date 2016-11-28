using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;

public class UITool  {
    static  string titleName="UITool/";


    //[ExecuteInEditMode]
    [MenuItem("UITool/EditorStart  %h")]
    static void EditorStart()
    {
        //EditorApplication.update = EditorUpdate;
        EditorUpdate();
    }
    #region 测试Selection类
    [MenuItem("UITool/SelectionFun ")]
    static void SelectionFun()
    {
        Transform obj = Selection.activeTransform;
        if(obj)
        Debug.Log("1activeTransform-----"+ obj.name);
        Transform[] trans;
        trans = Selection.transforms;
        if(trans.Length>0)
        foreach (Transform tr in trans)
        {
            Debug.Log("2transforms-----" + tr.name);
        }
        GameObject[] gms;
        gms = Selection.gameObjects;
        if(gms.Length>0)
        foreach (GameObject gm in gms)
        {
            Debug.Log("3gameObjects-----" + gm.name);
        }
        GameObject actgm = Selection.activeGameObject;
        if(actgm)
        Debug.Log("4activeGameObject-----" + actgm.name);
        Object actobj = Selection.activeObject;
        if(actobj)
        Debug.Log("5activeObject-----" + actobj.name);
        Object[] objs;
        objs = Selection.objects;
        if(objs.Length>0)
        foreach (Object objlist in objs)
        {
            Debug.Log("6objects-----" + objlist.name);
        }
        //if (selection.contains(gameobject))
        //    debug.log("i'm selected!");
        Transform[] selection = Selection.GetTransforms(
        SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
        if(selection.Length>0)
        foreach (Transform t in selection)
            Debug.Log("7GetTransforms-----" + t.name);


        Object[] activeGOs =
            Selection.GetFiltered(
                typeof(GameObject),
                SelectionMode.Editable | SelectionMode.TopLevel);
        if(activeGOs.Length>0)
        foreach (GameObject activeGO in activeGOs)
            Debug.Log("8GetFiltered-----" + activeGO.name);


    }
    #endregion

    [MenuItem("UITool/PrefabCheck")]
    static void PrefabCheck()
    {
        Transform[] trans;
        trans = Selection.transforms;
        if (trans.Length > 0)
            foreach (Transform tr in trans)
            {
                if (PrefabUtility.GetPrefabType(tr)==PrefabType.PrefabInstance)
                {                    
                    Object obj= PrefabUtility.GetPrefabParent(tr);
                    string path= AssetDatabase.GetAssetPath(obj);
                    int n= path.LastIndexOf("/");
                    if (path.Substring(0, n + 1) == "Assets/RefAssets/UI/")
                    {
                        Debug.Log("路径设置正确");
                    }
                    else
                    {
                        Debug.LogFormat("路径{0}设置错误", path);
                    }

                }
            }


    }

    [MenuItem("UITool/GameObjectHide %g")]
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
    [MenuItem("UITool/EditorFinish")]
    static void EditorFinish()
    {
        EditorApplication.update = null;

    }
    [MenuItem("UITool/resources")]
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
    [MenuItem("UITool/textFun")]
    public static void textFun()
    {
        Debug.Log("Path"+Application.dataPath);
        Debug.Log("Path" + Application.persistentDataPath);
        Debug.Log("Path" + Application.streamingAssetsPath);
        Debug.Log("Path" + Application.temporaryCachePath);
    }

    [MenuItem("UITool/GameObjectList")]
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

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using ZFrame.UGUI;
public class UITool
{
    static string titleName = "UITool/";

    static UIInput ui;
    //[ExecuteInEditMode]
    [MenuItem("UITool/EditorStart  %h")]
    static void EditorStart()
    {
        //EditorApplication.update = EditorUpdate;
        EditorUpdate();
    }
    [MenuItem("UITool/AlterVersion ")]
    static void AlterVersion()
    {

    }

    #region 测试Selection类
    [MenuItem("UITool/SelectionFun ")]
    static void SelectionFun()
    {
        Transform obj = Selection.activeTransform;
        if (obj)
            Debug.Log("1activeTransform-----" + obj.name);
        Transform[] trans;
        trans = Selection.transforms;
        if (trans.Length > 0)
            foreach (Transform tr in trans)
            {
                Debug.Log("2transforms-----" + tr.name);
            }
        GameObject[] gms;
        gms = Selection.gameObjects;
        if (gms.Length > 0)
            foreach (GameObject gm in gms)
            {
                Debug.Log("3gameObjects-----" + gm.name);
            }
        GameObject actgm = Selection.activeGameObject;
        if (actgm)
            Debug.Log("4activeGameObject-----" + actgm.name);
        Object actobj = Selection.activeObject;
        if (actobj)
            Debug.Log("5activeObject-----" + actobj.name);
        Object[] objs;
        objs = Selection.objects;
        if (objs.Length > 0)
            foreach (Object objlist in objs)
            {
                Debug.Log("6objects-----" + objlist.name);
            }
        //if (selection.contains(gameobject))
        //    debug.log("i'm selected!");
        Transform[] selection = Selection.GetTransforms(
        SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);
        if (selection.Length > 0)
            foreach (Transform t in selection)
                Debug.Log("7GetTransforms-----" + t.name);


        Object[] activeGOs =
            Selection.GetFiltered(
                typeof(GameObject),
                SelectionMode.Editable | SelectionMode.TopLevel);
        if (activeGOs.Length > 0)
            foreach (GameObject activeGO in activeGOs)
                Debug.Log("8GetFiltered-----" + activeGO.name);


    }
    [MenuItem("UITool/FindObjectsOfTypeAll")]
    static void FindObjectsOfTypeAll()
    {
        GameObject[] gms = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];//FindObjectsOfTypeAll<GameObject>();//FindSceneObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject gm in gms)
        {
            //Debug.Log(gm.name);
            //if (gm.name.Substring(0,6) == "UIROOT")
            //{
            //    //gm.SetActive(!gm.activeSelf);
            //    Debug.Log("----------" + gm.name);

            //}
            if (gm.name.Length > 5)
                if (gm.name.Substring(0, 6) == "UIROOT")
                {
                    //if(gm.hideFlags==HideFlags.HideInHierarchy)
                    //gm.SetActive(!gm.activeSelf);
                    if (!AssetDatabase.Contains(gm))
                        Debug.Log("----------" + gm.hideFlags.ToString());


                }
        }

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
                if (PrefabUtility.GetPrefabType(tr) == PrefabType.PrefabInstance)
                {
                    Object obj = PrefabUtility.GetPrefabParent(tr);
                    string path = AssetDatabase.GetAssetPath(obj);
                    int n = path.LastIndexOf("/");
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
    [MenuItem("UITool/DebugCopy")]
    public static void DebugCopy()
    {
        string rootPath = @"C:\Users\cks\AppData\Local\Unity\Editor\";
        string path = Path.Combine(rootPath, "Editor.log");
        string path1 = Path.Combine(rootPath, "Editor1.log");
        string path2 = Path.Combine(rootPath, "Editor2.log");
        if (File.Exists(path))
        {
            File.Copy(path, rootPath+@"Editor1.log", true);
        }
     }
    [MenuItem("UITool/DebugPut")]
    public static void DebugPut()
    {
        string rootPath = @"C:\Users\cks\AppData\Local\Unity\Editor\";
        string path = Path.Combine(rootPath, "Editor.log");
        string path1 = Path.Combine(rootPath, "Editor1.log");
        string path2 = Path.Combine(rootPath, "Editor2.lua");
        if (File.Exists(path))
        {
            File.Copy(path, rootPath+@"Editor1.log", true);
            StreamReader sR = new StreamReader(path1);
            StreamWriter sW = new StreamWriter(path2,false);
            for (;;)
            {
                if (sR.Peek() > -1)
                {
                    string line = sR.ReadLine();
                    string str = "";
                    if (line.Length > 10)
                    {
                        str = line.Substring(0, 10);
                        if (line.Substring(0, 10) == "(Filename:")
                        {
                            sW.WriteLine(line);
                            sW.WriteLine(sR.ReadLine());
                            sW.WriteLine(sR.ReadLine());
                        }
                    }
                }

                else
                {
                    break;
                }

            }
            sR.Close();
            sW.Close();
            //foreach (string line in fileText) Debug.LogFormat("text:",line);

        }

    }



    [MenuItem("UITool/GameObjectHide %g")]
    static void GameObjectHide()
    {
        Transform[] trans = Selection.transforms;
        if (trans != null)
        {
            if (trans.Length == 0)
            {
                GameObject[] gms = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];//FindSceneObjectsOfType(typeof(GameObject)) as GameObject[];
                foreach (GameObject gm in gms)
                {
                    if (gm.name.Length > 5)

                        if (gm.name.Substring(0, 6) == "UIROOT")
                        {


                            if (!AssetDatabase.Contains(gm))
                                gm.SetActive(!gm.activeSelf);

                        }
                }
            }
            if (trans.Length == 1)
            {
                if (trans[0] != null)
                {

                    trans[0].gameObject.SetActive(!trans[0].gameObject.activeSelf);

                }
                else
                {

                }
            }
            if (trans.Length > 1)
            {
                foreach (Transform tr in trans)
                {
                    if (tr != null)
                    {

                        tr.gameObject.SetActive(!tr.gameObject.activeSelf);

                    }
                    else
                    {
                    }


                }
            }
        }
        else
        {


        }

    }
    
    [MenuItem("UITool/getCMD5")]
    static void getCMD5()
    {
        string rootPath = @"C:\Users\cks\Desktop\zzg\ERPWork1125\project\Issets\PersistentData\";
        //rootPath = @"C:\Users\cks\Desktop\zzg\ERPWork1125\project\Issets";
        string path = Path.Combine(rootPath, @"Updates\lua\script.unity3d"); //rootPath + @"Updates\lua";
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");
        path = Path.Combine(rootPath, @"Updates\lua\script.unity3d");

        string md5 = CMD5.MD5File(path);
        Debug.LogFormat(md5);
    }

    [MenuItem("UITool/AssetFileMove %&m")]
    public static void AssetFileMove()
    {
        string rootPath = @"C:\Users\cks\Desktop\zzg\ERPWork1125\project\Issets";
        if (Directory.Exists(Path.Combine(rootPath, @"PersistentData\AssetBundles")))
        {
            Directory.Delete(Path.Combine(rootPath, @"PersistentData\AssetBundles"), true);
        }
        if (Directory.Exists(Path.Combine(rootPath, @"PersistentData\Updates")))
        {
            Directory.Delete(Path.Combine(rootPath, @"PersistentData\Updates"), true);
            Directory.CreateDirectory(Path.Combine(rootPath, @"PersistentData\Updates"));
            Debug.LogFormat("创建成功");
        }
        else
        {
            Directory.CreateDirectory(Path.Combine(rootPath, @"PersistentData\Updates"));
            Debug.LogFormat("创建成功");
        }
        copyDirectoryFun(Path.Combine(rootPath, @"StreamingAssets\AssetBundles"), Path.Combine(rootPath, @"PersistentData\AssetBundles"));
    }
    /// <summary>
    /// 递归拷贝所有子目录。
    /// </summary>
    /// <param >源目录</param>
    /// <param >目的目录</param>
    private static void copyDirectoryFun(string sPath, string toPath)
    {
        string[] directories = System.IO.Directory.GetDirectories(sPath);
        if (!System.IO.Directory.Exists(toPath))
            System.IO.Directory.CreateDirectory(toPath);
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sPath);
        System.IO.DirectoryInfo[] dirs = dir.GetDirectories();
        CopyFileFun(dir, toPath);
        if (dirs.Length > 0)
        {
            foreach (System.IO.DirectoryInfo temDirectoryInfo in dirs)
            {
                string sourceDirectoryFullName = temDirectoryInfo.FullName;
                string destDirectoryFullName = sourceDirectoryFullName.Replace(sPath, toPath);
                if (!System.IO.Directory.Exists(destDirectoryFullName))
                {
                    System.IO.Directory.CreateDirectory(destDirectoryFullName);
                }
                CopyFileFun(temDirectoryInfo, destDirectoryFullName);
                copyDirectoryFun(sourceDirectoryFullName, destDirectoryFullName);
            }
        }

    }

    /// <summary>
    /// 拷贝目录下的所有文件到目的目录。
    /// </summary>
    /// <param >源路径</param>
    /// <param >目的路径</param>
    private static void CopyFileFun(System.IO.DirectoryInfo path, string desPath)
    {
        string sourcePath = path.FullName;
        System.IO.FileInfo[] files = path.GetFiles();
        foreach (System.IO.FileInfo file in files)
        {
            string sourceFileFullName = file.FullName;
            string destFileFullName = sourceFileFullName.Replace(sourcePath, desPath);
            file.CopyTo(destFileFullName, true);
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
        Debug.Log("Path" + Application.dataPath);
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

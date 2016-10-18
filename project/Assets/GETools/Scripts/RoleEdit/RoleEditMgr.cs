using GETools.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// warning CS0168: 声明了变量，但从未使用
// warning CS0219: 给变量赋值，但从未使用
#pragma warning disable 0168, 0219, 0414
namespace GETools.RoleEdit
{
    class RoleEditMgr : MonoBehaviour
    {
        private delegate T InstanceResData<T>(string name);

        Hashtable headList = new Hashtable();
        Hashtable bodyList = new Hashtable();
        Dropdown dl_head;
        Dropdown dl_body;

        private void Start()
        {



            LogMgr.D("in roleEditMgr start!");
        }

        private void Update()
        {
        }

        private void Awake()
        {
            dl_head = gameObject.transform.FindChild("dl_head").GetComponent<Dropdown>();
            GETAssert.notNull(dl_head, "dl_head 控件没找到");
            dl_body = gameObject.transform.FindChild("dl_body").GetComponent<Dropdown>();
            GETAssert.notNull(dl_head, "dl_body 控件没找到");

            dl_head.onValueChanged.AddListener(onDLHeadSelect);
            dl_body.onValueChanged.AddListener(onDLBodySelect);
            LogMgr.D("in roleEditMgr awake!");
        }

        private void OnEnable()
        {
            LogMgr.D("in roleEditMgr OnEnable!");
            //loadData();

            //进入场景
            //WNDLoading.LoadLevel("Stage-arena/Stage-arena-1/Stage-arena-1", "");
            //WNDLoading.LoadLevel("Scenes/Stage-hangmu-1/Stage-hangmu-1", "");
            //EditorApplication.LoadLevelAsyncInPlayMode("Assets/Scenes/Stage-hangmu-1/Stage-hangmu-1.unity");
        }
        private void OnDisable()
        {
            LogMgr.D("in roleEditMgr OnDisable!");
        }
        private void OnActiveChanged()
        {
            LogMgr.D("in roleEditMgr OnActiveChanged!");
        }

        /// <summary>
        /// 根据资源目录加载资源数据
        /// </summary>
        private void loadData()
        {
            //更新头资源信息
            dl_head.options = loadResInfo<WHeadInfo>(headList
                , (string name) => { return new WHeadInfo(name); }
                , "Assets/RefAssets/Models/Head"
                , WHeadInfo.KEY_NAME);

            dl_body.options = loadResInfo<WBodyInfo>(headList
                , (string name) => { return new WBodyInfo(name); }
                , "Assets/RefAssets/Models/Body"
                , WBodyInfo.KEY_NAME);
        }


        private void onDLHeadSelect(int id)
        {
            LogMgr.D("in roleEditMgr head selected:" + id);
        }
        private void onDLBodySelect(int id)
        {
            LogMgr.D("in roleEditMgr body selected:" + id);
        }


        private List<Dropdown.OptionData> loadResInfo<T>(Hashtable map
            ,InstanceResData<T> initData
            , string dirName
            , string keyName) where T : WResData
        {
            map.Clear();
            List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
            T info = null;
            //获取文件信息
            DirectoryInfo dir = new DirectoryInfo(dirName);
            GETAssert.isTrue(dir.Exists, "资源目录不存在：{0}", dir.FullName);


            string headName;
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo file in allFile)
            {
                headName = WResData.getNameByFile(file.Name, keyName);
                LogMgr.D(string.Format("process head file:{0}, name:{1}", file.Name, headName));
                if (headName == null || "".Equals(headName)) continue;
                if (headList[headName] == null)
                {
                    //装置数据
                    info = initData(headName);
                    headList[headName] = info;

                    list.Add(info);
                }

                ((T)headList[headName]).checkRes(file.Name, file.Extension);
            }
            return list;
        }
    }
}

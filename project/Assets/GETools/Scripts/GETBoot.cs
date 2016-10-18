using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GETools
{
    /// <summary>
    /// 启动器(单例MonoBehaviour类)
    /// </summary>
    class GETBoot : MonoSingleton<GETBoot>
    {
        private const string CFG_GET = "get.cfg";
        
        private static GETConfig m_GetCfg;


        public static GETConfig getCfg
        {
            get
            {
                GETAssert.notNull(m_GetCfg, "工具还未初始化");
                return m_GetCfg;
            }
        }


        private GameObject ui_MainUI;

        private void Start()
        {
            m_GetCfg = new GETConfig(CFG_GET);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5)) {
                checkShowMain();
            }
        }

        private void checkShowMain()
        {
            if (ui_MainUI) {
                ui_MainUI.SetActive(!ui_MainUI.IsActive());
            } else {
                //创建控件
                AssetsMgr.A.LoadAsync(typeof(GameObject), "get/TMainUI", false, (o, p) => {
                    if (ui_MainUI) return;

                    GameObject root = GoTools.AddForever(o as GameObject);
                    ui_MainUI = root.transform.FindChild("GETLevels").gameObject;
                    GETAssert.notNull(ui_MainUI, "没有找到预设：GETLevels");
                });
            }
        }
    }
}

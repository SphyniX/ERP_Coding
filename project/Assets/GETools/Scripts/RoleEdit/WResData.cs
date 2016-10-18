using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace GETools.RoleEdit
{
    abstract class WResData : Dropdown.OptionData
    {
        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string getNameByFile(string fileName,string keystr)
        {
            int len = fileName.ToLower().IndexOf(keystr);
            if (len < 1) return null;

            return fileName.Substring(0, len - 1);
        }

        public string name;
        public bool useAlpha = false;
        public bool tgaReady = false;
        public bool fbxReady = false;
        public bool isReady { get { return fbxReady && tgaReady; } }

        public WResData(string name)
        {
            this.name = name;
            this.text = name;
        }


        abstract public void checkRes(string fileName, string extName);

        /// <summary>
        /// 根据文件名检查资源文件的完整性
        /// </summary>
        /// <param name="fileName">全名</param>
        /// <param name="extName">扩展名</param>
        protected void checkRes(string fileName,string extName,string keystr)
        {
            extName = extName.ToLower();
            if (extName.Equals("meta")) return;

            fileName = fileName.ToLower();
            if (extName.Equals("fbx"))
            {
                fbxReady = true;
                return;
            }

            if (extName.Equals("tga"))
            {
                if(fileName.EndsWith(keystr+".tga")){
                    tgaReady = true;
                    return;
                }
                else if(fileName.EndsWith(keystr+"_a.tga"))
                {
                    useAlpha = true;
                    return;
                }
            }
        }
    }
}

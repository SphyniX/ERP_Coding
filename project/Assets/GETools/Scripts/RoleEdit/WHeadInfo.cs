using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace GETools.RoleEdit
{
    /// <summary>
    /// 头资源信息
    /// </summary>
    class WHeadInfo : WResData
    {
        public const string KEY_NAME = "head_h";

        public WHeadInfo(string name):base(name)
        {
        }

        /// <summary>
        /// 根据文件名检查资源文件的完整性
        /// </summary>
        /// <param name="fileName">全名</param>
        /// <param name="extName">扩展名</param>
        public override void checkRes(string fileName,string extName)
        {
            checkRes(fileName, extName, KEY_NAME);
        }
    }
}

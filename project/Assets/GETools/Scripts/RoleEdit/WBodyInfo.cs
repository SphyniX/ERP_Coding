using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GETools.RoleEdit
{
    class WBodyInfo : WResData
    {
        public const string KEY_NAME = "body_h";


        public WBodyInfo(string name)
            : base(name)
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

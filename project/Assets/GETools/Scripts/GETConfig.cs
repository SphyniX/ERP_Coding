using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GETools
{

    /// <summary>
    /// 配置文件工具类
    /// </summary>
    public class GETConfig
    {
        private String mFileName;
        private Dictionary<string, string> cfgMap = new Dictionary<string, string>();

        public GETConfig(string fileName)
        {
            GETAssert.isTrue(fileName != null && !"".Equals(fileName), "配置文件不能为空对象");
            mFileName = fileName;
        }

        /// <summary>
        /// 默认配置初始化
        /// </summary>
        public void initDefCfg(string[] keys, string[] values)
        {
            GETAssert.isTrue(keys != null && values != null, "初始化参数不能为空");
            GETAssert.isTrue(keys.Length == values.Length, "key,value需要一一对应！");
            cfgMap.Clear();
            for (int i = 0; i < keys.Length; i++)
            {
                cfgMap[keys[i]] = values[i];
            }
        }

        public void load()
        {
            if (System.IO.File.Exists(mFileName))
            {
                LogMgr.D("缺少配置文件，使用默认值。");
                return;
            }

            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(mFileName);
                String buf = streamReader.ReadLine();
                //开始解析配置，#为行注释
                while (buf != null)
                {
                    if (!"".Equals(buf) && !buf.StartsWith("#"))
                    {
                        int len = buf.IndexOf('=');
                        if (len > 0)
                        {
                            //更新配置
                            setCfg(buf.Substring(0, len).Trim(), buf.Substring(len + 1).Trim());
                        }
                    }
                    buf = streamReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                LogMgr.E(ex.Message);
            }
            finally
            {
                try
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                    }
                }
                catch
                {

                }
            }
        }

        public int getIntCfg(string key)
        {
            return getIntCfg(key, 0);
        }
        public int getIntCfg(String key, int def)
        {
            String v = cfgMap[key];
            if (v == null) return def;
            return Int32.Parse(v);
        }

        /// <summary>
        /// 获取配置项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getCfg(String key)
        {
            return getCfg(key, "");
        }
        public string getCfg(string key, string def)
        {
            string v = cfgMap[key];
            if (v == null) return def;
            return v;
        }

        /// <summary>
        /// 更新配置，并自动添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void setCfg(string key, string value)
        {
            cfgMap[key] = value;
        }
    }
}
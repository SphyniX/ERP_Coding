using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZFrame.UGUI
{
    [CreateAssetMenu(menuName = "UGUI/本地化文件")]
    public class Localization : ScriptableObject
    {
        public TextAsset localizeText;
        [SerializeField]
        private string[] m_CustomTexts;
#if UNITY_EDITOR
        public string[] customTexts { get { return m_CustomTexts; } }
#endif

        private string[] m_Langs;
        private Dictionary<string, string[]> m_Dict;

        private int m_CurrentLang;
        public string currentLang
        {
            get { return m_Langs[m_CurrentLang]; }
            set
            {
                var lang = value;
                if (m_Dict == null) {
                    m_Dict = new Dictionary<string, string[]>();
                    // 加载本地化文本
                    if (localizeText) {
                       LoadLocalization(localizeText.text, ref m_Langs, m_Dict);
                    } else {
                        LogMgr.W("本地化设置失败：本地化文本不存在");
                        return;
                    }
                }

                for (int i = 0; i < m_Langs.Length; ++i) {
                    if (string.Compare(lang, m_Langs[i], true) == 0) {
                        m_CurrentLang = i;
                        return;
                    }
                }

                LogMgr.W("不存在该本地化配置：{0}", lang);
            }
        }

        public void Reset()
        {
            m_Langs = null;
            m_Dict = null;
        }

        /// <summary>
        /// 获取本地化文本，如果不存在则返回key值
        /// </summary>
        public string Get(string key)
        {
            if (m_Dict == null) {
                LogMgr.W("本地化配置未初始化。");
                return key;
            }
            
            string[] values;
            if (m_Dict.TryGetValue(key, out values)) {
                if (values.Length > m_CurrentLang) {
                    return values[m_CurrentLang];
                }
            }
            
            return null;
        }

        /// <summary>
        /// 设置或者更改一个本地化配置
        /// </summary>
        public void Set(string key, string value)
        {
            if (m_Dict == null) {
                LogMgr.W("本地化配置未初始化。");
                return;
            }

            string[] values;
            m_Dict.TryGetValue(key, out values);

            if (values == null) {
                values = new string[m_CurrentLang + 1];
                values[0] = key;
            } else if (values.Length <= m_CurrentLang) {
                System.Array.Resize(ref values, m_CurrentLang + 1);                
            }
            values[m_CurrentLang] = value;

            m_Dict[key] = values;
        }

        /// <summary>
        /// 判断一个key是否被本地化
        /// </summary>
        public bool IsLocalized(string key)
        {
            if (m_Dict == null) {
                LogMgr.W("本地化配置未初始化。");
                return false;
            }

            string[] values;
            if (m_Dict.TryGetValue(key, out values)) {
                if (values.Length > m_CurrentLang) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 加载本地化数据
        /// </summary>
        private void LoadLocalization(string text, ref string[] langs, Dictionary<string, string[]> dict)
        {
            text = text.Trim();
            using (System.IO.StringReader reader = new System.IO.StringReader(text)) {
                var emptyChar = new char[] { ',' };
                // 表头
                var header = reader.ReadLine();
                m_Langs = header.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);

                for (;;) {
                    var line = reader.ReadLine();
                    if (line == null) break;

                    line = line.Replace("\\n", "\n");
                    var values = line.Split(emptyChar, System.StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length > 0) {
                        dict.Add(values[0], values);
                    }
                }
            }
        }

#if UNITY_EDITOR

		public void MarkLocalization()
		{
			foreach (var values in m_Dict.Values) {
				values[1] = null;
			}
		}

        public void SaveLocalization()
        {
            var path = UnityEditor.AssetDatabase.GetAssetPath(localizeText);
            using (System.IO.StreamWriter stream = new System.IO.StreamWriter(path)) {
                stream.Write("KEY");
                for (int i = 1; i < m_Langs.Length; ++i) {
                    stream.Write(","+ m_Langs[i]);
                }
                stream.WriteLine();
                foreach (var values in m_Dict.Values) {
					if (string.IsNullOrEmpty(values[1])) {
						LogMgr.D("移除：{0}", values[0]);
						continue;
					}

                    stream.Write(values[0].Replace("\n", "\\n"));
                    for (int i = 1; i < values.Length; ++i) {
                        var value = values[i].Replace("\n", "\\n");
                        stream.Write("," + value);
                    }
                    stream.WriteLine();
                }
            }
        }
#endif
    }
}


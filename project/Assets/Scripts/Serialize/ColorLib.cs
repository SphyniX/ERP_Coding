using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

[CreateAssetMenu(menuName = "战斗/颜色库")]
public class ColorLib : ScriptableObject
{
    [System.Serializable]
    public struct NamedColor
    {
        public string name;
        public Color color;
    }

    [System.Serializable]
    public struct TextColor
    {
        public string name;
        public Color outline;
        public Color gradient1, gradient2;
    }

    [SerializeField]
    private NamedColor[] m_NamedColors;

    [SerializeField][FormerlySerializedAs("m_FontColors")]
    private TextColor[] m_TextColors;

    public Color GetColor(string name)
    {
        for (int i = 0; i < m_NamedColors.Length; ++i) {
            if (m_NamedColors[i].name == name) {
                return m_NamedColors[i].color;
            }
        }

        LogMgr.W("库中不存在名称为'{0}'的颜色", name);
        return Color.clear;
    }

    public TextColor GetTextColor(string name)
    {
        for (int i = 0; i < m_TextColors.Length; ++i) {
            if (m_TextColors[i].name == name) {
                return m_TextColors[i];
            }
        }

        LogMgr.W("库中不存在名称为'{0}'的字体颜色", name);
        return new TextColor();
    }
}
using UnityEngine;
using UnityEditor;
using System.Collections;

public static  class CustomEditorStyles {
    private static GUIStyle m_titleStyle;
    public static GUIStyle titleStyle
    {
        get
        {
            if (m_titleStyle == null) {
                m_titleStyle = new GUIStyle(EditorStyles.helpBox);
                m_titleStyle.fontSize = 12;
                m_titleStyle.richText = true;
            }
            return m_titleStyle;
        }
    }

    private static GUIStyle m_richText;
    public static GUIStyle richText
    {
        get
        {
            if (m_richText == null) {
                m_richText = new GUIStyle(EditorStyles.label);
                m_richText.richText = true;
            }
            return m_richText;
        }
    }

	private static GUIStyle m_richTextBtn;
	public static GUIStyle richTextBtn
	{
		get
		{
			if (m_richTextBtn == null) {
				m_richTextBtn = new GUIStyle(EditorStyles.miniButton);
				m_richTextBtn.richText = true;
				m_richTextBtn.fontSize = 12;
			}
			return m_richTextBtn;
		}
	}

    private static GUIStyle m_ToggleTitle;
    public static GUIStyle toggleTitle
    {
        get
        {
            if (m_ToggleTitle == null) {
                m_ToggleTitle = new GUIStyle(EditorStyles.toggle);

                m_ToggleTitle.fontSize = 12;
            }
            return m_ToggleTitle;
        }
    }

    public static GUIContent
        addContent = new GUIContent("+", "添加一个元素"),
        rmContent = new GUIContent("-", "移除一个元素");
}

using UnityEngine;
using System.Collections;
using System.Text;

public class CodeGenerator 
{
    private StringBuilder m_sb;
    private int m_Tabs;

    public CodeGenerator()
    {
        Reset();
    }

    public void Reset()
    {
        m_sb = new StringBuilder();
        m_Tabs = 0;
    }

    public override string ToString ()
    {
        return m_sb.ToString();
    }

    public void DoStatement(string fmt, params object[] args)
    {
        InsertTabs();
        if (args == null || args.Length == 0) {
            m_sb.AppendLine(fmt);
        } else {
            m_sb.AppendLine(string.Format(fmt, args));
        }
    }

    public void BeginBlock(string fmt, params object[] args)
    {
        DoStatement(fmt, args);
        m_Tabs += 1;
    }

    public void EndBlock(string fmt, params object[] args)
    {
        m_Tabs -= 1;
        DoStatement(fmt, args);
    }

    private void InsertTabs()
    {
        for (int i = 0; i < m_Tabs; ++i) {
            m_sb.Append('\t');
        }
    }
}

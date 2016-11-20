using UnityEngine;
using System.Collections;

public class LogMgr : MonoSingleton<LogMgr>
{
    const string PROMPT1 = "{0} {1}";
    const string PROMPT2 = "{0} {1} {2}";
    const string PROMPT3 = "{0} {1} {2}";
    const string PROMPTD = "<color=orange>";
    const string PROMPTI = "<color=#ff9dabff>";
    const string PROMPTW = "<color=yellow>";
    const string PROMPTE = "<color=red>";
    const string PROMPTLua = "<color=#00aaaaff>";
    const string PROMPTEnd = "</color>";
    public enum LogLevel
    {
        I,
        D,
        W,
        E,
        Lua,
    }

#if UNITY_EDITOR || RY_DEBUG
    public LogLevel setLevel = LogLevel.D;
    private static LogLevel m_Level = LogLevel.D;
#elif UNITY_STANDALONE
    public LogLevel setLevel { get { return LogLevel.D; } }
    private static LogLevel m_Level = LogLevel.D;
#else
    public LogLevel setLevel { get { return LogLevel.E; } }
    private static LogLevel m_Level = LogLevel.E;
#endif

    public static LogLevel logLevel { get { return m_Level; } private set { m_Level = value; } }

    protected override void Awaking()
    {
        Debug.Log("Start Using LogMgr!");

        logLevel = setLevel;
#if UNITY_EDITOR
        string DEBUG_FILE = null;
#elif UNITY_STANDALONE
        string DEBUG_FILE = "debug.cfg";
#else
        string DEBUG_FILE = Application.persistentDataPath + "/debug.cfg";
#endif
        if (System.IO.File.Exists(DEBUG_FILE)) {
            string cfg = System.IO.File.ReadAllText(DEBUG_FILE);
            var js = TinyJSON.JSON.Load(cfg);
            logLevel = (LogLevel)(int)js["logLevel"];
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (logLevel != setLevel) {
            logLevel = setLevel;
        }
    }
#endif


    static void Log(LogLevel l, string format, params object[] Args)
    {

        if (l == LogLevel.D) {
            if (Args.Length == 1) {
                Debug.LogFormat(PROMPTD + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
            if (Args.Length == 2) {
                Debug.LogFormat(PROMPTD + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
        }
        if (l == LogLevel.I) {
            if (Args.Length == 1) {
                Debug.LogFormat(PROMPTI + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
            if (Args.Length == 2) {
                //Debug.Log(format);
                Debug.LogFormat(PROMPTI + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
        }
        if (l == LogLevel.Lua) {
            if (Args.Length == 1) {
                Debug.LogFormat(PROMPTLua + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
            if (Args.Length == 2) {
                //Debug.Log(format);
                Debug.LogFormat(PROMPTLua + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
        }
    }

    static void LogWarning(string format, params object[] Args)
    {
        var l = LogLevel.W;
        if (l == LogLevel.W) {
            if (Args.Length == 1) {
                Debug.LogWarningFormat(PROMPTW + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
            if (Args.Length == 2) {

                Debug.LogWarningFormat(PROMPTW + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
        }
    }

    static void LogError(string format, params object[] Args)
    {
        var l = LogLevel.E;
        if (l == LogLevel.E) {
            if (Args.Length == 1) {
                Debug.LogErrorFormat(PROMPTE + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
            if (Args.Length == 2) {
                Debug.LogErrorFormat(PROMPTE + PROMPT1 + PROMPTEnd, l, string.Format(format, Args));
            }
        }
    }

    static public void I(string format, params object[] Args)
    {
        Log(LogLevel.I, format, Args);
    }

    static public void Lua(string format, params object[] Args)
    {
        Log(LogLevel.Lua, format, Args);
    }

    static public void D(string format, params object[] Args)
    {
        Log(LogLevel.D, format, Args);
    }

    static public void W(string format, params object[] Args)
    {
        LogWarning(format, Args);
    }

    static public void E(string format, params object[] Args)
    {
        LogError(format, Args);
    }

    static public void Log(object message)
    {
        Log(LogLevel.D, "{0}", message);
    }

    static public void LogWarning(object message)
    {
        LogWarning("{0}", message);
    }

    static public void LogError(object message)
    {
        LogError("{0}", message);
    }
}

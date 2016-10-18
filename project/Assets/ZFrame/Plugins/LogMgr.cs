using UnityEngine;
using System.Collections;

public class LogMgr : MonoSingleton<LogMgr>
{
    const string PROMPT = "{0} {1}";
    public enum LogLevel
    {
        I,
        D,
        W,
        E,
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
		Debug.Log ("Start Using LogMgr!");

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
        if (logLevel <= l) {
            Debug.LogFormat(PROMPT, l, string.Format(format, Args));
        }
    }

    static void LogWarning(string format, params object[] Args)
    {
        var l = LogLevel.W;
        if (logLevel <= l) {
            Debug.LogWarningFormat(PROMPT, l, string.Format(format, Args));
        }
    }

    static void LogError(string format, params object[] Args)
    {
        var l = LogLevel.E;
        if (logLevel <= l) {
            Debug.LogErrorFormat(PROMPT, l, string.Format(format, Args));
        }
    }

    static public void I(string format, params object[] Args)
    {
        Log(LogLevel.I, format, Args);
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

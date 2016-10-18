using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using ZFrame;
using LuaInterface;

public class LuaClock : MonoBehaviour
{
    [SerializeField]
    private string m_LuaScript;
    [SerializeField]
    private string m_Method;
    public float interval = 1;
    public bool ignoreTimeScale = true;

    private float m_Time;    
    private int m_FuncRef;
    private LuaFunction m_Func;

    private void Start()
    {
        if (!string.IsNullOrEmpty(m_LuaScript)) {
            var L = LuaScriptMgr.Instance.L;
            var n = L.DoFile(m_LuaScript);
            Assert.IsTrue(n == 1);
            
            using (var tb = L.ToLuaTable(-1)) {
                m_Func = tb.RawGetFunc(m_Method);
            }

            L.Pop(1);
        }
        m_Time = 0;
    }

    private void Update()
    {
        m_Time += ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
        var pass = Mathf.FloorToInt(m_Time / interval);        
        if (pass > 0) {
            m_Time -= pass * interval;
            m_Func.Invoke(pass);
        }
    }

}

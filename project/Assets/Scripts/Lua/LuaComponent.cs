using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZFrame;
using ILuaState = System.IntPtr;

[AddComponentMenu("LUA/Lua Component")]
public class LuaComponent : MonoBehavior
{
    public const string FUNC_START = "start";
    public const string FUNC_UPDATEVIEW = "update_view";
    public const string FUNC_RECYCLE = "on_recycle";
    public const string FUNC_UPDATE = "update";
    public static System.Action<LuaComponent> OnStart;
    public static Dictionary<string, LuaComponent> dictLuaComs = new Dictionary<string, LuaComponent>();
    [HideInInspector]
    public string luaScript;
#if UNITY_EDITOR
    [HideInInspector]
    public List<string> LocalMethods = new List<string> { FUNC_START, FUNC_UPDATEVIEW, FUNC_RECYCLE };
#endif
    [System.NonSerialized]
    public int depth;
    public ILuaState lua { get { return LuaScriptMgr.Instance.L; } }

    public void CallMethod(string method, bool warnIfMissing, int nRet, params object[] args)
    {
        if (string.IsNullOrEmpty(luaScript)) return;

        var pntIndex = luaScript.LastIndexOf('.');
        var pkgName = pntIndex > -1 ? luaScript.Remove(pntIndex) : luaScript;
        lua.GetGlobal("PKG", pkgName, method);
        if (lua.IsFunction(-1)) {
            lua.DoCall(nRet, args);
        } else {
            lua.Pop(1);
            if (warnIfMissing) {
                LogMgr.W("Call {0}:{1}() failure!", luaScript, method);
            }
        }
    }
    private void Start()
    {
        dictLuaComs.AddIfNotExists(this.name, this);
        CallMethod(FUNC_START, true, 0, gameObject);
        if (OnStart != null) {
            OnStart.Invoke(this); 
        }
    }

    private void OnRecycle()
    {
        CallMethod(FUNC_RECYCLE, false, 0);
        dictLuaComs.Remove(this.name);
    }

    private void Update() {
        CallMethod(FUNC_UPDATE, false, 0);
            
    }
}

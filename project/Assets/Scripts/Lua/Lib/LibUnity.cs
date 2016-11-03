using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;

public static class LibUnity
{

    public const string LIB_NAME = "libunity.cs";

    public static void OpenLib(ILuaState lua)
    {
		Debug.Log ("Starting Open LibSystem Form Lua!");
        var define = new NameFuncPair[]
        {
            new NameFuncPair("AppQuit", AppQuit),

            // Debug
            new NameFuncPair("LogI", LogI),
            new NameFuncPair("LogD", LogD),
            new NameFuncPair("LogW", LogW),
            new NameFuncPair("LogE", LogE),

            // GameObject
            new NameFuncPair("IsObject", IsObject),
            new NameFuncPair("IsActive", IsActive),
            new NameFuncPair("Destroy", Destroy),
            new NameFuncPair("Delete", Delete),
            new NameFuncPair("FindGameObject", FindGameObject),
            new NameFuncPair("FindComponent", FindComponent),
            new NameFuncPair("AddChild", AddChild),
            new NameFuncPair("NewChild", NewChild),
            new NameFuncPair("NeedChild", NeedChild),
            new NameFuncPair("SetLayer", SetLayer),
            new NameFuncPair("AddComponent", AddComponent),
            new NameFuncPair("DelComponent", DelComponent),
            new NameFuncPair("SendMessage", SendMessage),
            new NameFuncPair("SetActive", SetActive),
            new NameFuncPair("SelfActive",SelfActive),
            new NameFuncPair("ReActive", ReActive),
            new NameFuncPair("SetEnable", SetEnable),
            new NameFuncPair("SetParent", SetParent),
            new NameFuncPair("SetSibling", SetSibling),

            // Render
			new NameFuncPair("RendererSetValue", RendererSetValue),
            new NameFuncPair("RendererSetTexture", RendererSetTexture),
            new NameFuncPair("SetMaterials", SetMaterials),
            new NameFuncPair("ClsMaterials", ClsMaterials),

            new NameFuncPair("AddCullingMask", AddCullingMask),
            new NameFuncPair("DelCullingMask", DelCullingMask),
            new NameFuncPair("HasCullingMask", HasCullingMask),

            // Async Methods
            new NameFuncPair("Invoke", Invoke),
            new NameFuncPair("InvokeRepeating", InvokeRepeating),
            new NameFuncPair("CancelInvoke", CancelInvoke),
            new NameFuncPair("StartCoroutine", StartCoroutine),

            new NameFuncPair("LoadLevel", LoadLevel),
            new NameFuncPair("NewLevel", NewLevel),

            new NameFuncPair("GC", GC),
        };

        lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
    }

    public static GameObject FindGO(Transform parent, string path)
    {
        GameObject ret = null;
        if (parent == null) {
            ret = GameObject.Find(path);
        } else {
            var t = parent.Find(path);
            ret = t ? t.gameObject : null;
        }
        return ret;
    }

    public static T FindCom<T>(ILuaState lua) where T : Component
    {
        T ret = null;
        var path = lua.OptString(3, null);
        if (path == null) {
            // 未指定子对象
            var uObj = lua.ToUnityObject(1);
            ret = uObj as T;
            if (ret == null) {
                var com = uObj as Component;
                var go = com ? com.gameObject : uObj as GameObject;
                if (go) ret = go.GetComponent<T>() ?? go.GetComponentInChildren<T>();
            }
        } else {
            // 指定了子对象
            var trans = lua.ToComponent<Transform>(1);
            var go = FindGO(trans, path);
            if (go) {
                ret = go.GetComponent<T>();
            }
        }

        if (ret == null) {
            LogMgr.W("FindCom: {0} 未找到<{1}>组件", lua.ToAnyObject(1), typeof(T));
        }
        return ret;
    }

    /// <summary>
    /// ======= 导出的接口 =======
    /// </summary>
	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int AppQuit(ILuaState lua)
    {
        LogMgr.I("游戏退出...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int LogI(ILuaState lua)
    {
        string format;
        object[] Args;
        lua.ToStringFromatArgs(1, out format, out Args);
        LogMgr.I(format, Args);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int LogD(ILuaState lua)
    {
        string format;
        object[] Args;
        lua.ToStringFromatArgs(1, out format, out Args);
        LogMgr.D(format, Args);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int LogW(ILuaState lua)
    {
        string format;
        object[] Args;
        lua.ToStringFromatArgs(1, out format, out Args);
        LogMgr.W(format, Args);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int LogE(ILuaState lua)
    {
        string format;
        object[] Args;
        lua.ToStringFromatArgs(1, out format, out Args);
        LogMgr.E(format, Args);
        return 0;
    }


	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int IsObject(ILuaState lua)
    {
        Object o = lua.ToUnityObject(1);
        lua.PushBoolean(o != null);
        return 1;
    }
    /// <summary>
    /// 判断对象是否是活动状态并把状态传递个lua
    /// </summary>
    /// <param name="lua"></param>
    /// <returns></returns>
	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int IsActive(ILuaState lua)
    {
        Debug.Log("判断对象状态");
        GameObject go = lua.ToGameObject(1);
        lua.PushBoolean(go != null && go.activeSelf);
        return 1;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lua"></param>
    /// <returns></returns>
	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int Destroy(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        float delay = (float)lua.OptNumber(2, 0f);
        if (go != null) {
            if (delay == 0f) {
                ObjectPoolManager.DestroyPooled(go);
            } else {
                ObjectPoolManager.DestroyPooled(go, delay);
            }
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int Delete(ILuaState lua)
    {
        Object obj = lua.ToUnityObject(1);
        float delay = (float)lua.OptNumber(2, 0f);
        if (obj != null) {
            Object.Destroy(obj, delay);
            if (lua.Type(1) == LuaTypes.LUA_TUSERDATA) {
                MetaMethods.__gc(lua);
            }
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int FindGameObject(ILuaState lua)
    {
        Transform trans = lua.ToComponent<Transform>(1);
        string path = lua.ChkString(2);
        GameObject ret = FindGO(trans, path);
        if (ret != null) {
            lua.PushLightUserData(ret);
        } else {
            lua.PushNil();
        }
        return 1;
    }


	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int FindComponent(ILuaState lua)
    {
        Transform trans = lua.ToComponent<Transform>(1);
        string path = lua.ToString(2);
        string comName = lua.ToString(3);
        GameObject ret = FindGO(trans, path);
        if (!ret) return 0;

        Component com = ret.GetComponent(comName);
        if (com != null) {
            lua.PushLightUserData(com);
        } else {
            lua.PushNil();
        }
        return 1;
    }


    public static void ApplyParentAndChild(ILuaState lua, out GameObject parent, out GameObject child)
    {
        parent = lua.ToGameObject(1);
        string strChild = null;
        if (lua.IsString(2)) {
            strChild = lua.ToString(2);
            child = AssetsMgr.A.Load<GameObject>(strChild);
        } else {
            child = lua.ToGameObject(2);
        }
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int AddChild(ILuaState lua)
    {
        GameObject parent, child;
        ApplyParentAndChild(lua, out parent, out child);
        if (child != null) {
            string goName = lua.OptString(3, child.name);
            int sibling = lua.OptInteger(4, -1);
            GameObject go = ObjectPoolManager.AddChild(parent, child, sibling);
            if (go != null) {
                go.name = goName;
                lua.PushLightUserData(go);
            } else {
                lua.PushNil();
            }
        } else {
            lua.PushNil();
        }

        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int NewChild(ILuaState lua)
    {
        GameObject parent, child;
        ApplyParentAndChild(lua, out parent, out child);
        if (child != null) {
            string goName = lua.OptString(3, child.name);
            GameObject go = GoTools.AddChild(parent, child);
            int sibling = lua.OptInteger(4, -1);
            if (parent) {
                if (sibling < 0) sibling = parent.transform.childCount + sibling;
                go.transform.SetSiblingIndex(sibling);
            }
            if (go != null) {
                go.name = goName;
                lua.PushLightUserData(go);
            } else {
                lua.PushNil();
            }
        } else {
            lua.PushNil();
        }
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int NeedChild(ILuaState lua)
    {
        var go = lua.ToGameObject(1);
        string path = lua.ChkString(2);
        lua.PushLightUserData(go.NeedChild(path));
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetLayer(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        string layerName = lua.ChkString(2);
        if (go) {
            go.SetLayerRecursively(LayerMask.NameToLayer(layerName));
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int AddComponent(ILuaState lua)
    {
        var go = lua.ToGameObject(1);
        var type = lua.ToUserData(2) as System.Type;
        if (go != null && type != null) {
            Component com = go.AddComponent(type);
            lua.PushLightUserData(com);
        } else {
            lua.PushNil();
        }

        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DelComponent(ILuaState lua)
    {
        var go = lua.ToGameObject(1);
        string comTypeName = lua.ChkString(2);
        if (go != null && !string.IsNullOrEmpty(comTypeName)) {
            Object.Destroy(go.GetComponent(comTypeName));
        }

        return 0;
    }


    private static IEnumerator SetActiveDelayed(GameObject go, bool active, float delay)
    {
        yield return new WaitForSeconds(delay);
        go.SetActive(active);
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetActive(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        bool active = lua.ToBoolean(2);
        float delay = (float)lua.OptNumber(3, 0f);
        if (go != null) {
            if (delay > 0) {
                ZFrame.UIManager.Instance.StartCoroutine(SetActiveDelayed(go, active, delay));                
            } else {
                go.SetActive(active);
            }
        }
        
        return 0;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SelfActive(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        lua.PushBoolean(go.activeSelf);

        return 0;
    }

    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int ReActive(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        if (go != null) {
            go.SetActive(false);
            go.SetActive(true);
        }
        return 0;
    }


    private static IEnumerator SetEnableDelayed(Behaviour bev, bool enabled, float delay)
    {
        yield return new WaitForSeconds(delay);
        bev.enabled = enabled;
    }


    private static IEnumerator SetEnableDelayed(Collider cld, bool enabled, float delay)
    {
        yield return new WaitForSeconds(delay);
        cld.enabled = enabled;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetEnable(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        string comName = lua.ChkString(2);
        bool enabled = lua.ToBoolean(3);
        float delay = (float)lua.OptNumber(4, 0f);
        var com = go.GetComponent(comName);

        var bev = com as Behaviour;
        if (bev) {
            if (delay <= 0) {
                bev.enabled = enabled;
            } else {
                ZFrame.UIManager.Instance.StartCoroutine(SetEnableDelayed(bev, enabled, delay));
            }
            return 0;
        }

        var cld = com as Collider;
        if (cld) {
            if (delay <= 0) {
                cld.enabled = enabled;
            } else {
                ZFrame.UIManager.Instance.StartCoroutine(SetEnableDelayed(cld, enabled, delay));
            }
            return 0;
        }

        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetParent(ILuaState lua)
    {
        Transform trans = lua.ToComponent<Transform>(1);
        Transform parent = lua.ToComponent<Transform>(2);
        bool worldPositionStays = lua.OptBoolean(3, true);
        if (trans) {
            trans.SetParent(parent, worldPositionStays);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetSibling(ILuaState lua)
    {
        Transform trans = lua.ToComponent<Transform>(1);
        int index = lua.ChkInteger(2);
        if (trans) {
            if (index > -1) {
                trans.SetSiblingIndex(index);
            } else {
                int n = trans.parent.childCount;
                trans.SetSiblingIndex(n + index);
            }
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int RendererSetValue(ILuaState lua)
    {
#if false
		Renderer renderer = toComponent<Renderer>((UnityEngine.Object)lua.ToUserData(1));
		if (renderer != null) {
			string propertyName = lua.L_CheckString(2);
			LuaType luaT = lua.Type(3);
			switch (luaT) {
			case LuaType.LUA_TSTRING: {
				string strColor = lua.L_CheckString(3);
				int iColor = System.Convert.ToInt32(strColor, 16);
				renderer.material.SetColor(propertyName, NGUIMath.IntToColor(iColor));
			} break;
			case LuaType.LUA_TUINT64: {
				int val = lua.L_CheckInteger(3);
				renderer.material.SetInt(propertyName, val);
			} break;
			case LuaType.LUA_TNUMBER: {
				float val = (float)lua.L_CheckNumber(3);
				renderer.material.SetFloat(propertyName, val);
			} break;
            case LuaType.LUA_TUSERDATA:
            case LuaType.LUA_TLIGHTUSERDATA: {
                System.Object o = lua.ToUserData(3);
                if (o is Texture) {
                    renderer.material.SetTexture(propertyName, (Texture)o);
                }
                } break;
			default: break;
			}
		}
#endif
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int RendererSetTexture(ILuaState lua)
    {
        Renderer renderer = lua.ToComponent<Renderer>(1);
        string propertyName = lua.ChkString(2);
        float tilingX = (float)lua.ChkNumber(3);
        float tilingY = (float)lua.ChkNumber(4);
        float offsetX = (float)lua.ChkNumber(5);
        float offsetY = (float)lua.ChkNumber(6);
        if (renderer != null) {
            renderer.material.SetTextureScale(propertyName, new Vector2(tilingX, tilingY));
            renderer.material.SetTextureOffset(propertyName, new Vector2(offsetX, offsetY));
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetMaterials(ILuaState lua)
    {
        Renderer renderer = lua.ToComponent<Renderer>(1);
        int nMat = lua.ChkInteger(2);
        Material[] mats = new Material[nMat];
        for (int i = 0; i < nMat; ++i) {
            mats[i] = lua.ToUserData(i + 3) as Material;
        }
        if (renderer) {
            renderer.enabled = true;
            renderer.materials = mats;
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int ClsMaterials(ILuaState lua)
    {
        Renderer renderer = lua.ToComponent<Renderer>(1);
        if (renderer) {
            renderer.enabled = false;
            renderer.materials = new Material[1];
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int AddCullingMask(ILuaState lua)
    {
        int cullingMask = lua.ChkInteger(1);
        string layerName = lua.ChkString(2);
        lua.PushInteger(cullingMask.AddCullingMask(layerName));
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DelCullingMask(ILuaState lua)
    {
        int cullingMask = lua.ChkInteger(1);
        string layerName = lua.ChkString(2);
        lua.PushInteger(cullingMask.DelCullingMask(layerName));
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int HasCullingMask(ILuaState lua)
    {
        int cullingMask = lua.ChkInteger(1);
        string layerName = lua.ChkString(2);
        lua.PushBoolean(cullingMask.HasCullingMask(layerName));
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SendMessage(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        string method = lua.ChkString(2);

        object obj = lua.ToAnyObject(3);
        if (go != null) {
            if (obj == null) {
                go.SendMessage(method, SendMessageOptions.RequireReceiver);
            } else {
                go.SendMessage(method, obj, SendMessageOptions.RequireReceiver);
            }
        }

        return 0;
    }

    public static IEnumerator LuaInvoke(LuaFunction func, object param, params object[] args)
    {
        yield return param;

        using (func) {
            var L = func.GetLuaState();
            var top = func.BeginPCall();
            for (int i = 0; i < args.Length; ++i) {
                L.PushAnyObject(args[i]);
            }
            func.PCall(top, args.Length);
            func.EndPCall(top);
        }
    }

    private static IEnumerator LuaInvokeRepeating(LuaFunction func, object delay, object wait, params object[] args)
    {
        if (delay != null) {
            yield return delay;
        }

        for (int i = 0; ; ++i) {
            var L = func.GetLuaState();
            var top = func.BeginPCall();
            L.PushInteger(i);
            for (int j = 0; j < args.Length; ++j) {
                L.PushAnyObject(args[j]);
            }
            func.PCall(top, 1 + args.Length);
            var finished = L.ChkBoolean(top + 1);
            func.EndPCall(top);
            if (finished) break;
            yield return wait;
        }

        func.Dispose();
    }

    /// <summary>
    /// Invoke一个Lua函数
    /// </summary

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int Invoke(ILuaState lua)
    {
        var mono = lua.ToComponent(1, typeof(LuaComponent)) as MonoBehaviour;
        if (mono == null) mono = ZFrame.UIManager.Instance;
        if (mono.isActiveAndEnabled) {
            var func = lua.ToLuaFunction(2);
            if (func != null) {
                object wait = lua.ToYieldValue(3);
                var args = lua.ToParamsObject(4, lua.GetTop() - 3);
                mono.StartCoroutine(LuaInvoke(func, wait, args));
            } else {
                LogMgr.W("{0}: function is nil for Invoke", mono);
            }
        } else {
            LogMgr.W("{0} is nor Active or Enable for Invoke", mono);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int InvokeRepeating(ILuaState lua)
    {        
        var mono = lua.ToComponent(1, typeof(LuaComponent)) as MonoBehaviour;
        if (mono == null) mono = ZFrame.UIManager.Instance;
        if (mono.isActiveAndEnabled) {
            var func = lua.ToLuaFunction(2);
            if (func != null) {
                object delay = lua.ToYieldValue(3);
                object wait = lua.ToYieldValue(4);
                var args = lua.ToParamsObject(5, lua.GetTop() - 4);
                mono.StartCoroutine(LuaInvokeRepeating(func, delay, wait, args));
            } else {
                LogMgr.W("{0}: function is nil for InvokeRepeating", mono);
            }
        } else {
            LogMgr.W("{0} is nor Active or Enable for InvokeRepeating", mono);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int CancelInvoke(ILuaState lua)
    {
        var mono = lua.ToComponent(1, typeof(LuaComponent)) as MonoBehaviour;
        if (mono == null) mono = ZFrame.UIManager.Instance;
        if (mono.isActiveAndEnabled) {
            mono.StopAllCoroutines();
        }
        return 0;
    }


    private static IEnumerator LuaCoroutine(ILuaState lua, LuaFunction luaRef, object yieldRet)
    {
        lua.GetGlobal("coroutine", "status");
        var coro_status = lua.L_Ref(LuaIndexes.LUA_REGISTRYINDEX);
        lua.GetGlobal("coroutine", "resume");
        var corou_resume = lua.L_Ref(LuaIndexes.LUA_REGISTRYINDEX);

        for (bool coroRet = true; ;) {
            yield return yieldRet;

            var oldTop = lua.GetTop();

            // 检查协程状态
            lua.GetRef(coro_status);
            luaRef.push(lua);
            lua.Call(1, 1);
            var coStat = lua.ToString(-1);
            lua.Pop(1);
            if (coStat == "dead") {                
                break;
            }
            
            // 再启动协程
            lua.GetRef(corou_resume);
            luaRef.push(lua);
            var status = lua.PCall(1, 2, 0);
            if (status != LuaThreadStatus.LUA_OK) {
                LogMgr.E(lua.ToString(-1));
                lua.SetTop(oldTop);
                break;
            }
            coroRet = lua.ToBoolean(-2);
            yieldRet = lua.ToAnyObject(-1);
            // 弹出返回值
            lua.Pop(2);
            if (!coroRet) {
                LogMgr.E("{0}", yieldRet);
                break;
            }
        }

        lua.L_Unref(LuaIndexes.LUA_REGISTRYINDEX, coro_status);
        lua.L_Unref(LuaIndexes.LUA_REGISTRYINDEX, corou_resume);
        luaRef.Dispose();
    }

    /// <summary>
    /// void function(@MonoBehaviour, function, [args, ...]) 
    /// ！！！严重注意：
    ///     Lua的协程有独立的栈空间和局部变量，
    ///     如果在这过程中保存变量的引用，
    ///     在退出协程后，其引用会失效。
    /// </summary>

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int StartCoroutine(ILuaState lua)
    {
        var oldTop = lua.GetTop();
        // 参数列表 (mono, func, [args, ...])
        var mono = lua.ToComponent(1, typeof(LuaComponent)) as MonoBehaviour;
        if (mono == null) mono = ZFrame.UIManager.Instance;
        if (mono.isActiveAndEnabled) {
            // 创建lua协程
            lua.GetGlobal("coroutine", "create");
            lua.PushValue(2);
            var status = lua.PCall(1, 1, 0);
            if (status == LuaThreadStatus.LUA_OK) {
                // 保存协程引用(这个其实不是函数，应该是LUA_TTHREAD类型)
                var r = lua.L_Ref(LuaIndexes.LUA_REGISTRYINDEX);
                var luaRef = new LuaFunction(r, lua);

                var top = lua.GetTop();
                // 协程启动方法入栈
                lua.GetGlobal("coroutine", "resume");
                // 协程入栈
                luaRef.push(lua);
                // 启动参数入栈
                for (int i = 3; i < top + 1; ++i) {
                    lua.PushValue(i);
                }
                
                // 总参数数量=协程+启动参数数量，即：1 + (top - 2)
                status = lua.PCall(top - 1, 2, 0);
                if (status == LuaThreadStatus.LUA_OK) {                    
                    var coroRet = lua.ToBoolean(-2);
                    var yieldRet = lua.ToAnyObject(-1);
                    // 弹出返回值
                    lua.Pop(2);                    
                    if (coroRet) {
                        var coro = mono.StartCoroutine(LuaCoroutine(lua, luaRef, yieldRet));
                        lua.PushLightUserData(coro);
                        return 1;
                    } else LogMgr.E("{0}", yieldRet);
                } else LogMgr.E("coroutine.resume FAIL!");
            } else LogMgr.E("coroutine.create FAIL!");
            lua.SetTop(oldTop);
        } else {
			LogMgr.W("MonoBehaviour<{0}> not exist OR not isActiveAndEnabled", mono);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int LoadLevel(ILuaState lua)
    {
        string levelName = lua.ChkString(1);
        string loadingFx = lua.OptString(2, null);
        WNDLoading.LoadLevel(levelName, loadingFx);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int NewLevel(ILuaState lua)
    {
        string levelName = lua.ChkString(1);
        AssetsMgr.A.Loader.UnloadAll(true);
        SceneManager.LoadScene(levelName);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GC(ILuaState lua)
    {
        AssetsMgr.GC();
        return 0;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ZFrame;
using ZFrame.UGUI;
using ZFrame.Tween;
using LuaInterface;
using System.IO;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;

public static class LibUGUI
{
    public const string LIB_NAME = "libugui.cs";

    public static void OpenLib(ILuaState lua)
    {
        var define = new NameFuncPair[]
        {
            new NameFuncPair("SetLocalize", SetLocalize),

            new NameFuncPair("GenLuaTable", GenLuaTable),
            new NameFuncPair("CreateWindow", CreateWindow),

            new NameFuncPair("Select", Select),
            new NameFuncPair("Deselect", Deselect),

            // Property Controll
            new NameFuncPair("SetAlpha", SetAlpha),
            new NameFuncPair("SetColor", SetColor),
            new NameFuncPair("SetGradient", SetGradient),
            new NameFuncPair("SetText", SetText),
            new NameFuncPair("SetSprite", SetSprite),
            new NameFuncPair("SetTexture", SetTexture),
            new NameFuncPair("SetPhoto", SetPhoto),
            new NameFuncPair("SetPos", SetPos),
            new NameFuncPair("SetSiz", SetSiz),
            new NameFuncPair("SetAnchor", SetAnchor),
            new NameFuncPair("SetPivot", SetPivot),
            new NameFuncPair("SetInteractable", SetInteractable),
            new NameFuncPair("SetGrayscale", SetGrayscale),
            new NameFuncPair("GetPos", GetPos),
            new NameFuncPair("GetSiz", GetSiz),
            new NameFuncPair("GetPadding", GetPadding),

            new NameFuncPair("GetTogglesOn", GetTogglesOn),
			new NameFuncPair("AllTogglesOff", AllTogglesOff),

            new NameFuncPair("IsScreenPointInRect", IsScreenPointInRect),

            new NameFuncPair("InsideScreen", InsideScreen),
            new NameFuncPair("DOSiblingByFar", DOSiblingByFar),
            new NameFuncPair("DOAnchor", DOAnchor),            
            new NameFuncPair("DOMethod", DOMethod),
            new NameFuncPair("DOFade", DOFade),
            new NameFuncPair("DOTween", DOTween),
            new NameFuncPair("FreeTween", FreeTween),
            new NameFuncPair("DOShake", DOShake),
            new NameFuncPair("KillTween", KillTween),
            new NameFuncPair("CompleteTween", CompleteTween),
			new NameFuncPair("WaitForTween", WaitForTween),

            new NameFuncPair("Overlay", Overlay),
            new NameFuncPair("Follow", Follow),

            // Layout Controll
            new NameFuncPair("SetArray", SetArray),

            new NameFuncPair("GetFileList", GetFileList),
            new NameFuncPair("CreateDirectory", CreateDirectory),
            new NameFuncPair("DeleteAllFile", DeleteAllFile),

        };

        lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
    }

    private static bool GenMonoRef(System.Type type, ILuaState lua, Transform trans, string keyName = null)
    {
        var com = trans.GetComponent(type);
        if (com) {
            if (string.IsNullOrEmpty(keyName)) keyName = trans.name;
            lua.SetDict(keyName, com);
            return true;
        }
        return false;
    }

    private static void GenGraphicRef(ILuaState lua, Transform trans)
    {
        var coms= trans.GetComponentsInChildren(typeof(Graphic));
        for (int i = 0; i < coms.Length; ++i) {
            var com = coms[i];
            var r = com.name[0];
            if (r == '&') continue;
            var c = com.name[com.name.Length - 1];
            if (c == '=' || c == '_') continue;
            lua.SetDict(com.name, com);
        }
#if UNITY_EDITOR
        //if (coms.Length > 1) LogMgr.D(trans.getHierarchy() + "#<Graphic> x " + coms.Length);
#endif
    }

    private static void GenMonoRefSelf(ILuaState lua, Transform trans)
    {
        if (GenMonoRef(typeof(Button), lua, trans, "btn")) return;
        if (GenMonoRef(typeof(Toggle), lua, trans, "tgl")) return;
        if (GenMonoRef(typeof(Dropdown), lua, trans, "drp")) return;
        if (GenMonoRef(typeof(Slider), lua, trans, "sld")) return;
    }

    private static void GenLuaTable(ILuaState lua, Transform trans)
    {
        foreach (Transform t in trans) {
            if (t.name.EndsWith("_")) continue;

            var preffix = t.name.Substring(0, 3);
            var isSub = preffix == "Sub";
            var isGrp = preffix == "Grp";
            if (isSub || isGrp) {
                lua.PushString(t.name);
                lua.NewTable(); {
                    GenMonoRefSelf(lua, t);
                    lua.SetDict("root", t.gameObject);
                    if (isGrp) {
                        lua.PushString("Ents");
                        lua.NewTable();
                        lua.SetTable(-3);
                    }

                    GenLuaTable(lua, t);
                }
                lua.SetTable(-3);
            } else if (preffix == "ent") {
                lua.PushString("Ent");
                lua.NewTable(); {
                    GenMonoRefSelf(lua, t);
                    lua.SetDict("go", t.gameObject);
                    GenLuaTable(lua, t);
                }
                lua.SetTable(-3);
                t.gameObject.SetActive(false);
            } else if (preffix == "Elm") {
                lua.SetDict(t.name, t);
            } else {
                if (GenMonoRef(typeof(Selectable), lua, t)) continue;
                GenGraphicRef(lua, t);
            }
        }
    }

    /******************************************
     导出的接口
    *******************************************/

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetLocalize(ILuaState lua)
    {
        if (!UILabel.LOC) {
            UILabel.LOC = AssetsMgr.A.Load(typeof(ScriptableObject), "Launch/Localization") as Localization;
        }
        if (UILabel.LOC) {
            UILabel.LOC.currentLang = lua.ToString(1);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GenLuaTable(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        string key = lua.ChkString(2);
        if (go) {
            go.SetActive(true);
            var trans = go.transform;
            lua.NewTable(); {
                lua.SetDict(key, go);
                GenMonoRefSelf(lua, trans);
                GenLuaTable(lua, trans);
            }
        }

        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int CreateWindow(ILuaState lua)
    {
        string prefab = lua.ChkString(1);
        int index = lua.OptInteger(2, 0);
        GameObject go = UIManager.Instance.CreateWindow(prefab, index);
        lua.PushLightUserData(go);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int Select(ILuaState lua)
    {
        var toSelect = lua.ToGameObject(1);
        if (toSelect) {
			if (!UnityEngine.EventSystems.EventSystem.current.alreadySelecting) {
            	UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(toSelect);
			}
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int Deselect(ILuaState lua)
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetAlpha(ILuaState lua)
    {
        var go = lua.ToGameObject(1);
        var alpha = lua.ToSingle(2);

        var cvGrp = go.GetComponent<CanvasGroup>();
        if (cvGrp) {
            cvGrp.alpha = alpha;
            return 0;
        }

        var graphic = go.GetComponent<Graphic>();
        if (graphic) {
            var c = graphic.color;
            c.a = alpha;
            graphic.color = c;
            return 0;
        }

        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetColor(ILuaState lua)
    {
        var all = lua.OptBoolean(3, false);
        if (all) {
            var go = lua.ToGameObject(1);
            if (go) {
                var color = lua.ToColor(2);
                Graphic[] graphics = go.GetComponentsInChildren<Graphic>();
                for (int i = 0; i < graphics.Length; ++i) {
                    graphics[i].color = color;
                }
            }
        } else {
            var graphic = lua.ToComponent<Graphic>(1);
            if (graphic) graphic.color = lua.ToColor(2);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetGradient(ILuaState lua)
    {
        var gradient = lua.ToComponent<ZFrame.UGUI.Gradient>(1);        
        if (gradient) {
            var c1 = lua.ToColor(2);
            var c2 = lua.ToColor(3);
            gradient.vertex1 = c1;
            gradient.vertex2 = c2;
        }
        return 0;
    }

    /// <summary>
    /// function(Component|GameObjct, string@text, [string@path])
    /// </summary>

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetText(ILuaState lua)
    {
        var text = LibUnity.FindCom<Text>(lua);
        if (text) text.text = lua.ToLuaString(2);

        return 0;
    }

    /// <summary>
    /// function(Component|GameObjct, string|Sprite, [string@path])
    /// </summary>

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetSprite(ILuaState lua)
    {
        if (lua.IsFunction(4)) {
            var sp = LibUnity.FindCom<UISprite>(lua);
            if (sp) {
                var texPath = lua.ChkString(2);
                var func = lua.ToLuaFunction(4);
                var param = lua.ToAnyObject(5);
                sp.Load(texPath, (o, p) => {
                    var L = func.GetLuaState();
                    var top = func.BeginPCall();
                    L.PushLightUserData(o);
                    L.PushAnyObject(p);
                    func.PCall(top, 2);
                    func.EndPCall(top);
                    func.Dispose();
                }, param);
            }
            return 0;
        }

        var obj = lua.ToAnyObject(2);
        var path = obj as string;
        if (path != null) {
            var sp = LibUnity.FindCom<UISprite>(lua);
            if (sp) {
                if (path.StartsWith("Atlas")) {
                    sp.spritePath = path;
                } else {
                    sp.spriteName = path;
                }
            }
            return 0;
        } 

        var sprite = obj as Sprite;
        if (sprite) {
            var img = LibUnity.FindCom<Image>(lua);
            if (img) img.sprite = obj as Sprite;
            return 0;
        }

        LogMgr.W("SetSprite: 未知的Sprite＝{0}", obj);
        return 0;
    }

    /// <summary>
    /// function(Component|GameObjct, string|Sprite, [string@path], [function])
    /// </summary>

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetTexture(ILuaState lua)
    {
        if (lua.IsFunction(4)) {
            var tex = LibUnity.FindCom<UITexture>(lua);            
            if (tex) {
                var texPath = lua.ChkString(2);
                var func = lua.ToLuaFunction(4);
                var param = lua.ToAnyObject(5);
                tex.Load(texPath, (o, p) => {
                    var L = func.GetLuaState();
                    var top = func.BeginPCall();
                    L.PushLightUserData(o);
                    L.PushAnyObject(p);
                    func.PCall(top, 2);
                    func.EndPCall(top);
                    func.Dispose();
                }, param);
            }
            return 0;
        }

        var obj = lua.ToAnyObject(2);
        var path = obj as string;
        if (path != null) {
            var tex = LibUnity.FindCom<UITexture>(lua);
            if (tex) tex.texturePath = path;
            return 0;
        } 

        var texture = obj as Texture;
        if (texture) {
            var img = LibUnity.FindCom<RawImage>(lua);
            if (img) img.texture = texture;
            return 0;
        }
        
        LogMgr.W("SetTexture: 未知的Texture＝{0}", obj);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetPhoto(ILuaState lua)
    {
        var tex = LibUnity.FindCom<UITexture>(lua);
        string name = lua.ChkString(2);
        var funcRef = lua.ToLuaFunction(3);
        ZFrame.Asset.DelegateObjectLoaded onLoaded = null;
        if (funcRef != null) {
            onLoaded = (o, p) => {
                LogMgr.D("loaded {0}, {1}", o, p);
                funcRef.Invoke(o, p);
                funcRef.Dispose();
                var disposer = p as System.IDisposable;
                if (disposer != null) disposer.Dispose();
            };
        }
        if (tex != null) {
            Debug.Log("UITexture组件存在");
        } else {
            Debug.Log("UITexture组件不存在");
        }
        SDKMgr.Instance.OnLoadPhoto(tex, name, onLoaded);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetPos(ILuaState lua)
    {
        var rect = lua.ToComponent<RectTransform>(1);
        if (rect) {
            var v3 = rect.anchoredPosition3D;
            if (lua.Type(2) == LuaTypes.LUA_TNUMBER) {
                v3.x = lua.ToSingle(2);
            }
            if (lua.Type(3) == LuaTypes.LUA_TNUMBER) {
                v3.y = lua.ToSingle(3);
            }
            if (lua.Type(4) == LuaTypes.LUA_TNUMBER) {
                v3.z = lua.ToSingle(4);
            }
            rect.anchoredPosition3D = v3; 
        } else LogMgr.W("SetPos Fail: target is NULL");
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetSiz(ILuaState lua)
    {
        var rect = lua.ToComponent<RectTransform>(1);
        if (lua.Type(2) == LuaTypes.LUA_TNUMBER) {
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lua.ToSingle(2));
        }
        if (lua.Type(3) == LuaTypes.LUA_TNUMBER) {
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lua.ToSingle(3));
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetAnchor(ILuaState lua)
    {
        var rect = lua.ToComponent<RectTransform>(1);
        var index = 2;

        var luaT = lua.Type(index);
        if (luaT == LuaTypes.LUA_TTABLE) {
            rect.anchorMin = lua.ToVector2(index);
            index += 1;
        } else if (luaT == LuaTypes.LUA_TNUMBER) {
            rect.anchorMin = new Vector2(lua.ToSingle(index), lua.ToSingle(index + 1));
            index += 2;
        } else {
            index += 1;
        }

        luaT = lua.Type(index);
        if (luaT == LuaTypes.LUA_TTABLE) {
            rect.anchorMax = lua.ToVector2(index);
            index += 1;
        } else if (luaT == LuaTypes.LUA_TNUMBER) {
            rect.anchorMax = new Vector2(lua.ToSingle(index), lua.ToSingle(index + 1));
            index += 2;
        } else {
            index += 1;
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetPivot(ILuaState lua)
    {
        var rect = lua.ToComponent<RectTransform>(1);
        var count = lua.GetTop(); 
        if (count == 2) {
            rect.pivot = lua.ToVector2(2);
        } else {
            rect.pivot = new Vector2(lua.ToSingle(2), lua.ToSingle(3));
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetInteractable(ILuaState lua)
    {
        var select = lua.ToComponent<Selectable>(1);
        if (select) select.interactable = lua.ToBoolean(2);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetGrayscale(ILuaState lua)
	{
		UGUITools.SetGrayscale(lua.ToGameObject(1), lua.ToBoolean(2));
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetPos(ILuaState lua)
    {
        var rect = lua.ToComponent<RectTransform>(1);
        if (rect) {
            lua.PushUData(rect.anchoredPosition3D);
            return 1;
        } else return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetSiz(ILuaState lua)
	{
		var rect = lua.ToComponent<RectTransform>(1);
		if (rect) {
			lua.PushUData(rect.rect.size);
			return 1;
		} else return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetPadding(ILuaState lua)
    {
        var layoutGrp = lua.ToComponent<LayoutGroup>(1);
        if (layoutGrp) {
            var pading = layoutGrp.padding;
            var hori = new Vector2(pading.left, pading.right);
            var vert = new Vector2(pading.top, pading.bottom);
            lua.PushUData(hori);
            lua.PushUData(vert);
            return 2;
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetTogglesOn(ILuaState lua)
    {
        var tglGrp = lua.ToComponent<ToggleGroup>(1);
        if (tglGrp) {
            var itor = tglGrp.ActiveToggles().GetEnumerator();
			lua.NewTable();
			int i = 0;
			while (itor.MoveNext()) {
				lua.PushInteger(++i);
				lua.PushLightUserData(itor.Current);
				lua.SetTable(-3);
			}
			return 1;
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int AllTogglesOff(ILuaState lua)
	{
		var tglGrp = lua.ToComponent<ToggleGroup>(1);
		if (tglGrp) tglGrp.SetAllTogglesOff();
		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int IsScreenPointInRect(ILuaState lua)
    {
        var screenPoint = lua.ToVector2(1);
        var rect = lua.ToComponent<RectTransform>(2);
        var camera = rect.GetComponentInParent<Canvas>().worldCamera;
        var ret = RectTransformUtility.RectangleContainsScreenPoint(rect, screenPoint, camera);
        lua.PushBoolean(ret);
        return 1;
    }

    /// <summary>
    /// 限制一个UI对象在屏幕内
    /// </summary>

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int InsideScreen(ILuaState lua)
    {
        var rect = lua.ToComponent<RectTransform>(1);
        var set = lua.OptBoolean(2, true);
        var pos = rect.InsideScreenPosition();
        if (set) {
            rect.anchoredPosition = pos;
        }
        lua.PushUData(pos);
        return 1;
    }

    /// <summary>
    /// 根据距离对<Transform>进行兄弟排列
    /// </summary>

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int DOSiblingByFar(ILuaState lua)
    {
        var parent = lua.ToComponent<Transform>(1);
        var center = lua.ToVector3(2);
        if (parent) {
            var origin = center;
            var list = new List<Transform>();
            for (int i = 0; i < parent.childCount; ++i) {
                list.Add(parent.GetChild(i));
            }
            list.Sort((Transform a, Transform b) => {
                var x = Vector3.Distance(origin, a.position);
                var y = Vector3.Distance(origin, b.position);
                var ret = (int)((x - y) * 10000);
                return -ret;
            });
            SiblingByFar.DOSibling(list, 0);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DOAnchor(ILuaState lua)
    {
        var from = lua.ToComponent<RectTransform>(1);        
        var pivotF = lua.ToVector2(2);
        var to = lua.ToComponent<RectTransform>(3);
        var pivotT = lua.ToVector2(4);
        var offset = lua.ToVector2(5);
        var set = lua.OptBoolean(6, true);
        var cv = from.GetComponentInParent<Canvas>();
        var v2Ret = cv.AnchorPosition(from, pivotF, to, pivotT, offset);
        if (set) from.anchoredPosition = v2Ret;
        lua.PushUData(v2Ret);
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DOMethod(ILuaState lua)
    {
		var com = lua.ToComponent<LuaComponent>(1);
		var method = lua.ChkString(2);
		if (com) com.CallMethod(method, true, 0);
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DOFade(ILuaState lua)
    {
        GameObject go = lua.ToGameObject(1);
        var group = lua.ToEnumValue(2, typeof(FadeGroup));        
        var func = lua.ToLuaFunction(3);
        bool reset = lua.OptBoolean(4, false);
        if (go) {
            ZTween.Stop(go);
            var tw = group != null ? FadeTool.DOFade(go, (FadeGroup)group, reset) : FadeTool.DOFade(go, reset);
            if (func != null) {
                if (tw != null) {
                    tw.CompleteWith((o) => { func.Invoke(go); func.Dispose(); });
                } else {
                    LuaScriptMgr.Instance.StartCoroutine(LibUnity.LuaInvoke(func, null, go));
                }
            }
        }
        return 0;
    }

    private static ITweenable ToTweenable(this ILuaState lua, object tweenObject, object tweenType)
    {
        ITweenable tweenable = null;
        if (tweenType == null) {
            tweenable = tweenObject as ITweenable;
        } else {
            var com = tweenObject as Component;
            GameObject go = com != null ? com.gameObject : tweenObject as GameObject;
            if (go) {
                if (tweenType is string) {
                    var tweenName = tweenType as string;
                    tweenable = go.GetComponent(tweenName) as ITweenable;
                    if (tweenable == null) {
                        switch (tweenName) {
                            case "TweenPosition": tweenable = go.AddComponent(typeof(TweenPosition)) as ITweenable; break;
                            case "TweenRotation": tweenable = go.AddComponent(typeof(TweenRotation)) as ITweenable; break;
                            case "TweenScaling": tweenable = go.AddComponent(typeof(TweenScaling)) as ITweenable; break;
                            case "TweenTransform": tweenable = go.AddComponent(typeof(TweenTransform)) as ITweenable; break;
                            case "TweenAlpha": tweenable = go.AddComponent(typeof(TweenAlpha)) as ITweenable; break;
                            case "TweenSize": tweenable = go.AddComponent(typeof(TweenSize)) as ITweenable; break;
                            default: break;
                        }
                    }
                } else if (tweenType is System.Type) {
                    var tweenerType = tweenType as System.Type;
                    tweenable = go.GetComponent(tweenerType) as ITweenable;
                    if (tweenable == null) {
                        tweenable = go.AddComponent(tweenerType) as ITweenable;
                    }
                }
            }
        }
        return tweenable;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DOTween(ILuaState lua)
    {
        ITweenable tweenable = lua.ToTweenable(lua.ToUserData(2), lua.ToAnyObject(1));
        object from = lua.ToAnyObject(3);
        object to = lua.ToAnyObject(4);
        float duration = lua.ToSingle(5);
        Ease ease = (Ease)lua.OptEnumValue(6, typeof(Ease), Ease.Linear);
        float delay = (float)lua.OptNumber(7, 0);
        var funcRef = lua.ToLuaFunction(8);
		bool ignoreTimescale = lua.OptBoolean(9, false);

        if (tweenable != null) {
			var tw = tweenable.Tween(from, to, duration)
				.EaseBy(ease).DelayFor(delay).SetUpdate(UpdateType.Normal, ignoreTimescale);
            if (funcRef != null) {
                tw.CompleteWith((o) => { funcRef.Invoke(o); funcRef.Dispose(); });
            }
            lua.PushLightUserData(tw);
        } else lua.PushNil();
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int FreeTween(ILuaState lua)
    {
        var tweenType = lua.ToAnyObject(1);
        var tweenObj = lua.ToUserData(2);
        ITweenable tweenable = lua.ToTweenable(tweenObj, tweenType);
        if (tweenable != null) {
            object from = lua.ToAnyObject(3);
            object to = lua.ToAnyObject(4);

            var tweenParam = lua.ToJsonObj(5);
            float duration = tweenParam.toValue<float>("duration");
            var ease = tweenParam.toValue("ease", Ease.Linear);
            var delay = tweenParam.toValue("delay", 0f);
            var loops = tweenParam.toValue("loops", 0);
            var loopType = tweenParam.toValue("loopType", LoopType.Restart);
            var updateType = tweenParam.toValue("updateType", UpdateType.Normal);
            var ignoreTimescale = tweenParam.toValue("ignoreTimescale", true);

            LuaFunction onUpdate = null;
            var joUpdate = tweenParam["update"];
            if (joUpdate != null) onUpdate = joUpdate.ToType(typeof(LuaFunction), null) as LuaFunction;

            LuaFunction onComplete = null;
            var joComplete = tweenParam["complete"];
            if (joComplete != null) onComplete = joComplete.ToType(typeof(LuaFunction), null) as LuaFunction;

            var tw = tweenable.Tween(from, to, duration);
            if (tw != null) {
                tw.EaseBy(ease).DelayFor(delay)
                .LoopFor(loops, loopType)
                .SetUpdate(updateType, ignoreTimescale);
                if (onUpdate != null) tw.UpdateWith((t, o) => onUpdate.Invoke(t, o));
                if (onComplete != null) tw.CompleteWith((o) => { onComplete.Invoke(o); onComplete.Dispose(); });
                lua.PushLightUserData(tw);
                return 1;
            }
        }
        LogMgr.W("Free tween fail: {0} of {1}", tweenType, tweenObj);
        return 0;
    }


	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DOShake(ILuaState lua)
    {
        var cam = lua.ToComponent<Camera>(1);
        var duration = lua.ToSingle(2);
        var strength = (float)lua.OptNumber(3, 3f);
        var vibrato = lua.OptInteger(4, 10);
		lua.PushLightUserData(cam.ShakePosition(duration, strength, vibrato));
        return 1;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int KillTween(ILuaState lua)
    {
        var go = lua.ToGameObject(1);
        if (go) {
            var tweens = go.GetComponents(typeof(ITweenable));
            for (int i = 0; i < tweens.Length; ++i) {
                ZTween.Stop(tweens[i]);
            }
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int CompleteTween(ILuaState lua)
    {
        var go = lua.ToGameObject(1);
        if (go) {
            var tweens = go.GetComponents(typeof(ITweenable));
            for (int i = 0; i < tweens.Length; ++i) {
				ZTween.Stop(tweens[i], true);
            }
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int WaitForTween(ILuaState lua)
	{
		var obj = lua.ToUserData(1);
		var tweener = obj as ZTweener;
		if (tweener != null) {
			lua.PushLightUserData(tweener.WaitForCompletion());
		} else {
			LogMgr.D("WaitForTween: {0} is NOT a tweener!", obj);
			lua.PushNil();
		}
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int Overlay(ILuaState lua)
    {
        Transform trans = lua.ToComponent<Transform>(1);
        Transform target = lua.ToComponent<Transform>(2);
        if (trans) {
            var z = lua.OptSingle(3, trans.position.z);
            trans.position = new Vector3(trans.position.x, trans.position.y, z);
            trans.Overlay(target);
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int Follow(ILuaState lua)
    {
        Transform trans = lua.ToComponent<Transform>(1);
        Transform target = lua.ToComponent<Transform>(2);
        if (trans) {
            var rectTrans = trans as RectTransform;
            var rectTarget = target as RectTransform;
            if (rectTarget) {
                // 场景对象跟随UI
                var follow = trans.gameObject.NeedComponent<FollowUITarget>();
                var smoothTime = (float)lua.OptNumber(4, 0f);
                if (smoothTime > 0f) follow.smoothTime = smoothTime;
				var alwaysSmooth = lua.OptBoolean(5, false);
				follow.alwaysSmooth = alwaysSmooth;
                follow.followTarget = rectTarget;
                follow.depthOfView = lua.ToSingle(3);
                lua.PushLightUserData(follow);
                return 1;
            } else if (rectTrans) {
                // UI跟随场景对象
                var follow = trans.gameObject.NeedComponent<UIFollowTarget>();
                follow.followTarget = target;
                lua.PushLightUserData(follow);
                return 1;
            }
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int SetArray(ILuaState lua)
    {
        var root = lua.ToComponent<Transform>(1);
        if (root) {
            root.gameObject.SetActive(true);
            var childCount = root.childCount;
            var n = lua.ToInteger(2);
            var max = lua.OptInteger(3, childCount);
            for (int i = 0; i < childCount; ++i) {
                var elm = root.GetChild(i);
                elm.gameObject.SetActive(i < max);
                var sp = elm.GetComponent<Graphic>();
                if (sp) {
                    sp.material = i < n ? null : UGUITools.grayScaleMat;
                } else {
                    elm.gameObject.SetActive(i < n);
                }
            }
        } else {
            LogMgr.W("SetArray: 不存在的根节点");
        }
        return 0;
    }
    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int GetFileList(ILuaState lua)
    {
    
        string path = lua.ChkString(1);
        string filter = lua.ChkString(2);
        LogMgr.W(path);
        Debug.Log(path);
        if (string.IsNullOrEmpty(path))
        {
            lua.PushNil();
        }
        else
        {
            if (filter == null || string.IsNullOrEmpty(path)) filter = "*.*";
            List<string> fileList = getFlList(path, filter);
            if (fileList != null)
            {
                lua.PushUData(fileList);
                lua.PushNumber(fileList.Count);
            }
            else
            {
                lua.PushNil();
                lua.PushNil();
            }
            

        }
        return 2;
    }
    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int CreateDirectory(ILuaState lua)
    {
        string path = lua.ChkString(1);
        if (string.IsNullOrEmpty(path))
        {
            lua.PushNil();
        }
        else
        {
            DirectoryInfo folder = new DirectoryInfo(path);
            folder.Create();
            folder = null;
        }
        return 1;
    }
    [MonoPInvokeCallback(typeof(LuaCSFunction))]
    private static int DeleteAllFile(ILuaState lua)
    {
        string path = lua.ChkString(1);
        string filter = lua.ChkString(2);
        if (string.IsNullOrEmpty(path))
        {
            lua.PushNil();
        }
        else
        {
            if (filter == null || string.IsNullOrEmpty(path)) filter = "*.*";
            List<string> fileList = getFlList(path, filter);
            FileInfo fileTemp;
            foreach (string fileName in fileList)
            {
                fileTemp = new FileInfo(fileName);
                fileTemp.Delete();
                fileTemp = null;
                
            }

        }
        return 1;
    }
    private static List<string> getFlList(string path,string filter)
    {
        
        List<string> fileList = new List<string>();
        DirectoryInfo folder = new DirectoryInfo(path);
        Debug.Log("lua调用c#IO类获取文件列表："+path);
        if (!folder.Exists)
        {
            Debug.LogError("lua调用c#IO类获取文件列表---不存在：" + path);
            return null;
        }
        foreach (FileInfo file in folder.GetFiles(filter))
        {
            fileList.Add(file.FullName);
        }
        folder = null;
        folder = null;
        return fileList;

    }

}

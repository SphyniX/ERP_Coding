
public static class WrapFile {

    public static BindType[] getBinds()
    {
        return new BindType[]
        {            
            // Unity - ValueType - 尽量不在此导出，需要Lua表化
                        
            // Unity - Object
            _GT(typeof(UnityEngine.Object)),
            _GT(typeof(UnityEngine.Collider)),
            _GT(typeof(UnityEngine.GameObject)),
            _GT(typeof(UnityEngine.Transform)),
            _GT(typeof(UnityEngine.RectTransform)),
            _GT(typeof(UnityEngine.Component)),
            _GT(typeof(UnityEngine.Behaviour)),
            _GT(typeof(UnityEngine.MonoBehaviour)),
            _GT(typeof(UnityEngine.Camera)),
            _GT(typeof(UnityEngine.Animator)),
            _GT(typeof(UnityEngine.Animation)),

            // Unity
            _GT(typeof(UnityEngine.WaitForSeconds)),            
        
            // Unity - Static
            _GT(typeof(UnityEngine.Time)),
            _GT(typeof(UnityEngine.Application)),
            _GT(typeof(UnityEngine.PlayerPrefs)),
            _GT(typeof(UnityEngine.SystemInfo)),
            _GT(typeof(UnityEngine.Screen)),

            // UGUI - Base
            _GT(typeof(UnityEngine.Canvas)),
            _GT(typeof(UnityEngine.CanvasGroup)),
            _GT(typeof(UnityEngine.EventSystems.UIBehaviour)),
            _GT(typeof(UnityEngine.EventSystems.BaseEventData)),
            _GT(typeof(UnityEngine.EventSystems.PointerEventData)),
            _GT(typeof(UnityEngine.UI.Graphic)),
            _GT(typeof(UnityEngine.UI.MaskableGraphic)),
            _GT(typeof(UnityEngine.UI.Text)),
            _GT(typeof(UnityEngine.UI.Image)),
            _GT(typeof(UnityEngine.UI.RawImage)),

            _GT(typeof(UnityEngine.UI.Selectable)),
            _GT(typeof(UnityEngine.UI.Button)),
            _GT(typeof(UnityEngine.UI.Toggle)),
            _GT(typeof(UnityEngine.UI.InputField)),
            _GT(typeof(UnityEngine.UI.Slider)),
            _GT(typeof(UnityEngine.UI.Dropdown)),
            _GT(typeof(UnityEngine.UI.Dropdown.OptionData)),
            _GT(typeof(UnityEngine.UI.Scrollbar)),
            _GT(typeof(UnityEngine.UI.ScrollRect)),

            // UGUI - Override
            _GT(typeof(ZFrame.UGUI.UILabel)),
            _GT(typeof(ZFrame.UGUI.UISprite)),
            _GT(typeof(ZFrame.UGUI.UITexture)),
            _GT(typeof(ZFrame.UGUI.UIButton)),
            _GT(typeof(ZFrame.UGUI.UIToggle)),
            _GT(typeof(ZFrame.UGUI.UIInput)),
            _GT(typeof(ZFrame.UGUI.UISlider)),
            _GT(typeof(ZFrame.UGUI.UIDropdown)),
            _GT(typeof(ZFrame.UGUI.UIScrollView)),
            _GT(typeof(ZFrame.UGUI.UISelectable)),
            _GT(typeof(ZFrame.UGUI.UIEventTrigger)),
            _GT(typeof(ZFrame.UGUI.FollowUITarget)),
            _GT(typeof(ZFrame.UGUI.UICloseButton)),
            _GT(typeof(ZFrame.UGUI.UIDragged)),
            _GT(typeof(ZFrame.UGUI.UIProgressBar)),
            _GT(typeof(ZFrame.UGUI.UILongpress)),

            _GT(typeof(ZFrame.NetEngine.NetworkMgr)),
            _GT(typeof(ZFrame.NetEngine.TcpClientHandler)),
            _GT(typeof(clientlib.net.NetMsg)),

            _GT(typeof(ZFrame.AudioManager)),

            // Tweener
            _GT(typeof(ZFrame.Tween.ZTweener)),
            _GT(typeof(ZFrame.Tween.BaseTweener)),

            //// Battle
            //_GT(typeof(Battle.BattleObj)),
            //_GT(typeof(Battle.TimerableObj)),
            //_GT(typeof(Battle.FightObj)),
            //_GT(typeof(Battle.RoleObject)),
            ////_GT(typeof(Battle.BatteryObject)),
            //_GT(typeof(Battle.Formation)),
            //_GT(typeof(Battle.InteractiveObj)),            

            //_GT(typeof(Battle.SpawnerBase)),
            //_GT(typeof(Battle.FighterSpawner)),
            //_GT(typeof(Battle.RoundTrigger)),

            //_GT(typeof(Battle.RoleBehavior)),

            //_GT(typeof(Battle.UI.ObjectHUD)),
            ////_GT(typeof(Battle.UI.FighterHUD)),
            //_GT(typeof(Battle.UI.ObjIndicator)),            
            //_GT(typeof(Battle.UI.RoleControl)),

            //_GT(typeof(Battle.BattleMgr)),
            //_GT(typeof(Battle.BattleCameraMgr)),

            // Other
            //_GT(typeof(MonoBehavior)),
            _GT(typeof(CMD5)),
            _GT(typeof(ThreeDButton)),
        };
    }

    public static BindType _GT(System.Type t) {
        return new BindType(t);
    }
}

//
// 该文件自动生成，没事别乱改。
//
using System;
using System.Collections.Generic;

public static class LuaBinder
{
	private static List<string> wrapList = new List<string>();
	
	public static bool IsBinded(string type) { return wrapList.Contains(type); }
	
	public static void Clear() { wrapList.Clear(); }
	
	public static System.Type Bind(IntPtr L, string typeName = null)
	{
		System.Type type = null;
		switch (typeName) {
			case "clientlib.net.NetMsg": type = clientlib_net_NetMsgWrap.Register(L); break;
			case "CMD5": type = CMD5Wrap.Register(L); break;
			case "ThreeDButton": type = ThreeDButtonWrap.Register(L); break;
			case "UnityEngine.Animation": type = UnityEngine_AnimationWrap.Register(L); break;
			case "UnityEngine.Animator": type = UnityEngine_AnimatorWrap.Register(L); break;
			case "UnityEngine.Application": type = UnityEngine_ApplicationWrap.Register(L); break;
			case "UnityEngine.Behaviour": type = UnityEngine_BehaviourWrap.Register(L); break;
			case "UnityEngine.Camera": type = UnityEngine_CameraWrap.Register(L); break;
			case "UnityEngine.CanvasGroup": type = UnityEngine_CanvasGroupWrap.Register(L); break;
			case "UnityEngine.Canvas": type = UnityEngine_CanvasWrap.Register(L); break;
			case "UnityEngine.Collider": type = UnityEngine_ColliderWrap.Register(L); break;
			case "UnityEngine.Component": type = UnityEngine_ComponentWrap.Register(L); break;
			case "UnityEngine.EventSystems.BaseEventData": type = UnityEngine_EventSystems_BaseEventDataWrap.Register(L); break;
			case "UnityEngine.EventSystems.PointerEventData": type = UnityEngine_EventSystems_PointerEventDataWrap.Register(L); break;
			case "UnityEngine.EventSystems.UIBehaviour": type = UnityEngine_EventSystems_UIBehaviourWrap.Register(L); break;
			case "UnityEngine.GameObject": type = UnityEngine_GameObjectWrap.Register(L); break;
			case "UnityEngine.MonoBehaviour": type = UnityEngine_MonoBehaviourWrap.Register(L); break;
			case "UnityEngine.Object": type = UnityEngine_ObjectWrap.Register(L); break;
			case "UnityEngine.PlayerPrefs": type = UnityEngine_PlayerPrefsWrap.Register(L); break;
			case "UnityEngine.RectTransform": type = UnityEngine_RectTransformWrap.Register(L); break;
			case "UnityEngine.Screen": type = UnityEngine_ScreenWrap.Register(L); break;
			case "UnityEngine.SystemInfo": type = UnityEngine_SystemInfoWrap.Register(L); break;
			case "UnityEngine.Time": type = UnityEngine_TimeWrap.Register(L); break;
			case "UnityEngine.Transform": type = UnityEngine_TransformWrap.Register(L); break;
			case "UnityEngine.UI.Button": type = UnityEngine_UI_ButtonWrap.Register(L); break;
			case "UnityEngine.UI.Dropdown": type = UnityEngine_UI_DropdownWrap.Register(L); break;
			case "UnityEngine.UI.Dropdown.OptionData": type = UnityEngine_UI_Dropdown_OptionDataWrap.Register(L); break;
			case "UnityEngine.UI.Graphic": type = UnityEngine_UI_GraphicWrap.Register(L); break;
			case "UnityEngine.UI.Image": type = UnityEngine_UI_ImageWrap.Register(L); break;
			case "UnityEngine.UI.InputField": type = UnityEngine_UI_InputFieldWrap.Register(L); break;
			case "UnityEngine.UI.MaskableGraphic": type = UnityEngine_UI_MaskableGraphicWrap.Register(L); break;
			case "UnityEngine.UI.RawImage": type = UnityEngine_UI_RawImageWrap.Register(L); break;
			case "UnityEngine.UI.Scrollbar": type = UnityEngine_UI_ScrollbarWrap.Register(L); break;
			case "UnityEngine.UI.ScrollRect": type = UnityEngine_UI_ScrollRectWrap.Register(L); break;
			case "UnityEngine.UI.Selectable": type = UnityEngine_UI_SelectableWrap.Register(L); break;
			case "UnityEngine.UI.Slider": type = UnityEngine_UI_SliderWrap.Register(L); break;
			case "UnityEngine.UI.Text": type = UnityEngine_UI_TextWrap.Register(L); break;
			case "UnityEngine.UI.Toggle": type = UnityEngine_UI_ToggleWrap.Register(L); break;
			case "UnityEngine.WaitForSeconds": type = UnityEngine_WaitForSecondsWrap.Register(L); break;
			case "ZFrame.AudioManager": type = ZFrame_AudioManagerWrap.Register(L); break;
			case "ZFrame.NetEngine.NetworkMgr": type = ZFrame_NetEngine_NetworkMgrWrap.Register(L); break;
			case "ZFrame.NetEngine.TcpClientHandler": type = ZFrame_NetEngine_TcpClientHandlerWrap.Register(L); break;
			case "ZFrame.Tween.BaseTweener": type = ZFrame_Tween_BaseTweenerWrap.Register(L); break;
			case "ZFrame.Tween.ZTweener": type = ZFrame_Tween_ZTweenerWrap.Register(L); break;
			case "ZFrame.UGUI.FollowUITarget": type = ZFrame_UGUI_FollowUITargetWrap.Register(L); break;
			case "ZFrame.UGUI.UIButton": type = ZFrame_UGUI_UIButtonWrap.Register(L); break;
			case "ZFrame.UGUI.UICloseButton": type = ZFrame_UGUI_UICloseButtonWrap.Register(L); break;
			case "ZFrame.UGUI.UIDragged": type = ZFrame_UGUI_UIDraggedWrap.Register(L); break;
			case "ZFrame.UGUI.UIDropdown": type = ZFrame_UGUI_UIDropdownWrap.Register(L); break;
			case "ZFrame.UGUI.UIEventTrigger": type = ZFrame_UGUI_UIEventTriggerWrap.Register(L); break;
			case "ZFrame.UGUI.UIInput": type = ZFrame_UGUI_UIInputWrap.Register(L); break;
			case "ZFrame.UGUI.UILabel": type = ZFrame_UGUI_UILabelWrap.Register(L); break;
			case "ZFrame.UGUI.UILongpress": type = ZFrame_UGUI_UILongpressWrap.Register(L); break;
			case "ZFrame.UGUI.UIProgressBar": type = ZFrame_UGUI_UIProgressBarWrap.Register(L); break;
			case "ZFrame.UGUI.UIScrollView": type = ZFrame_UGUI_UIScrollViewWrap.Register(L); break;
			case "ZFrame.UGUI.UISelectable": type = ZFrame_UGUI_UISelectableWrap.Register(L); break;
			case "ZFrame.UGUI.UISlider": type = ZFrame_UGUI_UISliderWrap.Register(L); break;
			case "ZFrame.UGUI.UISprite": type = ZFrame_UGUI_UISpriteWrap.Register(L); break;
			case "ZFrame.UGUI.UITexture": type = ZFrame_UGUI_UITextureWrap.Register(L); break;
			case "ZFrame.UGUI.UIToggle": type = ZFrame_UGUI_UIToggleWrap.Register(L); break;
			default : break;
		}
		wrapList.Add(typeName);
		return type;
	}
}

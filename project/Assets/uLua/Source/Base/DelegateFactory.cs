using UnityEngine;
using System;
using System.Collections.Generic;
using LuaInterface;
using Object = UnityEngine.Object;

public static class DelegateFactory
{
	delegate Delegate DelegateValue(LuaFunction func);
	static Dictionary<Type, DelegateValue> dict = new Dictionary<Type, DelegateValue>();

	[NoToLuaAttribute]
	public static void Register(IntPtr L)
	{
		dict.Add(typeof(Action<GameObject>), new DelegateValue(Action_GameObject));
		dict.Add(typeof(Action), new DelegateValue(Action));
		dict.Add(typeof(UnityEngine.Events.UnityAction), new DelegateValue(UnityEngine_Events_UnityAction));
		dict.Add(typeof(RectTransform.ReapplyDrivenProperties), new DelegateValue(RectTransform_ReapplyDrivenProperties));
		dict.Add(typeof(Camera.CameraCallback), new DelegateValue(Camera_CameraCallback));
		dict.Add(typeof(Application.LogCallback), new DelegateValue(Application_LogCallback));
		dict.Add(typeof(Application.AdvertisingIdentifierCallback), new DelegateValue(Application_AdvertisingIdentifierCallback));
		dict.Add(typeof(Canvas.WillRenderCanvases), new DelegateValue(Canvas_WillRenderCanvases));
		dict.Add(typeof(UnityEngine.UI.InputField.OnValidateInput), new DelegateValue(UnityEngine_UI_InputField_OnValidateInput));
		dict.Add(typeof(ZFrame.Asset.DelegateObjectLoaded), new DelegateValue(ZFrame_Asset_DelegateObjectLoaded));
		dict.Add(typeof(UnityEngine.Events.UnityAction<GameObject>), new DelegateValue(UnityAction_GameObject));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIButton>), new DelegateValue(UnityAction_ZFrame_UGUI_UIButton));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIToggle>), new DelegateValue(UnityAction_ZFrame_UGUI_UIToggle));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIInput>), new DelegateValue(UnityAction_ZFrame_UGUI_UIInput));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UISlider>), new DelegateValue(UnityAction_ZFrame_UGUI_UISlider));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIDropdown>), new DelegateValue(UnityAction_ZFrame_UGUI_UIDropdown));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger>), new DelegateValue(UnityAction_ZFrame_UGUI_UIEventTrigger));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.UGUI.UILongpress>), new DelegateValue(UnityAction_ZFrame_UGUI_UILongpress));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler,clientlib.net.INetMsg>), new DelegateValue(UnityAction_ZFrame_NetEngine_TcpClientHandler_clientlib_net_INetMsg));
		dict.Add(typeof(UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler>), new DelegateValue(UnityAction_ZFrame_NetEngine_TcpClientHandler));
	}

	[NoToLuaAttribute]
	public static Delegate CreateDelegate(Type t, LuaFunction func)
	{
		DelegateValue create = null;

		if (!dict.TryGetValue(t, out create))
		{
			Debugger.LogError("Delegate {0} not register", t.FullName);
			return null;
		}
		return create(func);
	}

	public static Delegate Action_GameObject(LuaFunction func)
	{
		Action<GameObject> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Action(LuaFunction func)
	{
		Action d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UnityEngine_Events_UnityAction(LuaFunction func)
	{
		UnityEngine.Events.UnityAction d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate RectTransform_ReapplyDrivenProperties(LuaFunction func)
	{
		RectTransform.ReapplyDrivenProperties d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Camera_CameraCallback(LuaFunction func)
	{
		Camera.CameraCallback d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Application_LogCallback(LuaFunction func)
	{
		Application.LogCallback d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushString(param0);
			L.PushString(param1);
			L.PushUData(param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Application_AdvertisingIdentifierCallback(LuaFunction func)
	{
		Application.AdvertisingIdentifierCallback d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushString(param0);
			L.PushBoolean(param1);
			L.PushString(param2);
			func.PCall(top, 3);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate Canvas_WillRenderCanvases(LuaFunction func)
	{
		Canvas.WillRenderCanvases d = () =>
		{
			func.Call();
		};
		return d;
	}

	public static Delegate UnityEngine_UI_InputField_OnValidateInput(LuaFunction func)
	{
		UnityEngine.UI.InputField.OnValidateInput d = (param0, param1, param2) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushString(param0);
			L.PushInteger(param1);
			L.PushLightUserData(param2);
			func.PCall(top, 3);
			object[] objs = func.PopValues(top);
			func.EndPCall(top);
			return (char)objs[0];
		};
		return d;
	}

	public static Delegate ZFrame_Asset_DelegateObjectLoaded(LuaFunction func)
	{
		ZFrame.Asset.DelegateObjectLoaded d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			L.PushAnyObject(param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_GameObject(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<GameObject> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_UGUI_UIButton(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.UGUI.UIButton> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_UGUI_UIToggle(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.UGUI.UIToggle> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_UGUI_UIInput(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.UGUI.UIInput> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_UGUI_UISlider(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.UGUI.UISlider> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_UGUI_UIDropdown(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.UGUI.UIDropdown> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_UGUI_UIEventTrigger(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.UGUI.UIEventTrigger> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_UGUI_UILongpress(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.UGUI.UILongpress> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_NetEngine_TcpClientHandler_clientlib_net_INetMsg(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler,clientlib.net.INetMsg> d = (param0, param1) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			L.PushLightUserData(param1);
			func.PCall(top, 2);
			func.EndPCall(top);
		};
		return d;
	}

	public static Delegate UnityAction_ZFrame_NetEngine_TcpClientHandler(LuaFunction func)
	{
		UnityEngine.Events.UnityAction<ZFrame.NetEngine.TcpClientHandler> d = (param0) =>
		{
			int top = func.BeginPCall();
			IntPtr L = func.GetLuaState();
			L.PushLightUserData(param0);
			func.PCall(top, 1);
			func.EndPCall(top);
		};
		return d;
	}

	public static void Clear()
	{
		dict.Clear();
	}

}

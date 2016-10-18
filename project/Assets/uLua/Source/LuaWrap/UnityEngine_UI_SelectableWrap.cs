using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_SelectableWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("IsInteractable", IsInteractable),
			new LuaMethod("FindSelectable", FindSelectable),
			new LuaMethod("FindSelectableOnLeft", FindSelectableOnLeft),
			new LuaMethod("FindSelectableOnRight", FindSelectableOnRight),
			new LuaMethod("FindSelectableOnUp", FindSelectableOnUp),
			new LuaMethod("FindSelectableOnDown", FindSelectableOnDown),
			new LuaMethod("OnMove", OnMove),
			new LuaMethod("OnPointerDown", OnPointerDown),
			new LuaMethod("OnPointerUp", OnPointerUp),
			new LuaMethod("OnPointerEnter", OnPointerEnter),
			new LuaMethod("OnPointerExit", OnPointerExit),
			new LuaMethod("OnSelect", OnSelect),
			new LuaMethod("OnDeselect", OnDeselect),
			new LuaMethod("Select", Select),
			new LuaMethod("new", _CreateUnityEngine_UI_Selectable),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("allSelectables", get_allSelectables, null),
			new LuaField("navigation", get_navigation, set_navigation),
			new LuaField("transition", get_transition, set_transition),
			new LuaField("colors", get_colors, set_colors),
			new LuaField("spriteState", get_spriteState, set_spriteState),
			new LuaField("animationTriggers", get_animationTriggers, set_animationTriggers),
			new LuaField("targetGraphic", get_targetGraphic, set_targetGraphic),
			new LuaField("interactable", get_interactable, set_interactable),
			new LuaField("image", get_image, set_image),
			new LuaField("animator", get_animator, null),
		};

		var type = typeof(UnityEngine.UI.Selectable);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_Selectable(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.Selectable class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.Selectable));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_allSelectables(IntPtr L)
	{
		L.PushUData(UnityEngine.UI.Selectable.allSelectables);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_navigation(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name navigation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index navigation on a nil value");
			}
		}

		L.PushLightUserData(obj.navigation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transition on a nil value");
			}
		}

		L.PushUData(obj.transition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_colors(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colors on a nil value");
			}
		}

		L.PushLightUserData(obj.colors);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_spriteState(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spriteState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spriteState on a nil value");
			}
		}

		L.PushLightUserData(obj.spriteState);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animationTriggers(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animationTriggers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animationTriggers on a nil value");
			}
		}

		L.PushLightUserData(obj.animationTriggers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetGraphic(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetGraphic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetGraphic on a nil value");
			}
		}

		L.PushLightUserData(obj.targetGraphic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_interactable(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interactable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interactable on a nil value");
			}
		}

		L.PushBoolean(obj.interactable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_image(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name image");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index image on a nil value");
			}
		}

		L.PushLightUserData(obj.image);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_animator(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animator");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animator on a nil value");
			}
		}

		L.PushLightUserData(obj.animator);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_navigation(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name navigation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index navigation on a nil value");
			}
		}

		obj.navigation = (UnityEngine.UI.Navigation)L.ChkUserData(3, typeof(UnityEngine.UI.Navigation));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_transition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transition on a nil value");
			}
		}

		obj.transition = (UnityEngine.UI.Selectable.Transition)L.ChkEnumValue(3, typeof(UnityEngine.UI.Selectable.Transition));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_colors(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name colors");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index colors on a nil value");
			}
		}

		obj.colors = (UnityEngine.UI.ColorBlock)L.ChkUserData(3, typeof(UnityEngine.UI.ColorBlock));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_spriteState(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name spriteState");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index spriteState on a nil value");
			}
		}

		obj.spriteState = (UnityEngine.UI.SpriteState)L.ChkUserData(3, typeof(UnityEngine.UI.SpriteState));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_animationTriggers(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name animationTriggers");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index animationTriggers on a nil value");
			}
		}

		obj.animationTriggers = (UnityEngine.UI.AnimationTriggers)L.ChkUserData(3, typeof(UnityEngine.UI.AnimationTriggers));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetGraphic(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetGraphic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetGraphic on a nil value");
			}
		}

		obj.targetGraphic = L.ToComponent(3, typeof(UnityEngine.UI.Graphic)) as UnityEngine.UI.Graphic;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_interactable(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name interactable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index interactable on a nil value");
			}
		}

		obj.interactable = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_image(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name image");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index image on a nil value");
			}
		}

		obj.image = L.ToComponent(3, typeof(UnityEngine.UI.Image)) as UnityEngine.UI.Image;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInteractable(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		bool o = obj.IsInteractable();
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectable(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		var arg0 = L.ToVector3(2);
		UnityEngine.UI.Selectable o = obj.FindSelectable(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnLeft(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnLeft();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnRight(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnRight();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnUp(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnUp();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int FindSelectableOnDown(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.UI.Selectable o = obj.FindSelectableOnDown();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnMove(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.EventSystems.AxisEventData arg0 = (UnityEngine.EventSystems.AxisEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.AxisEventData));
		obj.OnMove(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerDown(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerDown(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerUp(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerUp(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerEnter(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerEnter(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnPointerExit(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnPointerExit(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnSelect(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnSelect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeselect(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		UnityEngine.EventSystems.BaseEventData arg0 = (UnityEngine.EventSystems.BaseEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.BaseEventData));
		obj.OnDeselect(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Select(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Selectable obj = (UnityEngine.UI.Selectable)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Selectable");
		obj.Select();
		return 0;
	}
}


using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;

public class UnityEngine_EventSystems_PointerEventDataWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("IsPointerMoving", IsPointerMoving),
			new LuaMethod("IsScrolling", IsScrolling),
			new LuaMethod("ToString", ToString),
			new LuaMethod("new", _CreateUnityEngine_EventSystems_PointerEventData),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("hovered", get_hovered, set_hovered),
			new LuaField("pointerEnter", get_pointerEnter, set_pointerEnter),
			new LuaField("lastPress", get_lastPress, null),
			new LuaField("rawPointerPress", get_rawPointerPress, set_rawPointerPress),
			new LuaField("pointerDrag", get_pointerDrag, set_pointerDrag),
			new LuaField("pointerCurrentRaycast", get_pointerCurrentRaycast, set_pointerCurrentRaycast),
			new LuaField("pointerPressRaycast", get_pointerPressRaycast, set_pointerPressRaycast),
			new LuaField("eligibleForClick", get_eligibleForClick, set_eligibleForClick),
			new LuaField("pointerId", get_pointerId, set_pointerId),
			new LuaField("position", get_position, set_position),
			new LuaField("delta", get_delta, set_delta),
			new LuaField("pressPosition", get_pressPosition, set_pressPosition),
			new LuaField("clickTime", get_clickTime, set_clickTime),
			new LuaField("clickCount", get_clickCount, set_clickCount),
			new LuaField("scrollDelta", get_scrollDelta, set_scrollDelta),
			new LuaField("useDragThreshold", get_useDragThreshold, set_useDragThreshold),
			new LuaField("dragging", get_dragging, set_dragging),
			new LuaField("button", get_button, set_button),
			new LuaField("enterEventCamera", get_enterEventCamera, null),
			new LuaField("pressEventCamera", get_pressEventCamera, null),
			new LuaField("pointerPress", get_pointerPress, set_pointerPress),
		};

		var type = typeof(UnityEngine.EventSystems.PointerEventData);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_EventSystems_PointerEventData(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 1)
		{
			var arg0 = L.ToComponent(1, typeof(UnityEngine.EventSystems.EventSystem)) as UnityEngine.EventSystems.EventSystem;
			UnityEngine.EventSystems.PointerEventData obj = new UnityEngine.EventSystems.PointerEventData(arg0);
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: UnityEngine.EventSystems.PointerEventData.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.EventSystems.PointerEventData));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hovered(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hovered");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hovered on a nil value");
			}
		}

		L.PushUData(obj.hovered);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pointerEnter(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerEnter on a nil value");
			}
		}

		L.PushLightUserData(obj.pointerEnter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_lastPress(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name lastPress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index lastPress on a nil value");
			}
		}

		L.PushLightUserData(obj.lastPress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rawPointerPress(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rawPointerPress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rawPointerPress on a nil value");
			}
		}

		L.PushLightUserData(obj.rawPointerPress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pointerDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerDrag on a nil value");
			}
		}

		L.PushLightUserData(obj.pointerDrag);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pointerCurrentRaycast(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerCurrentRaycast");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerCurrentRaycast on a nil value");
			}
		}

		L.PushLightUserData(obj.pointerCurrentRaycast);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pointerPressRaycast(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerPressRaycast");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerPressRaycast on a nil value");
			}
		}

		L.PushLightUserData(obj.pointerPressRaycast);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eligibleForClick(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eligibleForClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eligibleForClick on a nil value");
			}
		}

		L.PushBoolean(obj.eligibleForClick);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pointerId(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerId on a nil value");
			}
		}

		L.PushInteger(obj.pointerId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_position(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}

		L.PushUData(obj.position);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_delta(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name delta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index delta on a nil value");
			}
		}

		L.PushUData(obj.delta);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pressPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pressPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pressPosition on a nil value");
			}
		}

		L.PushUData(obj.pressPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_clickTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clickTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clickTime on a nil value");
			}
		}

		L.PushNumber(obj.clickTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_clickCount(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clickCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clickCount on a nil value");
			}
		}

		L.PushInteger(obj.clickCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scrollDelta(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scrollDelta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scrollDelta on a nil value");
			}
		}

		L.PushUData(obj.scrollDelta);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useDragThreshold(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useDragThreshold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useDragThreshold on a nil value");
			}
		}

		L.PushBoolean(obj.useDragThreshold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_dragging(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dragging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dragging on a nil value");
			}
		}

		L.PushBoolean(obj.dragging);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_button(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name button");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index button on a nil value");
			}
		}

		L.PushUData(obj.button);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enterEventCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name enterEventCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index enterEventCamera on a nil value");
			}
		}

		L.PushLightUserData(obj.enterEventCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pressEventCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pressEventCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pressEventCamera on a nil value");
			}
		}

		L.PushLightUserData(obj.pressEventCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pointerPress(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerPress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerPress on a nil value");
			}
		}

		L.PushLightUserData(obj.pointerPress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hovered(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hovered");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hovered on a nil value");
			}
		}

		obj.hovered = (List<GameObject>)L.ChkUserData(3, typeof(List<GameObject>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pointerEnter(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerEnter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerEnter on a nil value");
			}
		}

		obj.pointerEnter = L.ToGameObject(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rawPointerPress(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rawPointerPress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rawPointerPress on a nil value");
			}
		}

		obj.rawPointerPress = L.ToGameObject(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pointerDrag(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerDrag");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerDrag on a nil value");
			}
		}

		obj.pointerDrag = L.ToGameObject(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pointerCurrentRaycast(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerCurrentRaycast");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerCurrentRaycast on a nil value");
			}
		}

		obj.pointerCurrentRaycast = (UnityEngine.EventSystems.RaycastResult)L.ChkUserData(3, typeof(UnityEngine.EventSystems.RaycastResult));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pointerPressRaycast(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerPressRaycast");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerPressRaycast on a nil value");
			}
		}

		obj.pointerPressRaycast = (UnityEngine.EventSystems.RaycastResult)L.ChkUserData(3, typeof(UnityEngine.EventSystems.RaycastResult));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eligibleForClick(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eligibleForClick");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eligibleForClick on a nil value");
			}
		}

		obj.eligibleForClick = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pointerId(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerId");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerId on a nil value");
			}
		}

		obj.pointerId = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_position(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name position");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index position on a nil value");
			}
		}

		obj.position = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_delta(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name delta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index delta on a nil value");
			}
		}

		obj.delta = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pressPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pressPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pressPosition on a nil value");
			}
		}

		obj.pressPosition = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_clickTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clickTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clickTime on a nil value");
			}
		}

		obj.clickTime = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_clickCount(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clickCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clickCount on a nil value");
			}
		}

		obj.clickCount = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scrollDelta(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scrollDelta");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scrollDelta on a nil value");
			}
		}

		obj.scrollDelta = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useDragThreshold(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useDragThreshold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useDragThreshold on a nil value");
			}
		}

		obj.useDragThreshold = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_dragging(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name dragging");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index dragging on a nil value");
			}
		}

		obj.dragging = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_button(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name button");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index button on a nil value");
			}
		}

		obj.button = (UnityEngine.EventSystems.PointerEventData.InputButton)L.ChkEnumValue(3, typeof(UnityEngine.EventSystems.PointerEventData.InputButton));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pointerPress(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pointerPress");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pointerPress on a nil value");
			}
		}

		obj.pointerPress = L.ToGameObject(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsPointerMoving(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)L.ChkUserDataSelf(1, "UnityEngine.EventSystems.PointerEventData");
		bool o = obj.IsPointerMoving();
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsScrolling(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)L.ChkUserDataSelf(1, "UnityEngine.EventSystems.PointerEventData");
		bool o = obj.IsScrolling();
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ToString(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.EventSystems.PointerEventData obj = (UnityEngine.EventSystems.PointerEventData)L.ChkUserDataSelf(1, "UnityEngine.EventSystems.PointerEventData");
		string o = obj.ToString();
		L.PushString(o);
		return 1;
	}
}


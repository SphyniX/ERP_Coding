using System;
using LuaInterface;

public class UnityEngine_UI_ScrollRectWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Rebuild", Rebuild),
			new LuaMethod("LayoutComplete", LayoutComplete),
			new LuaMethod("GraphicUpdateComplete", GraphicUpdateComplete),
			new LuaMethod("IsActive", IsActive),
			new LuaMethod("StopMovement", StopMovement),
			new LuaMethod("OnScroll", OnScroll),
			new LuaMethod("OnInitializePotentialDrag", OnInitializePotentialDrag),
			new LuaMethod("OnBeginDrag", OnBeginDrag),
			new LuaMethod("OnEndDrag", OnEndDrag),
			new LuaMethod("OnDrag", OnDrag),
			new LuaMethod("CalculateLayoutInputHorizontal", CalculateLayoutInputHorizontal),
			new LuaMethod("CalculateLayoutInputVertical", CalculateLayoutInputVertical),
			new LuaMethod("SetLayoutHorizontal", SetLayoutHorizontal),
			new LuaMethod("SetLayoutVertical", SetLayoutVertical),
			new LuaMethod("new", _CreateUnityEngine_UI_ScrollRect),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("content", get_content, set_content),
			new LuaField("horizontal", get_horizontal, set_horizontal),
			new LuaField("vertical", get_vertical, set_vertical),
			new LuaField("movementType", get_movementType, set_movementType),
			new LuaField("elasticity", get_elasticity, set_elasticity),
			new LuaField("inertia", get_inertia, set_inertia),
			new LuaField("decelerationRate", get_decelerationRate, set_decelerationRate),
			new LuaField("scrollSensitivity", get_scrollSensitivity, set_scrollSensitivity),
			new LuaField("viewport", get_viewport, set_viewport),
			new LuaField("horizontalScrollbar", get_horizontalScrollbar, set_horizontalScrollbar),
			new LuaField("verticalScrollbar", get_verticalScrollbar, set_verticalScrollbar),
			new LuaField("horizontalScrollbarVisibility", get_horizontalScrollbarVisibility, set_horizontalScrollbarVisibility),
			new LuaField("verticalScrollbarVisibility", get_verticalScrollbarVisibility, set_verticalScrollbarVisibility),
			new LuaField("horizontalScrollbarSpacing", get_horizontalScrollbarSpacing, set_horizontalScrollbarSpacing),
			new LuaField("verticalScrollbarSpacing", get_verticalScrollbarSpacing, set_verticalScrollbarSpacing),
			new LuaField("onValueChanged", get_onValueChanged, set_onValueChanged),
			new LuaField("velocity", get_velocity, set_velocity),
			new LuaField("normalizedPosition", get_normalizedPosition, set_normalizedPosition),
			new LuaField("horizontalNormalizedPosition", get_horizontalNormalizedPosition, set_horizontalNormalizedPosition),
			new LuaField("verticalNormalizedPosition", get_verticalNormalizedPosition, set_verticalNormalizedPosition),
			new LuaField("minWidth", get_minWidth, null),
			new LuaField("preferredWidth", get_preferredWidth, null),
			new LuaField("flexibleWidth", get_flexibleWidth, null),
			new LuaField("minHeight", get_minHeight, null),
			new LuaField("preferredHeight", get_preferredHeight, null),
			new LuaField("flexibleHeight", get_flexibleHeight, null),
			new LuaField("layoutPriority", get_layoutPriority, null),
		};

		var type = typeof(UnityEngine.UI.ScrollRect);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_ScrollRect(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.ScrollRect class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.ScrollRect));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_content(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name content");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index content on a nil value");
			}
		}

		L.PushLightUserData(obj.content);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontal(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontal on a nil value");
			}
		}

		L.PushBoolean(obj.horizontal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_vertical(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vertical");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vertical on a nil value");
			}
		}

		L.PushBoolean(obj.vertical);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_movementType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name movementType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index movementType on a nil value");
			}
		}

		L.PushUData(obj.movementType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_elasticity(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name elasticity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index elasticity on a nil value");
			}
		}

		L.PushNumber(obj.elasticity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_inertia(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inertia");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inertia on a nil value");
			}
		}

		L.PushBoolean(obj.inertia);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_decelerationRate(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name decelerationRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index decelerationRate on a nil value");
			}
		}

		L.PushNumber(obj.decelerationRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scrollSensitivity(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scrollSensitivity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scrollSensitivity on a nil value");
			}
		}

		L.PushNumber(obj.scrollSensitivity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_viewport(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name viewport");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index viewport on a nil value");
			}
		}

		L.PushLightUserData(obj.viewport);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalScrollbar(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbar on a nil value");
			}
		}

		L.PushLightUserData(obj.horizontalScrollbar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalScrollbar(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbar on a nil value");
			}
		}

		L.PushLightUserData(obj.verticalScrollbar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalScrollbarVisibility(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarVisibility on a nil value");
			}
		}

		L.PushUData(obj.horizontalScrollbarVisibility);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalScrollbarVisibility(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarVisibility on a nil value");
			}
		}

		L.PushUData(obj.verticalScrollbarVisibility);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalScrollbarSpacing(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarSpacing on a nil value");
			}
		}

		L.PushNumber(obj.horizontalScrollbarSpacing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalScrollbarSpacing(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarSpacing on a nil value");
			}
		}

		L.PushNumber(obj.verticalScrollbarSpacing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		L.PushLightUserData(obj.onValueChanged);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_velocity(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocity on a nil value");
			}
		}

		L.PushUData(obj.velocity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_normalizedPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalizedPosition on a nil value");
			}
		}

		L.PushUData(obj.normalizedPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_horizontalNormalizedPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalNormalizedPosition on a nil value");
			}
		}

		L.PushNumber(obj.horizontalNormalizedPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_verticalNormalizedPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalNormalizedPosition on a nil value");
			}
		}

		L.PushNumber(obj.verticalNormalizedPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minWidth(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minWidth on a nil value");
			}
		}

		L.PushNumber(obj.minWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preferredWidth(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preferredWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preferredWidth on a nil value");
			}
		}

		L.PushNumber(obj.preferredWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_flexibleWidth(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flexibleWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flexibleWidth on a nil value");
			}
		}

		L.PushNumber(obj.flexibleWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minHeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name minHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index minHeight on a nil value");
			}
		}

		L.PushNumber(obj.minHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preferredHeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preferredHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preferredHeight on a nil value");
			}
		}

		L.PushNumber(obj.preferredHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_flexibleHeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name flexibleHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index flexibleHeight on a nil value");
			}
		}

		L.PushNumber(obj.flexibleHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layoutPriority(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layoutPriority");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layoutPriority on a nil value");
			}
		}

		L.PushInteger(obj.layoutPriority);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_content(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name content");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index content on a nil value");
			}
		}

		obj.content = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontal(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontal");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontal on a nil value");
			}
		}

		obj.horizontal = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_vertical(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name vertical");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index vertical on a nil value");
			}
		}

		obj.vertical = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_movementType(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name movementType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index movementType on a nil value");
			}
		}

		obj.movementType = (UnityEngine.UI.ScrollRect.MovementType)L.ChkEnumValue(3, typeof(UnityEngine.UI.ScrollRect.MovementType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_elasticity(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name elasticity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index elasticity on a nil value");
			}
		}

		obj.elasticity = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_inertia(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name inertia");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index inertia on a nil value");
			}
		}

		obj.inertia = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_decelerationRate(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name decelerationRate");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index decelerationRate on a nil value");
			}
		}

		obj.decelerationRate = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scrollSensitivity(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scrollSensitivity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scrollSensitivity on a nil value");
			}
		}

		obj.scrollSensitivity = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_viewport(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name viewport");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index viewport on a nil value");
			}
		}

		obj.viewport = L.ToComponent(3, typeof(UnityEngine.RectTransform)) as UnityEngine.RectTransform;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalScrollbar(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbar on a nil value");
			}
		}

		obj.horizontalScrollbar = L.ToComponent(3, typeof(UnityEngine.UI.Scrollbar)) as UnityEngine.UI.Scrollbar;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalScrollbar(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbar on a nil value");
			}
		}

		obj.verticalScrollbar = L.ToComponent(3, typeof(UnityEngine.UI.Scrollbar)) as UnityEngine.UI.Scrollbar;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalScrollbarVisibility(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarVisibility on a nil value");
			}
		}

		obj.horizontalScrollbarVisibility = (UnityEngine.UI.ScrollRect.ScrollbarVisibility)L.ChkEnumValue(3, typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalScrollbarVisibility(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarVisibility");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarVisibility on a nil value");
			}
		}

		obj.verticalScrollbarVisibility = (UnityEngine.UI.ScrollRect.ScrollbarVisibility)L.ChkEnumValue(3, typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalScrollbarSpacing(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalScrollbarSpacing on a nil value");
			}
		}

		obj.horizontalScrollbarSpacing = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalScrollbarSpacing(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalScrollbarSpacing");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalScrollbarSpacing on a nil value");
			}
		}

		obj.verticalScrollbarSpacing = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onValueChanged(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name onValueChanged");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index onValueChanged on a nil value");
			}
		}

		obj.onValueChanged = (UnityEngine.UI.ScrollRect.ScrollRectEvent)L.ChkUserData(3, typeof(UnityEngine.UI.ScrollRect.ScrollRectEvent));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_velocity(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name velocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index velocity on a nil value");
			}
		}

		obj.velocity = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_normalizedPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name normalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index normalizedPosition on a nil value");
			}
		}

		obj.normalizedPosition = L.ToVector2(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_horizontalNormalizedPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name horizontalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index horizontalNormalizedPosition on a nil value");
			}
		}

		obj.horizontalNormalizedPosition = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_verticalNormalizedPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name verticalNormalizedPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index verticalNormalizedPosition on a nil value");
			}
		}

		obj.verticalNormalizedPosition = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rebuild(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		var arg0 = (UnityEngine.UI.CanvasUpdate)L.ChkEnumValue(2, typeof(UnityEngine.UI.CanvasUpdate));
		obj.Rebuild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LayoutComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		obj.LayoutComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GraphicUpdateComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		obj.GraphicUpdateComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsActive(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		bool o = obj.IsActive();
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopMovement(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		obj.StopMovement();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnScroll(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnScroll(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnInitializePotentialDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnInitializePotentialDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeginDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnBeginDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnEndDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnEndDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDrag(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		UnityEngine.EventSystems.PointerEventData arg0 = (UnityEngine.EventSystems.PointerEventData)L.ChkUserData(2, typeof(UnityEngine.EventSystems.PointerEventData));
		obj.OnDrag(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputHorizontal(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		obj.CalculateLayoutInputHorizontal();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputVertical(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		obj.CalculateLayoutInputVertical();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayoutHorizontal(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		obj.SetLayoutHorizontal();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayoutVertical(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.ScrollRect obj = (UnityEngine.UI.ScrollRect)L.ChkUnityObjectSelf(1, "UnityEngine.UI.ScrollRect");
		obj.SetLayoutVertical();
		return 0;
	}
}


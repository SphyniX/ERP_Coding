using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_ImageWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("OnBeforeSerialize", OnBeforeSerialize),
			new LuaMethod("OnAfterDeserialize", OnAfterDeserialize),
			new LuaMethod("SetNativeSize", SetNativeSize),
			new LuaMethod("CalculateLayoutInputHorizontal", CalculateLayoutInputHorizontal),
			new LuaMethod("CalculateLayoutInputVertical", CalculateLayoutInputVertical),
			new LuaMethod("IsRaycastLocationValid", IsRaycastLocationValid),
			new LuaMethod("new", _CreateUnityEngine_UI_Image),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("sprite", get_sprite, set_sprite),
			new LuaField("overrideSprite", get_overrideSprite, set_overrideSprite),
			new LuaField("type", get_type, set_type),
			new LuaField("preserveAspect", get_preserveAspect, set_preserveAspect),
			new LuaField("fillCenter", get_fillCenter, set_fillCenter),
			new LuaField("fillMethod", get_fillMethod, set_fillMethod),
			new LuaField("fillAmount", get_fillAmount, set_fillAmount),
			new LuaField("fillClockwise", get_fillClockwise, set_fillClockwise),
			new LuaField("fillOrigin", get_fillOrigin, set_fillOrigin),
			new LuaField("eventAlphaThreshold", get_eventAlphaThreshold, set_eventAlphaThreshold),
			new LuaField("mainTexture", get_mainTexture, null),
			new LuaField("hasBorder", get_hasBorder, null),
			new LuaField("pixelsPerUnit", get_pixelsPerUnit, null),
			new LuaField("minWidth", get_minWidth, null),
			new LuaField("preferredWidth", get_preferredWidth, null),
			new LuaField("flexibleWidth", get_flexibleWidth, null),
			new LuaField("minHeight", get_minHeight, null),
			new LuaField("preferredHeight", get_preferredHeight, null),
			new LuaField("flexibleHeight", get_flexibleHeight, null),
			new LuaField("layoutPriority", get_layoutPriority, null),
		};

		var type = typeof(UnityEngine.UI.Image);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_Image(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.Image class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.Image));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sprite(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sprite on a nil value");
			}
		}

		L.PushLightUserData(obj.sprite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overrideSprite(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overrideSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overrideSprite on a nil value");
			}
		}

		L.PushLightUserData(obj.overrideSprite);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		L.PushUData(obj.type);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_preserveAspect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preserveAspect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preserveAspect on a nil value");
			}
		}

		L.PushBoolean(obj.preserveAspect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillCenter(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillCenter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillCenter on a nil value");
			}
		}

		L.PushBoolean(obj.fillCenter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillMethod(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillMethod");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillMethod on a nil value");
			}
		}

		L.PushUData(obj.fillMethod);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillAmount(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillAmount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillAmount on a nil value");
			}
		}

		L.PushNumber(obj.fillAmount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillClockwise(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillClockwise");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillClockwise on a nil value");
			}
		}

		L.PushBoolean(obj.fillClockwise);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fillOrigin(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillOrigin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillOrigin on a nil value");
			}
		}

		L.PushInteger(obj.fillOrigin);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eventAlphaThreshold(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eventAlphaThreshold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eventAlphaThreshold on a nil value");
			}
		}

		L.PushNumber(obj.eventAlphaThreshold);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mainTexture(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name mainTexture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index mainTexture on a nil value");
			}
		}

		L.PushLightUserData(obj.mainTexture);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hasBorder(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasBorder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasBorder on a nil value");
			}
		}

		L.PushBoolean(obj.hasBorder);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelsPerUnit(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelsPerUnit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelsPerUnit on a nil value");
			}
		}

		L.PushNumber(obj.pixelsPerUnit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minWidth(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

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
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

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
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

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
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

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
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

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
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

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
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

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
	static int set_sprite(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sprite on a nil value");
			}
		}

		obj.sprite = (Sprite)L.ChkUnityObject(3, typeof(Sprite));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overrideSprite(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overrideSprite");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overrideSprite on a nil value");
			}
		}

		obj.overrideSprite = (Sprite)L.ChkUnityObject(3, typeof(Sprite));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name type");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index type on a nil value");
			}
		}

		obj.type = (UnityEngine.UI.Image.Type)L.ChkEnumValue(3, typeof(UnityEngine.UI.Image.Type));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_preserveAspect(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name preserveAspect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index preserveAspect on a nil value");
			}
		}

		obj.preserveAspect = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fillCenter(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillCenter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillCenter on a nil value");
			}
		}

		obj.fillCenter = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fillMethod(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillMethod");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillMethod on a nil value");
			}
		}

		obj.fillMethod = (UnityEngine.UI.Image.FillMethod)L.ChkEnumValue(3, typeof(UnityEngine.UI.Image.FillMethod));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fillAmount(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillAmount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillAmount on a nil value");
			}
		}

		obj.fillAmount = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fillClockwise(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillClockwise");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillClockwise on a nil value");
			}
		}

		obj.fillClockwise = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fillOrigin(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fillOrigin");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fillOrigin on a nil value");
			}
		}

		obj.fillOrigin = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eventAlphaThreshold(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eventAlphaThreshold");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eventAlphaThreshold on a nil value");
			}
		}

		obj.eventAlphaThreshold = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnBeforeSerialize(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Image");
		obj.OnBeforeSerialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnAfterDeserialize(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Image");
		obj.OnAfterDeserialize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetNativeSize(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Image");
		obj.SetNativeSize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputHorizontal(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Image");
		obj.CalculateLayoutInputHorizontal();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateLayoutInputVertical(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Image");
		obj.CalculateLayoutInputVertical();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsRaycastLocationValid(IntPtr L)
	{
		L.ChkArgsCount(3);
		UnityEngine.UI.Image obj = (UnityEngine.UI.Image)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Image");
		var arg0 = L.ToVector2(2);
		var arg1 = L.ToComponent(3, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		bool o = obj.IsRaycastLocationValid(arg0,arg1);
		L.PushBoolean(o);
		return 1;
	}
}


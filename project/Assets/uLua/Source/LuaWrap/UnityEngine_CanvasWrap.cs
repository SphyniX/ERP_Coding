using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_CanvasWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetDefaultCanvasMaterial", GetDefaultCanvasMaterial),
			new LuaMethod("ForceUpdateCanvases", ForceUpdateCanvases),
			new LuaMethod("new", _CreateCanvas),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("renderMode", get_renderMode, set_renderMode),
			new LuaField("isRootCanvas", get_isRootCanvas, null),
			new LuaField("worldCamera", get_worldCamera, set_worldCamera),
			new LuaField("pixelRect", get_pixelRect, null),
			new LuaField("scaleFactor", get_scaleFactor, set_scaleFactor),
			new LuaField("referencePixelsPerUnit", get_referencePixelsPerUnit, set_referencePixelsPerUnit),
			new LuaField("overridePixelPerfect", get_overridePixelPerfect, set_overridePixelPerfect),
			new LuaField("pixelPerfect", get_pixelPerfect, set_pixelPerfect),
			new LuaField("planeDistance", get_planeDistance, set_planeDistance),
			new LuaField("renderOrder", get_renderOrder, null),
			new LuaField("overrideSorting", get_overrideSorting, set_overrideSorting),
			new LuaField("sortingOrder", get_sortingOrder, set_sortingOrder),
			new LuaField("targetDisplay", get_targetDisplay, set_targetDisplay),
			new LuaField("sortingLayerID", get_sortingLayerID, set_sortingLayerID),
			new LuaField("cachedSortingLayerValue", get_cachedSortingLayerValue, null),
			new LuaField("sortingLayerName", get_sortingLayerName, set_sortingLayerName),
		};

		var type = typeof(Canvas);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCanvas(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Canvas obj = new Canvas();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Canvas.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Canvas));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_renderMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderMode on a nil value");
			}
		}

		L.PushUData(obj.renderMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isRootCanvas(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isRootCanvas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isRootCanvas on a nil value");
			}
		}

		L.PushBoolean(obj.isRootCanvas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldCamera on a nil value");
			}
		}

		L.PushLightUserData(obj.worldCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelRect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelRect on a nil value");
			}
		}

		L.PushLightUserData(obj.pixelRect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_scaleFactor(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleFactor on a nil value");
			}
		}

		L.PushNumber(obj.scaleFactor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_referencePixelsPerUnit(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name referencePixelsPerUnit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index referencePixelsPerUnit on a nil value");
			}
		}

		L.PushNumber(obj.referencePixelsPerUnit);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overridePixelPerfect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overridePixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overridePixelPerfect on a nil value");
			}
		}

		L.PushBoolean(obj.overridePixelPerfect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelPerfect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelPerfect on a nil value");
			}
		}

		L.PushBoolean(obj.pixelPerfect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_planeDistance(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name planeDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index planeDistance on a nil value");
			}
		}

		L.PushNumber(obj.planeDistance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_renderOrder(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderOrder on a nil value");
			}
		}

		L.PushInteger(obj.renderOrder);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_overrideSorting(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overrideSorting");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overrideSorting on a nil value");
			}
		}

		L.PushBoolean(obj.overrideSorting);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sortingOrder(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}

		L.PushInteger(obj.sortingOrder);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetDisplay(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetDisplay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetDisplay on a nil value");
			}
		}

		L.PushInteger(obj.targetDisplay);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sortingLayerID(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}

		L.PushInteger(obj.sortingLayerID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cachedSortingLayerValue(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cachedSortingLayerValue");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cachedSortingLayerValue on a nil value");
			}
		}

		L.PushInteger(obj.cachedSortingLayerValue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sortingLayerName(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}

		L.PushString(obj.sortingLayerName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_renderMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderMode on a nil value");
			}
		}

		obj.renderMode = (RenderMode)L.ChkEnumValue(3, typeof(RenderMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_worldCamera(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldCamera");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldCamera on a nil value");
			}
		}

		obj.worldCamera = L.ToComponent(3, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_scaleFactor(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name scaleFactor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index scaleFactor on a nil value");
			}
		}

		obj.scaleFactor = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_referencePixelsPerUnit(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name referencePixelsPerUnit");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index referencePixelsPerUnit on a nil value");
			}
		}

		obj.referencePixelsPerUnit = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overridePixelPerfect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overridePixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overridePixelPerfect on a nil value");
			}
		}

		obj.overridePixelPerfect = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pixelPerfect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelPerfect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelPerfect on a nil value");
			}
		}

		obj.pixelPerfect = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_planeDistance(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name planeDistance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index planeDistance on a nil value");
			}
		}

		obj.planeDistance = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_overrideSorting(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name overrideSorting");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index overrideSorting on a nil value");
			}
		}

		obj.overrideSorting = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sortingOrder(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingOrder");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingOrder on a nil value");
			}
		}

		obj.sortingOrder = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetDisplay(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetDisplay");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetDisplay on a nil value");
			}
		}

		obj.targetDisplay = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sortingLayerID(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerID");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerID on a nil value");
			}
		}

		obj.sortingLayerID = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sortingLayerName(IntPtr L)
	{
		object o = L.ToUserData(1);
		Canvas obj = (Canvas)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name sortingLayerName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index sortingLayerName on a nil value");
			}
		}

		obj.sortingLayerName = L.ChkLuaString(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDefaultCanvasMaterial(IntPtr L)
	{
		L.ChkArgsCount(0);
		Material o = Canvas.GetDefaultCanvasMaterial();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ForceUpdateCanvases(IntPtr L)
	{
		L.ChkArgsCount(0);
		Canvas.ForceUpdateCanvases();
		return 0;
	}
}


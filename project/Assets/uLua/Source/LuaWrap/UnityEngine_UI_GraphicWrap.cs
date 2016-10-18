using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_UI_GraphicWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetAllDirty", SetAllDirty),
			new LuaMethod("SetLayoutDirty", SetLayoutDirty),
			new LuaMethod("SetVerticesDirty", SetVerticesDirty),
			new LuaMethod("SetMaterialDirty", SetMaterialDirty),
			new LuaMethod("Rebuild", Rebuild),
			new LuaMethod("LayoutComplete", LayoutComplete),
			new LuaMethod("GraphicUpdateComplete", GraphicUpdateComplete),
			new LuaMethod("SetNativeSize", SetNativeSize),
			new LuaMethod("Raycast", Raycast),
			new LuaMethod("PixelAdjustPoint", PixelAdjustPoint),
			new LuaMethod("GetPixelAdjustedRect", GetPixelAdjustedRect),
			new LuaMethod("CrossFadeColor", CrossFadeColor),
			new LuaMethod("CrossFadeAlpha", CrossFadeAlpha),
			new LuaMethod("RegisterDirtyLayoutCallback", RegisterDirtyLayoutCallback),
			new LuaMethod("UnregisterDirtyLayoutCallback", UnregisterDirtyLayoutCallback),
			new LuaMethod("RegisterDirtyVerticesCallback", RegisterDirtyVerticesCallback),
			new LuaMethod("UnregisterDirtyVerticesCallback", UnregisterDirtyVerticesCallback),
			new LuaMethod("RegisterDirtyMaterialCallback", RegisterDirtyMaterialCallback),
			new LuaMethod("UnregisterDirtyMaterialCallback", UnregisterDirtyMaterialCallback),
			new LuaMethod("new", _CreateUnityEngine_UI_Graphic),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("defaultGraphicMaterial", get_defaultGraphicMaterial, null),
			new LuaField("color", get_color, set_color),
			new LuaField("raycastTarget", get_raycastTarget, set_raycastTarget),
			new LuaField("depth", get_depth, null),
			new LuaField("rectTransform", get_rectTransform, null),
			new LuaField("canvas", get_canvas, null),
			new LuaField("canvasRenderer", get_canvasRenderer, null),
			new LuaField("defaultMaterial", get_defaultMaterial, null),
			new LuaField("material", get_material, set_material),
			new LuaField("materialForRendering", get_materialForRendering, null),
			new LuaField("mainTexture", get_mainTexture, null),
		};

		var type = typeof(UnityEngine.UI.Graphic);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUnityEngine_UI_Graphic(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UnityEngine.UI.Graphic class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(UnityEngine.UI.Graphic));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultGraphicMaterial(IntPtr L)
	{
		L.PushLightUserData(UnityEngine.UI.Graphic.defaultGraphicMaterial);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_color(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}

		L.PushUData(obj.color);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_raycastTarget(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name raycastTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index raycastTarget on a nil value");
			}
		}

		L.PushBoolean(obj.raycastTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_depth(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depth on a nil value");
			}
		}

		L.PushInteger(obj.depth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rectTransform(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rectTransform");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rectTransform on a nil value");
			}
		}

		L.PushLightUserData(obj.rectTransform);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_canvas(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canvas");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canvas on a nil value");
			}
		}

		L.PushLightUserData(obj.canvas);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_canvasRenderer(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name canvasRenderer");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index canvasRenderer on a nil value");
			}
		}

		L.PushLightUserData(obj.canvasRenderer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultMaterial(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name defaultMaterial");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index defaultMaterial on a nil value");
			}
		}

		L.PushLightUserData(obj.defaultMaterial);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_material(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name material");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index material on a nil value");
			}
		}

		L.PushLightUserData(obj.material);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_materialForRendering(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name materialForRendering");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index materialForRendering on a nil value");
			}
		}

		L.PushLightUserData(obj.materialForRendering);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_mainTexture(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

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
	static int set_color(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name color");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index color on a nil value");
			}
		}

		obj.color = L.ToColor(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_raycastTarget(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name raycastTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index raycastTarget on a nil value");
			}
		}

		obj.raycastTarget = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_material(IntPtr L)
	{
		object o = L.ToUserData(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name material");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index material on a nil value");
			}
		}

		obj.material = (Material)L.ChkUnityObject(3, typeof(Material));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetAllDirty(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		obj.SetAllDirty();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayoutDirty(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		obj.SetLayoutDirty();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetVerticesDirty(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		obj.SetVerticesDirty();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetMaterialDirty(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		obj.SetMaterialDirty();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rebuild(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		var arg0 = (UnityEngine.UI.CanvasUpdate)L.ChkEnumValue(2, typeof(UnityEngine.UI.CanvasUpdate));
		obj.Rebuild(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LayoutComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		obj.LayoutComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GraphicUpdateComplete(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		obj.GraphicUpdateComplete();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetNativeSize(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		obj.SetNativeSize();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Raycast(IntPtr L)
	{
		L.ChkArgsCount(3);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		var arg0 = L.ToVector2(2);
		var arg1 = L.ToComponent(3, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		bool o = obj.Raycast(arg0,arg1);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PixelAdjustPoint(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		var arg0 = L.ToVector2(2);
		Vector2 o = obj.PixelAdjustPoint(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPixelAdjustedRect(IntPtr L)
	{
		L.ChkArgsCount(1);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		Rect o = obj.GetPixelAdjustedRect();
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CrossFadeColor(IntPtr L)
	{
		L.ChkArgsCount(5);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		var arg0 = L.ToColor(2);
		var arg1 = (float)L.ChkNumber(3);
		var arg2 = L.ChkBoolean(4);
		var arg3 = L.ChkBoolean(5);
		obj.CrossFadeColor(arg0,arg1,arg2,arg3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CrossFadeAlpha(IntPtr L)
	{
		L.ChkArgsCount(4);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		var arg0 = (float)L.ChkNumber(2);
		var arg1 = (float)L.ChkNumber(3);
		var arg2 = L.ChkBoolean(4);
		obj.CrossFadeAlpha(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterDirtyLayoutCallback(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		UnityEngine.Events.UnityAction arg0 = null;
		LuaTypes funcType2 = L.Type(2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (UnityEngine.Events.UnityAction)L.ChkUserData(2, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ChkLuaFunction(2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.RegisterDirtyLayoutCallback(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnregisterDirtyLayoutCallback(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		UnityEngine.Events.UnityAction arg0 = null;
		LuaTypes funcType2 = L.Type(2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (UnityEngine.Events.UnityAction)L.ChkUserData(2, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ChkLuaFunction(2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.UnregisterDirtyLayoutCallback(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterDirtyVerticesCallback(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		UnityEngine.Events.UnityAction arg0 = null;
		LuaTypes funcType2 = L.Type(2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (UnityEngine.Events.UnityAction)L.ChkUserData(2, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ChkLuaFunction(2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.RegisterDirtyVerticesCallback(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnregisterDirtyVerticesCallback(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		UnityEngine.Events.UnityAction arg0 = null;
		LuaTypes funcType2 = L.Type(2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (UnityEngine.Events.UnityAction)L.ChkUserData(2, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ChkLuaFunction(2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.UnregisterDirtyVerticesCallback(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RegisterDirtyMaterialCallback(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		UnityEngine.Events.UnityAction arg0 = null;
		LuaTypes funcType2 = L.Type(2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (UnityEngine.Events.UnityAction)L.ChkUserData(2, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ChkLuaFunction(2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.RegisterDirtyMaterialCallback(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnregisterDirtyMaterialCallback(IntPtr L)
	{
		L.ChkArgsCount(2);
		UnityEngine.UI.Graphic obj = (UnityEngine.UI.Graphic)L.ChkUnityObjectSelf(1, "UnityEngine.UI.Graphic");
		UnityEngine.Events.UnityAction arg0 = null;
		LuaTypes funcType2 = L.Type(2);

		if (funcType2 != LuaTypes.LUA_TFUNCTION)
		{
			 arg0 = (UnityEngine.Events.UnityAction)L.ChkUserData(2, typeof(UnityEngine.Events.UnityAction));
		}
		else
		{
			LuaFunction func = L.ChkLuaFunction(2);
			arg0 = () =>
			{
				func.Call();
			};
		}

		obj.UnregisterDirtyMaterialCallback(arg0);
		return 0;
	}
}


using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_CameraWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SetTargetBuffers", SetTargetBuffers),
			new LuaMethod("ResetWorldToCameraMatrix", ResetWorldToCameraMatrix),
			new LuaMethod("ResetProjectionMatrix", ResetProjectionMatrix),
			new LuaMethod("ResetAspect", ResetAspect),
			new LuaMethod("ResetFieldOfView", ResetFieldOfView),
			new LuaMethod("SetStereoViewMatrices", SetStereoViewMatrices),
			new LuaMethod("ResetStereoViewMatrices", ResetStereoViewMatrices),
			new LuaMethod("SetStereoProjectionMatrices", SetStereoProjectionMatrices),
			new LuaMethod("ResetStereoProjectionMatrices", ResetStereoProjectionMatrices),
			new LuaMethod("WorldToScreenPoint", WorldToScreenPoint),
			new LuaMethod("WorldToViewportPoint", WorldToViewportPoint),
			new LuaMethod("ViewportToWorldPoint", ViewportToWorldPoint),
			new LuaMethod("ScreenToWorldPoint", ScreenToWorldPoint),
			new LuaMethod("ScreenToViewportPoint", ScreenToViewportPoint),
			new LuaMethod("ViewportToScreenPoint", ViewportToScreenPoint),
			new LuaMethod("ViewportPointToRay", ViewportPointToRay),
			new LuaMethod("ScreenPointToRay", ScreenPointToRay),
			new LuaMethod("GetAllCameras", GetAllCameras),
			new LuaMethod("Render", Render),
			new LuaMethod("RenderWithShader", RenderWithShader),
			new LuaMethod("SetReplacementShader", SetReplacementShader),
			new LuaMethod("ResetReplacementShader", ResetReplacementShader),
			new LuaMethod("RenderDontRestore", RenderDontRestore),
			new LuaMethod("SetupCurrent", SetupCurrent),
			new LuaMethod("RenderToCubemap", RenderToCubemap),
			new LuaMethod("CopyFrom", CopyFrom),
			new LuaMethod("AddCommandBuffer", AddCommandBuffer),
			new LuaMethod("RemoveCommandBuffer", RemoveCommandBuffer),
			new LuaMethod("RemoveCommandBuffers", RemoveCommandBuffers),
			new LuaMethod("RemoveAllCommandBuffers", RemoveAllCommandBuffers),
			new LuaMethod("GetCommandBuffers", GetCommandBuffers),
			new LuaMethod("CalculateObliqueMatrix", CalculateObliqueMatrix),
			new LuaMethod("new", _CreateCamera),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("onPreCull", get_onPreCull, set_onPreCull),
			new LuaField("onPreRender", get_onPreRender, set_onPreRender),
			new LuaField("onPostRender", get_onPostRender, set_onPostRender),
			new LuaField("fieldOfView", get_fieldOfView, set_fieldOfView),
			new LuaField("nearClipPlane", get_nearClipPlane, set_nearClipPlane),
			new LuaField("farClipPlane", get_farClipPlane, set_farClipPlane),
			new LuaField("renderingPath", get_renderingPath, set_renderingPath),
			new LuaField("actualRenderingPath", get_actualRenderingPath, null),
			new LuaField("hdr", get_hdr, set_hdr),
			new LuaField("orthographicSize", get_orthographicSize, set_orthographicSize),
			new LuaField("orthographic", get_orthographic, set_orthographic),
			new LuaField("opaqueSortMode", get_opaqueSortMode, set_opaqueSortMode),
			new LuaField("transparencySortMode", get_transparencySortMode, set_transparencySortMode),
			new LuaField("depth", get_depth, set_depth),
			new LuaField("aspect", get_aspect, set_aspect),
			new LuaField("cullingMask", get_cullingMask, set_cullingMask),
			new LuaField("eventMask", get_eventMask, set_eventMask),
			new LuaField("backgroundColor", get_backgroundColor, set_backgroundColor),
			new LuaField("rect", get_rect, set_rect),
			new LuaField("pixelRect", get_pixelRect, set_pixelRect),
			new LuaField("targetTexture", get_targetTexture, set_targetTexture),
			new LuaField("pixelWidth", get_pixelWidth, null),
			new LuaField("pixelHeight", get_pixelHeight, null),
			new LuaField("cameraToWorldMatrix", get_cameraToWorldMatrix, null),
			new LuaField("worldToCameraMatrix", get_worldToCameraMatrix, set_worldToCameraMatrix),
			new LuaField("projectionMatrix", get_projectionMatrix, set_projectionMatrix),
			new LuaField("velocity", get_velocity, null),
			new LuaField("clearFlags", get_clearFlags, set_clearFlags),
			new LuaField("stereoEnabled", get_stereoEnabled, null),
			new LuaField("stereoSeparation", get_stereoSeparation, set_stereoSeparation),
			new LuaField("stereoConvergence", get_stereoConvergence, set_stereoConvergence),
			new LuaField("cameraType", get_cameraType, set_cameraType),
			new LuaField("stereoMirrorMode", get_stereoMirrorMode, set_stereoMirrorMode),
			new LuaField("targetDisplay", get_targetDisplay, set_targetDisplay),
			new LuaField("main", get_main, null),
			new LuaField("current", get_current, null),
			new LuaField("allCameras", get_allCameras, null),
			new LuaField("allCamerasCount", get_allCamerasCount, null),
			new LuaField("useOcclusionCulling", get_useOcclusionCulling, set_useOcclusionCulling),
			new LuaField("layerCullDistances", get_layerCullDistances, set_layerCullDistances),
			new LuaField("layerCullSpherical", get_layerCullSpherical, set_layerCullSpherical),
			new LuaField("depthTextureMode", get_depthTextureMode, set_depthTextureMode),
			new LuaField("clearStencilAfterLightingPass", get_clearStencilAfterLightingPass, set_clearStencilAfterLightingPass),
			new LuaField("commandBufferCount", get_commandBufferCount, null),
		};

		var type = typeof(Camera);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateCamera(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Camera obj = new Camera();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Camera.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Camera));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPreCull(IntPtr L)
	{
		L.PushUData(Camera.onPreCull);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPreRender(IntPtr L)
	{
		L.PushUData(Camera.onPreRender);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onPostRender(IntPtr L)
	{
		L.PushUData(Camera.onPostRender);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fieldOfView(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fieldOfView");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fieldOfView on a nil value");
			}
		}

		L.PushNumber(obj.fieldOfView);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_nearClipPlane(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name nearClipPlane");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index nearClipPlane on a nil value");
			}
		}

		L.PushNumber(obj.nearClipPlane);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_farClipPlane(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name farClipPlane");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index farClipPlane on a nil value");
			}
		}

		L.PushNumber(obj.farClipPlane);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_renderingPath(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderingPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderingPath on a nil value");
			}
		}

		L.PushUData(obj.renderingPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_actualRenderingPath(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name actualRenderingPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index actualRenderingPath on a nil value");
			}
		}

		L.PushUData(obj.actualRenderingPath);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hdr(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hdr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hdr on a nil value");
			}
		}

		L.PushBoolean(obj.hdr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_orthographicSize(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name orthographicSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index orthographicSize on a nil value");
			}
		}

		L.PushNumber(obj.orthographicSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_orthographic(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name orthographic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index orthographic on a nil value");
			}
		}

		L.PushBoolean(obj.orthographic);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_opaqueSortMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name opaqueSortMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index opaqueSortMode on a nil value");
			}
		}

		L.PushUData(obj.opaqueSortMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_transparencySortMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transparencySortMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transparencySortMode on a nil value");
			}
		}

		L.PushUData(obj.transparencySortMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_depth(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

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

		L.PushNumber(obj.depth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_aspect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name aspect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index aspect on a nil value");
			}
		}

		L.PushNumber(obj.aspect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cullingMask(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingMask");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingMask on a nil value");
			}
		}

		L.PushInteger(obj.cullingMask);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_eventMask(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eventMask");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eventMask on a nil value");
			}
		}

		L.PushInteger(obj.eventMask);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_backgroundColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name backgroundColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index backgroundColor on a nil value");
			}
		}

		L.PushUData(obj.backgroundColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rect on a nil value");
			}
		}

		L.PushLightUserData(obj.rect);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

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
	static int get_targetTexture(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetTexture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetTexture on a nil value");
			}
		}

		L.PushLightUserData(obj.targetTexture);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelWidth(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelWidth");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelWidth on a nil value");
			}
		}

		L.PushInteger(obj.pixelWidth);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pixelHeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pixelHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pixelHeight on a nil value");
			}
		}

		L.PushInteger(obj.pixelHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cameraToWorldMatrix(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cameraToWorldMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cameraToWorldMatrix on a nil value");
			}
		}

		L.PushLightUserData(obj.cameraToWorldMatrix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_worldToCameraMatrix(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldToCameraMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldToCameraMatrix on a nil value");
			}
		}

		L.PushLightUserData(obj.worldToCameraMatrix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_projectionMatrix(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name projectionMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index projectionMatrix on a nil value");
			}
		}

		L.PushLightUserData(obj.projectionMatrix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_velocity(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

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
	static int get_clearFlags(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clearFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clearFlags on a nil value");
			}
		}

		L.PushUData(obj.clearFlags);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stereoEnabled(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stereoEnabled");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stereoEnabled on a nil value");
			}
		}

		L.PushBoolean(obj.stereoEnabled);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stereoSeparation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stereoSeparation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stereoSeparation on a nil value");
			}
		}

		L.PushNumber(obj.stereoSeparation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stereoConvergence(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stereoConvergence");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stereoConvergence on a nil value");
			}
		}

		L.PushNumber(obj.stereoConvergence);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cameraType(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cameraType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cameraType on a nil value");
			}
		}

		L.PushUData(obj.cameraType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stereoMirrorMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stereoMirrorMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stereoMirrorMode on a nil value");
			}
		}

		L.PushBoolean(obj.stereoMirrorMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetDisplay(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

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
	static int get_main(IntPtr L)
	{
		L.PushLightUserData(Camera.main);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_current(IntPtr L)
	{
		L.PushLightUserData(Camera.current);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_allCameras(IntPtr L)
	{
		L.PushUData(Camera.allCameras);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_allCamerasCount(IntPtr L)
	{
		L.PushInteger(Camera.allCamerasCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useOcclusionCulling(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useOcclusionCulling");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useOcclusionCulling on a nil value");
			}
		}

		L.PushBoolean(obj.useOcclusionCulling);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layerCullDistances(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layerCullDistances");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layerCullDistances on a nil value");
			}
		}

		L.PushUData(obj.layerCullDistances);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layerCullSpherical(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layerCullSpherical");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layerCullSpherical on a nil value");
			}
		}

		L.PushBoolean(obj.layerCullSpherical);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_depthTextureMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depthTextureMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depthTextureMode on a nil value");
			}
		}

		L.PushUData(obj.depthTextureMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_clearStencilAfterLightingPass(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clearStencilAfterLightingPass");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clearStencilAfterLightingPass on a nil value");
			}
		}

		L.PushBoolean(obj.clearStencilAfterLightingPass);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_commandBufferCount(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name commandBufferCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index commandBufferCount on a nil value");
			}
		}

		L.PushInteger(obj.commandBufferCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPreCull(IntPtr L)
	{
		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			Camera.onPreCull = (Camera.CameraCallback)L.ChkUserData(3, typeof(Camera.CameraCallback));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			Camera.onPreCull = (param0) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPreRender(IntPtr L)
	{
		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			Camera.onPreRender = (Camera.CameraCallback)L.ChkUserData(3, typeof(Camera.CameraCallback));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			Camera.onPreRender = (param0) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onPostRender(IntPtr L)
	{
		LuaTypes funcType = L.Type(3);

		if (funcType != LuaTypes.LUA_TFUNCTION)
		{
			Camera.onPostRender = (Camera.CameraCallback)L.ChkUserData(3, typeof(Camera.CameraCallback));
		}
		else
		{
			LuaFunction func = L.ToLuaFunction(3);
			Camera.onPostRender = (param0) =>
			{
				int top = func.BeginPCall();
				L.PushLightUserData(param0);
				func.PCall(top, 1);
				func.EndPCall(top);
			};
		}
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fieldOfView(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fieldOfView");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fieldOfView on a nil value");
			}
		}

		obj.fieldOfView = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_nearClipPlane(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name nearClipPlane");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index nearClipPlane on a nil value");
			}
		}

		obj.nearClipPlane = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_farClipPlane(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name farClipPlane");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index farClipPlane on a nil value");
			}
		}

		obj.farClipPlane = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_renderingPath(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name renderingPath");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index renderingPath on a nil value");
			}
		}

		obj.renderingPath = (RenderingPath)L.ChkEnumValue(3, typeof(RenderingPath));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_hdr(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hdr");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hdr on a nil value");
			}
		}

		obj.hdr = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_orthographicSize(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name orthographicSize");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index orthographicSize on a nil value");
			}
		}

		obj.orthographicSize = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_orthographic(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name orthographic");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index orthographic on a nil value");
			}
		}

		obj.orthographic = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_opaqueSortMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name opaqueSortMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index opaqueSortMode on a nil value");
			}
		}

		obj.opaqueSortMode = (UnityEngine.Rendering.OpaqueSortMode)L.ChkEnumValue(3, typeof(UnityEngine.Rendering.OpaqueSortMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_transparencySortMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name transparencySortMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index transparencySortMode on a nil value");
			}
		}

		obj.transparencySortMode = (TransparencySortMode)L.ChkEnumValue(3, typeof(TransparencySortMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_depth(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

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

		obj.depth = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_aspect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name aspect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index aspect on a nil value");
			}
		}

		obj.aspect = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cullingMask(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingMask");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingMask on a nil value");
			}
		}

		obj.cullingMask = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_eventMask(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name eventMask");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index eventMask on a nil value");
			}
		}

		obj.eventMask = (int)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_backgroundColor(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name backgroundColor");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index backgroundColor on a nil value");
			}
		}

		obj.backgroundColor = L.ToColor(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rect");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rect on a nil value");
			}
		}

		obj.rect = (Rect)L.ChkUserData(3, typeof(Rect));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pixelRect(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

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

		obj.pixelRect = (Rect)L.ChkUserData(3, typeof(Rect));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetTexture(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetTexture");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetTexture on a nil value");
			}
		}

		obj.targetTexture = (RenderTexture)L.ChkUnityObject(3, typeof(RenderTexture));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_worldToCameraMatrix(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name worldToCameraMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index worldToCameraMatrix on a nil value");
			}
		}

		obj.worldToCameraMatrix = (Matrix4x4)L.ChkUserData(3, typeof(Matrix4x4));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_projectionMatrix(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name projectionMatrix");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index projectionMatrix on a nil value");
			}
		}

		obj.projectionMatrix = (Matrix4x4)L.ChkUserData(3, typeof(Matrix4x4));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_clearFlags(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clearFlags");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clearFlags on a nil value");
			}
		}

		obj.clearFlags = (CameraClearFlags)L.ChkEnumValue(3, typeof(CameraClearFlags));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stereoSeparation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stereoSeparation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stereoSeparation on a nil value");
			}
		}

		obj.stereoSeparation = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stereoConvergence(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stereoConvergence");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stereoConvergence on a nil value");
			}
		}

		obj.stereoConvergence = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cameraType(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cameraType");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cameraType on a nil value");
			}
		}

		obj.cameraType = (CameraType)L.ChkEnumValue(3, typeof(CameraType));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stereoMirrorMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stereoMirrorMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stereoMirrorMode on a nil value");
			}
		}

		obj.stereoMirrorMode = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_targetDisplay(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

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
	static int set_useOcclusionCulling(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name useOcclusionCulling");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index useOcclusionCulling on a nil value");
			}
		}

		obj.useOcclusionCulling = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_layerCullDistances(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layerCullDistances");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layerCullDistances on a nil value");
			}
		}

		obj.layerCullDistances = L.ToArrayNumber<float>(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_layerCullSpherical(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layerCullSpherical");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layerCullSpherical on a nil value");
			}
		}

		obj.layerCullSpherical = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_depthTextureMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name depthTextureMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index depthTextureMode on a nil value");
			}
		}

		obj.depthTextureMode = (DepthTextureMode)L.ChkEnumValue(3, typeof(DepthTextureMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_clearStencilAfterLightingPass(IntPtr L)
	{
		object o = L.ToUserData(1);
		Camera obj = (Camera)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name clearStencilAfterLightingPass");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index clearStencilAfterLightingPass on a nil value");
			}
		}

		obj.clearStencilAfterLightingPass = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTargetBuffers(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && L.CheckTypes(1, typeof(Camera), typeof(RenderBuffer[]), typeof(RenderBuffer)))
		{
			Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
			RenderBuffer[] objs0 = L.ToArrayObject<RenderBuffer>(2);
			RenderBuffer arg1 = (RenderBuffer)L.ToUserData(3);
			obj.SetTargetBuffers(objs0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Camera), typeof(RenderBuffer), typeof(RenderBuffer)))
		{
			Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
			RenderBuffer arg0 = (RenderBuffer)L.ToUserData(2);
			RenderBuffer arg1 = (RenderBuffer)L.ToUserData(3);
			obj.SetTargetBuffers(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Camera.SetTargetBuffers");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetWorldToCameraMatrix(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.ResetWorldToCameraMatrix();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetProjectionMatrix(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.ResetProjectionMatrix();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetAspect(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.ResetAspect();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetFieldOfView(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.ResetFieldOfView();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetStereoViewMatrices(IntPtr L)
	{
		L.ChkArgsCount(3);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		Matrix4x4 arg0 = (Matrix4x4)L.ChkUserData(2, typeof(Matrix4x4));
		Matrix4x4 arg1 = (Matrix4x4)L.ChkUserData(3, typeof(Matrix4x4));
		obj.SetStereoViewMatrices(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetStereoViewMatrices(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.ResetStereoViewMatrices();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetStereoProjectionMatrices(IntPtr L)
	{
		L.ChkArgsCount(3);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		Matrix4x4 arg0 = (Matrix4x4)L.ChkUserData(2, typeof(Matrix4x4));
		Matrix4x4 arg1 = (Matrix4x4)L.ChkUserData(3, typeof(Matrix4x4));
		obj.SetStereoProjectionMatrices(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetStereoProjectionMatrices(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.ResetStereoProjectionMatrices();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WorldToScreenPoint(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Vector3 o = obj.WorldToScreenPoint(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int WorldToViewportPoint(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Vector3 o = obj.WorldToViewportPoint(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewportToWorldPoint(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Vector3 o = obj.ViewportToWorldPoint(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScreenToWorldPoint(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Vector3 o = obj.ScreenToWorldPoint(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScreenToViewportPoint(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Vector3 o = obj.ScreenToViewportPoint(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewportToScreenPoint(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Vector3 o = obj.ViewportToScreenPoint(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ViewportPointToRay(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Ray o = obj.ViewportPointToRay(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ScreenPointToRay(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector3(2);
		Ray o = obj.ScreenPointToRay(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAllCameras(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera[] objs0 = L.ToArrayObject<Camera>(1);
		int o = Camera.GetAllCameras(objs0);
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Render(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.Render();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RenderWithShader(IntPtr L)
	{
		L.ChkArgsCount(3);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		Shader arg0 = (Shader)L.ChkUnityObject(2, typeof(Shader));
		var arg1 = L.ToLuaString(3);
		obj.RenderWithShader(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetReplacementShader(IntPtr L)
	{
		L.ChkArgsCount(3);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		Shader arg0 = (Shader)L.ChkUnityObject(2, typeof(Shader));
		var arg1 = L.ToLuaString(3);
		obj.SetReplacementShader(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetReplacementShader(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.ResetReplacementShader();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RenderDontRestore(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.RenderDontRestore();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetupCurrent(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToComponent(1, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		Camera.SetupCurrent(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RenderToCubemap(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Camera), typeof(RenderTexture)))
		{
			Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
			RenderTexture arg0 = (RenderTexture)L.ToUserData(2);
			bool o = obj.RenderToCubemap(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Camera), typeof(Cubemap)))
		{
			Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
			Cubemap arg0 = (Cubemap)L.ToUserData(2);
			bool o = obj.RenderToCubemap(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Camera), typeof(RenderTexture), typeof(int)))
		{
			Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
			RenderTexture arg0 = (RenderTexture)L.ToUserData(2);
			var arg1 = (int)L.ToNumber(3);
			bool o = obj.RenderToCubemap(arg0,arg1);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Camera), typeof(Cubemap), typeof(int)))
		{
			Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
			Cubemap arg0 = (Cubemap)L.ToUserData(2);
			var arg1 = (int)L.ToNumber(3);
			bool o = obj.RenderToCubemap(arg0,arg1);
			L.PushBoolean(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Camera.RenderToCubemap");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CopyFrom(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToComponent(2, typeof(UnityEngine.Camera)) as UnityEngine.Camera;
		obj.CopyFrom(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddCommandBuffer(IntPtr L)
	{
		L.ChkArgsCount(3);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = (UnityEngine.Rendering.CameraEvent)L.ChkEnumValue(2, typeof(UnityEngine.Rendering.CameraEvent));
		UnityEngine.Rendering.CommandBuffer arg1 = (UnityEngine.Rendering.CommandBuffer)L.ChkUserData(3, typeof(UnityEngine.Rendering.CommandBuffer));
		obj.AddCommandBuffer(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveCommandBuffer(IntPtr L)
	{
		L.ChkArgsCount(3);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = (UnityEngine.Rendering.CameraEvent)L.ChkEnumValue(2, typeof(UnityEngine.Rendering.CameraEvent));
		UnityEngine.Rendering.CommandBuffer arg1 = (UnityEngine.Rendering.CommandBuffer)L.ChkUserData(3, typeof(UnityEngine.Rendering.CommandBuffer));
		obj.RemoveCommandBuffer(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveCommandBuffers(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = (UnityEngine.Rendering.CameraEvent)L.ChkEnumValue(2, typeof(UnityEngine.Rendering.CameraEvent));
		obj.RemoveCommandBuffers(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveAllCommandBuffers(IntPtr L)
	{
		L.ChkArgsCount(1);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		obj.RemoveAllCommandBuffers();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCommandBuffers(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = (UnityEngine.Rendering.CameraEvent)L.ChkEnumValue(2, typeof(UnityEngine.Rendering.CameraEvent));
		UnityEngine.Rendering.CommandBuffer[] o = obj.GetCommandBuffers(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CalculateObliqueMatrix(IntPtr L)
	{
		L.ChkArgsCount(2);
		Camera obj = (Camera)L.ChkUnityObjectSelf(1, "Camera");
		var arg0 = L.ToVector4(2);
		Matrix4x4 o = obj.CalculateObliqueMatrix(arg0);
		L.PushLightUserData(o);
		return 1;
	}
}


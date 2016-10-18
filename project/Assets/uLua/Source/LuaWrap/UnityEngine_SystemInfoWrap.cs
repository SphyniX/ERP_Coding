using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_SystemInfoWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("SupportsRenderTextureFormat", SupportsRenderTextureFormat),
			new LuaMethod("SupportsTextureFormat", SupportsTextureFormat),
			new LuaMethod("new", _CreateSystemInfo),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("operatingSystem", get_operatingSystem, null),
			new LuaField("processorType", get_processorType, null),
			new LuaField("processorFrequency", get_processorFrequency, null),
			new LuaField("processorCount", get_processorCount, null),
			new LuaField("systemMemorySize", get_systemMemorySize, null),
			new LuaField("graphicsMemorySize", get_graphicsMemorySize, null),
			new LuaField("graphicsDeviceName", get_graphicsDeviceName, null),
			new LuaField("graphicsDeviceVendor", get_graphicsDeviceVendor, null),
			new LuaField("graphicsDeviceID", get_graphicsDeviceID, null),
			new LuaField("graphicsDeviceVendorID", get_graphicsDeviceVendorID, null),
			new LuaField("graphicsDeviceType", get_graphicsDeviceType, null),
			new LuaField("graphicsDeviceVersion", get_graphicsDeviceVersion, null),
			new LuaField("graphicsShaderLevel", get_graphicsShaderLevel, null),
			new LuaField("graphicsMultiThreaded", get_graphicsMultiThreaded, null),
			new LuaField("supportsShadows", get_supportsShadows, null),
			new LuaField("supportsRawShadowDepthSampling", get_supportsRawShadowDepthSampling, null),
			new LuaField("supportsRenderTextures", get_supportsRenderTextures, null),
			new LuaField("supportsRenderToCubemap", get_supportsRenderToCubemap, null),
			new LuaField("supportsImageEffects", get_supportsImageEffects, null),
			new LuaField("supports3DTextures", get_supports3DTextures, null),
			new LuaField("supportsComputeShaders", get_supportsComputeShaders, null),
			new LuaField("supportsInstancing", get_supportsInstancing, null),
			new LuaField("supportsSparseTextures", get_supportsSparseTextures, null),
			new LuaField("supportedRenderTargetCount", get_supportedRenderTargetCount, null),
			new LuaField("supportsStencil", get_supportsStencil, null),
			new LuaField("npotSupport", get_npotSupport, null),
			new LuaField("deviceUniqueIdentifier", get_deviceUniqueIdentifier, null),
			new LuaField("deviceName", get_deviceName, null),
			new LuaField("deviceModel", get_deviceModel, null),
			new LuaField("supportsAccelerometer", get_supportsAccelerometer, null),
			new LuaField("supportsGyroscope", get_supportsGyroscope, null),
			new LuaField("supportsLocationService", get_supportsLocationService, null),
			new LuaField("supportsVibration", get_supportsVibration, null),
			new LuaField("deviceType", get_deviceType, null),
			new LuaField("maxTextureSize", get_maxTextureSize, null),
		};

		var type = typeof(SystemInfo);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSystemInfo(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			SystemInfo obj = new SystemInfo();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: SystemInfo.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(SystemInfo));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_operatingSystem(IntPtr L)
	{
		L.PushString(SystemInfo.operatingSystem);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_processorType(IntPtr L)
	{
		L.PushString(SystemInfo.processorType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_processorFrequency(IntPtr L)
	{
		L.PushInteger(SystemInfo.processorFrequency);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_processorCount(IntPtr L)
	{
		L.PushInteger(SystemInfo.processorCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_systemMemorySize(IntPtr L)
	{
		L.PushInteger(SystemInfo.systemMemorySize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsMemorySize(IntPtr L)
	{
		L.PushInteger(SystemInfo.graphicsMemorySize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsDeviceName(IntPtr L)
	{
		L.PushString(SystemInfo.graphicsDeviceName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsDeviceVendor(IntPtr L)
	{
		L.PushString(SystemInfo.graphicsDeviceVendor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsDeviceID(IntPtr L)
	{
		L.PushInteger(SystemInfo.graphicsDeviceID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsDeviceVendorID(IntPtr L)
	{
		L.PushInteger(SystemInfo.graphicsDeviceVendorID);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsDeviceType(IntPtr L)
	{
		L.PushUData(SystemInfo.graphicsDeviceType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsDeviceVersion(IntPtr L)
	{
		L.PushString(SystemInfo.graphicsDeviceVersion);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsShaderLevel(IntPtr L)
	{
		L.PushInteger(SystemInfo.graphicsShaderLevel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_graphicsMultiThreaded(IntPtr L)
	{
		L.PushBoolean(SystemInfo.graphicsMultiThreaded);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsShadows(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsShadows);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsRawShadowDepthSampling(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsRawShadowDepthSampling);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsRenderTextures(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsRenderTextures);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsRenderToCubemap(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsRenderToCubemap);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsImageEffects(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsImageEffects);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supports3DTextures(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supports3DTextures);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsComputeShaders(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsComputeShaders);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsInstancing(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsInstancing);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsSparseTextures(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsSparseTextures);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportedRenderTargetCount(IntPtr L)
	{
		L.PushInteger(SystemInfo.supportedRenderTargetCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsStencil(IntPtr L)
	{
		L.PushInteger(SystemInfo.supportsStencil);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_npotSupport(IntPtr L)
	{
		L.PushUData(SystemInfo.npotSupport);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deviceUniqueIdentifier(IntPtr L)
	{
		L.PushString(SystemInfo.deviceUniqueIdentifier);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deviceName(IntPtr L)
	{
		L.PushString(SystemInfo.deviceName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deviceModel(IntPtr L)
	{
		L.PushString(SystemInfo.deviceModel);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsAccelerometer(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsAccelerometer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsGyroscope(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsGyroscope);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsLocationService(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsLocationService);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_supportsVibration(IntPtr L)
	{
		L.PushBoolean(SystemInfo.supportsVibration);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deviceType(IntPtr L)
	{
		L.PushUData(SystemInfo.deviceType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxTextureSize(IntPtr L)
	{
		L.PushInteger(SystemInfo.maxTextureSize);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SupportsRenderTextureFormat(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = (RenderTextureFormat)L.ChkEnumValue(1, typeof(RenderTextureFormat));
		bool o = SystemInfo.SupportsRenderTextureFormat(arg0);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SupportsTextureFormat(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = (TextureFormat)L.ChkEnumValue(1, typeof(TextureFormat));
		bool o = SystemInfo.SupportsTextureFormat(arg0);
		L.PushBoolean(o);
		return 1;
	}
}


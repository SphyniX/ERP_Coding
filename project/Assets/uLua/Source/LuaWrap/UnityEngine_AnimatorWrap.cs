using System;
using UnityEngine;
using LuaInterface;

public class UnityEngine_AnimatorWrap
{
	public static System.Type Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetFloat", GetFloat),
			new LuaMethod("SetFloat", SetFloat),
			new LuaMethod("GetBool", GetBool),
			new LuaMethod("SetBool", SetBool),
			new LuaMethod("GetInteger", GetInteger),
			new LuaMethod("SetInteger", SetInteger),
			new LuaMethod("SetTrigger", SetTrigger),
			new LuaMethod("ResetTrigger", ResetTrigger),
			new LuaMethod("IsParameterControlledByCurve", IsParameterControlledByCurve),
			new LuaMethod("GetIKPosition", GetIKPosition),
			new LuaMethod("SetIKPosition", SetIKPosition),
			new LuaMethod("GetIKRotation", GetIKRotation),
			new LuaMethod("SetIKRotation", SetIKRotation),
			new LuaMethod("GetIKPositionWeight", GetIKPositionWeight),
			new LuaMethod("SetIKPositionWeight", SetIKPositionWeight),
			new LuaMethod("GetIKRotationWeight", GetIKRotationWeight),
			new LuaMethod("SetIKRotationWeight", SetIKRotationWeight),
			new LuaMethod("GetIKHintPosition", GetIKHintPosition),
			new LuaMethod("SetIKHintPosition", SetIKHintPosition),
			new LuaMethod("GetIKHintPositionWeight", GetIKHintPositionWeight),
			new LuaMethod("SetIKHintPositionWeight", SetIKHintPositionWeight),
			new LuaMethod("SetLookAtPosition", SetLookAtPosition),
			new LuaMethod("SetLookAtWeight", SetLookAtWeight),
			new LuaMethod("SetBoneLocalRotation", SetBoneLocalRotation),
			new LuaMethod("GetLayerName", GetLayerName),
			new LuaMethod("GetLayerIndex", GetLayerIndex),
			new LuaMethod("GetLayerWeight", GetLayerWeight),
			new LuaMethod("SetLayerWeight", SetLayerWeight),
			new LuaMethod("GetCurrentAnimatorStateInfo", GetCurrentAnimatorStateInfo),
			new LuaMethod("GetNextAnimatorStateInfo", GetNextAnimatorStateInfo),
			new LuaMethod("GetAnimatorTransitionInfo", GetAnimatorTransitionInfo),
			new LuaMethod("GetCurrentAnimatorClipInfo", GetCurrentAnimatorClipInfo),
			new LuaMethod("GetNextAnimatorClipInfo", GetNextAnimatorClipInfo),
			new LuaMethod("IsInTransition", IsInTransition),
			new LuaMethod("GetParameter", GetParameter),
			new LuaMethod("MatchTarget", MatchTarget),
			new LuaMethod("InterruptMatchTarget", InterruptMatchTarget),
			new LuaMethod("CrossFadeInFixedTime", CrossFadeInFixedTime),
			new LuaMethod("CrossFade", CrossFade),
			new LuaMethod("PlayInFixedTime", PlayInFixedTime),
			new LuaMethod("Play", Play),
			new LuaMethod("SetTarget", SetTarget),
			new LuaMethod("GetBoneTransform", GetBoneTransform),
			new LuaMethod("StartPlayback", StartPlayback),
			new LuaMethod("StopPlayback", StopPlayback),
			new LuaMethod("StartRecording", StartRecording),
			new LuaMethod("StopRecording", StopRecording),
			new LuaMethod("HasState", HasState),
			new LuaMethod("StringToHash", StringToHash),
			new LuaMethod("Update", Update),
			new LuaMethod("Rebind", Rebind),
			new LuaMethod("ApplyBuiltinRootMotion", ApplyBuiltinRootMotion),
			new LuaMethod("new", _CreateAnimator),
			new LuaMethod("GetType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("isOptimizable", get_isOptimizable, null),
			new LuaField("isHuman", get_isHuman, null),
			new LuaField("hasRootMotion", get_hasRootMotion, null),
			new LuaField("humanScale", get_humanScale, null),
			new LuaField("isInitialized", get_isInitialized, null),
			new LuaField("deltaPosition", get_deltaPosition, null),
			new LuaField("deltaRotation", get_deltaRotation, null),
			new LuaField("velocity", get_velocity, null),
			new LuaField("angularVelocity", get_angularVelocity, null),
			new LuaField("rootPosition", get_rootPosition, set_rootPosition),
			new LuaField("rootRotation", get_rootRotation, set_rootRotation),
			new LuaField("applyRootMotion", get_applyRootMotion, set_applyRootMotion),
			new LuaField("linearVelocityBlending", get_linearVelocityBlending, set_linearVelocityBlending),
			new LuaField("updateMode", get_updateMode, set_updateMode),
			new LuaField("hasTransformHierarchy", get_hasTransformHierarchy, null),
			new LuaField("gravityWeight", get_gravityWeight, null),
			new LuaField("bodyPosition", get_bodyPosition, set_bodyPosition),
			new LuaField("bodyRotation", get_bodyRotation, set_bodyRotation),
			new LuaField("stabilizeFeet", get_stabilizeFeet, set_stabilizeFeet),
			new LuaField("layerCount", get_layerCount, null),
			new LuaField("parameters", get_parameters, null),
			new LuaField("parameterCount", get_parameterCount, null),
			new LuaField("feetPivotActive", get_feetPivotActive, set_feetPivotActive),
			new LuaField("pivotWeight", get_pivotWeight, null),
			new LuaField("pivotPosition", get_pivotPosition, null),
			new LuaField("isMatchingTarget", get_isMatchingTarget, null),
			new LuaField("speed", get_speed, set_speed),
			new LuaField("targetPosition", get_targetPosition, null),
			new LuaField("targetRotation", get_targetRotation, null),
			new LuaField("cullingMode", get_cullingMode, set_cullingMode),
			new LuaField("playbackTime", get_playbackTime, set_playbackTime),
			new LuaField("recorderStartTime", get_recorderStartTime, set_recorderStartTime),
			new LuaField("recorderStopTime", get_recorderStopTime, set_recorderStopTime),
			new LuaField("recorderMode", get_recorderMode, null),
			new LuaField("runtimeAnimatorController", get_runtimeAnimatorController, set_runtimeAnimatorController),
			new LuaField("avatar", get_avatar, set_avatar),
			new LuaField("layersAffectMassCenter", get_layersAffectMassCenter, set_layersAffectMassCenter),
			new LuaField("leftFeetBottomHeight", get_leftFeetBottomHeight, null),
			new LuaField("rightFeetBottomHeight", get_rightFeetBottomHeight, null),
			new LuaField("logWarnings", get_logWarnings, set_logWarnings),
			new LuaField("fireEvents", get_fireEvents, set_fireEvents),
		};

		var type = typeof(Animator);
		L.WrapCSharpObject(type, regs, fields);
		return type;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAnimator(IntPtr L)
	{
		int count = L.GetTop();

		if (count == 0)
		{
			Animator obj = new Animator();
			L.PushLightUserData(obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		int count = L.GetTop();
		if (count == 0) {
			L.PushLightUserData(typeof(Animator));
			return 1;
		} else {
			return MetaMethods.GetType(L);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isOptimizable(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isOptimizable");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isOptimizable on a nil value");
			}
		}

		L.PushBoolean(obj.isOptimizable);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isHuman(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isHuman");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isHuman on a nil value");
			}
		}

		L.PushBoolean(obj.isHuman);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hasRootMotion(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasRootMotion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasRootMotion on a nil value");
			}
		}

		L.PushBoolean(obj.hasRootMotion);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_humanScale(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name humanScale");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index humanScale on a nil value");
			}
		}

		L.PushNumber(obj.humanScale);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isInitialized(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isInitialized");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isInitialized on a nil value");
			}
		}

		L.PushBoolean(obj.isInitialized);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deltaPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name deltaPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index deltaPosition on a nil value");
			}
		}

		L.PushUData(obj.deltaPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_deltaRotation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name deltaRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index deltaRotation on a nil value");
			}
		}

		L.PushUData(obj.deltaRotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_velocity(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

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
	static int get_angularVelocity(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name angularVelocity");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index angularVelocity on a nil value");
			}
		}

		L.PushUData(obj.angularVelocity);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rootPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootPosition on a nil value");
			}
		}

		L.PushUData(obj.rootPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rootRotation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootRotation on a nil value");
			}
		}

		L.PushUData(obj.rootRotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_applyRootMotion(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name applyRootMotion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index applyRootMotion on a nil value");
			}
		}

		L.PushBoolean(obj.applyRootMotion);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_linearVelocityBlending(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name linearVelocityBlending");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index linearVelocityBlending on a nil value");
			}
		}

		L.PushBoolean(obj.linearVelocityBlending);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_updateMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name updateMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index updateMode on a nil value");
			}
		}

		L.PushUData(obj.updateMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_hasTransformHierarchy(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name hasTransformHierarchy");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index hasTransformHierarchy on a nil value");
			}
		}

		L.PushBoolean(obj.hasTransformHierarchy);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gravityWeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name gravityWeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index gravityWeight on a nil value");
			}
		}

		L.PushNumber(obj.gravityWeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bodyPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyPosition on a nil value");
			}
		}

		L.PushUData(obj.bodyPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bodyRotation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyRotation on a nil value");
			}
		}

		L.PushUData(obj.bodyRotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_stabilizeFeet(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stabilizeFeet");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stabilizeFeet on a nil value");
			}
		}

		L.PushBoolean(obj.stabilizeFeet);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layerCount(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layerCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layerCount on a nil value");
			}
		}

		L.PushInteger(obj.layerCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parameters(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parameters");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parameters on a nil value");
			}
		}

		L.PushUData(obj.parameters);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_parameterCount(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name parameterCount");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index parameterCount on a nil value");
			}
		}

		L.PushInteger(obj.parameterCount);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_feetPivotActive(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name feetPivotActive");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index feetPivotActive on a nil value");
			}
		}

		L.PushNumber(obj.feetPivotActive);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pivotWeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pivotWeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pivotWeight on a nil value");
			}
		}

		L.PushNumber(obj.pivotWeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pivotPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pivotPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pivotPosition on a nil value");
			}
		}

		L.PushUData(obj.pivotPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isMatchingTarget(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name isMatchingTarget");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index isMatchingTarget on a nil value");
			}
		}

		L.PushBoolean(obj.isMatchingTarget);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_speed(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name speed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index speed on a nil value");
			}
		}

		L.PushNumber(obj.speed);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetPosition on a nil value");
			}
		}

		L.PushUData(obj.targetPosition);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_targetRotation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name targetRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index targetRotation on a nil value");
			}
		}

		L.PushUData(obj.targetRotation);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cullingMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingMode on a nil value");
			}
		}

		L.PushUData(obj.cullingMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_playbackTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playbackTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playbackTime on a nil value");
			}
		}

		L.PushNumber(obj.playbackTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recorderStartTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recorderStartTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recorderStartTime on a nil value");
			}
		}

		L.PushNumber(obj.recorderStartTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recorderStopTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recorderStopTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recorderStopTime on a nil value");
			}
		}

		L.PushNumber(obj.recorderStopTime);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_recorderMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recorderMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recorderMode on a nil value");
			}
		}

		L.PushUData(obj.recorderMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_runtimeAnimatorController(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name runtimeAnimatorController");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index runtimeAnimatorController on a nil value");
			}
		}

		L.PushLightUserData(obj.runtimeAnimatorController);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_avatar(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name avatar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index avatar on a nil value");
			}
		}

		L.PushLightUserData(obj.avatar);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_layersAffectMassCenter(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layersAffectMassCenter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layersAffectMassCenter on a nil value");
			}
		}

		L.PushBoolean(obj.layersAffectMassCenter);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_leftFeetBottomHeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name leftFeetBottomHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index leftFeetBottomHeight on a nil value");
			}
		}

		L.PushNumber(obj.leftFeetBottomHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_rightFeetBottomHeight(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rightFeetBottomHeight");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rightFeetBottomHeight on a nil value");
			}
		}

		L.PushNumber(obj.rightFeetBottomHeight);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_logWarnings(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name logWarnings");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index logWarnings on a nil value");
			}
		}

		L.PushBoolean(obj.logWarnings);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_fireEvents(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fireEvents");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fireEvents on a nil value");
			}
		}

		L.PushBoolean(obj.fireEvents);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rootPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootPosition on a nil value");
			}
		}

		obj.rootPosition = L.ToVector3(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_rootRotation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name rootRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index rootRotation on a nil value");
			}
		}

		obj.rootRotation = L.ToQuaternion(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_applyRootMotion(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name applyRootMotion");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index applyRootMotion on a nil value");
			}
		}

		obj.applyRootMotion = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_linearVelocityBlending(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name linearVelocityBlending");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index linearVelocityBlending on a nil value");
			}
		}

		obj.linearVelocityBlending = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_updateMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name updateMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index updateMode on a nil value");
			}
		}

		obj.updateMode = (AnimatorUpdateMode)L.ChkEnumValue(3, typeof(AnimatorUpdateMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bodyPosition(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyPosition");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyPosition on a nil value");
			}
		}

		obj.bodyPosition = L.ToVector3(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bodyRotation(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name bodyRotation");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index bodyRotation on a nil value");
			}
		}

		obj.bodyRotation = L.ToQuaternion(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_stabilizeFeet(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name stabilizeFeet");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index stabilizeFeet on a nil value");
			}
		}

		obj.stabilizeFeet = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_feetPivotActive(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name feetPivotActive");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index feetPivotActive on a nil value");
			}
		}

		obj.feetPivotActive = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_speed(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name speed");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index speed on a nil value");
			}
		}

		obj.speed = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cullingMode(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name cullingMode");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index cullingMode on a nil value");
			}
		}

		obj.cullingMode = (AnimatorCullingMode)L.ChkEnumValue(3, typeof(AnimatorCullingMode));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_playbackTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name playbackTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index playbackTime on a nil value");
			}
		}

		obj.playbackTime = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recorderStartTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recorderStartTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recorderStartTime on a nil value");
			}
		}

		obj.recorderStartTime = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_recorderStopTime(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name recorderStopTime");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index recorderStopTime on a nil value");
			}
		}

		obj.recorderStopTime = (float)L.ChkNumber(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_runtimeAnimatorController(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name runtimeAnimatorController");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index runtimeAnimatorController on a nil value");
			}
		}

		obj.runtimeAnimatorController = (RuntimeAnimatorController)L.ChkUnityObject(3, typeof(RuntimeAnimatorController));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_avatar(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name avatar");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index avatar on a nil value");
			}
		}

		obj.avatar = (Avatar)L.ChkUnityObject(3, typeof(Avatar));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_layersAffectMassCenter(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name layersAffectMassCenter");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index layersAffectMassCenter on a nil value");
			}
		}

		obj.layersAffectMassCenter = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_logWarnings(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name logWarnings");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index logWarnings on a nil value");
			}
		}

		obj.logWarnings = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_fireEvents(IntPtr L)
	{
		object o = L.ToUserData(1);
		Animator obj = (Animator)o;

		if (obj == null)
		{
			LuaTypes types = L.Type(1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name fireEvents");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index fireEvents on a nil value");
			}
		}

		obj.fireEvents = L.ChkBoolean(3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFloat(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			float o = obj.GetFloat(arg0);
			L.PushNumber(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			float o = obj.GetFloat(arg0);
			L.PushNumber(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.GetFloat");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetFloat(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			obj.SetFloat(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			obj.SetFloat(arg0,arg1);
			return 0;
		}
		else if (count == 5 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float), typeof(float), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (float)L.ToNumber(4);
			var arg3 = (float)L.ToNumber(5);
			obj.SetFloat(arg0,arg1,arg2,arg3);
			return 0;
		}
		else if (count == 5 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float), typeof(float), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (float)L.ToNumber(4);
			var arg3 = (float)L.ToNumber(5);
			obj.SetFloat(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.SetFloat");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBool(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			bool o = obj.GetBool(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			bool o = obj.GetBool(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.GetBool");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetBool(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(bool)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = L.ToBoolean(3);
			obj.SetBool(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(bool)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = L.ToBoolean(3);
			obj.SetBool(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.SetBool");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInteger(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			int o = obj.GetInteger(arg0);
			L.PushInteger(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			int o = obj.GetInteger(arg0);
			L.PushInteger(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.GetInteger");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetInteger(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (int)L.ToNumber(3);
			obj.SetInteger(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (int)L.ToNumber(3);
			obj.SetInteger(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.SetInteger");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTrigger(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			obj.SetTrigger(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			obj.SetTrigger(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.SetTrigger");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetTrigger(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			obj.ResetTrigger(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			obj.ResetTrigger(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.ResetTrigger");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsParameterControlledByCurve(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			bool o = obj.IsParameterControlledByCurve(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			bool o = obj.IsParameterControlledByCurve(arg0);
			L.PushBoolean(o);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.IsParameterControlledByCurve");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIKPosition(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		Vector3 o = obj.GetIKPosition(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetIKPosition(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		var arg1 = L.ToVector3(3);
		obj.SetIKPosition(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIKRotation(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		Quaternion o = obj.GetIKRotation(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetIKRotation(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		var arg1 = L.ToQuaternion(3);
		obj.SetIKRotation(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIKPositionWeight(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		float o = obj.GetIKPositionWeight(arg0);
		L.PushNumber(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetIKPositionWeight(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		var arg1 = (float)L.ChkNumber(3);
		obj.SetIKPositionWeight(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIKRotationWeight(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		float o = obj.GetIKRotationWeight(arg0);
		L.PushNumber(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetIKRotationWeight(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKGoal)L.ChkEnumValue(2, typeof(AvatarIKGoal));
		var arg1 = (float)L.ChkNumber(3);
		obj.SetIKRotationWeight(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIKHintPosition(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKHint)L.ChkEnumValue(2, typeof(AvatarIKHint));
		Vector3 o = obj.GetIKHintPosition(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetIKHintPosition(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKHint)L.ChkEnumValue(2, typeof(AvatarIKHint));
		var arg1 = L.ToVector3(3);
		obj.SetIKHintPosition(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIKHintPositionWeight(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKHint)L.ChkEnumValue(2, typeof(AvatarIKHint));
		float o = obj.GetIKHintPositionWeight(arg0);
		L.PushNumber(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetIKHintPositionWeight(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarIKHint)L.ChkEnumValue(2, typeof(AvatarIKHint));
		var arg1 = (float)L.ChkNumber(3);
		obj.SetIKHintPositionWeight(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLookAtPosition(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = L.ToVector3(2);
		obj.SetLookAtPosition(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLookAtWeight(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (float)L.ChkNumber(2);
			obj.SetLookAtWeight(arg0);
			return 0;
		}
		else if (count == 3)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (float)L.ChkNumber(2);
			var arg1 = (float)L.ChkNumber(3);
			obj.SetLookAtWeight(arg0,arg1);
			return 0;
		}
		else if (count == 4)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (float)L.ChkNumber(2);
			var arg1 = (float)L.ChkNumber(3);
			var arg2 = (float)L.ChkNumber(4);
			obj.SetLookAtWeight(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 5)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (float)L.ChkNumber(2);
			var arg1 = (float)L.ChkNumber(3);
			var arg2 = (float)L.ChkNumber(4);
			var arg3 = (float)L.ChkNumber(5);
			obj.SetLookAtWeight(arg0,arg1,arg2,arg3);
			return 0;
		}
		else if (count == 6)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (float)L.ChkNumber(2);
			var arg1 = (float)L.ChkNumber(3);
			var arg2 = (float)L.ChkNumber(4);
			var arg3 = (float)L.ChkNumber(5);
			var arg4 = (float)L.ChkNumber(6);
			obj.SetLookAtWeight(arg0,arg1,arg2,arg3,arg4);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.SetLookAtWeight");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetBoneLocalRotation(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (HumanBodyBones)L.ChkEnumValue(2, typeof(HumanBodyBones));
		var arg1 = L.ToQuaternion(3);
		obj.SetBoneLocalRotation(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLayerName(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		string o = obj.GetLayerName(arg0);
		L.PushString(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLayerIndex(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = L.ToLuaString(2);
		int o = obj.GetLayerIndex(arg0);
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLayerWeight(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		float o = obj.GetLayerWeight(arg0);
		L.PushNumber(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLayerWeight(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		var arg1 = (float)L.ChkNumber(3);
		obj.SetLayerWeight(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCurrentAnimatorStateInfo(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		AnimatorStateInfo o = obj.GetCurrentAnimatorStateInfo(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNextAnimatorStateInfo(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		AnimatorStateInfo o = obj.GetNextAnimatorStateInfo(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAnimatorTransitionInfo(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		AnimatorTransitionInfo o = obj.GetAnimatorTransitionInfo(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCurrentAnimatorClipInfo(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		AnimatorClipInfo[] o = obj.GetCurrentAnimatorClipInfo(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNextAnimatorClipInfo(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		AnimatorClipInfo[] o = obj.GetNextAnimatorClipInfo(arg0);
		L.PushUData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsInTransition(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		bool o = obj.IsInTransition(arg0);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetParameter(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		AnimatorControllerParameter o = obj.GetParameter(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MatchTarget(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 6)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ToVector3(2);
			var arg1 = L.ToQuaternion(3);
			var arg2 = (AvatarTarget)L.ChkEnumValue(4, typeof(AvatarTarget));
			MatchTargetWeightMask arg3 = (MatchTargetWeightMask)L.ChkUserData(5, typeof(MatchTargetWeightMask));
			var arg4 = (float)L.ChkNumber(6);
			obj.MatchTarget(arg0,arg1,arg2,arg3,arg4);
			return 0;
		}
		else if (count == 7)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ToVector3(2);
			var arg1 = L.ToQuaternion(3);
			var arg2 = (AvatarTarget)L.ChkEnumValue(4, typeof(AvatarTarget));
			MatchTargetWeightMask arg3 = (MatchTargetWeightMask)L.ChkUserData(5, typeof(MatchTargetWeightMask));
			var arg4 = (float)L.ChkNumber(6);
			var arg5 = (float)L.ChkNumber(7);
			obj.MatchTarget(arg0,arg1,arg2,arg3,arg4,arg5);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.MatchTarget");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InterruptMatchTarget(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			obj.InterruptMatchTarget();
			return 0;
		}
		else if (count == 2)
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkBoolean(2);
			obj.InterruptMatchTarget(arg0);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.InterruptMatchTarget");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CrossFadeInFixedTime(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			obj.CrossFadeInFixedTime(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			obj.CrossFadeInFixedTime(arg0,arg1);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			obj.CrossFadeInFixedTime(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			obj.CrossFadeInFixedTime(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 5 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			var arg3 = (float)L.ToNumber(5);
			obj.CrossFadeInFixedTime(arg0,arg1,arg2,arg3);
			return 0;
		}
		else if (count == 5 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			var arg3 = (float)L.ToNumber(5);
			obj.CrossFadeInFixedTime(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.CrossFadeInFixedTime");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CrossFade(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			obj.CrossFade(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			obj.CrossFade(arg0,arg1);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			obj.CrossFade(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			obj.CrossFade(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 5 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(float), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			var arg3 = (float)L.ToNumber(5);
			obj.CrossFade(arg0,arg1,arg2,arg3);
			return 0;
		}
		else if (count == 5 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(float), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (float)L.ToNumber(3);
			var arg2 = (int)L.ToNumber(4);
			var arg3 = (float)L.ToNumber(5);
			obj.CrossFade(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.CrossFade");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PlayInFixedTime(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			obj.PlayInFixedTime(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			obj.PlayInFixedTime(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (int)L.ToNumber(3);
			obj.PlayInFixedTime(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (int)L.ToNumber(3);
			obj.PlayInFixedTime(arg0,arg1);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (int)L.ToNumber(3);
			var arg2 = (float)L.ToNumber(4);
			obj.PlayInFixedTime(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (int)L.ToNumber(3);
			var arg2 = (float)L.ToNumber(4);
			obj.PlayInFixedTime(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.PlayInFixedTime");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Play(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			obj.Play(arg0);
			return 0;
		}
		else if (count == 2 && L.CheckTypes(1, typeof(Animator), typeof(string)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			obj.Play(arg0);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (int)L.ToNumber(3);
			obj.Play(arg0,arg1);
			return 0;
		}
		else if (count == 3 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(int)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (int)L.ToNumber(3);
			obj.Play(arg0,arg1);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(string), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = L.ChkLuaString(2);
			var arg1 = (int)L.ToNumber(3);
			var arg2 = (float)L.ToNumber(4);
			obj.Play(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4 && L.CheckTypes(1, typeof(Animator), typeof(int), typeof(int), typeof(float)))
		{
			Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
			var arg0 = (int)L.ToNumber(2);
			var arg1 = (int)L.ToNumber(3);
			var arg2 = (float)L.ToNumber(4);
			obj.Play(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Animator.Play");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTarget(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (AvatarTarget)L.ChkEnumValue(2, typeof(AvatarTarget));
		var arg1 = (float)L.ChkNumber(3);
		obj.SetTarget(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBoneTransform(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (HumanBodyBones)L.ChkEnumValue(2, typeof(HumanBodyBones));
		Transform o = obj.GetBoneTransform(arg0);
		L.PushLightUserData(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartPlayback(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		obj.StartPlayback();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopPlayback(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		obj.StopPlayback();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartRecording(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		obj.StartRecording(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopRecording(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		obj.StopRecording();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasState(IntPtr L)
	{
		L.ChkArgsCount(3);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (int)L.ChkNumber(2);
		var arg1 = (int)L.ChkNumber(3);
		bool o = obj.HasState(arg0,arg1);
		L.PushBoolean(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StringToHash(IntPtr L)
	{
		L.ChkArgsCount(1);
		var arg0 = L.ToLuaString(1);
		int o = Animator.StringToHash(arg0);
		L.PushInteger(o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		L.ChkArgsCount(2);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		var arg0 = (float)L.ChkNumber(2);
		obj.Update(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Rebind(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		obj.Rebind();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ApplyBuiltinRootMotion(IntPtr L)
	{
		L.ChkArgsCount(1);
		Animator obj = (Animator)L.ChkUnityObjectSelf(1, "Animator");
		obj.ApplyBuiltinRootMotion();
		return 0;
	}
}


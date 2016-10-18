using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;
using System.Collections.Generic;
//using Battle;

public static class PrefabTemplate {

    private const string MODEL_NAME = ""; //FightObj.MODEL_NAME;
    private const string BODY_NAME = "Body";
    private const string HUD_NAME = "HUD";
    private const string MUZZLE_NAME = "Muzzle";
    
    private static SkinnedMeshRenderer init_Skin(GameObject go)
    {
        var skin = go.GetComponentInChildren<SkinnedMeshRenderer>();        
        skin.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        skin.receiveShadows = false;
        //skin.useLightProbes = false;
        //skin.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
		return skin;
    }

    #region 怪物预设生成
    private static RuntimeAnimatorController genZombieAnimatorController(string name)
    {
		var path = string.Format("Assets/Artwork/Model/Role/{0}/{0}Controller.overrideController", name);
		var overrideController = AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(path);
		if (overrideController == null) {
			overrideController = new AnimatorOverrideController();
			AssetDatabase.CreateAsset(overrideController, path);
		}

		var zombieAnimPath = "Assets/Artwork/Animation/Zombie/ZombieController.controller";
		overrideController.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(zombieAnimPath);
		var fmt = "Assets/Artwork/Model/Role/{0}/{0}@{1}.FBX";
        var bornClip = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "born"));
        if (bornClip) {
            overrideController["entry"] = bornClip;
        } else {
            var clipPath = string.Format("Assets/Artwork/Model/Role/{0}/{0}.FBX", name);
            overrideController["entry"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(clipPath);
        }
        overrideController["_attack"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "attack"));
		overrideController["_dead"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "dead"));
        overrideController["_hitaway"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "hitaway"));
        overrideController["_hitawayup"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "hitawayup"));
		overrideController["_idle"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "idle"));
		overrideController["_run"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "run"));
		overrideController["_skill01"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "skill01"));
		overrideController["_skill02"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "skill02"));
		overrideController["_skill03"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "skill03"));
		overrideController["_stun"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "stun"));

		return overrideController;
    }

    private static void chkZombiePrefab(GameObject go)
    {
        //var path = AssetDatabase.GetAssetPath(go);
        //var roleAni = go.GetComponent<Battle.Anim.ZombieAnimator>();
        //GameObject goRole = null;
        //if (!roleAni) {
        //    if (!path.StartsWith("Assets/Artwork/Model/Role") || go.name.Contains("@")) {
        //        Debug.LogErrorFormat("已忽略错误的资源路径：{0}", path);
        //        return;
        //    }
        //    goRole = GoTools.AddChild(null, go);
        //    roleAni = goRole.NeedComponent<Battle.Anim.ZombieAnimator>();
        //} else {
        //    goRole = roleAni.gameObject;
        //}

        //goRole.SetLayerRecursively(LayerMask.NameToLayer("Role"));
        
        //// 动作
        //roleAni.minSpeed = 0.3f;
        //var ani = roleAni.GetComponent<Animator>();
        //ani.runtimeAnimatorController = genZombieAnimatorController(goRole.name);

        //init_Skin(goRole);

        //var hit = new GameObject(FightObj.HIT_NAME);
        //hit.transform.SetParent(goRole.transform.FindByName("Bip01 Spine1"), false);

        //var fire = new GameObject(FightObj.FIRE_NAME);
        //fire.transform.SetParent(goRole.transform.FindByName("Bip01 R Hand"), false);
    }

    [MenuItem("Custom/预设生成/怪物预设")]
    private static void CreateZombiePrefab()
    {
        GameObject[] goes = Selection.gameObjects;
        foreach (var go in goes) {
            chkZombiePrefab(go);
        }
        AssetDatabase.SaveAssets();
    }
    #endregion

    #region 炮塔类预设生成
    private static RuntimeAnimatorController genBatteryAnimatorController(string name)
    {
        var path = string.Format("Assets/Artwork/Model/Role/{0}/{0}Controller.overrideController", name);
        var overrideController = AssetDatabase.LoadAssetAtPath<AnimatorOverrideController>(path);
        if (overrideController == null) {
            overrideController = new AnimatorOverrideController();
            AssetDatabase.CreateAsset(overrideController, path);
        }

        var batteryAnimPath = "Assets/Artwork/Animation/Zombie/BatteryController.controller";
        overrideController.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(batteryAnimPath);
        var fmt = "Assets/Artwork/Model/Role/{0}/{0}@{1}.FBX";
        overrideController["entry"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "born"));
        overrideController["_attack"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "attack"));
        overrideController["_dead"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "dead"));
        overrideController["_idle"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "idle"));
        overrideController["_skill01"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "skill01"));
        overrideController["_skill02"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "skill02"));
        overrideController["_skill03"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "skill03"));
        overrideController["_stun"] = AssetDatabase.LoadAssetAtPath<AnimationClip>(string.Format(fmt, name, "stun"));
        
        return overrideController;
    }

    private static void chkBatteryPrefab(GameObject go)
    {
        //var path = AssetDatabase.GetAssetPath(go);
        //var ani = go.GetComponent<Battle.Anim.BatteryAnimator>();
        //GameObject goRole = null;
        //if (!ani) {
        //    if (!path.StartsWith("Assets/Artwork/Model/Role") || go.name.Contains("@")) {
        //        Debug.LogErrorFormat("已忽略错误的资源路径：{0}", path);
        //        return;
        //    }
        //    goRole = GoTools.AddChild(null, go);
        //    ani = goRole.NeedComponent<Battle.Anim.BatteryAnimator>();
        //} else {
        //    goRole = ani.gameObject;
        //}

        //goRole.SetLayerRecursively(LayerMask.NameToLayer("Role"));

        //// 动作
        //ani.anim.runtimeAnimatorController = genBatteryAnimatorController(goRole.name);

        //init_Skin(goRole);

        //var bipSpine = goRole.transform.FindByName("Bip01 Spine1");

        //var hit = new GameObject(FightObj.HIT_NAME);
        //hit.transform.SetParent(bipSpine, false);

        //var fire = new GameObject(FightObj.FIRE_NAME);
        //fire.transform.SetParent(goRole.transform.FindByName("Bip01 R Hand"), false);
    }

    [MenuItem("Custom/预设生成/炮塔预设")]
    private static void CreateBatteryPrefab()
    {
        GameObject[] goes = Selection.gameObjects;
        foreach (var go in goes) {
            chkBatteryPrefab(go);
        }
        AssetDatabase.SaveAssets();
    }
    #endregion

    private static void chkWeaponPrefab(GameObject go)
    {
        var root = go.transform;

        var modelTrans = root.Find(MODEL_NAME);
        if (modelTrans == null) {
            string path = string.Format("Assets/Artwork/Model/Weapon/{0}.FBX", go.name);
            GameObject model = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (model) {
                modelTrans = GoTools.AddChild(go, model).transform;                
                modelTrans.name = MODEL_NAME;
            } else {
                EditorUtility.DisplayDialog("提示",
                    string.Format("未找到模型:\n{0}", path), "确定");
                return;
            }
        }
        modelTrans.localRotation = Quaternion.Euler(0, -90, -90);

        var rdr = modelTrans.GetComponentInChildren<SkinnedMeshRenderer>();
        if (rdr) {
            var mesh = rdr.gameObject.AddComponent<MeshFilter>();
            mesh.sharedMesh = rdr.sharedMesh;
            var meshRdr = rdr.gameObject.AddComponent<MeshRenderer>();
            meshRdr.sharedMaterials = rdr.sharedMaterials;
            meshRdr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRdr.receiveShadows = false;
            meshRdr.useLightProbes = false;
            meshRdr.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            SkinnedMeshRenderer.DestroyImmediate(rdr);
        }

        var muzzle = root.Find(MUZZLE_NAME);
        if (muzzle == null) {
            var goM = GoTools.AddChild(go, (GameObject)null);
            goM.name = MUZZLE_NAME;
            muzzle = goM.transform;            
            muzzle.SetParent(root);
        }
        muzzle.localRotation = Quaternion.Euler(0, -90, 0);

        go.SetLayerRecursively(LayerMask.NameToLayer("Role"));
    }

    [MenuItem("Custom/预设生成/武器预设")]
    private static void CraeteWeaponPrefab()
    {
        GameObject[] goes = Selection.gameObjects;
        if (goes != null && goes.Length == 1) {
            chkWeaponPrefab(goes[0]);
        } else {
            EditorUtility.DisplayDialog("提示", "必须且只能选择一个对象", "确定");
        }
    }

}

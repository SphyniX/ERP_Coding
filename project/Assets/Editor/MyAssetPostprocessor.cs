using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using DelegateModelImport = System.Action<UnityEditor.ModelImporter>;
using DelegateTextureImport = System.Action<UnityEditor.TextureImporter, string>;
using DelegateAudioImport = System.Action<UnityEditor.AudioImporter>;

public class MyAssetPostprocessor : AssetPostprocessor
{
    #region 模型处理
    private static void PreprocessRoleModel(ModelImporter mi)
    {
        if (mi.name.Contains("@")) {
            mi.animationType = ModelImporterAnimationType.Generic;
            mi.importAnimation = true;
        }
    }

    private static void PreprocessWeaponModel(ModelImporter mi)
    {
        mi.animationType = ModelImporterAnimationType.Generic;
        mi.importAnimation = true;
    }
    
    private static Dictionary<string, DelegateModelImport> dictOnPreprocessModelAction = new Dictionary<string, DelegateModelImport>() {
        { "Assets/Artwork/Model/Role", PreprocessRoleModel },
        { "Assets/Artwork/Model/Weapon", PreprocessWeaponModel },
    };
        
    /// <summary>
    /// 预载入模型设置
    /// </summary>
    private void OnPreprocessModel()
    {
        foreach (var kv in dictOnPreprocessModelAction) {
            if (assetPath.Contains(kv.Key)) {
                kv.Value.Invoke(assetImporter as ModelImporter);
                break;
            }
        }
    }

    private static void PostprocessRoleModel(ModelImporter mi)
    {
        if (mi.assetPath.EndsWith("@run.fbx", System.StringComparison.OrdinalIgnoreCase)) {
             Object[] objects = AssetDatabase.LoadAllAssetsAtPath(mi.assetPath);
             foreach (Object obj in objects) {
                 AnimationClip clip = obj as AnimationClip;
                 if (clip != null) {
                     clip.wrapMode = WrapMode.Loop;
                 }
             }
            //var clipAnimations = mi.clipAnimations;
            //foreach (var clip in clipAnimations) {
            //    clip.loopTime = true;
            //}
            //AssetDatabase.ImportAsset(mi.assetPath);
        }
    }

    private static Dictionary<string, DelegateModelImport> dictOnPostprocessModelAction = new Dictionary<string, DelegateModelImport>() {
        { "Assets/Artwork/Model/Role", PostprocessRoleModel },
    };

    /// <summary>
    /// 模型已加载设置
    /// </summary>
    private void OnPostprocessModel(GameObject root)
    {
        foreach (var kv in dictOnPostprocessModelAction) {
            if (assetPath.Contains(kv.Key)) {
                kv.Value.Invoke(assetImporter as ModelImporter);
                break;
            }
        }
    }

    #endregion

    #region 图片处理
    private static void PreprocessUITexture(TextureImporter ti, string folder)
    {
        ti.textureType = TextureImporterType.Sprite;
        ti.mipmapEnabled = false;
        ti.textureFormat = TextureImporterFormat.AutomaticTruecolor;
        if (ti.maxTextureSize > 1024) {
            ti.maxTextureSize = 1024;
        }

        ti.spritePackingTag = folder;
    }
    private static void PreprocessRawTexture(TextureImporter ti, string folder)
    {
        ti.textureType = TextureImporterType.Sprite;
        ti.mipmapEnabled = false;
        //ti.textureFormat = TextureImporterFormat.AutomaticTruecolor;
        ti.maxTextureSize = 2048;

        ti.spritePackingTag = "";
    }

	private static void PreprocessRoleTexture(TextureImporter ti, string folder)
	{
		ti.textureType = TextureImporterType.Advanced;
		ti.wrapMode = TextureWrapMode.Clamp;
		ti.filterMode = FilterMode.Bilinear;
		ti.mipmapEnabled = false;
        ti.textureFormat = TextureImporterFormat.AutomaticCompressed;
		ti.compressionQuality = 100;
		ti.maxTextureSize = 512;
	}

	private static void PreprocessWeaponTexture(TextureImporter ti, string folder)
	{
		ti.textureType = TextureImporterType.Advanced;
		ti.wrapMode = TextureWrapMode.Clamp;
		ti.filterMode = FilterMode.Bilinear;
		ti.mipmapEnabled = false;
        ti.textureFormat = TextureImporterFormat.AutomaticCompressed;
		ti.compressionQuality = 100;
		ti.maxTextureSize = 256;
	}

	private static void PreprocessFxTexture(TextureImporter ti, string folder)
	{
		ti.textureType = TextureImporterType.Advanced;
		ti.filterMode = FilterMode.Bilinear;
        ti.textureFormat = TextureImporterFormat.AutomaticCompressed;
		ti.compressionQuality = 100;
        ti.maxTextureSize = 128;
	}

    private static Dictionary<string, DelegateTextureImport> dictTextureImportActions = new Dictionary<string, DelegateTextureImport>() {
        { "Assets/Artwork/UI", PreprocessUITexture },
        { "Assets/RefAssets/Atlas", PreprocessUITexture },
        { "Assets/RefAssets/RawImage", PreprocessRawTexture },
		{ "Assets/Artwork/Model/Role", PreprocessRoleTexture },
        { "Assets/Artwork/Model/Body", PreprocessRoleTexture },
        { "Assets/Artwork/Model/Head", PreprocessRoleTexture },
		{ "Assets/Artwork/Model/Weapon", PreprocessWeaponTexture },
		{ "Assets/RefAssets/FX", PreprocessFxTexture },
    };

	private void OnPreprocessTexture()
    {
        // 目录
        var parent = Path.GetDirectoryName(assetPath).Replace("\\", "/");
        var folder = Path.GetFileName(parent);
        foreach (var kv in dictTextureImportActions) {
            if (assetPath.Contains(kv.Key)) {
                //TextureImporter ti = (TextureImporter)assetImporter;
                //ti.textureFormat = TextureImporterFormat.AutomaticTruecolor;
                kv.Value.Invoke(assetImporter as TextureImporter, folder);
                break;
            }
        }
	}

    #endregion


    #region 音频资源


    public static void PreprocessFxAudio(AudioImporter ai)
    {
        var settings = ai.defaultSampleSettings;
        settings.compressionFormat = AudioCompressionFormat.ADPCM;
        ai.defaultSampleSettings = settings;
    }

    private static Dictionary<string, DelegateAudioImport> dictAudioImportActions = new Dictionary<string, DelegateAudioImport>() {        
        { "Assets/RefAssets/FX", PreprocessFxAudio },
    };

    private void OnPreprocessAudio()
    {
        foreach (var kv in dictAudioImportActions) {
            if (assetPath.Contains(kv.Key)) {
                kv.Value.Invoke(assetImporter as AudioImporter);
                break;
            }
        }
    }
    #endregion

    private void OnPostProcessGameObjectWithUserProperties(
		GameObject go, 
		string[] propNames, System.Object[] values)
	{
		Debug.Log(go);
	}

    private void OnPreprocessAssetbundleNameChanged(string assetPath, string previousAssetBundleName, string newAssetBundleName)
    {
        
    }
}

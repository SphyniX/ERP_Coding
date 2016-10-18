using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
//using Battle.FX;

namespace Artwork
{
    public static class FxChecker
    {
        [MenuItem("Assets/美术检查/战斗特效（选中）")]
        private static void CheckFxPrefab()
        {
            //if (Application.isPlaying) {
            //    Debug.LogWarningFormat("不能在游戏运行时执行检查");
            //    return;
            //}

            //var goes = Selection.gameObjects;
            //for (int i = 0; i < goes.Length; ++i) {
            //    var fxCfg = goes[i].GetComponent(typeof(IFxCfg)) as IFxCfg;
            //    if (fxCfg == null) continue;
            //    var assetPath = AssetDatabase.GetAssetPath(goes[i]);
            //    for (int n = 0; ; n++) {
            //        var fxCtrl = fxCfg[n];
            //        if (fxCtrl == null) break;
            //        if (fxCtrl.prefab == null) continue;

            //        // 检查是否出现多个FxCtrl                
            //        var fxCtrls = fxCtrl.prefab.GetComponentsInChildren(typeof(IFxCtrl), true);
            //        var fxName = fxCtrl.prefab.name;
            //        if (fxCtrls.Length > 1) {
            //            Debug.LogErrorFormat("【多余的特效脚本】{0}/{1}。不应该有多个<FxCtrl>", assetPath, fxName);
            //        }

            //        var fxAnchor = fxCtrl.prefab.GetComponent(typeof(IFxAnchor));
            //        if (fxAnchor == null) {
            //            Debug.LogErrorFormat("【特效脚本缺失】{0}/{1}未挂载<IFxAnchor>脚本（缺少{1}或{2}", assetPath, fxName, typeof(FxBoneType), typeof(FxBoneName));
            //        }

            //        // 检查依赖
            //        var assetRoot = Path.GetDirectoryName(assetPath);
            //        var assetPaths = AssetDatabase.GetDependencies(new string[] { assetPath });
            //        foreach (var path in assetPaths) {
            //            if (path.EndsWith(".cs", System.StringComparison.OrdinalIgnoreCase) 
            //                || path.EndsWith(".shader", System.StringComparison.OrdinalIgnoreCase)) continue;

            //            if (!path.StartsWith(assetRoot)) {
            //                Debug.LogErrorFormat("【依赖项位置错误】[{0}/{1}]依赖了[{2}]", assetPath, fxName, path);
            //            }
            //        }
            //    }
            //}

            
            Debug.Log("检查完成");
        }
    }
}

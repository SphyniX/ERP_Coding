using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ZFrame.UGUI;

namespace Artwork
{
    public static class UIChecker
    {
        private static string getObjectRelativePath(string rootPath, Object obj)
        {
            Assert.IsNotNull(obj, "获取资源相对路径势必：对象为空！");

            var path = AssetDatabase.GetAssetPath(obj).Replace('\\', '/');
            return path.Substring(rootPath.Length);
        }

        [MenuItem("Assets/界面检查/查找未使用的Sprite")]
        public static void CheckUnuseSprite()
        {
            string UIRootPath = "/Assets/Artwork/UI";
            // 使用中的图片
            var liPath = new List<string>();
            liPath.AddRange(AssetDatabase.GetAssetPathsFromAssetBundle("ui.unity3d"));
            liPath.AddRange(AssetDatabase.GetAssetPathsFromAssetBundle("qte.unity3d"));
            Dictionary<string, int> UsingSprites = new Dictionary<string, int>();
            List<string> IgnoreSprites = new List<string>();

            // 忽略的图片
            foreach (var p in liPath) {
                var prefab = AssetDatabase.LoadAssetAtPath(p, typeof(GameObject)) as GameObject;
                if (prefab.name.StartsWith("Atlas@")) {
                    var lib = prefab.GetComponent<ObjectLibrary>();
                    foreach (var o in lib.Objects) {
                        var key = getObjectRelativePath(UIRootPath, o);
                        IgnoreSprites.Add(key);
                    }
                }
            }

            foreach (var p in liPath) {
                var prefab = AssetDatabase.LoadAssetAtPath(p, typeof(GameObject)) as GameObject;
                if (prefab.name.StartsWith("Atlas@")) continue;
                if (prefab) {
                    var imgs = prefab.GetComponentsInChildren<Image>(true);
                    foreach (var img in imgs) {
                        if (img.sprite) {
                            var path = AssetDatabase.GetAssetPath(img.sprite);
                            var ai = AssetImporter.GetAtPath(path);
                            if (ai == null) {
                                LogMgr.W("{0}/{1} ==> {2} 使用了内置的UI资源!",
                                    prefab.name, img.rectTransform.GetHierarchy(prefab.transform), path);
                            }
                            if (ai != null && string.IsNullOrEmpty(ai.assetBundleName)) {
                                var n = 0;
                                if (img.sprite) {
                                    var key = getObjectRelativePath(UIRootPath, img.sprite);
                                    if (UsingSprites.TryGetValue(key, out n)) {
                                        UsingSprites[key] = n + 1;
                                    } else {
                                        UsingSprites.Add(key, 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // 存在的图片
            var uipath = (Application.dataPath + "/Artwork/UI/").Replace('\\', '/');
            var dir = new DirectoryInfo(uipath);
            var files = dir.GetFiles("*.png", SearchOption.AllDirectories);
            var count = 0;
            foreach (var f in files) {
                var spPath = f.FullName.Replace('\\', '/');
                spPath = spPath.Substring(uipath.Length);
                if (IgnoreSprites.Contains(spPath)) continue;
                if (!UsingSprites.ContainsKey(spPath)) {
                    count += 1;
                    LogMgr.D("未使用的UI图片：{0}", spPath);
                }
            }
            LogMgr.D("共{0}个。", count);
        }


        [MenuItem("Assets/界面检查/清除Sprite引用")]
        public static void ClearIconReference()
        {
            var paths = AssetDatabase.GetAssetPathsFromAssetBundle("ui.unity3d");
            foreach (var p in paths) {
                var prefab = AssetDatabase.LoadAssetAtPath(p, typeof(GameObject)) as GameObject;
                if (prefab) {
                    GameObject go = GameObject.Instantiate(prefab);
                    bool flag = false;

                    var imgs = go.GetComponentsInChildren<Image>(true);
                    foreach (var img in imgs) {
                        if (!img.sprite) continue;
                        var ai = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(img.sprite));
                        if (ai && !string.IsNullOrEmpty(ai.assetBundleName)) {
                            AssetPacker.Log("清除{0} <- {1}/{2}", img.sprite, prefab.name, img.GetHierarchy(go.transform));
                            img.sprite = null;
                            flag = true;
                        }
                    }

                    var rawImgs = go.GetComponentsInChildren<RawImage>(true);
                    foreach (var img in rawImgs) {
                        if (!img.texture) continue;
                        var ai = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(img.texture));
                        if (ai && !string.IsNullOrEmpty(ai.assetBundleName)) {
                            AssetPacker.Log("清除{0} <- {1}/{2}", img.texture, prefab.name, img.GetHierarchy(go.transform));
                            img.texture = null;
                            flag = true;
                        }
                    }

                    if (flag) {
                        PrefabUtility.ReplacePrefab(go, prefab, ReplacePrefabOptions.ReplaceNameBased);
                    }
                    GameObject.DestroyImmediate(go);
                }
            }
            AssetPacker.Log("清除UI图标引用: 已完成。");
            AssetDatabase.SaveAssets();
        }

        [MenuItem("Assets/界面检查/检查本地化勾选")]
        public static void MarkLocalize()
        {
            var paths = AssetDatabase.GetAssetPathsFromAssetBundle("ui.unity3d");
            foreach (var p in paths) {
                var prefab = AssetDatabase.LoadAssetAtPath(p, typeof(GameObject)) as GameObject;
                if (prefab) {
                    GameObject go = GameObject.Instantiate(prefab);
                    bool flag = false;

                    var lbs = go.GetComponentsInChildren<UILabel>(true);
                    foreach (var lb in lbs) {
                        var c = lb.name[lb.name.Length - 1];
                        if (c == '=' || c == '_') {
                            if (!lb.localize) {
                                lb.localize = true;
                                flag = true;
                                AssetPacker.Log("勾选本地化： {0}/{1}", prefab.name, lb.rectTransform.GetHierarchy(go.transform));
                            }
                        } else if (lb.localize) {
                            LogMgr.W("不正确的本地化勾选：{0}/{1}",
                                prefab.name, lb.rectTransform.GetHierarchy(go.transform));
                        }
                    }

                    if (flag) {
                        AssetPacker.Log("替换：{0}", prefab);
                        PrefabUtility.ReplacePrefab(go, prefab, ReplacePrefabOptions.ReplaceNameBased);
                    }
                    GameObject.DestroyImmediate(go);
                }
            }
            AssetPacker.Log("标志本地化: 已完成。");
            AssetDatabase.SaveAssets();
        }

        [MenuItem("Assets/界面检查/检查本地化配置")]
        public static void CheckLocalize()
        {
            var loc = AssetDatabase.LoadAssetAtPath<Localization>("Assets/RefAssets/Launch/Localization.asset");
            loc.Reset();
            loc.currentLang = "zhCN";
    		loc.MarkLocalization();

            // 记录自定义的需要本地化的字符串
            foreach (var txt in loc.customTexts) {
                if (string.IsNullOrEmpty(txt)) continue;

                if (!loc.IsLocalized(txt)) {
                    AssetPacker.Log("自定义文本添加：{0}", txt);
                }
                loc.Set(txt, txt);
            }

            // 记录UI中需要本地化的字符串
            var paths = AssetDatabase.GetAssetPathsFromAssetBundle("ui.unity3d");
            foreach (var p in paths) {
                var prefab = AssetDatabase.LoadAssetAtPath(p, typeof(GameObject)) as GameObject;
                if (!prefab) continue;

                var lbs = prefab.GetComponentsInChildren<UILabel>(true);
                foreach (var lb in lbs) {
                    if (!lb.localize) continue;
                    if (string.IsNullOrEmpty(lb.rawText)) continue;

                    if (!loc.IsLocalized(lb.rawText)) {
    					AssetPacker.Log("静态文本添加：{0} @{1}/{2}", lb.rawText,
                            prefab.name, lb.rectTransform.GetHierarchy(prefab.transform));
                    }
                    loc.Set(lb.rawText, lb.rawText);
                }
            }
            loc.SaveLocalization();

            AssetPacker.Log("检查本地化: 已完成。");
            AssetDatabase.Refresh();
        }
    }
}

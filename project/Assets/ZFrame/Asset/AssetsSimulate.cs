using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
namespace ZFrame.Asset
{
	using UnityEditor;

	public sealed class AssetsSimulate : AssetLoader
	{
	    private class SimAssetBundle : AbstractAssetBundleRef
	    {
			private class SimAsset
			{
				public string name { get; private set; }
				public string path { get; private set; }
				private Dictionary<System.Type, Object> m_ObjRefs;
				public SimAsset(string path)
				{
					this.path = path;
					this.name = Path.GetFileNameWithoutExtension(path);
				}
				public Object Load(System.Type type)
				{
					if (m_ObjRefs == null) {
						m_ObjRefs = new Dictionary<System.Type, Object>();
					}
					Object objRef;
					if (!m_ObjRefs.TryGetValue(type, out objRef)) {
						objRef = AssetDatabase.LoadAssetAtPath(path, type);
						m_ObjRefs.Add(type, objRef);
					}
					return objRef;
				}
				public void Unload()
				{
					if (m_ObjRefs != null) {
						m_ObjRefs.Clear();
					}
				}
				
				public override string ToString()
				{
					return string.Format("[({0}), ({1})]", name, path);
				}
			}

			public bool loaded;
	        private Dictionary<string, SimAsset> m_AllAssets;
            public SimAssetBundle(string assetbundleName, string[] assetPaths)
            {
                this.name = assetbundleName;

                m_AllAssets = new Dictionary<string, SimAsset>();
                foreach (var a in assetPaths) {
                    var asset = new SimAsset(a);
                    if (!m_AllAssets.ContainsKey(asset.name)) {
                        m_AllAssets.Add(asset.name, asset);
                    } else {
                        LogMgr.E("资源已加载: {0}", asset);
                    }
                }
                loaded = false;
            }

			public override bool IsEmpty()
			{
				return !loaded;
			}

	        public override Object Load(string assetName, System.Type type)
	        {
				SimAsset asset;
				if (m_AllAssets.TryGetValue(assetName, out asset)) {
					return asset.Load(type);
				}
				return null;
	        }

			public override IEnumerator LoadAsync(string assetName, System.Type type, ObjectOut output)
			{
				yield return null;
				output.loadedObj = Load(assetName, type);
			}

			protected override void UnloadAssets()
			{
				foreach (var asset in m_AllAssets.Values) {
					asset.Unload();
				}
				loaded = false;
			}

			public string GetAssetPath(string assetName)
			{
				SimAsset asset;
				if (m_AllAssets.TryGetValue(assetName, out asset)) {
					return asset.path;
				}
				return null;
			}

			public void SetAllowUnload(bool allowUnload) 
			{
				this.allowUnload = allowUnload;
			}

			public string ToDetailString()
			{
				var strbld = new System.Text.StringBuilder(string.Format("[SimAB: {0}]\n", name));
				foreach (var asset in m_AllAssets) {
					strbld.AppendLine(asset.ToString());
				}
				strbld.AppendLine();
				return strbld.ToString();
			}
	    }

	    private Dictionary<string, SimAssetBundle> m_AllAssetBundles;

	    private void Awake()
	    {
	        m_AllAssetBundles = new Dictionary<string, SimAssetBundle>();
	        // 分析资源结构，做虚拟加载使用
	        var assetNames = AssetDatabase.GetAllAssetBundleNames();
	        foreach (var ab in assetNames) {
	            var assets = AssetDatabase.GetAssetPathsFromAssetBundle(ab);
	            m_AllAssetBundles.Add(ab, new SimAssetBundle(ab, assets));
	        }

	        /*
	        foreach (var ab in m_allAssetBundles.Values) {
	            LogMgr.D(ab.ToString());
	        }
	        //*/
	    }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <param name="output"></param>
        /// <returns></returns>
		protected override IEnumerator PerformTask(AsyncLoadingTask task, ABRefOut output)
		{
			yield return null;

			SimAssetBundle abRef;
			if (m_AllAssetBundles.TryGetValue(task.assetbundleName, out abRef)) {
				abRef.loaded = true;
				abRef.SetAllowUnload(task.allowUnload);
				output.abRef = abRef;
				output.md5 = "";
			}
		}

	    public override AsyncOperation LoadLevelAsync(string path)
	    {
	        // 解析出资源包和资源对象
	        string assetbundleName, assetName;
	        GetAssetpath(path, out assetbundleName, out assetName);

	        SimAssetBundle assetbundle;
	        if (m_AllAssetBundles.TryGetValue(assetbundleName, out assetbundle)) {
	            var assetPath = assetbundle.GetAssetPath(assetName);
				if (assetPath != null) {
					return EditorApplication.LoadLevelAsyncInPlayMode(assetPath);
	            }
	        }
			LogMgr.E(string.Format("场景未标志为<AssetBundle>：{0}({1}, {2})", path, assetbundleName, assetName));
	        return null;
	    }

	}
}
#endif
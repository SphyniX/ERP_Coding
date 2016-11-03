using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ZFrame.Asset
{
    public delegate void DelegateObjectLoaded(Object loadedObj, object param);
	public delegate Coroutine DelegateAssetBundleLoaded(AbstractAssetBundleRef ab, string md5);	

	/// <summary>
	/// 抽象的AssetBundle引用，管理已加载的AssetBundle
	/// </summary>
	public abstract class AbstractAssetBundleRef
	{
		public class ObjectOut
		{
			public Object loadedObj;
		}

        /// <summary>
        /// AssetBundle名，唯一
        /// </summary>
        private string m_Name;
		public string name
        {
            get { return m_Name; }
            protected set
            {
                m_Name = value;
                group = Path.GetDirectoryName(m_Name);
            }
        }
		
		/// <summary>
		/// AssetBundle组名
		/// </summary>
		public string group { get; private set; }
		
		/// <summary>
		/// 是否允许在切换场景时完全卸载
		/// </summary>
		public bool allowUnload { get; protected set; }
		
		/// <summary>
		/// 上一次加载的时间
		/// </summary>
		public float lastLoaded { get; set; }

		protected abstract void UnloadAssets();
		public abstract bool IsEmpty();
		public abstract Object Load(string assetName, System.Type type);
		public abstract IEnumerator LoadAsync(string assetName, System.Type type, ObjectOut output);

		public void Unload(bool forced = false)
		{
			if (allowUnload || forced) {
				AssetLoader.Log("{0}Unload {1}", forced ? "Forced " : "", this);
				UnloadAssets();
			} else {
				AssetLoader.Log("Skip {0}", this.ToString());
			}
		}
        
		public override string ToString()
		{
			return string.Format("[<{0}> {1} @{2}; unload={3}]", group, name, lastLoaded, allowUnload);
		}
		
		public static int Compare(AbstractAssetBundleRef a, AbstractAssetBundleRef b)
		{
			return (int)(a.lastLoaded * 1000000 - b.lastLoaded * 1000000);
		}
	}

	public abstract class AssetLoader : MonoBehaviour
	{
        /// <summary>
        /// 资源包/资源加载任务
        /// </summary>
		protected class AsyncLoadingTask
		{
            /// <summary>
            /// 资源包参数
            /// </summary>
			public string assetbundleName { get; private set; }
			public DelegateAssetBundleLoaded onABLoaded { get; private set; }
			public bool allowUnload { get; private set; }
			public bool needMD5 { get; private set; }
			public AsyncLoadingTask() { }
			public void SetBundle(string assetbundleName, bool allowUnload, DelegateAssetBundleLoaded onLoaded, bool needMD5)
			{
				this.assetbundleName = assetbundleName;
				this.allowUnload = allowUnload;
				this.onABLoaded = onLoaded;
				this.needMD5 = needMD5;
			}
            
            /// <summary>
            /// 资源参数
            /// </summary>
			public System.Type type;
			public string assetName { get; private set; }
			public DelegateObjectLoaded onObjectLoaded { get; private set; }
			public object param { get; private set; }
			public void SetAsset(System.Type type, string assetName, DelegateObjectLoaded onLoaded, object param)
			{
				this.type = type;
				this.assetName = assetName;
				this.onObjectLoaded = onLoaded;
				this.param = param;
			}

            /// <summary>
            /// 是否强制从原始资源加载
            /// </summary>
			public bool forcedStreaming;

			public void Reset()
			{
				assetbundleName = null;
				onABLoaded = null;

				assetName = null;
				onObjectLoaded = null;
			}

			public override string ToString()
			{
				return string.Format("[Task:{0}@{1})]", type, assetbundleName);
			}
		}

		protected class ABRefOut
		{
			public AbstractAssetBundleRef abRef;            
			public string md5;
			public AssetsTransfer transfer;

            public void Reset()
            {
                abRef = null;                
                md5 = null;
				transfer = null;
            }

		}

		private ABRefOut abOut = new ABRefOut();
		private AbstractAssetBundleRef.ObjectOut objOut = new AbstractAssetBundleRef.ObjectOut();
		protected Pool<AsyncLoadingTask> m_TaskPool = new Pool<AsyncLoadingTask>(null, (task) => task.Reset());

        /// <summary>
        /// 资源从磁盘载入
        /// </summary>
		protected abstract IEnumerator PerformTask(AsyncLoadingTask task, ABRefOut output);
        /// <summary>
        /// 场景加载方法
        /// </summary>
		public abstract AsyncOperation LoadLevelAsync(string path);

		public static void Log(string fmt, params object[] Args)
		{
			if (LogMgr.logLevel == LogMgr.LogLevel.I) {
				Debug.Log(string.Format("[Asset] " + fmt, Args));
			}
		}
		
		public const string ASSET_EXT = ".unity3d";
		public static void GetAssetpath(string path, out string assetbundleName, out string assetName)
		{
            Debug.Log("<color=#ff00ff>AssetLoader.cs资源名字" + path + "</color>");
            assetbundleName = Path.GetDirectoryName(path) + ASSET_EXT;
			assetName = Path.GetFileNameWithoutExtension(path);
            Debug.Log("<color=#ffff00>AssetLoader.cs资源名字"+assetbundleName +"-----"+ assetName+"</color>");
            assetbundleName = assetbundleName.ToLower();
		}

		#region 资源管理
		private Dictionary<string, AbstractAssetBundleRef> m_LoadedAssetBundles = new Dictionary<string, AbstractAssetBundleRef>();
        /// <summary>
        /// 某个资源包是否存在内存中
        /// </summary>
		private bool TryGetAssetBundle(string assetbundleName, out AbstractAssetBundleRef abRef)
		{
			abRef = null;

			if (string.IsNullOrEmpty(assetbundleName)) return false;

			if (m_LoadedAssetBundles.TryGetValue(assetbundleName, out abRef)) {
				return !abRef.IsEmpty();
			}
			return false;
		}

        /// <summary>
        /// 执行加载任务
        /// </summary>
		private IEnumerator LoadAssetBundleOnebyone()
		{
			float totalLoadTime = 0;
			for (;;) {
				float loadTime = Time.realtimeSinceStartup;
				m_CurrentTask = m_Tasks.Dequeue();

                abOut.Reset();
                var abName = m_CurrentTask.assetbundleName;                
				if (!string.IsNullOrEmpty(abName) && !TryGetAssetBundle(abName, out abOut.abRef)) {
					yield return StartCoroutine(PerformTask(m_CurrentTask, abOut));
					if (abOut.abRef != null) {
                        abOut.abRef.lastLoaded = Time.realtimeSinceStartup;
                        Log("Ready: {0}", abOut.abRef);
                        if (!m_LoadedAssetBundles.ContainsKey(abName)) {                            
							m_LoadedAssetBundles.Add(abName, abOut.abRef);
						}
					} else {
						if (abName.EndsWith(ASSET_EXT)) {
                            Log("加载失败：{0}", abName);
						}
                    }
					loadTime = Time.realtimeSinceStartup - loadTime;
					Log("Done: {0} in {1} secs", m_CurrentTask, loadTime);
				}

				if (m_CurrentTask.onABLoaded != null) {
					yield return m_CurrentTask.onABLoaded.Invoke(abOut.abRef, abOut.md5);
				}

                if (abOut.transfer != null) {
                    // 把资源包内容从原始位置(streamingAssetsPath)缓存到数据位置(persistentDataPath)
#if UNITY_EDITOR
                    // But! 编辑器内不缓存AssetBundle，每次都使用最新的
#else
				    abOut.transfer.Begin();
				    abOut.transfer = null;
#endif
                }

                Object loadedObj = null;                
                if (!string.IsNullOrEmpty(m_CurrentTask.assetName)) {
                    if (abOut.abRef != null) {
                        // loadedObj = abOut.abRef.Load(m_CurrentTask.assetName, m_CurrentTask.type);
                        // 异步
                        objOut.loadedObj = null;
                        yield return StartCoroutine(abOut.abRef.LoadAsync(
                            m_CurrentTask.assetName, m_CurrentTask.type, objOut));
                        loadedObj = objOut.loadedObj;
                    } else {
                        LogMgr.W("[{0}]未加载。", m_CurrentTask.assetbundleName);
                    }
                }

                if (m_CurrentTask.onObjectLoaded != null) {
					m_CurrentTask.onObjectLoaded.Invoke(loadedObj, m_CurrentTask.param);
				}

                totalLoadTime += loadTime;
				m_TaskPool.Release(m_CurrentTask);
				m_CurrentTask = null;
				if (m_Tasks.Count == 0) {
					break;
				}
			}
			Log("Total loaded Time = " + totalLoadTime);
		}
		#endregion

		#region 资源加载/释放
        /// <summary>
        /// 从某个位置加载某个类型的资源
        /// </summary>
		public Object Load(System.Type type, string path, bool warnIfMissing = true)
		{
			string assetbundleName, assetName;
			GetAssetpath(path, out assetbundleName, out assetName);
			
			AbstractAssetBundleRef abRef;
            Object loadedObj = null;
            Debug.Log("1不存在prefab--------" + assetName);
            if (TryGetAssetBundle(assetbundleName, out abRef)) {
                Debug.Log("2不存在prefab--------" + assetName);
                loadedObj = abRef.Load(assetName, type);
                Debug.Log("3不存在prefab--------" + assetName);
                if (loadedObj) {
                    Debug.Log("4不存在prefab--------"+ assetName);
                    return loadedObj;
                } else {
                    Debug.Log("5不存在prefab--------" + assetName);
                    if (warnIfMissing) {
                        //LogMgr.W("{0}<{1}>不存在资源。[{2} {3}]", path, type, assetbundleName, assetbundleName);
                        Debug.Log(path +"<" +type+">不存在资源。"+"[{ "+assetbundleName+"} { "+ assetName + @"}]");
                    } else {
                        Log("{0}<{1}> not exist.[{2} {3}]", path, type, assetbundleName, assetName);
                    }
                }
            } else {
                if (warnIfMissing) {
                    LogMgr.W("[{0}]未加载。", assetbundleName);
                } else {
                    Log("[{0}] isn't loaded.", assetbundleName);
                }
            }
			
			return null;
		}

        /// <summary>
        /// 从某个位置加载某个类型的资源（异步）
        /// </summary>
        public void LoadAsync(System.Type type, string path, bool allowUnload, DelegateObjectLoaded onObjectLoaded, object param)
		{
			if (string.IsNullOrEmpty(path)) {
				var task = m_TaskPool.Get();
				task.SetAsset(type, path, onObjectLoaded, param);
				ScheduleTask(task);
			} else {
				string assetbundleName, assetName;
				GetAssetpath(path, out assetbundleName, out assetName);
				var task = m_TaskPool.Get();
				task.SetBundle(assetbundleName, allowUnload, null, false);
				task.SetAsset(type, assetName, onObjectLoaded, param);
				ScheduleTask(task);
			}
		}

		/// <summary>
        /// 释放某个资源包
        /// </summary>
		public void Unload(string path)
		{
			AbstractAssetBundleRef abRef;
            string assetbundleName, assetName;
            GetAssetpath(path, out assetbundleName, out assetName);
            if (TryGetAssetBundle(assetbundleName, out abRef)) {
				if (abRef != null) abRef.Unload();
			}
		}

        /// <summary>
        /// 释放所有资源包
        /// </summary>
		public void UnloadAll(bool forced = false)
		{
			Log("UnloadAll: {0}", forced);
			if (forced) ClearPreload();
			using (var itor = m_LoadedAssetBundles.Values.GetEnumerator()) {
				while (itor.MoveNext()) {
					var abRef = itor.Current;
					if (m_PreloadAssetBundles.ContainsKey(abRef.name)) {
						Log("Keep " + abRef.ToString());
					} else {
						abRef.Unload(forced);
					}
				}
			}

            CollectGarbage();            
		}
		
        /// <summary>
        /// 清空任务队列，停止加载资源包
        /// </summary>
		public void StopLoading()
		{
			m_Tasks.Clear();
		}

        /// <summary>
        /// 限制某一组的资源包的最大数量，多出来的比较早使用的会被释放。
        /// </summary>
		public void LimitAssetBundle(string group, int limit)
		{
			List<AbstractAssetBundleRef> list = new List<AbstractAssetBundleRef>();
			using (var itor = m_LoadedAssetBundles.Values.GetEnumerator()) {
				while (itor.MoveNext()) {
					var abf = itor.Current;
					if (abf.allowUnload && abf.group == group && !abf.IsEmpty()) {
						list.Add(abf);                        
                    }                    
				}
			}

            if (list.Count > limit) {
				list.Sort(AbstractAssetBundleRef.Compare);
				var count = list.Count - limit;
				for (int i = 0; i < count; ++i) {
					Log("Limit Assets for [{0}]:{1}/{2}, unload {3}", group, list.Count, limit, list[i]);
					list[i].Unload();
				}
			}
		}
		#endregion

		#region 管理加载任务队列
		private Queue<AsyncLoadingTask> m_Tasks = new Queue<AsyncLoadingTask>();
		private AsyncLoadingTask m_CurrentTask;
		
        /// <summary>
        /// 添加一个资源包加载任务
        /// </summary>
		protected void ScheduleTask(AsyncLoadingTask task)
		{
			AbstractAssetBundleRef abRef;
			if (TryGetAssetBundle(task.assetbundleName, out abRef)) {
                abRef.lastLoaded = Time.realtimeSinceStartup;
                Log("Loaded: {0}", task);
				if (task.onABLoaded != null) {
					task.onABLoaded.Invoke(abRef, null);
				}
				if (task.onObjectLoaded != null) {
					task.onObjectLoaded.Invoke(abRef.Load(task.assetName, task.type), task.param);
				}
			} else {
				Log("Enqueue: {0}", task);
				m_Tasks.Enqueue(task);
				if (m_CurrentTask == null) {
					StartCoroutine(LoadAssetBundleOnebyone());
				}
			}
		}

        /// <summary>
        /// 把一个资源包名称加入加载任务列表。
        /// </summary>
        public void BundleTask(string assetbundleName, bool allowUnload, DelegateAssetBundleLoaded onLoaded = null, bool needMD5 = false)
		{
			var task = m_TaskPool.Get();
			task.SetBundle(assetbundleName.ToLower(), allowUnload, onLoaded, needMD5);
			ScheduleTask(task);
		}
		
		/// <summary>
		/// 标志某批加载的最终一个加载回调
		/// </summary>
		public void FinalTask(DelegateAssetBundleLoaded onLoaded)
		{
			BundleTask("", true, onLoaded);
		}
        #endregion

        #region 管理载入场景前需要预先加载的资源
        private Dictionary<string, bool> m_PreloadAssetBundles = new Dictionary<string, bool>();
        /// <summary>
        /// 缓存需要预加载的资源包路径
        /// </summary>
		public void CachedPreload(string path, bool allowUnload)
		{
			path = path.ToLower();
			if (!m_PreloadAssetBundles.ContainsKey(path)) {
				m_PreloadAssetBundles.Add(path, allowUnload);        
			}
		}

        /// <summary>
        /// 清除预载缓存
        /// </summary>
		public void ClearPreload()
		{
			m_PreloadAssetBundles.Clear();
		}

        /// <summary>
        /// 执行预加载
        /// </summary>
		public int ExecutePreload(DelegateAssetBundleLoaded onLoaded = null, DelegateAssetBundleLoaded onLoadedAll = null)
		{
			var count =  m_PreloadAssetBundles.Count;
			Log("Load Need AssetBundles = " + count);
			
			using (var itor = m_PreloadAssetBundles.GetEnumerator()) {
				while (itor.MoveNext()) {
					var kv = itor.Current;
					string assetbundleName, assetName;
					GetAssetpath(kv.Key, out assetbundleName, out assetName);
					BundleTask(assetbundleName, kv.Value, onLoaded);
				}
			}
			FinalTask(onLoadedAll);
			m_PreloadAssetBundles.Clear();
			return count;
		}
        #endregion
                
        private static void GC()
        {
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
        private static System.Action s_gccallback = GC;
        public static System.Action CollectGarbage
        {
            set { if (value != null) s_gccallback = value; }
            get { return s_gccallback; }
        }
    }

}

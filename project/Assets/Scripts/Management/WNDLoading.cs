using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZFrame;
using ZFrame.Asset;
using ZFrame.NetEngine;
using ZFrame.UGUI;

public class WNDLoading : MonoBehavior {

    static string mloadedLevel;
    static public string loadedLevelName { get { return mloadedLevel;  } }
    static WNDLoading instance;

	public float assetsProg = 0.5f;
    public float barStep = 0.066f;
	public UISlider sldLoading;
    private string levelToLoad;
	private string loadingFx;
    private float assetsRate;
    private int nAssetBundleToLoad;
    private int nAssetBundleLoaded;

    System.Text.StringBuilder strTrace;
    float beginTime;
    float lastTime;

    private void Awake()
    {
		instance = this;
	}

	// Use this for initialization
    private IEnumerator Start()
    {
        AssetsMgr.A.Loader.StopLoading();
		AssetsMgr.A.Loader.UnloadAll();
        sldLoading.gameObject.SetActive(false);
        yield return null;

        if (!string.IsNullOrEmpty(loadingFx)) {            
            AssetsMgr.A.LoadAsync(typeof(GameObject), loadingFx, true, (uObj, o) => {
                var prefab = uObj as GameObject;
                if (prefab.GetComponent<RectTransform>()) {
                    GoTools.AddChild(transform.parent.gameObject, prefab);
                } else {
                    GoTools.AddChild(null, prefab);
                }
            });
        } else {
            sldLoading.gameObject.SetActive(true);
            sldLoading.value = 0f;
        }

        strTrace = new System.Text.StringBuilder();
        beginTime = Time.realtimeSinceStartup;
        lastTime = beginTime;

        // 执行预加载
        nAssetBundleLoaded = 0;
        nAssetBundleToLoad = AssetsMgr.A.Loader.ExecutePreload(OnAssetBundlesLoading, OnAssetBundlesLoaded);

        float progress = 0;
		while (nAssetBundleLoaded < nAssetBundleToLoad) {
            float add = assetsRate - progress;
            progress += Mathf.Min(add, barStep);
            sldLoading.value = progress * assetsProg;
			yield return null;
		}
        assetsProg = sldLoading.value;

        var asynOpt = AssetsMgr.A.Loader.LoadLevelAsync(levelToLoad);
        progress = 0;
        while (!asynOpt.isDone) {
            float add = asynOpt.progress - progress;
            progress += Mathf.Min(add, barStep);
            sldLoading.value = progress * (1 - assetsProg) + assetsProg;
			yield return null;
        }

        while (sldLoading.value < 1) {
            sldLoading.value += barStep;
			yield return null;
        }

        yield return new WaitForSeconds(2);

        finishLoading();
        //StartCoroutine("FinishLoading");
        ObjectPoolManager.DestroyPooled(gameObject);
	}

    private void LogTime(string str)
    {
        float curr = Time.realtimeSinceStartup;
        strTrace.Append(curr - beginTime).Append("@").Append(curr - lastTime).Append("#").AppendLine(str);
        lastTime = curr;
    }
    
    private Coroutine OnAssetBundlesLoading(AbstractAssetBundleRef abf, string md5)
	{		
		nAssetBundleLoaded += 1;
		assetsRate = (float)nAssetBundleLoaded / nAssetBundleToLoad;
        LogTime("ABF#" + abf);
        return null;
	}

    private Coroutine OnAssetBundlesLoaded(AbstractAssetBundleRef abf, string md5)
	{
		assetsRate = 1;
        return null;
	}
    	
	private void finishLoading()
	{
        mloadedLevel = System.IO.Path.GetFileNameWithoutExtension(levelToLoad);
        var L = LuaScriptMgr.Instance.L;
        L.GetGlobal(LuaScriptMgr.F_ON_LEVEL_LOADED);
        L.DoCall(0, loadedLevelName);
        LogMgr.I("Loading Level Trace:\n{0}", strTrace.ToString());
	}

    IEnumerator FinishLoading() {
        
        mloadedLevel = System.IO.Path.GetFileNameWithoutExtension(levelToLoad);
        var L = LuaScriptMgr.Instance.L;
        L.GetGlobal(LuaScriptMgr.F_ON_LEVEL_LOADED);
        //yield return new WaitForSeconds(3f);
        L.DoCall(0, loadedLevelName);
        LogMgr.I("Loading Level Trace:\n{0}", strTrace.ToString());
        yield return null;
    }
	public static void LoadLevel(string levelPath, string loadingFx)
	{
        if (instance == null || !instance.gameObject.activeInHierarchy) {
            UIManager.CleanLevel();

            GameObject go = UIManager.Instance.CreateWindow("UI/WNDLoading", 1);
            WNDLoading wnd = go.GetComponent<WNDLoading>();
            wnd.levelToLoad = levelPath;
			wnd.loadingFx = loadingFx;;
        }
	}
}

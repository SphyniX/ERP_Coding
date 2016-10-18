using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameEntry : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (AssetsMgr.Instance == null) {
            InitEntry();
        }
	}

    public static void InitEntry()
    {
#if UNITY_EDITOR
        var obj = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/" + ZFrame.Asset.AssetBundleLoader.DIR_ASSETS + "/AssetsMgr.prefab", typeof(GameObject));
        GoTools.AddChild(null, obj as GameObject);
        LogMgr.D("Game Launched from Level: {0}", SceneManager.GetActiveScene().name);
#endif
    }

}

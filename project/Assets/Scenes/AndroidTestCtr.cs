/**
 * 文  件  名：AndroidTest 
 * 作       者：浪浪
 * 生成日期：16-08-09 15:28:16"
 * 功       能：
 * 修改日志：
 * 说明：
**/
using UnityEngine;
using System.Collections;
using ZFrame;
using UnityEngine.UI;
public class AndroidTestCtr : MonoBehaviour {
    public Text lbLog;
    public RawImage texture;
	// Use this for DSB
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void on_click_btn(int i) {
        string ret = null;
#if UNITY_ANDROID
        switch (i) {
            case 1:
                ret = SDKManager.CallApiReturn<string>("com.rongygame.util.XMsgTest", "GetGPS");
                LogMgr.E("CallApiReturn info:{0}", ret);
                break;
            case 2:
                ret = SDKManager.CallApiReturn<string>("com.rongygame.sdk.SDKApi", "OnGameMessageReturn",
                   "{\"method\":\"doTakePhoto\",\"param\":\"takephoto\"}");
                LogMgr.E("CallApiReturn info:{0}", ret);
                break;
            case 3:
                
                ret = SDKManager.CallApiReturn<string>("com.rongygame.sdk.SDKApi", "OnGameMessageReturn",
                   "{\"method\":\"doTakePhoto\",\"param\":\"other\"}");
                LogMgr.E("CallApiReturn info:{0}", ret);
                break;
            default:
                ret = "nothing";
                break;
        }
        lbLog.text = ret;
#endif
    }
    public void OnSDKMessage(string str) {
        LogMgr.E("OnSDKMessage: {0}", str);
        lbLog.text = str;
        StartCoroutine(LoadTexture("image.png"));
    }

    public void OnLoadPhoto(string name) {
        StartCoroutine(LoadTexture(name));
    }
    IEnumerator LoadTexture(string name)
    {
        //注解1
        string path = "file://" + Application.persistentDataPath + "/" + name;
        LogMgr.E("Texture path : {0}", path);

        WWW www = new WWW(path);
        while (!www.isDone) {

        }
        yield return www;
        //为贴图赋值
        texture.texture = www.texture;
    }
}

/**
 * 文  件  名：SDKMgr 
 * 作       者：浪浪
 * 生成日期：16-08-09 17:06:34"
 * 功       能：
 * 修改日志：
 * 说明：
**/
using UnityEngine;
using System.Collections;
using ZFrame.UGUI;
using System.IO;
using ZFrame;
using UnityEngine.Assertions;
using LuaInterface;

public class SDKMgr : MonoSingleton<SDKMgr> {
    private LuaTable m_Tb;
    public const string START = "start";
    public const string SDK_MESSAGE = "on_sdk_message";

    [SerializeField]
    private string m_LuaScript;

    private static string m_persistentDataPath;
    static public string persistentDataPath {
        get {
            if (m_persistentDataPath == null) {
#if UNITY_EDITOR || UNITY_STANDALONE
                m_persistentDataPath = Path.GetDirectoryName(Application.dataPath) + "/Issets/PersistentData";

#elif UNITY_IOS
				m_persistentDataPath = Application.persistentDataPath;
#else
                m_persistentDataPath = Application.persistentDataPath;
#endif
            }
            return m_persistentDataPath;
        }
    }
    public const string IMAGE_FOLDER = "Image";
    static string m_imageRoot;
    static public string imageRootPath {
        get {
            if (m_imageRoot == null) {
				#if UNITY_IOS
				m_imageRoot = persistentDataPath;
				#else
                m_imageRoot = Path.Combine(persistentDataPath, IMAGE_FOLDER);
				#endif
            }
            return m_imageRoot;
        }
    }

    protected override void Awaking()
    {
        if (!string.IsNullOrEmpty(m_LuaScript)) {
            var L = LuaScriptMgr.Instance.L;
            var n = L.DoFile(m_LuaScript);
            Assert.IsTrue(n == 1);
            m_Tb = L.ToLuaTable(-1);
            L.Pop(1);
            Assert.IsNotNull(m_Tb);
        }
    }

    void Start() {
        m_Tb.CallFunc(START, 0);
    }

    // Use this for DSBs
    void OnSDKMessage(string msg) {
        LogMgr.E("OnSDKMessage :{0}", msg);
        m_Tb.CallFunc(SDK_MESSAGE, 0, msg); 
    }
    
    public void OnLoadPhoto(UITexture tex, string name, ZFrame.Asset.DelegateObjectLoaded onLoaded)
    {
        StartCoroutine(LoadTexture( tex, name, onLoaded));
    }
    IEnumerator LoadTexture(UITexture tex, string name, ZFrame.Asset.DelegateObjectLoaded onLoaded)
    {
        //注解1
		Debug.Log ("Starting Load Image!");

        string path = get_image_path(name);
//		Debug.Log (path);
//		if (File.Exists (path)) {
			WWW www = new WWW (path);
		int i = 0;
			while (!www.isDone) {
			if (i > 100) {
				break;
				}
			i++;
			}
			yield return www;
		
			if (www.error == null) {
				tex.enabled = true;
				tex.texture = www.texture;
				onLoaded (www.texture, true);
				Debug.Log ("Loaded Image!");
			} else {
				onLoaded (null, false);
			}
//		} else {
//			Debug.Log ("File is not Exist!");
//		}
    }

    static public string get_image_path(string name) {
        
		Debug.Log ("file:/" + imageRootPath + "/" + name);
#if UNITY_IOS
	    return "file://" + Application.persistentDataPath + "/" + name;
#else 
		return "file://" + imageRootPath + "/" + name;
#endif
    }

    static public string get_buteimage_path(string name) {
#if UNITY_EDITOR || UNITY_STANDALONE
        return "Issets/PersistentData/Image/" + name;
#else
        return get_image_path(name);
#endif
    }
    //参数是图片的路径
    //public byte[] GetPictureData(string imagePath)
    //{
    //    FileStream fs = new FileStream(imagePath, FileMode.Open);
    //    byte[] byteData = new byte[fs.Length];
    //    fs.Read(byteData, 0, byteData.Length);
    //    fs.Close();
    //    return byteData;
    //}

    ////将Image转换成流数据，并保存为byte[] 
    //public byte[] PhotoImageInsert(System.Drawing.Image imgPhoto)
    //{
    //    MemoryStream mstream = new MemoryStream();
    //    imgPhoto.Save(mstream, System.Drawing.Imaging.ImageFormat.Bmp);
    //    byte[] byData = new Byte[mstream.Length];
    //    mstream.Position = 0;
    //    mstream.Read(byData, 0, byData.Length); mstream.Close();
    //    return byData;
    //}

    //public void WritePhoto(byte[] streamByte)
    //{
    //    // Response.ContentType 的默认值为默认值为“text/html”
    //    Response.ContentType = "image/GIF";
    //    //图片输出的类型有: image/GIF     image/JPEG
    //    Response.BinaryWrite(streamByte);
    //}

    //public System.Drawing.Image ReturnPhoto(byte[] streamByte)
    //{
    //    System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
    //    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
    //    return img;
    //}

}

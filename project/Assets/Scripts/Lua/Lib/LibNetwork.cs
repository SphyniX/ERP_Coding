using UnityEngine;
using System.Collections.Generic;
using ZFrame.NetEngine;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;
using System.IO;

public static class LibNetwork {

	public const string LIB_NAME = "libnetwork.cs";
	
	public static void OpenLib(ILuaState lua)
	{
		var define = new NameFuncPair[]
		{
			new NameFuncPair("HttpGet", HttpGet),
			new NameFuncPair("HttpPost", HttpPost),
            new NameFuncPair("HttpUpPhoto", HttpUpPhoto),
            new NameFuncPair("HttpUpMorePhoto", HttpUpMorePhoto),

            new NameFuncPair("HttpDownload", HttpDownload),

			new NameFuncPair("SetParam", SetParam),
		};
		
		lua.L_Register(LIB_NAME, define);
        lua.Pop(1);
	}

	public static string KeyValue2Param<T>(IEnumerable<KeyValuePair<string, T>> enumrable) where T : System.IConvertible
	{
		var itor = enumrable.GetEnumerator();
		System.Text.StringBuilder strbld = new System.Text.StringBuilder();
		while (itor.MoveNext()) {
			string key = itor.Current.Key;
			string value = itor.Current.Value.ToString(null);
			strbld.AppendFormat("{0}={1}&", WWW.EscapeURL(key), WWW.EscapeURL(value));
		}
		return strbld.ToString();
	}
    public static WWWForm KeyValue2Form<T>(IEnumerable<KeyValuePair<string, T>> enumrable, WWWForm wf = null) where T : System.IConvertible
    {
        var itor = enumrable.GetEnumerator();
        if (wf == null) {
            wf = new WWWForm();
        }
        string ff = "";
        while (itor.MoveNext()) {
            string key = itor.Current.Key;
            string value = itor.Current.Value.ToString(null);
            ff += key + ":" + value + "\n";
            wf.AddField(key, value);
        }
        LogMgr.D("{0}", ff);
        return wf;
    }

    public static WWWForm KeyPhoto2Form<T>(IEnumerable<KeyValuePair<string, T>> enumrable, WWWForm wf = null) where T : System.IConvertible
    {
        var itor = enumrable.GetEnumerator();
        if (wf == null) {
            wf = new WWWForm();
        }
        string ff = "";
        while (itor.MoveNext()) {
            string key = itor.Current.Key;
            string value = itor.Current.Value.ToString(null);
            string path = SDKMgr.get_image_path(value);
            ////byte[] btTex = SDKMgr.Instance.GetPictureData(path);
            byte[] btTex = File.ReadAllBytes(path);

            //Texture2D tex = itor.Current.Value as Texture2D;
            //byte[] btTex = tex.EncodeToPNG();

            ff += key + ":" + btTex.ToString() + "\n";
            wf.AddBinaryData(key, btTex);
        }
        LogMgr.D("{0}", ff);
        return wf;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int HttpGet(ILuaState lua)
	{
		string tag = lua.ChkString(1);
		string url = lua.ChkString(2);
        float timeout = (float)lua.OptNumber(4, 10);
        string param = null;
        var luaT = lua.Type(3);
        if (luaT == LuaTypes.LUA_TSTRING) {
            param = lua.ToString(3);
        } else {
            var joParam = lua.ToJsonObj(3) as TinyJSON.ProxyObject;
            if (joParam != null) {
				param = KeyValue2Param(joParam);
            }
        }

        var httpHandler = NetworkMgr.Instance.GetHttpHandler("HTTP");
        if (httpHandler) httpHandler.StartGet(tag, url, param, timeout);
        Debug.LogFormat("HttpGet()---xxx---tag:{0}-----xxx--url:{1}", tag, url);
        return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	static int HttpPost(ILuaState lua)
	{
		string tag = lua.ChkString(1);
		string url = lua.ChkString(2);
        string strHeader = lua.ChkString(5);
        float timeout = (float)lua.OptNumber(6, 10);

        string param = null;
        var luaT = lua.Type(3);
        if (luaT == LuaTypes.LUA_TSTRING) {
            param = lua.ToString(3);
        } else {
            var joParam = lua.ToJsonObj(3) as TinyJSON.ProxyObject;
            if (joParam != null) {
                param = KeyValue2Param(joParam);
            }
        }

        //string postData = null;
        //var luaTD = lua.Type(4);
        //if (luaTD == LuaTypes.LUA_TSTRING) {
        //	postData = lua.ToString(4);
        //} else {
        //	var joPost = lua.ToJsonObj(4) as TinyJSON.ProxyObject;
        //	if (joPost != null) {
        //		postData = KeyValue2Param(joPost);
        //	}
        //}
        WWWForm wf = null;
        var luaTD = lua.Type(4);
        var joPost = lua.ToJsonObj(4) as TinyJSON.ProxyObject;
        if (joPost != null) {
            wf = KeyValue2Form(joPost);
        }
        // "key:value\nkey:value"
        Dictionary<string, string> headers = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(strHeader)) {
            string[] segs = strHeader.Split('\n');
            foreach (string seg in segs) {
                string[] kv = seg.Split(':');
                if (kv.Length == 2) {
                    headers.Add(kv[0].Trim(), kv[1].Trim());
                }
            }
        }

        var httpHandler = NetworkMgr.Instance.GetHttpHandler("HTTP");
        if (httpHandler) {
            httpHandler.StartPost(tag, url, param, wf, headers, timeout);
            Debug.LogFormat("HttpPost()--xxx-- :WWW Post: tag:{0}--xx--param:{1}--xx--KeyValue2Param:{2}---xx--wf:{3}", url + "?" + tag,param, KeyValue2Param(joPost), wf.ToString());
        }
        return 0;
	}


	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int HttpUpPhoto(ILuaState lua) {
        string tag = lua.ChkString(1);
        string url = lua.ChkString(2);
        string strHeader = lua.ChkString(6);
        float timeout = (float)lua.OptNumber(7, 10);

        string param = null;
        var luaT = lua.Type(3);
        if (luaT == LuaTypes.LUA_TSTRING) {
            param = lua.ToString(3);
        } else {
            var joParam = lua.ToJsonObj(3) as TinyJSON.ProxyObject;
            if (joParam != null) {
                param = KeyValue2Param(joParam);
            }
        }
        WWWForm wf = null;
        var joPost = lua.ToJsonObj(4) as TinyJSON.ProxyObject;
        if (joPost != null) {
            wf = KeyValue2Form(joPost);
        }
        var obj = lua.ToAnyObject(5);
        Texture2D texture = obj as Texture2D;
        wf.AddBinaryData("photo", texture.EncodeToPNG());

        //var joPhoto = lua.ToJsonObj(5) as TinyJSON.ProxyObject;
        //if (joPhoto != null) {
        //    wf = KeyPhoto2Form(joPhoto, wf);
        //}

        // "key:value\nkey:value"
        Dictionary<string, string> headers = new Dictionary<string, string>();
        if (!string.IsNullOrEmpty(strHeader)) {
            string[] segs = strHeader.Split('\n');
            foreach (string seg in segs) {
                string[] kv = seg.Split(':');
                if (kv.Length == 2) {
                    headers.Add(kv[0].Trim(), kv[1].Trim());
                }
            }
        }

        var httpHandler = NetworkMgr.Instance.GetHttpHandler("HTTP");
        if (httpHandler) {
            httpHandler.StartPost(tag, url, param, wf, headers, timeout);
            NetworkMgr.Log("WWW Post: {0}\n{1}", url + "?" + param, KeyValue2Param(joPost));
        }
        return 0;
    }

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int HttpUpMorePhoto(ILuaState lua)
    {
        string tag = lua.ChkString(1);
        string url = lua.ChkString(2);
        float timeout = (float)lua.OptNumber(10, 10);

        WWWForm wf = null;
        var joPost = lua.ToJsonObj(3) as TinyJSON.ProxyObject;
        if (joPost != null) {
            wf = KeyValue2Form(joPost);
        }
        //for (int i = 0; i < 6; i++) {
        //    var obj = lua.ToAnyObject(4 + i);
        //    if (obj.Equals(null)) {

        //        continue;
        //    }
        //    Texture2D texture = obj as Texture2D;
        //    if (texture.Equals(null)) {
        //        continue;
        //    }
        //    wf.AddBinaryData("photo", texture.EncodeToPNG());
        //}

        // "key:value\nkey:value"
        Dictionary<string, string> headers = new Dictionary<string, string>();

        var httpHandler = NetworkMgr.Instance.GetHttpHandler("HTTP");
        if (httpHandler) {
            httpHandler.StartPost(tag, url, "", wf, headers, timeout);
            NetworkMgr.Log("WWW Post: {0}\n{1}", url, KeyValue2Param(joPost));
        }
        return 0;
    }
    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="lua"></param>
    /// <returns></returns>
	[MonoPInvokeCallback(typeof(LuaCSFunction))]
    static int HttpDownload(ILuaState lua)
	{
		string url = lua.ChkString(1);
		int range = lua.ChkInteger(2);
		string path = lua.ChkString(3);
		float timeout = (float)lua.OptNumber(4, 10);

        var httpHandler = NetworkMgr.Instance.GetHttpHandler("HTTP-DL");
        if (httpHandler) httpHandler.StartDownload(url, range, path, timeout);

		return 0;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int SetParam(ILuaState lua)
	{
        ExceptionReporter.Instance.SetParam(lua.ToLuaString(1), lua.ToLuaString(2));
		return 0;
	}

}

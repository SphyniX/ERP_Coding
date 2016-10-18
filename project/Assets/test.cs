using UnityEngine;
using System.Collections.Generic;
using ZFrame.NetEngine;
using LuaInterface;
using NameFuncPair = LuaMethod;
using ILuaState = System.IntPtr;
using System.IO;
using System.Collections;
using System;

public class test : MonoBehaviour {

	// Use this for DSB
	void Start () {
        StartCoroutine(coro());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator coro() {

        Texture2D tx = new Texture2D(100, 100);
        byte[] bt = tx.EncodeToPNG();

        WWWForm wf = new WWWForm();
        wf.AddField("user_phone", "19912345678");
        wf.AddField("user_password", "1");
        wf.AddField("user_name", "1");
        wf.AddField("user_age", "1");
        wf.AddField("user_sex", "1");
        wf.AddField("user_height", "1");
        wf.AddField("user_weight", "1");
        wf.AddField("user_wechat", "1");
        wf.AddField("user_qq", "1");
        wf.AddField("user_email", "1");
        wf.AddField("user_idnumber", "1");
        wf.AddField("user_cardNo", "1");
        wf.AddField("user_city", "1");
        wf.AddField("supname", "2");
        wf.AddBinaryData("1", bt);
        wf.AddBinaryData("2", bt);
        wf.AddBinaryData("3", bt);
        wf.AddBinaryData("4", bt);
        wf.AddBinaryData("5", bt);
        wf.AddBinaryData("6", bt);
        //wf.AddBinaryData("7", bt);
        //wf.AddBinaryData("8", bt);

        using (WWW www = new WWW("http://api.richer.net.cn:8080/api/user/insert", wf)) {
            while (!www.isDone) {
                yield return null;
            }
            LogMgr.D("{0}", www.text);
        }
        using (WWW www = new WWW("http://139.196.109.3:8080/api/user/insert", wf)) {
            while (!www.isDone) {
                yield return null;
            }
            LogMgr.D("{0}", www.text);
        }
        
        WWWForm wf1 = new WWWForm();
        wf1.AddField("phone", "13012345678");
        wf1.AddField("password", "123456");

        using (WWW www = new WWW("http://api.richer.net.cn:8080/api/user/login", wf1)) {
            while (!www.isDone) {
                yield return null;
            }
            LogMgr.D("{0}", www.text);
        }

        WWWForm wf2 = new WWWForm();
        wf2.AddField("UserID", "1");
        wf2.AddField("Typeid", "1");
        wf2.AddBinaryData("1", bt);

        using (WWW www = new WWW("http://api.richer.net.cn:8080/api/photo/uploadphoto", wf2)) {
            while (!www.isDone) {
                yield return null;
            }
            LogMgr.D("{0}", www.text);
        }
    }
}

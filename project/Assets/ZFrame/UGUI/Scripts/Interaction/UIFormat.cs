/**
 * 文  件  名：UIFormat 
 * 作       者：浪浪
 * 生成日期：16-08-17 21:24:56"
 * 功       能：
 * 修改日志：
 * 说明：
**/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ZFrame.UGUI;
[System.Serializable]
public struct Format {
    public Behaviour name;
    public Color clr;
    public Sprite sp;
    public string tx;
}
public class UIFormat : MonoBehaviour {

    public List<Format> lisFormat = new List<Format>();
	// Use this for DSB
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using clientlib.net;
using LuaInterface;
using ILuaState = System.IntPtr;

namespace ZFrame.NetEngine
{
    public class NetworkMgr : MonoSingleton<NetworkMgr>
    {
        [SerializeField]
        private string luaScript = null;
        // TCP
        [SerializeField]
        private string onInit = null;
        // HTTP
        [SerializeField]
        private string onHttpRes = null, onHttpDownload = null;
                
        public static NetworkMgr Inst { get { return Instance; } }

        private LuaTable m_Tb;
		private ILuaState m_Lua { get { return LuaScriptMgr.Instance.L; } }

        [NoToLua]
        public static void Log(string fmt, params object[] Args)
        {
            LogMgr.I("[NW] " + fmt, Args);
        }

		private void Start()
		{
            var L = LuaScriptMgr.Instance.L;
            int n = L.DoFile(luaScript);
            Assert.IsTrue(n == 1);

            m_Tb = L.ToLuaTable(-1);
            L.Pop(1);
            Assert.IsNotNull(m_Tb);
            
            m_Tb.CallFunc(onInit, 0, this);
		}
        
        private void OnHttpResponse(string tag, string resp, bool isDone, string error)
        {
            m_Tb.CallFunc(onHttpRes, 0, tag, resp, isDone, error);
        }

        private void OnHttpDownload(string url, uint current, uint total, object error)
        {
            m_Tb.CallFunc(onHttpDownload, 0, url, current, total, error);
        }
        public TcpClientHandler GetTcpHandler(string tcpName)
        {
            TcpClientHandler tcpHandler = null;
            var trans = cachedTransform.Find(tcpName);
            if (!trans) {
                var go = GoTools.AddChild(gameObject);
                go.name = tcpName;
                tcpHandler = go.AddComponent<TcpClientHandler>();
            } else {
                tcpHandler = trans.GetComponent<TcpClientHandler>();
            }
            return tcpHandler;
        }

        public HttpHandler GetHttpHandler(string httpName)
        {
            HttpHandler httpHandler = null;
            var trans = cachedTransform.Find(httpName);
            if (!trans) {
                var go = GoTools.AddChild(gameObject);
                go.name = httpName;
                httpHandler = go.AddComponent<HttpHandler>();
                httpHandler.onHttpResp = OnHttpResponse;
                httpHandler.onHttpDL = OnHttpDownload;
            } else {
                httpHandler = trans.GetComponent<HttpHandler>();
            }
            return httpHandler;
        }
    }
}

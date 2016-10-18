using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace ZFrame.NetEngine
{
    public delegate void DelegateHttpResponse(string tag, string resp, bool isDone, string error);
    public delegate void DelegateHttpDownload(string url, uint current, uint total, object error);
    public class HttpHandler : MonoBehaviour
    {
        public DelegateHttpResponse onHttpResp;
        public DelegateHttpDownload onHttpDL;

        private void HandleHttpResp(WWW www, string tag)
        {
            if (onHttpResp != null) {
                var isDone = www.isDone;
                if (isDone) {
                    onHttpResp.Invoke(tag, www.text, isDone, www.error);
                } else {
                    onHttpResp.Invoke(tag, www.url, isDone, www.error);
                }
            }
        }

        private IEnumerator CoroHttpGet(string tag, string uri, string param, float timeout)
        {
            float time = Time.realtimeSinceStartup + timeout;
            if (!string.IsNullOrEmpty(param)) {
                uri = uri + "?" + param;
            }

            NetworkMgr.Log("WWW Get: {0}", uri);
            using (WWW www = new WWW(uri)) {
                while (!www.isDone) {
                    if (time < Time.realtimeSinceStartup) {
                        break;
                    }
                    yield return null;
                }
                HandleHttpResp(www, tag);
            }
        }
        //Post byte
        private IEnumerator CoroHttpPost(string tag, string uri, string param, byte[] postData, Dictionary<string, string> headers, float timeout)
        {
            float time = Time.realtimeSinceStartup + timeout;
            if (!string.IsNullOrEmpty(param)) {
                uri = uri + "?" + param;
            }
            NetworkMgr.Log("WWW Post: {0}\n{1}", uri, System.Text.Encoding.UTF8.GetString(postData));
            using (WWW www = new WWW(uri, postData, headers)) {
                while (!www.isDone) {
                    if (time < Time.realtimeSinceStartup) {
                        break;
                    }
                    yield return null;
                }

                HandleHttpResp(www, tag);
            }
        }
        //Post Form
        private IEnumerator CoroHttpPost(string tag, string uri, string param, WWWForm wf, Dictionary<string, string> headers, float timeout)
        {
            float time = Time.realtimeSinceStartup + timeout;
            if (!string.IsNullOrEmpty(param)) {
                uri = uri + "?" + param;
            }
            NetworkMgr.Log("WWW Post: {0}\n{1}", uri, wf.ToString());
            using (WWW www = new WWW(uri, wf)) {
                while (!www.isDone) {
                    if (time < Time.realtimeSinceStartup) {
                        break;
                    }
                    yield return null;
                }
                LogMgr.D("{0}", www.text);
                HandleHttpResp(www, tag);
            }
        }

        private IEnumerator CoroHttpDownload(string url, int range, string savePath, float timeout)
        {
            float time = Time.realtimeSinceStartup + timeout;
            float progress = 0;
            var httpReq = HttpRequester.Download(url, range, savePath);
            for (;;) {
                yield return null;
                float realtimeSinceStartup = Time.realtimeSinceStartup;
                if (httpReq.progress == progress) {
                    if (time < realtimeSinceStartup) {
                        httpReq.error = (int)HttpStatusCode.RequestTimeout;
                        break;
                    }
                    continue;
                } else {
                    time = realtimeSinceStartup + timeout;
                }

                if (onHttpDL != null) {
                    onHttpDL.Invoke(url, (uint)httpReq.current, (uint)httpReq.total, httpReq.error);
                }
                if (httpReq.isDone || httpReq.error != null) break;
                progress = httpReq.progress;
            }
        }

        public void StartGet(string tag, string url, string param, float timeout)
        {
            StartCoroutine(CoroHttpGet(tag, url, param, timeout));
        }

        // startpost byte
        public void StartPost(string tag, string url, string param,byte[] postData, Dictionary<string, string> headers, float timeout)
        {
            StartCoroutine(CoroHttpPost(tag, url, param, postData, headers, timeout));
        }

        //startpost form
        public void StartPost(string tag, string url, string param, WWWForm wf, Dictionary<string, string> headers, float timeout)
        {
            StartCoroutine(CoroHttpPost(tag, url, param, wf, headers, timeout));
        }

        public void StartDownload(string url, int range, string savePath, float timeout)
        {
            if (System.IO.File.Exists(savePath)) {
                var file = new System.IO.FileInfo(savePath);
                range = (int)file.Length;
            }
            StartCoroutine(CoroHttpDownload(url, range, savePath, timeout));
        }
    }
}

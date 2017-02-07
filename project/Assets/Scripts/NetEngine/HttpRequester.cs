using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using System.IO;

namespace ZFrame.NetEngine
{
    public class HttpRequester
    {

        static int BYTE_LEN = 1024*5;

        public delegate void ProcessDelegate(HttpRequester httpReq, long current, long total);
        public delegate void RespDelegate(HttpRequester httpReq, string resp, System.Exception e);

        public string reqUri;
        public string reqMethod;
        public string reqPara;
        public string rspFile;
        float mprog;
        public float progress { get { return mprog; } }
        public long current { get; private set; }
        public long total { get; private set; }
        private bool m_IsDone;
        public bool isDone { get { return progress == 1f && m_IsDone; } }
        public object error;
        long storageSiz = 0;

        public HttpWebRequest wrq;
        public ProcessDelegate onProcess;
        public RespDelegate onResponse;

        public IAsyncResult result;
        public HttpRequester(string uri, string param, string method, string savePath = null, ProcessDelegate process = null, RespDelegate resp = null)
        {
            reqUri = uri;
            reqPara = param;
            reqMethod = method;
            rspFile = savePath;
            onProcess = process;
            onResponse = resp;
            storageSiz = 1024 * 1024 * 100;

            mprog = 0;
            current = 0;
            total = 1;
            m_IsDone = false;
            error = null;
        }

        public void Start()
        {
            LogMgr.I("HttpRequester.start（）----{0} {1}?{2}", reqMethod, reqUri, reqPara);
            switch (reqMethod)
            {
                case "GET":
                    wrq = (HttpWebRequest)WebRequest.Create(reqUri + "?" + reqPara);
                    break;
                case "POST":
                    {
                        wrq = (HttpWebRequest)WebRequest.Create(reqUri);
                        wrq.Method = "POST";
                        wrq.ContentType = "application/x-www-form-urlencoded";

                        if (reqPara != null)
                        {
                            byte[] SomeBytes = Encoding.UTF8.GetBytes(reqPara);
                            Stream newStream = wrq.GetRequestStream();
                            newStream.Write(SomeBytes, 0, SomeBytes.Length);
                            newStream.Close();
                            wrq.ContentLength = reqPara.Length;
                        }
                        else
                        {
                            wrq.ContentLength = 0;
                        }
                    }
                    break;
                case "GETF":
                    {
                        wrq = (HttpWebRequest)HttpWebRequest.Create(reqUri);
                        wrq.AddRange(System.Int32.Parse(reqPara));
                    }
                    break;
                default:
                    return;
            }
            result = wrq.BeginGetResponse(new AsyncCallback(f_processHttpResponseAsync), wrq);

        }

        public void Stop()
        {
            if (wrq != null)
            {
                wrq = null;
            }
        }

        /// <summary>
        /// 异步调用函数
        /// </summary>
        /// <param name="iar"></param>
        private void f_processHttpResponseAsync(IAsyncResult iar)
        {
            StringBuilder rsb = new StringBuilder();
            HttpWebRequest req = iar.AsyncState as HttpWebRequest;

            FileStream fStream = null;
            try
            {
                HttpWebResponse response = req.EndGetResponse(iar) as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                if (response.ContentLength / 1000 / 1000 > storageSiz)
                    throw new System.IO.IOException(string.Format("Disk Full ({0} / {1})", response.ContentLength / 1000 / 1000, storageSiz));
                total = response.ContentLength;
                if (reqMethod == "GETF")
                {
                    if (!Directory.Exists(Path.GetDirectoryName(rspFile)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(rspFile));
                    }
                    Debug.LogFormat("HttpRequester.f_processHttpResponseAsync--创建成功");
                    if (File.Exists(rspFile))
                    {
                        fStream = File.OpenWrite(rspFile);
                        fStream.Seek(0, SeekOrigin.End);
                    }
                    else
                    {
                        fStream = new System.IO.FileStream(rspFile, System.IO.FileMode.Create);
                        if (File.Exists(rspFile))
                        {
                            Debug.LogFormat("HttpRequester.f_processHttpResponseAsync()----文件不存在--创建文件---成功,path:{0}", rspFile);
                        }
                        else
                        {
                            Debug.LogFormat("HttpRequester.f_processHttpResponseAsync()----文件不存在--创建文件---失败,path:{0}", rspFile);
                        }
                    }
                    total += System.Int32.Parse(reqPara);
                }
                int forCount = 0;
                byte[] read = new byte[BYTE_LEN];
                for (;;)
                {
                    forCount++;
                    int count = responseStream.Read(read, 0, BYTE_LEN);
                    Debug.LogFormat("HttpRequester.f_processHttpResponseAsync()--XX--写入流{0}",count);
                    if (count > 0)
                    {
                        if (wrq == null)
                        {
                            throw new System.Net.WebException("Request Canceled", WebExceptionStatus.RequestCanceled);
                        }
                        if (fStream == null)
                        {

                            string str = System.Text.Encoding.UTF8.GetString(read);
                            rsb.Append(str);
                            current = rsb.Length;
                            if (onProcess != null) onProcess(this, rsb.Length, total);
                        }
                        else
                        {

                            fStream.Write(read, 0, count);
                            current = fStream.Length;
                            if (onProcess != null) onProcess(this, fStream.Length, total);
                        }
                        mprog = current * 1.0f / total;
                    }
                    else
                    {
                        break;
                    }
                }
                if (fStream.Length != total)
                {
                    throw new System.Net.WebException("Request Unfinished", WebExceptionStatus.RequestCanceled);
                }
                if (responseStream != null)
                {
                    responseStream.Dispose();
                }
                response.Close();
            }
            catch (WebException e)
            {
                HttpWebResponse resp = e.Response as HttpWebResponse;
                if (resp != null && resp.StatusCode == HttpStatusCode.RequestedRangeNotSatisfiable)  //HttpStatusCode.RequestedRangeNotSatisfiable:等效于 HTTP 416 状态。 RequestedRangeNotSatisfiable 指示从资源请求的数据范围不能返回，或者因为范围的开始处，然后该资源的开头或范围的末尾后在资源的结尾。
                {
                    rsb.Append(rspFile);
                    current = total;
                    mprog = 1;
                }
                else
                {//if (e.Status != WebExceptionStatus.RequestCanceled) {
                    error = (int)resp.StatusCode;        //NotFound	等效于 HTTP 状态 404。 NotFound 指示所请求的资源不存在的服务器上。
                    if (onResponse != null) onResponse(this, null, e);
                    return;
                }
            }
            catch (Exception e)
            {
                req.Abort();
                error = e.Message;
                if (onResponse != null) onResponse(this, null, e);
            }
            finally
            {
                // GetResponse Stop
                if (fStream != null)
                {
                    fStream.Close();
                }
            }

            // GetResponse Success
            if (fStream != null)
            {
                rsb.Append(fStream.Name);
                fStream.Close();
            }
            if (onResponse != null) onResponse(this, rsb.ToString(), null);

            m_IsDone = true;

#if false
		异步调用例子
			IAsyncResult result = request.BeginGetResponse(new AsyncCallback(f_processHttpResponseAsync), request);
		//处理超时请求
		ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, 
		                                       new WaitOrTimerCallback(f_asyncTimeout), request, 1000 * 60 * 10, true);
#endif
        }

        // HTTP通讯 异步
        static public HttpRequester SendRequest(string url, string para, string method, ProcessDelegate onProcess = null, RespDelegate onResponse = null)
        {
            HttpRequester reqInfo = new HttpRequester(url, para, method.ToUpper(), null, onProcess, onResponse);
            reqInfo.Start();
            return reqInfo;
        }

        // HTTP DOWNLOAD 异步
        static public HttpRequester Download(string url, int range, string savePath, ProcessDelegate onProcess = null, RespDelegate onResponse = null)
        {
            HttpRequester reqInfo = new HttpRequester(url, range.ToString(), "GETF", savePath, onProcess, onResponse);
            reqInfo.Start();
            return reqInfo;
        }
    }
}
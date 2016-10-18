using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace clientlib.net
{
    public delegate void NetCallback(NetSession session);
    /// <summary>
    /// Session
    /// </summary>
    public class NetSession
    {

#if UNITY_IOS && !UNITY_EDITOR
	    [DllImport("__Internal")]
#else
        [DllImport("SYBuffer")]
#endif
        private static extern byte readHead(byte[] buf,out int len);

        //#if UNITY_IOS && !UNITY_EDITOR
        //	    [DllImport("__Internal")]
        //#else
        //        [DllImport("SYBuffer")]
        //#endif
        private static int readLen(byte[] buf, byte ver) {
            int n = 0;
            n = buf[3];
            n = (n << 8) | buf[2];
            n = (n << 8) | buf[1];
            n = (n << 8) | buf[0];
            return n;
        }

        private const int READ_WAIT = 0;
        private const int READ_HEAD = 1;
        private const int READ_LEN = 2; 
        private const int READ_BODY = 3;

        //public byte ver;
        public int ver;
        public NetMsg msg;

        private TcpClient _tcp;
        private IoBuffer _headBuf;

        private NetCallback _connectFunc;
        private NetCallback _readFunc;


        private bool _isFree = false;
        private Exception _lastErr;
        private int _waitReadSize;
        private int _readSize;
        private int _nowAction;

        public System.Action<Exception> onException;

        public NetSession(){
            _headBuf = new IoBuffer(32);
        }

        /// <summary>
        /// 最新的错误信息
        /// </summary>
        public Exception LastErr
        {
            get { return _lastErr; }
        }

        public bool isConnected
        {
            get
            {
                return _tcp != null && _tcp.Connected;
            }
        }

        /// <summary>
        /// 连接到某个网络地址
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void Connect(String host, int port, NetCallback connectFunc, NetCallback readFunc)
        {
            if (_isFree) throw new Exception("netSession is free!");
            if (String.IsNullOrEmpty(host) || port < 1)
            {
                throw new Exception("error host or port!");
            }

            _connectFunc = connectFunc;
            _readFunc = readFunc;

            if (_tcp != null)
            {
                _tcp.Close();
                _tcp = null;
            }
            try {
                _tcp = new TcpClient();
                _tcp.BeginConnect(host, port, new AsyncCallback(TcpConnectCallback), this);
            } catch (Exception ex) {
                OnException(ex);
            }
            
        }

        public void BeginReadMsg()
        {
            if (_isFree) throw new Exception("netSession is free!");

            DoRead(null);
        }


        /// <summary>
        /// 连接完成处理
        /// </summary>
        /// <param name="ar"></param>
        private void TcpConnectCallback(IAsyncResult ar)
        {

            if (_isFree) return;
            try {
                _tcp.EndConnect(ar);
                _connectFunc(this);
            } catch (Exception ex) {
                OnException(ex);
            }
        }

        /// <summary>
        /// 有错误发生
        /// </summary>
        /// <param name="ex"></param>
        private void OnException(Exception ex)
        {
            _lastErr = ex;
            if (onException != null) onException.Invoke(ex);

            OnClosed();
        }
        
        /// <summary>
        /// 关闭连接
        /// </summary>
        private void OnClosed()
        {
            Free();

            //调用完成读取来通知外部，连接断开
            if (_readFunc != null)
            {
                _readFunc(this);
            }
        }

        private void DoRead(IAsyncResult ar)
        {
            if (_isFree) return;
            if (ar == null)
            {
                //初始化消息读取，从头开始读取
                _nowAction = READ_WAIT;
            }

            int len = 0;
            switch (_nowAction)
            {
                case READ_WAIT:
                    _nowAction = READ_HEAD;

                    //读取2个字节的版本信息
                    //AsyncRead(_headBuf, 0, 2);
                    //读取3个字节的版本信息
                    AsyncRead(_headBuf, 0, 8);
                    break;
                case READ_HEAD:
                    //解析版本信息，并读取消息长度
                    //ver = readHead(_headBuf.array,out len);
                    //if (ver == 0)
                    //{
                    //    OnException(new Exception("error msg ver!"));
                    //    OnClosed();
                    //    return;
                    //}
                    _nowAction = READ_LEN;
                    //AsyncRead(_headBuf, 0,len);
                    AsyncRead(_headBuf, 0, 4);
                    break;
                case READ_LEN:
                    len = readLen(_headBuf.array, 0x03);
                    //创建消息
                    msg = NetMsg.createReadMsg(len);
                    _nowAction = READ_BODY;
                    AsyncRead(msg.buffer, 0, len);
                    break;
                case READ_BODY:
                    //完成消息读取
                    _readFunc(this);
                    //开始下一个消息读取
                    DoRead(null);
                    break;
            }
        }
        
        
        /// <summary>
        /// 读取指定字节数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="pos"></param>
        /// <param name="size"></param>
        private void AsyncRead(IoBuffer buffer, int pos, int size)
        {
            if (_isFree) return;
            _readSize = pos;
            _waitReadSize = size;

            try
            {
                NetworkStream stream = _tcp.GetStream();
                stream.BeginRead(buffer.array, _readSize, _waitReadSize - _readSize, new AsyncCallback(DoAsyncRead), buffer);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        private void DoAsyncRead(IAsyncResult ar)
        {
            try
            {
                IoBuffer buffer = (IoBuffer)ar.AsyncState;
                NetworkStream stream = _tcp.GetStream();
                int len = stream.EndRead(ar);
                if (len < 1)
                {
                    //网络断开
                    OnException(new Exception("disconnected!"));
                    return;
                }
                _readSize += len;

                if (_readSize < _waitReadSize)
                {
                    //不足，需要继续读取
                    stream.BeginRead(buffer.array, _readSize, _waitReadSize - _readSize, new AsyncCallback(DoAsyncRead), buffer);
                    return;
                }

                //循环读取
                DoRead(ar);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }


        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool send(INetMsg message)
        {
            if (!isConnected)
            {
                return false;
            }
            NetMsg msg = (NetMsg)message;
            if (msg == null) return false;

            //序列化
            msg.serialization();

            NetworkStream stream = _tcp.GetStream();
            if (stream.CanWrite)
            {
                try
                {
                    msg.buffer.position = 0;
                    stream.BeginWrite(msg.buffer.array, msg.buffer.position, msg.buffer.limit - msg.buffer.position, new AsyncCallback(AsyncWrite), null);
                }
                catch (Exception ex)
                {
                    OnException(ex);
                    return false;
                }
            }

            return true;
        }

        private void AsyncWrite(IAsyncResult ar)
        {
            try
            {
                NetworkStream stream = _tcp.GetStream();
                stream.EndWrite(ar);
            }
            catch (System.Exception ex)
            {
                OnException(ex);
            }
        }

        public void Free()
        {
            if (_tcp != null)
            {
                _tcp.Close();
                _tcp = null;
            }
            onException = null;
            _isFree = true;
        }
    }
}

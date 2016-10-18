using System;
using System.Collections.Generic;

// warning CS0168: 声明了变量，但从未使用
// warning CS0219: 给变量赋值，但从未使用
#pragma warning disable 0168, 0219, 0414
namespace clientlib.net
{
    /// <summary>
    /// 网络客户端
    /// </summary>
    public class NetClient : INetClient
    {
        public delegate void ConnectedHnadler(NetClient client);
        public delegate void DisconnectedHnadler(NetClient client);


        public ConnectedHnadler onConnected;
        public DisconnectedHnadler onDisconnected;

        private NetSession _nowSession;

        private Queue<INetMsg> _receiveQueue;

        public System.Action<string> errLogger;
        string mErr;
        public string error { get { return mErr; } private set { mErr = value.Trim(); if (errLogger != null) errLogger(mErr); } }

        private void onError(System.Exception ex)
        {
            error = ex.Message;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiveQueue">接受消息队列</param>
        /// <param name="onConnected">连接上服务器端回调</param>
        /// <param name="onDisconnected">连接断开回调</param>
        public NetClient(Queue<INetMsg> receiveQueue, ConnectedHnadler onConnected, DisconnectedHnadler onDisconnected)
        {
            _receiveQueue = receiveQueue;
            this.onDisconnected = onDisconnected;
            this.onConnected = onConnected;
            this.errLogger = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiveQueue">接受消息队列</param>
        /// <param name="onConnected">连接上服务器端回调</param>
        /// <param name="onDisconnected">连接断开回调</param>
        public NetClient(Queue<INetMsg> receiveQueue, ConnectedHnadler onConnected, DisconnectedHnadler onDisconnected, System.Action<string> logger)
        {
            _receiveQueue = receiveQueue;
            this.onDisconnected = onDisconnected;
            this.onConnected = onConnected;
            this.errLogger = logger;
        }

        /// <summary>
        /// 是否连接上服务器
        /// </summary>
        public bool Connected
        {

            get
            {
                return _nowSession != null && _nowSession.isConnected;
            }
        }

        /// <summary>
        /// 连接到某个网络地址
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void Connect(String host, int port)
        {
            if (String.IsNullOrEmpty(host) || port < 1) return;
            mErr = null;
            if (_nowSession != null)
            {
                _nowSession.Free();
            }

            _nowSession = new NetSession();
            _nowSession.onException = onError; 
            _nowSession.Connect(host, port, OnConnected, OnReadMsg);
            
        }

        private void OnConnected(NetSession session)
        {
            if (session != _nowSession)
            {
                session.Free();
                return;
            }

            if (session.isConnected)
            {
                //开始读取数据
                session.BeginReadMsg();
                if (onConnected != null) onConnected(this);
            }
        }

        private void OnReadMsg(NetSession session)
        {
            if (session != _nowSession)
            {
                session.Free();
                return;
            }
            //判断session状态，只有保持连接状态才能进行消息读取，否则是连接断开或者发生错误
            if (!session.isConnected)
            {
                //连接断开，需要判断错误信息
                if (session.LastErr != null)
                {
                    //错误信息
                    error = session.LastErr.ToString();
                }

                if (onDisconnected != null)
                {
                    onDisconnected(this);
                }
                return;
            }

            //获得消息

            //反序列化
            session.msg.deserialization();

            _receiveQueue.Enqueue(session.msg);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            //关闭连接
            try
            {
                if (_nowSession != null)
                {
                    _nowSession.Free();
                }
                _nowSession = null;
            }
            finally
            {
            }

            if (onDisconnected != null)
            {
                onDisconnected(this);
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool send(INetMsg message)
        {
            if (_nowSession == null) return false;
            return _nowSession.send(message);
        }
    }
}

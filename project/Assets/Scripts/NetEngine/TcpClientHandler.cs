using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using clientlib.net;

namespace ZFrame.NetEngine
{
	public class TcpClientHandler : MonoBehaviour 
	{
		private NetClient m_NC;
		private Queue<INetMsg> m_Msgs = new Queue<INetMsg>();
		private Coroutine m_Coro;

        public bool IsConnected { get { return m_NC.Connected;  } }
        public string Error {  get { return m_NC.error; } }

        public bool autoRecieve;
        
		public UnityAction<TcpClientHandler, INetMsg> doRecieving;

        public UnityAction<TcpClientHandler> onConnected;
        public UnityAction<TcpClientHandler> onDisconnected;

        private string m_Name;
		private void Logger(string message)
		{
			LogMgr.W("{0}:{1}", m_Name, message);
		}

		private void Awake()
		{
			m_NC = new NetClient(m_Msgs, null, null, Logger);
            autoRecieve = true;
        }

        private void Start()
        {
            m_Name = this.name;
        }

        private void Update()
        {
            if (autoRecieve) {
                RecieveAll(doRecieving);
            }
        }
        
        private void OnDestroy()
        {
            m_NC.Close();
        }

        private void OnConnected()
		{
            if (onConnected != null) onConnected.Invoke(this);
		}

		private void OnDisconnected()
		{
            if (onDisconnected != null) onDisconnected.Invoke(this);
        }

		private IEnumerator CoroNetworkState(float timeout)
		{
			timeout += Time.realtimeSinceStartup;
			for (; ; ) {
				yield return null;

				if (timeout < Time.realtimeSinceStartup || m_NC.error != null) {
					m_NC.Close();
					OnDisconnected();
					yield break;
				}

				if (m_NC.Connected) {
					NetworkMgr.Log("Connected");
					// 清空前一次连接的消息
					m_Msgs.Clear();
					OnConnected();
					break;
				}
			}

			for (; ; ) {
				if (!m_NC.Connected) {
					OnDisconnected();
					break;
				}
				yield return null;
			}
		}
        
		public void Connect(string host, int port, float timeout)
		{
			if (m_NC != null) {
				NetworkMgr.Log("Connect -> {0}:{1}", host, port);
				if (m_Coro != null) StopCoroutine(m_Coro);
				m_NC.Connect(host, port);
				m_Coro = StartCoroutine(CoroNetworkState(timeout));
			}
		}

        [NoToLua]
        public void RecieveAll(UnityAction<TcpClientHandler, INetMsg> callback)
        {
            if (callback == null) callback = doRecieving;
            if (callback != null) {
                while (m_Msgs.Count > 0) {
                    callback.Invoke(this, m_Msgs.Dequeue());
                }
            }
        }

        public void Disconnect()
        {
            if (m_NC != null) {
                m_NC.Close();
            }
        }

		public void Send(INetMsg nm)
		{
			if (m_NC != null && m_NC.Connected) {
				m_NC.send(nm);
			}
		}        
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace clientlib.net
{
    /// <summary>
    /// 消息
    /// </summary>
    public class NetMsg : INetMsg
    {
        private const int HEAD_SIZE = 12;

        //#if UNITY_IOS && !UNITY_EDITOR
        //        	    [DllImport("__Internal")]
        //#else
        //        [DllImport("SYBuffer")]
        //#endif
        //        private static extern int putHead(byte[] buf, int len, int flag);
        private static int putHead(byte[] buf, int len, int flag)
        {
            int offset = 0;
            buf[offset] = 0x01;
            buf[offset + 1] = 0x00;
            buf[offset + 2] = 0x00;
            buf[offset + 3] = 0x00;
            buf[offset + 4] = 0x00;
            buf[offset + 5] = 0x00;
            buf[offset + 6] = 0x00;
            buf[offset + 7] = 0x00;
            buf[offset + 11] = (byte)(len >> 24);
            buf[offset + 10] = (byte)(len >> 16);
            buf[offset + 9] = (byte)(len >> 8);
            buf[offset + 8] = (byte)len;

            return 12;
        }



        private static int sendNum = 0;

        /// <summary>
        /// 接收消息初始化
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static NetMsg createReadMsg(int size)
        {
            NetMsg msg = new NetMsg();
            msg.msgSize = size;
            msg.initRead(size);
            return msg;
        }

        /// <summary>
        /// 创建发送消息
        /// </summary>
        /// <returns></returns>
        public static NetMsg createMsg(int type)
        {
            return createMsg(type, IoBuffer.BLOCK_SIZE);
        }

        /// <summary>
        /// 创建发送消息（自定义缓冲区大小）
        /// </summary>
        /// <returns></returns>
        public static NetMsg createMsg(int type, int size)
        {
            NetMsg msg = new NetMsg();
            msg.type = type;
            msg.initWrite(size);
            return msg;
        }

        /// <summary>
        /// 读取消息长度，临时变量
        /// </summary>
        public int msgSize;
        public int readSize { get { return msgSize; } }
        public short writeSize { get { return (short)_buffer.position; } }

        private int _type;
        private IoBuffer _buffer;


        /// <summary>
        /// 默认的消息，缓冲区长度1024
        /// </summary>
        private NetMsg()
        {
        }

        private void init(int size)
        {
            int n = size / IoBuffer.BLOCK_SIZE + ((size % IoBuffer.BLOCK_SIZE > 0) ? 1 : 0);
            _buffer = new IoBuffer(n * IoBuffer.BLOCK_SIZE);
        }

        private void initRead(int size)
        {
            init(size);
        }

        private void initWrite(int size)
        {
            if (_type < 1) throw new Exception("msg type mast > 0!");
            init(size);

            _buffer.position = HEAD_SIZE;
            //writeU64(_type);
            writeInt(_type);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void deserialization()
        {
            _buffer.flip();
            _type = readU32();
            //_type = readInt();
        }

        public void reset(int type = 0)
        {
            _buffer.position = 0;
            if (type > 0) {
                _type = type;
            }
            writeU32(_type);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void serialization()
        {
            _buffer.flip();

            int len = _buffer.limit - HEAD_SIZE;
            int offset = putHead(_buffer.array, len, sendNum++);
            if (offset > 0) {
                _buffer.position = offset;
            }
        }

        public int type {
            get {
                return _type;
            }
            set {
                _type = value;
            }
        }

        public byte read()
        {
            return _buffer.read();
        }

        public int readU32()
        {
            return _buffer.readU32();
        }

        public string readU64()
        {
            return _buffer.readU64().ToString();
        }

        public double readDouble()
        {
            return _buffer.readDouble();
        }

        public float readFloat()
        {
            return _buffer.readFloat();
        }

        public String readString()
        {
            return _buffer.readString();
        }
        public int readInt() {
            return _buffer.readInt();
        }

        public INetMsg write(byte value)
        {
            _buffer.write(value);
            return this;
        }

        public INetMsg writeU32(int value)
        {
            _buffer.writeU32(value);
            return this;
        }

        public INetMsg writeU64(long value)
        {
            _buffer.writeU64(value);
            return this;
        }

        public INetMsg writeU64(string value)
        {
            long numVal = long.Parse(value);
            _buffer.writeU64(numVal);
            return this;
        }

        public INetMsg writeString(String value)
        {
            _buffer.writeString(value);
            return this;
        }

        public INetMsg writeInt(int value) {
            _buffer.writeInt(value);
            return this;
        }

        public IoBuffer buffer
        {
            get{
                return _buffer;
            }
        }

        public int limit
        {
            get
            {
                return _buffer.limit;
            }
        }

        public int posession
        {

            get
            {
                return _buffer.position;
            }
        }
    }
}

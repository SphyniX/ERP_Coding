using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace clientlib.net
{
    public class IoBuffer
    {

        //#if UNITY_IOS && !UNITY_EDITOR
        //	    [DllImport("__Internal")]
        //#else
        //        [DllImport("SYBuffer")]
        //#endif
        //        private static extern void putU32(byte[] buf,out int pPos, int value);
        private static void putU32(byte[] buf, ref int pPos, int value) {
            byte h = 0x00;
            if (value < 0) {
                h = 0x40;
                value *= -1;
            }
            if (value < 0x00000040) {
                buf[pPos++] = (byte)((value & 0x3F) | h);
                return;
            }
            h |= 0x80;
            if (value < 0x00002000) {
                buf[pPos++] = (byte)((value & 0x3F) | h);
                buf[pPos++] = (byte)((value >> 6) & 0x7F);
            } else if (value < 0x00100000) {
                buf[pPos++] = (byte)((value & 0x3F) | h);
                buf[pPos++] = (byte)((value >> 6) | 0x80);
                buf[pPos++] = (byte)((value >> 13) & 0x7F);
            } else if (value < 0x08000000) {
                buf[pPos++] = (byte)((value & 0x3F) | h);
                buf[pPos++] = (byte)((value >> 6) | 0x80);
                buf[pPos++] = (byte)((value >> 13) | 0x80);
                buf[pPos++] = (byte)((value >> 20) & 0x7F);
            } else {
                buf[pPos++] = (byte)((value & 0x3F) | h);
                buf[pPos++] = (byte)((value >> 6) | 0x80);
                buf[pPos++] = (byte)((value >> 13) | 0x80);
                buf[pPos++] = (byte)((value >> 20) | 0x80);
                buf[pPos++] = (byte)((value >> 27) & 0x7F);
            }
        }


//#if UNITY_IOS && !UNITY_EDITOR
//	    [DllImport("__Internal")]
//#else
//        [DllImport("SYBuffer")]
//#endif
        private static void putU64(byte[] buf, ref int pPos, long value)
        {
            putU32(buf, ref pPos, (int)(value >> 32));
            putU32(buf, ref pPos, (int)value);
        }

        //#if UNITY_IOS && !UNITY_EDITOR
        //	    [DllImport("__Internal")]
        //#else
        //        [DllImport("SYBuffer")]
        //#endif
        private static int getU32(byte[] buf, ref int pPos) {

            int result = buf[pPos++];
            bool fat = 0 < (result & 0x00000040);
            if (0 < (result & 0x00000080)) {
                result = (result & 0x0000003f) | buf[pPos++] << 6;
                if (0 < (result & 0x00002000)) {
                    result = (result & 0x00001fff) | buf[pPos++] << 13;
                    if (0 < (result & 0x00100000)) {
                        result = (result & 0x000fffff) | buf[pPos++] << 20;
                        if (0 < (result & 0x08000000)) {
                            result = (result & 0x07ffffff) | buf[pPos++] << 27;
                        }
                    }
                }
            } else {
                result = result & 0x0000003f;
            }
            return fat ? -result : result;
        }


        //#if UNITY_IOS && !UNITY_EDITOR
        //	    [DllImport("__Internal")]
        //#else
        //        [DllImport("SYBuffer")]
        //#endif
        private static long getU64(byte[] buf, ref int pPos) {

            long value = getU32(buf, ref pPos);
            value = value << 32;
            value = value | (getU32(buf, ref pPos) & 0xFFFFFFFFL);
            return value;
        }


#if UNITY_IOS && !UNITY_EDITOR
	    [DllImport("__Internal")]
#else
        [DllImport("SYBuffer")]
#endif
        private static extern void Decrypt(byte[] buf, int pPos);
        

        public static void init(){

            Decrypt(null, 260769);
            Decrypt(new byte[1], 3);
        }
        
        public const int MAX_BUFFER_SIZE = 4096;
        /// <summary>
        /// 分块大小
        /// </summary>
        public const int BLOCK_SIZE = 1024;

        /// <summary>
        /// 缓冲区
        /// </summary>
        private byte[] _byteBuffer;

        /// <summary>
        /// 当前数组长度
        /// </summary>
        private int _length = 0;
        /// <summary>
        /// 缓冲器大小
        /// </summary>
        private int _size = 0;

        /// <summary>
        /// 当前指针位置
        /// </summary>
        private int _currentPosition = 0;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IoBuffer(int size)
        {
            this.Initialize(size);
        }

        public byte[] array
        {
            get
            {
                return _byteBuffer;
            }
        }

        /// <summary>
        /// 初始化ByteBuffer的每一个元素,并把当前指针指向头一位
        /// </summary>
        private void Initialize(int size)
        {
            _byteBuffer = new byte[size];
            _byteBuffer.Initialize();
            _length = size;
            _currentPosition = 0;
        }

        /// <summary>
        /// 清除position之前的内容
        /// </summary>
        public void compact()
        {
            if (_length<1 || _currentPosition>=_length)
            {
                _length = 0;
                _currentPosition = 0;
                return;
            }

            for (int i = _currentPosition, j = 0; i < _length;i++,j++ )
            {
                _byteBuffer[j] = _byteBuffer[i];
            }
            _length -= _currentPosition;
            _currentPosition = 0;
        }

        /// <summary>
        /// 完成写入
        /// </summary>
        public void flip()
        {
            if (_currentPosition < 1) return;
            _length = _currentPosition;
            _currentPosition = 0;
        }

        /// <summary>
        /// 操作上限
        /// </summary>
        public int limit
        {
            get
            {
                return _length;
            }
        }

        /// <summary>
        /// 操作位置
        /// </summary>
        public int position
        {
            get{
                return _currentPosition;
            }
            set
            {
                if (value <= _length)
                {
                    _currentPosition = value;
                }
            }
        }

        /// <summary>
        /// 剩余长度
        /// </summary>
        public int remaining
        {
            get
            {
                return _length-_currentPosition;
            }
        }

        /// <summary>
        /// 内容上限
        /// </summary>
        public int size
        {
            get
            {
                return _size;
            }
        }

        public void clear()
        {
            _currentPosition = 0;
        }

        /// <summary>
        /// 向ByteBuffer压入一个字节
        /// </summary>
        /// <param name="by">一位字节</param>
        public IoBuffer write(byte value)
        {
            _byteBuffer[_currentPosition++] = value;
            _length++;
            return this;
        }

        public IoBuffer write(int index, byte value)
        {
            _byteBuffer[index] = value;
            return this;
        }

        public IoBuffer write(byte[] buffer, int offset, int length)
        {
            for (int i = offset; i < length; i++)
            {
                write(buffer[i]);
            }
            return this;
        }

        public byte read()
        {
            if (_currentPosition >= limit)
            {
                throw new Exception("out of buffer!");
            }
            return _byteBuffer[_currentPosition++];
        }

        public byte read(int index)
        {

            return _byteBuffer[index];
        }

        public double readDouble()
        {
            if (limit - _currentPosition < 8)
            {
                throw new Exception("out of buffer!");
            }
            Array.Reverse(_byteBuffer, _currentPosition, 8);
            double value = BitConverter.ToDouble(_byteBuffer, _currentPosition);
            _currentPosition += 8;
            return value;
        }

        public float readFloat()
        {
            if (limit - _currentPosition < 4)
            {
                throw new Exception("out of buffer!");
            }
            Array.Reverse(_byteBuffer, _currentPosition, 4);
            float value = BitConverter.ToSingle(_byteBuffer, _currentPosition);
            _currentPosition += 4;
            return value;
        }

        public IoBuffer read(byte[] buffer, int offset, int length)
        {
            int len = length > 0 ? length : buffer.Length - offset;
            if ((len + _currentPosition) >= limit)
            {
                throw new Exception("out of buffer!");
            }
            int i = 0;
            for (; i < len; i++)
            {
                buffer[offset + i] = _byteBuffer[_currentPosition++];
            }
            return this;
        }

        public short readShort()
        {
            short result = (short)(read() << 8);
            result |= read();
            return result;
        }

        public IoBuffer writeShort(short value)
        {
            write((byte)(value >> 8)).write((byte)value);
            return this;
        }

        public IoBuffer writeShort(int pos, short value)
        {
            write(pos, (byte)(value >> 8)).write(pos + 1, (byte)(value));
            return this;
        }

        /// <summary>
        /// 非压缩方式的整型
        /// </summary>
        /// <returns></returns>
        public int readInt()
        {
            int result = read();
            result |= read() << 8;
            result |= read() << 16;
            result |= read() << 24;
            return result;
        }

        /// <summary>
        /// 写入非压缩整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IoBuffer writeInt(int value)
        {
            write((byte)(value)).write((byte)(value >> 8)).write((byte)(value >> 16)).write((byte)(value >> 24));
            return this;
        }

        public IoBuffer writeInt(int pos, int value)
        {
            write(pos, (byte)(value >> 24)).write(pos + 1, (byte)(value >> 16)).write(pos + 2, (byte)(value >> 8)).write(pos + 3, (byte)(value));
            return this;
        }

        /// <summary>
        /// 读取整型
        /// </summary>
        /// <returns></returns>
        public int readU32()
        {
            if (_currentPosition >= limit)
            {
                throw new Exception("out of buffer!");
            }
            int ret = getU32(_byteBuffer, ref _currentPosition);
            return ret;
        }

        /// <summary>
        /// 读取64位整型
        /// </summary>
        /// <returns></returns>
        public long readU64()
        {
            if (_currentPosition+1 >= limit)
            {
                throw new Exception("out of buffer!");
            }

            long ret = getU64(_byteBuffer, ref _currentPosition);
            return ret;
        }

        /// <summary>
        /// 读取U64为字符串
        /// </summary>
        /// <returns></returns>
        public String readU64Str()
        {
            long n = readU64();
            return n.ToString();
        }

        /// <summary>
        /// 将Long字符串写入
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IoBuffer writeU64(String value)
        {
            return writeU64(long.Parse(value));
        }

        /// <summary>
        /// 写入64位整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IoBuffer writeU64(long value)
        {
            putU64(_byteBuffer, ref _currentPosition, value);
            return this;
        }

        /// <summary>
        /// 写入整型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IoBuffer writeU32(int value)
        {
            putU32(_byteBuffer, ref _currentPosition, value);
            return this;
        }



        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IoBuffer writeString(String value)
        {
            byte[] bt = null;
            bt = Encoding.UTF8.GetBytes(value);
            writeU32(bt.Length);
            write(bt,0,bt.Length);
            return this;
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <returns></returns>
        public String readString()
        {
            int len = readU32();
            byte[] bt = new byte[len];
            read(bt, 0, len);
            return Encoding.UTF8.GetString(bt);
        }
    }
}

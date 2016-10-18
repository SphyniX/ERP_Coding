using System;
using System.Collections.Generic;
using System.Text;

namespace clientlib.net
{
    public interface INetMsg
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        int type { get;}
        byte read();
        int readU32();
        string readU64();
        float readFloat();
        double readDouble();
        string readString();
        INetMsg write(byte value);
        INetMsg writeU32(int value);
        INetMsg writeU64(long value);
        INetMsg writeU64(string value);
        INetMsg writeString(string value);
    }
}

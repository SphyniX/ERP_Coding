using System;
using System.Collections.Generic;
using System.Text;

namespace clientlib.utils
{
    /// <summary>
    /// Int32
    /// </summary>
    public class WInt32 : WIntBase
    {
        private const int BYTE_SIZE = 4;

        public WInt32()
            :this(0)
        {
        }
        public WInt32(int value)
            : base(BYTE_SIZE)
        {
            this.value = value;
        }

        public int value
        {
            get
            {
                return BitConverter.ToInt32(readValue(), 0);
            }
            set
            {
                byte[] byteV = BitConverter.GetBytes(value);
                writeValue(byteV);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace clientlib.utils
{
    /// <summary>
    /// Int16
    /// </summary>
    public class WInt16 :WIntBase
    {
        private const int BYTE_SIZE = 2;
        public WInt16()
            :this(0)
        {
        }

        public WInt16(short value)
            : base(BYTE_SIZE)
        {
            this.value = value;
        }

        public short value
        {
            get
            {
                return BitConverter.ToInt16(readValue(), 0);
            }
            set
            {
                byte[] byteV = BitConverter.GetBytes(value);
                writeValue(byteV);
            }
        }
    }
}

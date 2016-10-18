using System;
using System.Collections.Generic;
using System.Text;

namespace clientlib.utils
{
    /// <summary>
    /// Int64
    /// </summary>
    public class WInt64 : WIntBase
    {
        private const int BYTE_SIZE = 8;

        public WInt64()
            :this(0)
        {
        }

        public WInt64(string value)
            : this(Int64.Parse(value))
        {
        }

        public WInt64(long value)
            : base(BYTE_SIZE)
        {
            this.value = value;
        }

        public string strValue
        {
            get
            {
                return BitConverter.ToInt64(readValue(), 0).ToString();
            }
            set
            {
                byte[] byteV = BitConverter.GetBytes(Int64.Parse(value));
                writeValue(byteV);
            }
        }

        public long value
        {
            get
            {
                return BitConverter.ToInt64(readValue(), 0);
            }
            set
            {
                byte[] byteV = BitConverter.GetBytes(value);
                writeValue(byteV);
            }
        }
    }
}

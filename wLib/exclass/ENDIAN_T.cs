using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wLib
{
    public static class ENDIAN
    {
        public static byte[] Swap(byte[] src, int src_offset, int length)
        {
            byte[] temp = new byte[length];
            Array.Copy(src, src_offset, temp, 0, length);

            Array.Reverse(temp);

            return temp;
        }

        public static void Swap(ref byte[] src, int src_offset, int length)
        {
            byte[] temp = new byte[length];
            Array.Copy(src, src_offset, temp, 0, length);

            Array.Reverse(temp);

            Array.Copy(temp, 0, src, src_offset, length);
        }

        public static Int16 Swap(Int16 value)
        {
            byte[] temp = BitConverter.GetBytes(value);

            Array.Reverse(temp);

            return BitConverter.ToInt16(temp, 0);
        }

        public static UInt16 Swap(UInt16 value)
        {
            byte[] temp = BitConverter.GetBytes(value);

            Array.Reverse(temp);

            return BitConverter.ToUInt16(temp, 0);
        }

        public static Int32 Swap(Int32 value)
        {
            byte[] temp = BitConverter.GetBytes(value);

            Array.Reverse(temp);

            return BitConverter.ToInt32(temp, 0);
        }

        public static UInt32 Swap(UInt32 value)
        {
            byte[] temp = BitConverter.GetBytes(value);

            Array.Reverse(temp);

            return BitConverter.ToUInt32(temp, 0);
        }

        public static float Swap(float value)
        {
            byte[] temp = BitConverter.GetBytes(value);

            Array.Reverse(temp);

            return BitConverter.ToSingle(temp, 0);
        }
    }
}

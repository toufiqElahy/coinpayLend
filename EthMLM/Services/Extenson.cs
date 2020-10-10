using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthMLM.Services
{
    public static class Extenson
    {
        public static byte[] ToBytes(this ushort[] data)
        {
            var bytes = new byte[data.Length * 2];

            for (int i = 0; i < data.Length; i++)
            {
                var shortWord = data[i];

                bytes[i * 2] = (byte)((shortWord >> 8) & 0xFF);
                bytes[i * 2 + 1] = (byte)(shortWord & 0xFF);
            }

            return bytes;
        }

        public static byte[] ToSolidityBytes(this string self)
        {
            var bts = Encoding.ASCII.GetBytes(self);
            var sixe = bts.Length - 2 / 2;

            var bArray = new byte[sixe];

            uint b;
            uint b1;

            for (uint i = 2; i < bts.Length; i += 2)
            {

                var v = bts[i];
                b = Convert.ToUInt32(bts[i]);
                b1 = Convert.ToUInt32(bts[i + 1]);

                //left digit
                if (b >= 48 && b <= 57)
                    b -= 48;
                //A-F
                else if (b >= 65 && b <= 70)
                    b -= 55;
                //a-f
                else if (b >= 97 && b <= 102)
                    b -= 87;


                //right digit
                if (b1 >= 48 && b1 <= 57)
                    b1 -= 48;
                //A-F
                else if (b1 >= 65 && b1 <= 70)
                    b1 -= 55;
                //a-f
                else if (b1 >= 97 && b1 <= 102)
                    b1 -= 87;

                bArray[i / 2 - 1] = Convert.ToByte(16 * b + b1);
            }

            return bArray;
        }
    }
}

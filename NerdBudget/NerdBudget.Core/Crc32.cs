using System.Text;
using Augment;

namespace NerdBudget.Core
{
    public static class Crc32
    {
        static uint[] _table;

        static Crc32()
        {
            uint poly = 0xedb88320;

            _table = new uint[256];

            for (uint i = 0; i < _table.Length; ++i)
            {
                uint temp = i;

                for (int j = 8; j > 0; --j)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = (uint)((temp >> 1) ^ poly);
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }

                _table[i] = temp;
            }
        }

        public static string Hash(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);

            uint crc = 0xffffffff;

            for (int i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(((crc) & 0xff) ^ bytes[i]);

                crc = (uint)((crc >> 8) ^ _table[index]);
            }

            return "{0:x}".FormatArgs(~crc);
        }
    }
}
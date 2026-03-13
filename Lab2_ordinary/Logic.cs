using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab2_ordinary
{
    public class Logic
    {
        int[] register1;
        public static bool IsValidKey(string key)
        {
            if (key.Length != 36)
                return false;

            foreach (char c in key)
            {
                if (c != '0' && c != '1')
                    return false;
            }

            return true;
        }
        public static string BytesToBits(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;

            foreach (byte b in data)
            {
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
                sb.Append(" ");
            }

            return sb.ToString();
        }
        public (byte[], string) Encrypt(string key1, byte[] data)
        {
            register1 = new int[data.Length];
            register1 = ParseKey(key1);

            byte[] encrypted = new byte[data.Length];

            StringBuilder keyStream1 = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                byte keyByte = 0;

                for (int j = 0; j < 8; j++)
                {
                    int x = NextBit(register1);

                    keyStream1.Append(x);

                    keyByte = (byte)((keyByte << 1) | x);
                }

                encrypted[i] = (byte)(data[i] ^ keyByte);
            }

            return (encrypted, keyStream1.ToString());
        }
        private int[] ParseKey(string key)
        {
            int[] bits = new int[key.Length];

            for (int i = 0; i < key.Length; i++)
            {
                bits[i] = key[i] - '0';
            }

            return bits;
        }
        private int NextBit(int[] register)
        {
            int newBit = register[0] ^ register[register.Length - 11];
            int res = register[0];
            for (int i = 0; i < register.Length - 1; i++)
            {
                register[i] = register[i + 1];
            }
            register[register.Length - 1] = newBit;
            return res;
        }
    }
}

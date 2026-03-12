using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace lab2
{
    public class Logic
    {
        int[] register1, register2, register3;
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

                //count++;

                //if (count % 8 == 0)
                //    sb.AppendLine();
            }

            return sb.ToString();
        }
        public (byte[], string, string, string) Encrypt(string key1, string key2, string key3, byte[] data)
        {
            register1 = ParseKey(key1);
            register2 = ParseKey(key2);
            register3 = ParseKey(key3);

            byte[] encrypted = new byte[data.Length];

            StringBuilder keyStream1 = new StringBuilder();
            StringBuilder keyStream2 = new StringBuilder();
            StringBuilder keyStream3 = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                byte keyByte = 0;

                for (int j = 0; j < 8; j++)
                {
                    int x = NextBit(register1);
                    int y = NextBit(register2);
                    int z = NextBit(register3);

                    keyStream1.Append(x);
                    keyStream2.Append(y);
                    keyStream3.Append(z);

                    int geffe = (x & y) ^ ((1 - x) & z);

                    keyByte = (byte)((keyByte << 1) | geffe);
                }

                encrypted[i] = (byte)(data[i] ^ keyByte);
            }

            return (encrypted, keyStream1.ToString(), keyStream2.ToString(), keyStream3.ToString());
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

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class Encryptor
    {
        public static void Encrypt(string input, string output, BigInteger n, BigInteger b, Action<string> srcAction, Action<string> outputAction)
        {
            byte[] data = File.ReadAllBytes(input);

            using var writer = new BinaryWriter(File.Open(output, FileMode.Create));
            int count = 0;

            foreach (byte m in data)
            {
                BigInteger c = (m * (m + b)) % n;

                if (count < 1000)
                {
                    srcAction?.Invoke(c.ToString());
                    outputAction?.Invoke(m.ToString());
                    count++;
                }

                writer.Write((uint)c);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class Decryptor
    {
        StringBuilder srcBuffer = new StringBuilder();
        StringBuilder outBuffer = new StringBuilder();
        int counter = 0;
        public static void Decrypt(string input, string output, BigInteger p, BigInteger q, BigInteger b, Action<string> srcAction, Action<string> outputAction)
        {
            BigInteger n = p * q;
            BigInteger inv2 = UsefulAlgorithms.ModInverse(2, n);

            // yp*p + yq*q = 1
            UsefulAlgorithms.ExtendedGcd(p, q, out BigInteger yp, out BigInteger yq);
            
            using var reader = new BinaryReader(File.Open(input, FileMode.Open));
            using var writer = new BinaryWriter(File.Open(output, FileMode.Create));

            int count = 0;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                BigInteger c = reader.ReadUInt32();

                BigInteger D = (b * b + 4 * c) % n;

                BigInteger mp = UsefulAlgorithms.ModPow(D, (p + 1) / 4, p);
                BigInteger mq = UsefulAlgorithms.ModPow(D, (q + 1) / 4, q);

                BigInteger r1 = (yp * p * mq + yq * q * mp) % n;
                BigInteger r2 = (n - r1) % n;
                BigInteger r3 = (yp * p * mq - yq * q * mp) % n;
                BigInteger r4 = (n - r3) % n;

                BigInteger[] roots = { r1, r2, r3, r4 };

                byte mByte = 0;

                foreach (var r in roots)
                {
                    BigInteger t = (r - b) % n;
                    if (t < 0) t += n;

                    BigInteger m = (t * inv2) % n;

                    if (m >= 0 && m <= 255)
                    {
                        mByte = (byte)m;
                        if (count < 1000)
                        {
                            srcAction?.Invoke(c.ToString());
                            outputAction?.Invoke(m.ToString());
                            count++;
                        }
                        break;
                    }
                }

                writer.Write(mByte);
            }
        }
    }
}

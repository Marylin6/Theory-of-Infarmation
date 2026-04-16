using System.Numerics;

namespace lab3
{
    internal class UsefulAlgorithms
    {
        public static BigInteger ModPow(BigInteger baseVal, BigInteger exp, BigInteger mod)
        {
            BigInteger result = 1;
            baseVal %= mod;

            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    result = (result * baseVal) % mod;

                baseVal = (baseVal * baseVal) % mod;
                exp >>= 1;
            }

            return result;
        }

        public static BigInteger ExtendedGcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            BigInteger gcd = ExtendedGcd(b, a % b, out BigInteger x1, out BigInteger y1);

            x = y1;
            y = x1 - (a / b) * y1;

            return gcd;
        }
        public static BigInteger ModInverse(BigInteger a, BigInteger mod)
        {
            ExtendedGcd(a, mod, out BigInteger x, out _);
            return (x % mod + mod) % mod;
        }
    }
}

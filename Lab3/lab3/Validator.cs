using System.Numerics;
using System.Windows;


namespace lab3
{
    public static class Validator
    {
        public static bool Validate(BigInteger p, BigInteger q, BigInteger b)
        {
            if (p == q)
            {
                MessageBox.Show("p and q must be different");
                return false;
            }

            if (p % 4 != 3 || q % 4 != 3)
            {
                MessageBox.Show("p and q must be ≡ 3 (mod 4)");
                return false;
            }

            if (!IsPrime(p) || !IsPrime(q))
            {
                MessageBox.Show("p and q must be prime");
                return false;
            }

            BigInteger n = p * q;

            if (n < 256)
            {
                MessageBox.Show("p and q must be greater than 256");
                return false;
            }

            if (b >= n)
            {
                MessageBox.Show("b must be less than n");
                return false;
            }

            if (n >= uint.MaxValue)
            {
                MessageBox.Show("n must fit into 4 bytes");
                return false;
            }

            return true;
        }

        private static bool IsPrime(BigInteger n)
        {
            if (n < 2) return false;

            for (BigInteger i = 2; i * i <= n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }
    }
}


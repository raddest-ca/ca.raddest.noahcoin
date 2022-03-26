namespace NoahCoin.Extensions;

using System.Linq;
using System.Numerics;

public static class BigIntegerExtensions
{
    public static BigInteger Pow(this BigInteger a, int exp)
    {
        return BigInteger.Pow(a, exp);
    }

    public static BigInteger Mod(this BigInteger self, BigInteger mod)
    {
        var remainder = self % mod;
        return remainder < 0 ? remainder + mod : remainder;
    }

    public static BigInteger ParseHex(this string input)
    {
        return BigInteger.Parse(input, System.Globalization.NumberStyles.HexNumber);
    }

    public static BigInteger ModInverse(this BigInteger n, BigInteger p)
    {
        // https://asecuritysite.com/ecc/ecc_add
        if (n < 0) return p - ModInverse(-n, p);

        var result = BigIntegerHelpers.ExtendedEuclideanAlgorithm(n, p);
        return result.X.Mod(p);
    }

    public static byte[] ToByteArray(this BigInteger self, int length)
    {
        if (self < 0) throw new ArgumentException(nameof(self), "Must be positive");
        // ditch sign bit, flip bit order
        var x = self.ToByteArray(isBigEndian: true, isUnsigned: true);
        var zerosToAdd = length - x.Length;
        var rtn = new byte[zerosToAdd].Concat(x).ToArray();
        return rtn;
    }
}

public static class BigIntegerHelpers
{
    public static (BigInteger GCD, BigInteger X, BigInteger Y) ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
    {
        BigInteger old_r = a, r = b;
        BigInteger old_s = 1, s = 0;
        BigInteger old_t = 0, t = 1;
        while (r != 0)
        {
            var quotient = BigInteger.Divide(old_r, r);
            (old_r, r) = (r, old_r - quotient * r);
            (old_s, s) = (s, old_s - quotient * s);
            (old_t, t) = (t, old_t - quotient * t);
        }
        return (old_r, old_s, old_t);
    }
}
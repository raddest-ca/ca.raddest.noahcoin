namespace NoahCoinService.Extensions;
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
        if (n < 0) return p - ModInverse(-n, p);
        var result = BigIntegerHelpers.ExtendedEuclideanAlgorithm(n, p);
        return result.X.Mod(p);
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
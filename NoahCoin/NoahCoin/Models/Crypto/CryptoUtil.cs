namespace NoahCoin.Models.Crypto;

public static class CryptoUtil
{
    public static BigInteger GetSecureRandomNumber(BigInteger maxExclusive)
    {
        var rng = RandomNumberGenerator.Create();
        var bitsToGenerate = (int) Math.Ceiling(BigInteger.Log(maxExclusive, 2));
        var data = new byte[bitsToGenerate];
        rng.GetBytes(data);
        return new BigInteger(data, isUnsigned: true, isBigEndian: true).Mod(maxExclusive);
    }
}
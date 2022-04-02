namespace NoahCoin.Models.Crypto;

public record PrivateKey
{
    public Generator Generator { get; init; }
    public BigInteger Value { get; init; }

    public bool IsValid
    {
        get { return Value > 0 && Value < Generator.Order; }
    }

    public PrivateKey()
    {
        Generator = Generator.Default;
        Value = CryptoUtil.GetSecureRandomNumber(Generator.Order);
    }

    public PrivateKey(
        BigInteger Value
    )
    {
        this.Value = Value;
        Generator = Generator.Default;
    }

    public PrivateKey(
        string phrase
    )
    {
        Generator = Generator.Default;
        Value = IHashable.GetHash(phrase).IntegerValue;
    }

    public PublicKey GetPublicKey()
    {
        return Generator.GeneratePublicKey(this);
    }

    /**
     * https://en.bitcoin.it/wiki/Elliptic_Curve_Digital_Signature_Algorithm#Signing_Algorithm
     */
    public Signature GetSignature(
        Hash messageHash
    )
    {
        var z = messageHash.IntegerValue;
        while (true)
        {
            var n = Generator.Order;
            var k = CryptoUtil.GetSecureRandomNumber(n - 1) + 2;
            var xy = k * Generator.Point;
            var r = xy.X.Mod(n);
            if (r == 0) continue;
            var s = (k.ModInverse(n) * (z + r * Value)).Mod(n);
            if (s == 0) continue;
            return new Signature(new Point(Generator.Point.Curve, r, s));
        }
    }
}
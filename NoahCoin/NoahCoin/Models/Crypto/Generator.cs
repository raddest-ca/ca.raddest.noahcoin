namespace NoahCoin.Models.Crypto;

public record Generator
{
    public Point Point { get; init; }
    public BigInteger Order { get; init; }
    public static readonly Generator Default = new Generator()
    {
        Point = new Point(
            Curve.bitcoin_curve,
            "079BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798".ParseHex(),
            "0483ada7726a3c4655da4fbfc0e1108a8fd17b448a68554199c47d08ffb10d4b8".ParseHex()
        ),
        Order = "0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141".ParseHex()
    };

    public PublicKey GeneratePublicKey(PrivateKey pk)
    {
        return new PublicKey()
        {
            Generator = this,
            Point = pk.Value * Point
        };
    }
};
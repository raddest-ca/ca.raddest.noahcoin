namespace NoahCoin.Models.Crypto;

public record Signature : IHashable
{
    public Signature()
    {
    }


    public Signature(
        Point value
    )
    {
        Value = value;
    }


    public Point Value { get; init; }

    public BigInteger R => Value.X;
    public BigInteger S => Value.Y;

    public Hash GetHash()
    {
        return Value.GetHash();
    }

    public string Encode()
    {
        return Value.Encode();
    }

    public static Signature Decode(string encoded)
    {
        return new Signature(Point.Decode(encoded));
    }

    public bool IsValid(
        PublicKey publicKey,
        IHashable message
    )
    {
        if (Value.Curve != publicKey.Generator.Point.Curve) return false;
        var n = publicKey.Generator.Order;
        var z = message.GetHash().IntegerValue;
        var G = publicKey.Generator.Point;
        if (R < 1 && R > n - 1) return false;
        if (S < 1 && S > n - 1) return false;
        var s1 = S.ModInverse(n);
        var RPrime = z * s1 * G + R * s1 * publicKey.Point;
        if (RPrime.IsInfinity()) return false;
        var rPrime = RPrime.X;
        return rPrime == R;
    }
}
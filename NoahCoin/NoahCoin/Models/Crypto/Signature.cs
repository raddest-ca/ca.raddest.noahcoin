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

    public Point Value {get; init;}
    
    public BigInteger R => Value.X;
    public BigInteger S => Value.Y;
    
    public Hash GetHash()
    {
        return Value.GetHash();
    }

    public bool IsValid(
        PublicKey publicKey,
        IHashable message
    )
    {
        if (Value.Curve != publicKey.Generator.Point.Curve) return false;
        var n = publicKey.Generator.Order;
        var z = message.GetHash().IntegerValue;
        if (R <= 1 && R >= n - 1) return false;
        if (S <= 1 && S >= n - 1) return false;
        var u1 = (z * S.ModInverse(n)).ModInverse(n);
        var u2 = (R * S.ModInverse(n)).ModInverse(n);
        var xy = u1 * publicKey.Generator.Point + u2 * publicKey.Point;
        if (xy.IsInfinity()) return false;
        return R == xy.X.Mod(n);
    }
}
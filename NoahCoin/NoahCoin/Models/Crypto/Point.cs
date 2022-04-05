namespace NoahCoin.Models.Crypto;

public record Point : IHashable
{
    public BigInteger X { get; init; }
    public BigInteger Y { get; init; }
    public Curve Curve { get; init; } = Curve.bitcoin_curve;

    public Point(){}
    public Point (Curve Curve, BigInteger X, BigInteger Y)
    {
        this.Curve = Curve;
        this.X = X;
        this.Y = Y;
    }

    public static Point Decode(
        string encoded
    )
    {
        var chunks = encoded.Split(".");
        var x = new BigInteger(
            Convert.FromHexString(chunks[0]),
            isUnsigned: true,
            isBigEndian: true
        );
        var y = new BigInteger(
            Convert.FromHexString(chunks[1]),
            isUnsigned: true,
            isBigEndian: true
        );

        return new Point
        {
            X = x,
            Y = y
        };
    }

    public string Encode()
    {
        return X.ToByteArray(32).ToHexString()
               + "."
               + Y.ToByteArray(32).ToHexString();
    }

    public bool IsMember()
    {
        return (Y.Pow(2) - X.Pow(3) - 7).Mod(Curve.p) == 0;
    }

    public static Point Infinity = new()
    {
        Curve = new Curve(0, 0, 0),
        X = BigInteger.Zero,
        Y = BigInteger.Zero,
    };

    public bool IsInfinity() => Object.ReferenceEquals(this, Infinity);

    public static Point operator +(Point a) => a;
    public static Point operator +(Point P, Point Q)
    {
        // https://www.youtube.com/watch?v=NTztut15D2E
        // https://crypto.stackexchange.com/a/66296
        if (P.IsInfinity()) return Q;
        if (Q.IsInfinity()) return P;
        if (P.X == Q.X && P.Y == -Q.Y) return Infinity;

        BigInteger x1 = P.X,
        x2 = Q.X,
        y1 = P.Y,
        y2 = Q.Y,
        p = P.Curve.p,
        a = P.Curve.a,
        x3, y3;

        BigInteger slope;
        if (P != Q)
            slope = (y2 - y1) * (x2 - x1).ModInverse(p);
        else
            slope = (3 * x1.Pow(2) + a) * (2 * y1).ModInverse(p);
        x3 = (slope.Pow(2) - x1 - x2).Mod(p);
        y3 = (slope * (x1 - x3) - y1).Mod(p);
        return new Point
        {
            Curve = P.Curve,
            X = x3,
            Y = y3,
        };
    }

    public static Point operator *(Point a, BigInteger b)
    {
        var result = Infinity;
        var append = a;
        while (b > 0)
        {
            if ((b & 1) == 1) result += append;
            append += append;
            b >>= 1;
        }
        return result;
    }

    public static Point operator *(BigInteger a, Point b) => b * a;

    public Hash GetHash() => IHashable.GetHash(X, Y);
}
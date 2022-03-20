namespace NoahCoinService;
using System.Numerics;
using NoahCoinService.Extensions;

// http://karpathy.github.io/2021/06/21/blockchain/
public record Curve(
    BigInteger p,
    BigInteger a,
    BigInteger b
)
{
    public static Curve bitcoin_curve = new Curve(
        p: "0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F".ParseHex(),
        a: "00000000000000000000000000000000000000000000000000000000000000000".ParseHex(), // a = 0
        b: "00000000000000000000000000000000000000000000000000000000000000007".ParseHex() // b = 7
    );
}

public record Point(
    Curve Curve,
    BigInteger X, 
    BigInteger Y
){
    public bool IsMember()
    {
        return (Y.Pow(2) - X.Pow(3) - 7) % Curve.p == 0;
    }

    public static Point Infinity = new(null, BigInteger.Zero, BigInteger.Zero);

    public static Point operator +(Point a) => a;
    public static Point operator +(Point a, Point b) {
        if (a == Point.Infinity) return b;
        if (b == Point.Infinity) return a;
        if (a.X == b.X && a.Y != b.Y) return Point.Infinity;
        BigInteger m;
        if (a.X == b.X) m = (3 * a.X.Pow(2) + a.Curve.a) * (2 * a.Y).ModInverse(a.Curve.p);
        else m = (a.Y - b.Y) * (a.X - b.X).ModInverse(a.Curve.p);
        var rx = (m.Pow(2) - a.X - b.X) % a.Curve.p;
        var ry = (-(m * (rx - a.X) + a.Y)) % a.Curve.p;
        return new Point(a.Curve, rx, ry);
    }

    public static Point operator *(Point a, int b){
        //// Basic iteration algorithm, not used since slow
        // Point rtn = a;
        // for (int i=0; i<b; i++)
        // {
        //     rtn += a;
        // }
        // return rtn;

        var result = new Point(a.Curve, BigInteger.Zero, BigInteger.Zero);
        var append = a;
        while (b > 0)
        {
            if ((b & 1) == 1) result += append;
            append += append;
            b >>= 1;
        }
        return result;
    }
}

public record Generator(
    Point Point,
    BigInteger Order
) {
    public static readonly Generator Default =  new Generator(
        new Point(
            Curve.bitcoin_curve,
            "079BE667EF9DCBBAC55A06295CE870B07029BFCDB2DCE28D959F2815B16F81798".ParseHex(),
            "0483ada7726a3c4655da4fbfc0e1108a8fd17b448a68554199c47d08ffb10d4b8".ParseHex()
        ),
        "0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEBAAEDCE6AF48A03BBFD25E8CD0364141".ParseHex()
    );
};

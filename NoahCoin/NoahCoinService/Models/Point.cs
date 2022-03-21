namespace NoahCoinService.Models;

public record Point(
    Curve Curve,
    BigInteger X,
    BigInteger Y
)
{
    public bool IsMember()
    {
        return (Y.Pow(2) - X.Pow(3) - 7).Mod(Curve.p) == 0;
    }

    public static Point Infinity = new(new Curve(0, 0, 0), BigInteger.Zero, BigInteger.Zero);

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
        return new Point(P.Curve, x3, y3);

        // BigInteger slope;
        // if (self.X != other.X)
        //     slope = (
        //         (other.Y - self.Y)
        //         * (other.X - self.X).ModInverse(self.Curve.p)
        //     ).Mod(self.Curve.p);
        // else
        //     slope = (
        //         (3 * self.X.Pow(2) + self.Curve.a) 
        //         * (2 * self.Y).ModInverse(self.Curve.p)
        //     ).Mod(self.Curve.p);
        // var rx = (slope.Pow(2) - self.X - other.X).Mod(self.Curve.p);
        // var ry = (slope * (self.X - rx) - self.Y).Mod(self.Curve.p);
        // // var ry = (-(slope * (rx - self.X) + self.Y)).Mod(self.Curve.p);

        // return new Point(self.Curve, rx, ry);



        // BigInteger slope;
        // if (self.X != other.X)
        //     slope = (
        //         (self.Y - other.Y)
        //         * (self.X - other.X).ModInverse(self.Curve.p)
        //     ).Mod(self.Curve.p);
        // else
        //     slope = (
        //         (3 * self.X.Pow(2) + self.Curve.a) 
        //         * (2 * self.Y).ModInverse(self.Curve.p)
        //     ).Mod(self.Curve.p);
        // var rx = (slope.Pow(2) - self.X - other.X).Mod(self.Curve.p);
        // var ry = (-(slope * (rx - self.X) + self.Y));
        // ry = ry.Mod(self.Curve.p);
        // // var ry = (-(slope * (rx - self.X) + self.Y)).Mod(self.Curve.p);

        // return new Point(self.Curve, rx, ry);


        // BigInteger m;
        // if (self.X == other.X)
        //     m = (3 * self.X.Pow(2) + self.Curve.a) * (2 * self.Y).ModInverse(self.Curve.p);
        // else
        //     m = (self.Y - other.Y) * (self.X - other.X).ModInverse(self.Curve.p);

        // var rx = (m.Pow(2) - self.X - other.X).Mod(self.Curve.p);
        // // var ry = (-(m * (rx - self.X) + self.Y)).Mod(self.Curve.p);
        // var ry = ((m * (self.X - rx) - self.Y)).Mod(self.Curve.p);
        // return new Point(self.Curve, rx, ry);
    }

    public static Point operator *(Point a, int b)
    {
        //// Basic iteration algorithm, not used since slow
        // Point rtn = a;
        // for (int i=1; i<b; i++)
        // {
        //     rtn += a;
        // }
        // return rtn;

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

    public static Point operator *(int a, Point b) => b * a;
}
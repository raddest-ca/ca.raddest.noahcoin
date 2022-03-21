using System;

namespace NoahCoinService.Tests;

public class GeneratorTestsS
{
    [Fact]
    public void GeneratorIsOnCurve()
    {
        Assert.True(Generator.Default.Point.IsMember());
    }

    [Fact]
    public void RandomPointIsNotOnCurve()
    {
        Random rand = new Random(1337);
        BigInteger x = rand.NextInt64();
        BigInteger y = rand.NextInt64();
        Point p = new Point(Curve.bitcoin_curve, x, y);
        Assert.False(p.IsMember());
    }
}
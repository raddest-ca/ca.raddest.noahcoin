
using NoahCoinService.Extensions;

namespace NoahCoinService.Tests;

public class PointTests
{

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    public void Multiply(int value)
    {
        // Point p = Generator.Default.Point;
        Point p = new Point(new Curve(17, 2, 3), 5, 11);
        Point answer = Point.Infinity;
        for (int i=1; i<=value; i++)
        {
            answer += p;
        }
        Point check = p * value;
        Assert.Equal(answer, check);
    }

    [Fact]
    public void ModInverse()
    {
        BigInteger a = 5;
        BigInteger p = 17;
        Assert.Equal(7, a.ModInverse(p));
    }
    
    [Theory]
    [ClassData(typeof(Range100))]
    public void ModInverseMany(BigInteger i)
    {
        BigInteger p = 17;
        var result = (i * i.ModInverse(p)).Mod(p);
        if (i.Mod(p) == 0)
            Assert.Equal(0, result);
        else
            Assert.Equal(1, result);
    }

    [Theory]
    [InlineData(5, 10, 5)]
    [InlineData(11, 10, 1)]
    [InlineData(-4, 10, 6)]
    [InlineData(-10, 10, 0)]
    [InlineData(45, 10, 5)]
    public void Mod(int a, int p, int answer)
    {
        var result = new BigInteger(a).Mod(p);
        Assert.Equal(answer,result);
    }

    [Fact]
    public void BasicAdd1()
    {
        Curve Curve = new Curve(17, 2, 3);
        Point P = new Point(Curve, 5, 11);
        Point Q = new Point(Curve, 15, 5);
        Point Expected = new Point(Curve, 13, 4);
        Assert.Equal(Expected, P + Q);
        Assert.Equal(Expected, Q + P);
    }

    [Fact]
    public void BasicAdd2()
    {
        Curve Curve = new Curve(17, 2, 3);
        Point P = new Point(Curve, 5, 11);
        Point Q = new Point(Curve, 8, 15);
        Point Expected = new Point(Curve, 2, 10);
        Assert.Equal(Expected, P + Q);
        Assert.Equal(Expected, Q + P);
    }

    [Fact]
    public void BasicAdd3()
    {
        Curve Curve = new Curve(17, 2, 3);
        Point P = new Point(Curve, 11, 8);
        Point Q = new Point(Curve, 12, 2);
        Point Expected = new Point(Curve, 13, 4);
        Assert.Equal(Expected, P + Q);
        Assert.Equal(Expected, Q + P);
    }

    [Fact]
    public void BasicDouble()
    {
        Curve curve = new Curve(17, 2, 3);
        Point point = new Point(curve, 5, 11);
        Point expected = new Point(curve, 15, 5);
        Assert.Equal(expected, point + point);
        Assert.Equal(expected, point * 2);
    }

    
    [Fact]
    public void BasicMultiply()
    {
        Curve curve = new Curve(17, 2, 3);
        Point point = new Point(curve, 5, 11);
        Point expected = new Point(curve, 13, 4);
        Assert.Equal(expected, point * 25);
    }
}
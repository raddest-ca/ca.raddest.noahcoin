namespace NoahCoin.Tests;

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
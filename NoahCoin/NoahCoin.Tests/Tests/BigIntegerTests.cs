namespace NoahCoin.Tests;

public class BigIntegerTests
{
    [Fact]
    public void ModInverse()
    {
        BigInteger a = 5;
        BigInteger p = 17;
        Assert.Equal(7, a.ModInverse(p));
    }

    [Theory]
    [ClassData(typeof(ModTestRange))]
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
    public void PythonToBytesCompare1()
    {
        var x = new BigInteger(12);
        var bytes = x.ToByteArray(32);
        var hex = bytes.ToHexString();
        var expected = "000000000000000000000000000000000000000000000000000000000000000C";
        Assert.Equal(expected, hex);
    }

    [Fact]
    public void PythonToBytesCompare2()
    {
        var x = BigInteger.Parse("83998262154709529558614902604110599582969848537757180553516367057821848015989");
        var bytes = x.ToByteArray(32);
        var hex = bytes.ToHexString();
        var expected = "b9b554e25022c2ae549b0c30c18df0a8e0495223f627ae38df0992efb4779475".ToUpperInvariant();
        Assert.Equal(expected, hex);
    }
}
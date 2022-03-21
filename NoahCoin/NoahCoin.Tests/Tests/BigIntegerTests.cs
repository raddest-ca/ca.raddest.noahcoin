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

}
namespace NoahCoin.Tests;

public class KeyTests
{
    [Fact]
    public void VerifyPublicKeys()
    {
        // var secretKey = BigInteger.Parse("22265090479312778178772228083027296664144");
        var secretKey = BigInteger.Parse("1");
        var publicKey = Generator.Default.Point;
        Assert.True(publicKey.IsMember());

        secretKey = BigInteger.Parse("2");
        publicKey = Generator.Default.Point + Generator.Default.Point;
        Assert.True(publicKey.IsMember());

        secretKey = BigInteger.Parse("2");
        publicKey = Generator.Default.Point * 3;
        Assert.True(publicKey.IsMember());
    }
}
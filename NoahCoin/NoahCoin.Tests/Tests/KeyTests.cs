using System.Security.Cryptography;
using Xunit.Abstractions;

namespace NoahCoin.Tests;

public class KeyTests
{
    private readonly ITestOutputHelper _output;
    public KeyTests(ITestOutputHelper output)
    {
        _output = output;
    }

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

    [Fact]
    public void SecretKeyTest()
    {
        var SecretKey = new BigInteger(System.Text.Encoding.ASCII.GetBytes("howdy" + "\0"));
        Assert.True(SecretKey >= 1);
        Assert.True(SecretKey <= Generator.Default.Order);
        var PublicKey = SecretKey * Generator.Default.Point;
        Assert.True(PublicKey.IsMember());
    }

    [Fact]
    public void RIPEMD160Test()
    {
        var Hasher = RIPEMD160.Create();
        var SecretKey = Hasher.ComputeHash("hello this is a test".ToBytes());
        var Result = SecretKey.ToHexString();
        Assert.Equal("f51960af7dd4813a587ab26388ddab3b28d1f7b4".ToUpperInvariant(), Result);
    }

    [Fact]
    public void BitcoinAddress1()
    {
        var key = new PublicKey(Curve.bitcoin_curve, 
            BigInteger.Parse("83998262154709529558614902604110599582969848537757180553516367057821848015989"),
            BigInteger.Parse("37676469766173670826348691885774454391218658108212372128812329274086400588247")
        );
        var addr = key.GetAddress("test", true);
        var expected = "mnNcaVkC35ezZSgvn8fhXEa9QTHSUtPfzQ";
        Assert.Equal(expected, addr);
    }

    [Fact]
    public void BitcoinAddress2()
    {
        var SecretKey = 1;
        var PublicKey = new PublicKey(SecretKey * Generator.Default.Point);
        var addr = PublicKey.GetAddress("test", true);
        var expected = "mrCDrCybB6J1vRfbwM5hemdJz73FwDBC8r";
        Assert.Equal(expected, addr);
    }

    [Fact]
    public void BitcoinAddress3()
    {
        var secretKey = BigInteger.Parse("22265090479312778178772228083027296664144");

        var publicKey = new PublicKey(secretKey * Generator.Default.Point);
        var expectedPublicKey = new PublicKey(
            Curve.bitcoin_curve,
            BigInteger.Parse("83998262154709529558614902604110599582969848537757180553516367057821848015989"),
            BigInteger.Parse("37676469766173670826348691885774454391218658108212372128812329274086400588247")
        );
        Assert.Equal(expectedPublicKey, publicKey);
        
        var address = publicKey.GetAddress("test", true);
        var expectedAddress = "mnNcaVkC35ezZSgvn8fhXEa9QTHSUtPfzQ";
        Assert.Equal(expectedAddress, address);
    }

    [Fact]
    public void B58EncodeTest()
    {
        var method = typeof(PublicKey).GetMethod( "b58encode", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );
        var param = new BigInteger(12).ToByteArray(25);
        var result = method!.Invoke(null, new[]{param});
    }
}
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
        var SecretKey = new PrivateKey("howdy");
        Assert.True(SecretKey.IsValid);
        SecretKey = SecretKey with { Value = -1 };
        Assert.False(SecretKey.IsValid);
        SecretKey = SecretKey with { Value = SecretKey.Generator.Order + 1 };
        Assert.False(SecretKey.IsValid);
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
        var key = new PublicKey
        {
            Generator = Generator.Default,
            Point = new Point
            {
                Curve = Curve.bitcoin_curve,
                X = BigInteger.Parse("83998262154709529558614902604110599582969848537757180553516367057821848015989"),
                Y = BigInteger.Parse("37676469766173670826348691885774454391218658108212372128812329274086400588247")
            }
        };
        var addr = key.GetAddress("test");
        var expected = "mnNcaVkC35ezZSgvn8fhXEa9QTHSUtPfzQ";
        Assert.Equal(expected, addr);
    }

    [Fact]
    public void BitcoinAddress2()
    {
        var SecretKey = new PrivateKey(1);
        var PublicKey = SecretKey.GetPublicKey();
        var addr = PublicKey.GetAddress("test");
        var expected = "mrCDrCybB6J1vRfbwM5hemdJz73FwDBC8r";
        Assert.Equal(expected, addr);
    }

    [Fact]
    public void BitcoinAddress3()
    {
        var secretKey = new PrivateKey(BigInteger.Parse("22265090479312778178772228083027296664144"));

        var publicKey = secretKey.GetPublicKey();
        var expectedPublicKey = new PublicKey
        {
            Generator = Generator.Default,
            Point = new Point
            {
                Curve = Curve.bitcoin_curve,
                X = BigInteger.Parse("83998262154709529558614902604110599582969848537757180553516367057821848015989"),
                Y = BigInteger.Parse("37676469766173670826348691885774454391218658108212372128812329274086400588247")
            }
        };
        Assert.Equal(expectedPublicKey, publicKey);

        var address = publicKey.GetAddress("test");
        var expectedAddress = "mnNcaVkC35ezZSgvn8fhXEa9QTHSUtPfzQ";
        Assert.Equal(expectedAddress, address);
    }

    [Fact]
    public void B58EncodeTest()
    {
        var method = typeof(PublicKey).GetMethod("b58encode", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
        var param = new BigInteger(12).ToByteArray(25);
        var result = method!.Invoke(null, new[] { param });
    }

    [Fact]
    public void RandomNewKey()
    {
        var pk = new PrivateKey();
        Assert.True(pk.IsValid);
    }

    [Fact]
    public void PublicKeyEncode()
    {
        var privateKey = new PrivateKey();
        var publicKey = privateKey.GetPublicKey();
        var encoded = publicKey.Encode();
        var decoded = PublicKey.Decode(encoded);
        Assert.Equal(publicKey, decoded);
    }
}
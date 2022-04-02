namespace NoahCoin.Tests;

public class SignatureTests
{
    [Fact]
    public void TestSignature()
    {
        var m = IHashable.GetHash("hello");
        var privateKey = new PrivateKey("suh dude");
        var sig = privateKey.GetSignature(m);
        var publicKey = privateKey.GetPublicKey();
        Assert.True(sig.IsValid(publicKey, m));   
    }
}
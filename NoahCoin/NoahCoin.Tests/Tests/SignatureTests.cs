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
        Signature modifiedSig;
        modifiedSig = sig with { Value = sig.Value with {X = sig.Value.X + 1}};
        Assert.False(modifiedSig.IsValid(publicKey, m));
        modifiedSig = sig with { Value = sig.Value with {X = sig.Value.Y, Y = sig.Value.X}};
        Assert.False(modifiedSig.IsValid(publicKey, m));
        modifiedSig = sig with { Value = Point.Infinity};
        Assert.False(modifiedSig.IsValid(publicKey, m));

    }

    [Fact]
    public void EncodeTest()
    {
        var privateKey = new PrivateKey();
        var publicKey = privateKey.GetPublicKey();
        var msg = IHashable.GetHash("howdy");
        var sig = privateKey.GetSignature(msg);
        var encoded = sig.Encode();
        var decoded = Signature.Decode(encoded);
        Assert.Equal(sig, decoded);
        Assert.True(sig.IsValid(publicKey, msg));
        Assert.True(decoded.IsValid(publicKey, msg));
    }
}
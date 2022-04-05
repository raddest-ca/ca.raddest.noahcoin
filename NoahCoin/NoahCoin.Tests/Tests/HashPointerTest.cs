using NoahCoin.Models.Datastructures;

namespace NoahCoin.Tests;

public class HashPointerTests
{
    [Fact]
    public void IsValidTest()
    {
        var hash = IHashable.GetHash(1);
        var p = new HashPointer<Hash>(hash);
        Assert.True(p.IsValid);
        var pBad = p with {Hash = new Hash(new byte[]{})};
        Assert.False(pBad.IsValid);
    }
}
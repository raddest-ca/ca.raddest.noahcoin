using NoahCoin.Models.Datastructures;

namespace NoahCoin.Tests;

public class HashPointerTests
{
    [Fact]
    public void IsValidTest()
    {
        var t = new Transaction()
        {
            Sender = BigInteger.Zero,
            Amount = BigInteger.One,
            Receiver = BigInteger.Zero
        };
        var p = new HashPointer<Transaction>(t);
        Assert.True(p.IsValid);
        var pBad = p with {Hash = new Hash(new byte[]{})};
        Assert.False(pBad.IsValid);
    }
}
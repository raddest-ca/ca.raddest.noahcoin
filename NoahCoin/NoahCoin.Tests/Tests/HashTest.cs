namespace NoahCoin.Tests;

public class HashTest
{
    [Fact]
    public void EmptyHash()
    {
        var hash = new Hash();
        Assert.True(hash.Value.Length == 0);
        Assert.Equal(new Hash(), hash);
    }

    [Fact]
    public void CustomHash()
    {
        var hash = new Hash()
        {
            Value = new byte[] { 0x01 }
        };
        Assert.True(hash.Value.Length == 1);
    }
}
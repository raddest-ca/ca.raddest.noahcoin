namespace NoahCoin.Tests;

public class BlockTests
{
    [Fact]
    public void Append()
    {
        Block b = new Block();
        Assert.NotNull(b.Header.PreviousBlock);
        Assert.True(b.Header.PreviousBlock.IsNull);
        Transaction tx = new Transaction();
        b = b.Append(new(tx));
        Assert.Equal(1,b.Header.Transactions.Count);
    }
}
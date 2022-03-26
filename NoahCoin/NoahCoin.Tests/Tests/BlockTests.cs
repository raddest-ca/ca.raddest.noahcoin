namespace NoahCoin.Tests;

public class BlockTests
{
    [Fact]
    public void VerifyHashIntegrity()
    {
        Block a = new Block
        {
            PreviousBlock = BigInteger.Zero,
            Transactions = new HashPointer<Transaction>[] { new(new(){
                Sender = BigInteger.One,
                Receiver = new BigInteger(2),
                Amount = new BigInteger(25)
            })},
        };
        Block b = a with {PreviousBlock = a.PreviousBlock+1};
        Block c = a with {Transactions = new HashPointer<Transaction>[] { }};
        Block d = a with {};
        Assert.NotEqual(a.Hash(), b.Hash());
        Assert.NotEqual(a.Hash(), c.Hash());
        Assert.Equal(a.Hash(), d.Hash());
    }

    [Fact]
    public void ValidTest1()
    {
        Block a = new Block
        {
            PreviousBlock = BigInteger.Zero,
            Transactions = new HashPointer<Transaction>[] { new(new (){
                Sender = BigInteger.One,
                Receiver = new BigInteger(2),
                Amount = new BigInteger(25)
            })},
        };
        Assert.True(a.IsValid);
        var BadTransactionPointer = new HashPointer<Transaction>{
            Reference = a.Transactions[0].Reference,
            Hash = new byte[]{},
        };
        Assert.False(a.IsValid);
        var BadBlock = a with {Transactions = new[]{BadTransactionPointer}};
        Assert.False(BadBlock.IsValid);
    }
}
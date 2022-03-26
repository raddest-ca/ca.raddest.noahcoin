namespace NoahCoin.Tests;

public class BlockTests
{
    [Fact]
    public void VerifyHashIntegrity()
    {
        Block a = new Block
        {
            Header = new()
            {
                IsGenesisBlock = true,
                Transactions = new HashPointer<Transaction>[] { new(new(){
                    Sender = BigInteger.One,
                    Receiver = new BigInteger(2),
                    Amount = new BigInteger(25)
                })},
            },
        };
        Block b = a with { Header = a.Header with { Transactions = new HashPointer<Transaction>[] { } } };
        Block c = a with { Header = a.Header, Nonce = a.Nonce };
        Assert.NotEqual(a.Hash(), b.Hash());
        Assert.Equal(a.Hash(), c.Hash());
    }

    [Fact]
    public void ValidTest1()
    {
        Block a = new Block
        {
            Header = new(){
                IsGenesisBlock = true,
                Transactions = new HashPointer<Transaction>[] { new(new (){
                    Sender = BigInteger.One,
                    Receiver = new BigInteger(2),
                    Amount = new BigInteger(25)
                })},
            }
        };
        Assert.True(a.IsValid);
        var BadTransactionPointer = new HashPointer<Transaction>
        {
            Reference = a.Header.Transactions[0].Reference,
            Hash = new byte[] { },
        };
        Assert.False(BadTransactionPointer.IsValid);
        var BadBlock = a with { Header = new() {Transactions = new[] { BadTransactionPointer } }};
        Assert.False(BadBlock.IsValid);
    }
}
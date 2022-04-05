using NoahCoin.Models.Datastructures;

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
                Transactions = new HashPointer<Transaction>[] { new(new())},
            },
        };
        Block b = a with { Header = a.Header with { Transactions = new HashPointer<Transaction>[] { } } };
        Block c = a with { Header = a.Header, Nonce = a.Nonce };
        Assert.NotEqual(a.GetHash(), b.GetHash());
        Assert.Equal(a.GetHash(), c.GetHash());
    }

    [Fact]
    public void ValidTest1()
    {
        Block a = new Block
        {
            Header = new(){
                IsGenesisBlock = true,
                Transactions = new HashPointer<Transaction>[] { new(new ())},
            }
        };
        Assert.True(a.IsValid);
        var BadTransactionPointer = new HashPointer<Transaction>
        {
            Reference = a.Header.Transactions[0].Reference,
            Hash = new Hash(new byte[] { }),
        };
        Assert.False(BadTransactionPointer.IsValid);
        var BadBlock = a with { Header = new() {Transactions = new[] { BadTransactionPointer } }};
        Assert.False(BadBlock.IsValid);
    }
}
using System;
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
        Block b = a with { Header = a.Header with { Transactions = Array.Empty<HashPointer<Transaction>>() } };
        Block c = a with { Header = a.Header, Nonce = a.Nonce };
        Assert.NotEqual(a.GetHash(), b.GetHash());
        Assert.Equal(a.GetHash(), c.GetHash());
    }
}
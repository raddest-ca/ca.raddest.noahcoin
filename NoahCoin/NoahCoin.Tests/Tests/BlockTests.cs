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
                Transactions = new()
            },
        };
        Block b = a with
        {
            Header = a.Header with
            {
                Transactions = new MerkelTree().Append(
                    new(new Transaction
                    {
                        Inputs = new[]
                        {
                            new TransactionInput(
                                HashPointer<Transaction>
                                    .GetNullPointer<Transaction>(),
                                0
                            )
                        }
                    })
                )
            }
        };
        Block c = a with { Header = a.Header, Nonce = a.Nonce };
        Assert.NotEqual(a.GetHash(), b.GetHash());
        Assert.Equal(a.GetHash(), c.GetHash());
    }
}
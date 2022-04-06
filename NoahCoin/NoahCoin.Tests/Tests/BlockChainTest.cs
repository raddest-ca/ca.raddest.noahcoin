using System.Linq;
using Microsoft.CSharp.RuntimeBinder;

namespace NoahCoin.Tests;

public class BlockChainTest
{
    [Fact]
    public void TransactionVerify()
    {
        var privateKey = new PrivateKey(0);
        var publicKey = privateKey.GetPublicKey();
        var address = publicKey.GetAddress();
        var txOutput = new TransactionOutput(25, address);
        // var txInput = new TransactionInput()
    }

    [Fact]
    public void Append()
    {
        var bc = new BlockChain();
        Assert.Empty(bc);
        var block1 = new Block();
        bc = bc.Append(new(block1));
        Assert.Single(bc);
        var block2 = new Block();
        bc = bc.Append(new(block2));
        Assert.Equal(2, bc.Count());
    }

    [Fact]
    public void SmallChain()
    {
        var firstPrivateKey = new PrivateKey();
        var firstPublicKey = firstPrivateKey.GetPublicKey();
        var secondPrivateKey = new PrivateKey();
        var secondPublicKey = firstPrivateKey.GetPublicKey();
        
        var bc = new BlockChain();
        {
            // mine a block
            var tx = Transaction.GetRewardTransaction(firstPublicKey.GetAddress(), bc.Reward);
            var block = new Block().Append(new(tx));
            var miner = new Miner
            {
                Block = block,
                Difficulty = bc.Difficulty
            };
            block = miner.MineBlock();
            bc = bc.Append(new(block));
        }
        {
            // create a transaction
            var tx = new Transaction
            {
                Inputs = new[]
                {
                    new TransactionInput
                    {
                        PreviousReference = new()
                        {
                            Block = bc.Head,
                            Transaction = bc.Head.Reference.First(),
                            TransactionIndex = 0
                        },
                        PreviousOutputIndex = 0
                    }
                },
                Outputs = new[]
                {
                    new TransactionOutput(15, secondPublicKey.GetAddress())
                }
            };
            tx = tx.AddSignature(firstPrivateKey, firstPublicKey, 0);
            bc = bc.Append(new(new Block().Append(new(tx))));
        }
        Assert.True(bc.IsValid());
    }
}
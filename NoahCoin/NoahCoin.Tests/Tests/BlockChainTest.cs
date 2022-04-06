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
        var privateKey = new PrivateKey();
        var publicKey = privateKey.GetPublicKey();
        var address = publicKey.GetAddress();
        var bc = new BlockChain();
        var tx = Transaction.GetRewardTransaction(address, bc.Reward);
        var block = new Block().Append(new(tx));
        var miner = new Miner
        {
            Block = block,
            Difficulty = bc.Difficulty
        };
        block = miner.MineBlock();
        bc = bc.Append(new(block));
        Assert.True(bc.IsValid());
    }
}
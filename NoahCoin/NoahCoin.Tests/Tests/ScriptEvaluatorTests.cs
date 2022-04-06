using System.Linq;

namespace NoahCoin.Tests;

public class ScriptEvaluatorTests
{
    [Fact]
    public void test()
    {
        var privateKey = new PrivateKey(1);
        var publicKey = privateKey.GetPublicKey();
        var address = publicKey.GetAddress();
        var bc = new BlockChain();

        var tx = new Transaction
        {
            Outputs = new[] { new TransactionOutput(25, address) },
            Inputs = new[] { new TransactionInput() }
        };


        bc = bc.Append(new(new Block().Append(new(tx))));

        tx = new Transaction
        {
            Inputs = new[]
            {
                new TransactionInput
                {
                    PreviousReference = new TransactionReference
                    {
                        Block = bc.First(),
                        TransactionIndex = 0,
                        Transaction = bc.First().Reference.First()
                    },
                    PreviousOutputIndex = 0
                }
            },
            Outputs = new[]
            {
                new TransactionOutput(25, address)
            }
        };
        tx = tx.AddSignature(privateKey, publicKey, 0);

        bc = bc.Append(new(new Block().Append(new(tx))));

        var runner = new ScriptEvaluator(
            bc,
            bc.Head.Reference.First().Reference
        );
        Assert.True(runner.TryValidate());
    }
}
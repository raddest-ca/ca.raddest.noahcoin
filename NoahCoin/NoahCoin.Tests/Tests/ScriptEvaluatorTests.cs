namespace NoahCoin.Tests;

public class ScriptEvaluatorTests
{
    [Fact]
    public void test()
    {
        var privateKey = new PrivateKey(1);
        var publicKey = privateKey.GetPublicKey();
        var address = publicKey.GetAddress();
        var previousTx = new HashPointer<Transaction>
            { Hash = IHashable.GetHash(0) };

        var tx = new Transaction
        {
            Outputs = new[] { new TransactionOutput(25, address) },
            Inputs = new[] { new TransactionInput(previousTx, 0) }
        };

        var sigContent = tx.GetPreSignedHash();
        var sig = privateKey.GetSignature(sigContent);

        var txNew = tx with
        {
            Inputs = new[]
            {
                tx.Inputs[0] with
                {
                    Script = TransactionInput.GetScript(sig, publicKey)
                }
            }
        };
        
        Assert.Equal(tx.GetPreSignedHash(), txNew.GetPreSignedHash());
        var runner = new ScriptEvaluator(txNew);
        Assert.True(runner.TryValidate());
    }
}
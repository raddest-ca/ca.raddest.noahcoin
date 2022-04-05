namespace NoahCoin.Tests;

public class ScriptEvaluatorTests
{
    [Fact]
    public void test()
    {
        var privateKey = new PrivateKey(1);
        var publicKey = privateKey.GetPublicKey();
        var address = publicKey.GetAddress();
        var tx = new Transaction()
        {
            Outputs = new[] {new TransactionOutput(25, address)}
        };
        var sigContent = tx.GetPreSignedHash();
        var sig = privateKey.GetSignature(sigContent);
        var txInput = new TransactionInput(
            new HashPointer<Transaction>
            {
                Hash = IHashable.GetHash(0)
            },
            0,
            sig,
            publicKey
        );
        tx = tx with
        {
            Inputs = new[] { txInput }
        };
        var runner = new ScriptEvaluator(tx);
        Assert.True(runner.TryValidate());
    }
}
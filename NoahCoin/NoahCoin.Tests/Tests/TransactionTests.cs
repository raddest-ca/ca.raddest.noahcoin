namespace NoahCoin.Tests;

public class TransactionTests
{
    [Fact]
    public void PreSignedHashConsistency()
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
        
        var newSigContent = txNew.GetPreSignedHash();
        Assert.Equal(sigContent, newSigContent);
        
        var newSig = privateKey.GetSignature(newSigContent);
        Assert.True(sig.IsValid(publicKey, sigContent));
        Assert.True(newSig.IsValid(publicKey, newSigContent));
    }
}
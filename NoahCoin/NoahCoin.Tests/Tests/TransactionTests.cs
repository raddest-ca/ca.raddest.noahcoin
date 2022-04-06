namespace NoahCoin.Tests;

public class TransactionTests
{
    [Fact]
    public void PreSignedHashConsistency()
    {
        var privateKey = new PrivateKey(1);
        var publicKey = privateKey.GetPublicKey();
        var address = publicKey.GetAddress();

        // create a transaction without an input script
        var tx = new Transaction
        {
            Inputs = new[]
            {
                new TransactionInput
                {
                    PreviousReference = new TransactionReference
                    {
                        Block = HashPointer<Block>.GetNullPointer(),
                        TransactionIndex = 0,
                        Transaction = HashPointer<Transaction>.GetNullPointer(),
                    }
                }
            },
            Outputs = new[]
            {
                new TransactionOutput(25, address)
            }
        };

        var sigContent = tx.GetPreSignedHash();
        var sig = privateKey.GetSignature(sigContent);

        // modify tx to include input script
        var txNew = tx with
        {
            Inputs = new[]
            {
                tx.Inputs[0] with
                {
                    Script = TransactionInput.GetClaimScript(sig, publicKey)
                }
            }
        };

        // ensure the presignedhash is the same with and without the input script
        var newSigContent = txNew.GetPreSignedHash();
        Assert.Equal(sigContent, newSigContent);

        var newSig = privateKey.GetSignature(newSigContent);
        Assert.True(sig.IsValid(publicKey, sigContent));
        Assert.True(newSig.IsValid(publicKey, newSigContent));
    }

    [Fact]
    public void EmptyTx()
    {
        var tx = new Transaction();
        Assert.True(tx.IsValid(new BlockChain()));
    }
}
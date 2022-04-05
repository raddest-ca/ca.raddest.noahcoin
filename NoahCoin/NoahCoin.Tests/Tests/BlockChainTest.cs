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
}
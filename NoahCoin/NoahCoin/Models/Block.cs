
namespace NoahCoin;

public class Block
{
    public Block(BigInteger previousBlock, Transaction[] transactions, BigInteger nonce)
    {
        PreviousBlock = previousBlock;
        Transactions = transactions;
        this.Nonce = nonce;
    }

    public BigInteger PreviousBlock {get; init;}
    public Transaction[] Transactions {get; init;}
    public BigInteger Nonce {get; set; }
    public byte[] Hash()
    {
        var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        hasher.AppendData(PreviousBlock.ToByteArray(32));
        foreach (var t in Transactions)
        {
            hasher.AppendData(t.Sender.ToByteArray(32));
            hasher.AppendData(t.Receiver.ToByteArray(32));
            hasher.AppendData(t.Amount.ToByteArray(32));
        }
        hasher.AppendData(Nonce.ToByteArray(32));
        return hasher.GetHashAndReset();
    }
}
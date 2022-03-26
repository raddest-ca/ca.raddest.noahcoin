
namespace NoahCoin;

public record Block : IHashable
{

    public BigInteger PreviousBlock {get; init;}
    public HashPointer<Transaction>[] Transactions {get; init;}
    public bool IsValid { get => Transactions.All(t => t.IsValid); }

    public BigInteger Nonce {get; set; }

    public byte[] Hash()
    {
        List<byte[]> ToHash = new();
        ToHash.Add(PreviousBlock.ToByteArray(32));
        foreach (var t in Transactions)
        {
            ToHash.Add(t.Hash);
        }
        ToHash.Add(Nonce.ToByteArray(32));
        return IHashable.Hash(ToHash);
    }

}
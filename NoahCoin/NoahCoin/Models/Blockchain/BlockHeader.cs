namespace NoahCoin.Models.Blockchain;

public record BlockHeader : IHashable
{
    public HashPointer<Block> PreviousBlock { get; init; }
    public HashPointer<Transaction>[] Transactions { get; init; }

    public bool IsGenesisBlock { get; init; } = false;

    public bool IsValid
    {
        get => (IsGenesisBlock || (PreviousBlock != null && PreviousBlock.IsValid))
            && Transactions.All(t => t.IsValid);
    }


    public byte[] Hash()
    {
        List<byte[]> ToHash = new();
        if (!IsGenesisBlock) ToHash.Add(PreviousBlock.Hash);
        foreach (var t in Transactions)
        {
            ToHash.Add(t.Hash);
        }
        return IHashable.Hash(ToHash);
    }

}
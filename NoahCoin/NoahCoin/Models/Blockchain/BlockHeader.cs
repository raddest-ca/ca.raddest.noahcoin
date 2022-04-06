using NoahCoin.Models.Datastructures;

namespace NoahCoin.Models.Blockchain;

public record BlockHeader : IHashable
{
    public HashPointer<Block> PreviousBlock { get; init; } =
        HashPointer<Block>.GetNullPointer();

    public MerkelTree Transactions { get; init; } = new();

    public bool IsValid =>
        PreviousBlock.IsNullOrValid && Transactions.IsValid;

    public Transaction? GetTransaction(
        int index,
        Hash txHash
    )
    {
        if (Transactions[index].Reference is not Transaction tx) return null;
        return tx.GetHash() == txHash ? tx : null;
    }

    public Hash GetHash() =>
        PreviousBlock.IsNull
            ? Transactions.GetHash()
            : IHashable.GetHash(Transactions.GetHash(), PreviousBlock);

    public BlockHeader Append(
        HashPointer<Transaction> tx
    ) =>
        this with
        {
            Transactions = Transactions.Append(new(tx.Reference))
        };
}
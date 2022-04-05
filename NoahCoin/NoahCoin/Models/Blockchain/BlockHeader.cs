using NoahCoin.Models.Datastructures;

namespace NoahCoin.Models.Blockchain;

public record BlockHeader : IHashable
{
    public HashPointer<Block>? PreviousBlock { get; init; }
    public HashPointer<Transaction>[] Transactions { get; init; }

    public bool IsGenesisBlock { get; init; } = false;

    public bool IsValid =>
        (IsGenesisBlock || (PreviousBlock != null && PreviousBlock.IsValid))
        && Transactions.All(t => t.IsValid);

    public Hash GetHash() =>
        IsGenesisBlock
            ? IHashable.GetHash(Transactions)
            : IHashable.GetHash(
                ((IEnumerable<IHashable>)Transactions).Append(PreviousBlock)
            );
}
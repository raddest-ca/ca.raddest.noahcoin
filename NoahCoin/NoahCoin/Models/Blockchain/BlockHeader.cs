using NoahCoin.Models.Datastructures;

namespace NoahCoin.Models.Blockchain;

public record BlockHeader : IHashable
{
    public HashPointer<Block> PreviousBlock { get; init; }
    public MerkelTree Transactions { get; init; }

    public bool IsGenesisBlock { get; init; } = false;

    public bool IsValid =>
        (IsGenesisBlock || PreviousBlock.IsValid) && Transactions.IsValid;


    public Hash GetHash() =>
        IsGenesisBlock
            ? Transactions.GetHash()
            : IHashable.GetHash(Transactions.GetHash(), PreviousBlock);
}
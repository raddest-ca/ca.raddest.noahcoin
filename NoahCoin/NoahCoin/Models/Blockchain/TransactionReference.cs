namespace NoahCoin.Models.Blockchain;

public record TransactionReference : IHashable
{
    public HashPointer<Block> Block { get; init; } = HashPointer<Block>.GetNullPointer();

    public HashPointer<Transaction> Transaction { get; init; } =
        HashPointer<Transaction>.GetNullPointer();

    public int TransactionIndex { get; init; } = -1;

    public Hash GetHash() => IHashable.GetHash(
        Block,
        Transaction,
        IHashable.GetHash(TransactionIndex)
    );

    public string? GetScript(
        BlockChain bc,
        int outputIndex
    )
    {
        var tx = GetTransaction(bc);
        if (tx == null) return null;
        if (outputIndex < 0 || outputIndex >= tx.Outputs.Length) return null;
        return tx.Outputs[outputIndex].Script;
    }

    public Transaction? GetTransaction(
        BlockChain bc
    ) =>
        bc.GetBlock(Block.GetHash())
            ?.Reference.GetTransaction(TransactionIndex, Transaction.GetHash());
}
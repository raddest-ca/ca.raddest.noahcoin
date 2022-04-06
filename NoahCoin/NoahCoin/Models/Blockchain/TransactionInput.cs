namespace NoahCoin.Models.Blockchain;

public record TransactionInput : IHashable
{
    public TransactionReference PreviousReference { get; init; } = new();
    public int PreviousOutputIndex { get; init; } = -1;
    public string Script { get; init; } = "";

    public TransactionInput()
    {
    }

    public string? GetPreviousScript(
        BlockChain bc
    ) => PreviousReference.GetScript(bc, PreviousOutputIndex);

    public static string GetClaimScript(
        Signature signature,
        PublicKey publicKey
    ) =>
        $@"
{signature.Encode()}
{publicKey.Encode()}
";

    public Hash GetHash() =>
        IHashable.GetHash(PreviousReference, IHashable.GetHash(Script));

    public bool IsValid(
        BlockChain bc
    )
    {
        var tx = PreviousReference.GetTransaction(bc);
        if (tx == null) return false;
        return PreviousOutputIndex >= 0
               && PreviousOutputIndex < tx.Outputs.Length;
    }
}
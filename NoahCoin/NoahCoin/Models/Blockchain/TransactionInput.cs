namespace NoahCoin.Models.Blockchain;

public record TransactionInput : IHashable
{
    public HashPointer<Transaction> PreviousTransactionHash { get; init; }
    public int PreviousTransactionOutputIndex { get; init; }
    public string Script { get; init; }

    public TransactionInput(
        HashPointer<Transaction> previousTransactionHash,
        int previousTransactionOutputIndex
    )
    {
        PreviousTransactionHash = previousTransactionHash;
        PreviousTransactionOutputIndex = previousTransactionOutputIndex;
        Script = ""; // for signing
    }
    public TransactionInput(
        HashPointer<Transaction> previousTransactionHash,
        int previousTransactionOutputIndex,
        Signature signature,
        PublicKey publicKey
    )
    {
        PreviousTransactionHash = previousTransactionHash;
        PreviousTransactionOutputIndex = previousTransactionOutputIndex;
        Script = GetScript(signature, publicKey);
    }

    public static string GetScript(
        Signature signature,
        PublicKey publicKey
    ) =>
        $@"
{signature.Encode()}
{publicKey.Encode()}
";

    public Hash GetHash() =>
        IHashable.GetHash(
            PreviousTransactionHash,
            IHashable.GetHash(PreviousTransactionOutputIndex),
            IHashable.GetHash(Script)
        );
}
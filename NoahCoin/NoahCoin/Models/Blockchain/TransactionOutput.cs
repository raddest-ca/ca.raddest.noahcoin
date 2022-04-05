namespace NoahCoin.Models.Blockchain;

public record TransactionOutput : IHashable
{
    public int Value { get; init; }
    public string Script { get; init; }

    public TransactionOutput()
    {
    }

    public TransactionOutput(
        int amount,
        string publicKeyHash
    )
    {
        Value = amount;
        Script = $@"
OP_DUP
OP_GETADDRESS
{publicKeyHash}
OP_EQUALVERIFY
OP_CHECKSIG
";
    }

    public Hash GetHash() => IHashable.GetHash(
        IHashable.GetHash(Value),
        IHashable.GetHash(Script)
    );
};
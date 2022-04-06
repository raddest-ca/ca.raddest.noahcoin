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
        string address
    )
    {
        Value = amount;
        Script = GetScriptPayableTo(address);
    }

    public static string GetScriptPayableTo(
        string address
    ) => $@"
OP_DUP
OP_GETADDRESS
{address}
OP_EQUALVERIFY
OP_CHECKSIG
";

    public Hash GetHash() => IHashable.GetHash(
        IHashable.GetHash(Value),
        IHashable.GetHash(Script)
    );
};
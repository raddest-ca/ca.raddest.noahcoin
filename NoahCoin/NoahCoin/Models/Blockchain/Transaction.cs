namespace NoahCoin.Models.Blockchain;

public record Transaction : IHashable
{
    public TransactionInput[] Inputs { get; init; } = Array.Empty<TransactionInput>();
    public TransactionOutput[] Outputs { get; init; } = Array.Empty<TransactionOutput>();

    public Hash GetHash() =>
        IHashable.GetHash(Inputs.Concat((IHashable[])Outputs));

    public Hash GetPreSignedHash() => IHashable.GetHash(
        Inputs.Select(i => i with { Script = "" }).Concat((IHashable[])Outputs)
    );
}
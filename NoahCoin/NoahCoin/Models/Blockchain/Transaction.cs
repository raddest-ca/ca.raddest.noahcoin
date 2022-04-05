namespace NoahCoin.Models.Blockchain;

public record Transaction : IHashable
{
    public TransactionInput[] Inputs { get; init; } =
        Array.Empty<TransactionInput>();

    public TransactionOutput[] Outputs { get; init; } =
        Array.Empty<TransactionOutput>();

    public Hash GetHash() =>
        IHashable.GetHash(
            Inputs.Select(x => x.GetHash())
                .Concat(Outputs.Select(v => v.GetHash()))
        );

    public Hash GetPreSignedHash() => IHashable.GetHash(
        Inputs.Select(i => (i with { Script = "" }).GetHash())
            .Concat(Outputs.Select(v => v.GetHash()))
    );
}
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

    public Transaction AddSignature(
        PrivateKey privateKey,
        PublicKey publicKey,
        int inputIndex
    )
    {
        TransactionInput[] inputs = new TransactionInput[Inputs.Length];
        var sig = privateKey.GetSignature(GetPreSignedHash());
        for (int i = 0; i < inputs.Length; i++)
        {
            var entry = Inputs[i];
            if (i == inputIndex)
            {
                entry = entry with
                {
                    Script = TransactionInput.GetClaimScript(sig, publicKey)
                };
            }

            inputs[i] = entry;
        }

        return this with { Inputs = inputs };
    }

    public bool IsValid(
        BlockChain bc
    )
    {
        if (!new ScriptEvaluator(bc, this).TryValidate()) return false;
        if (!Inputs.All(input => input.IsValid(bc))) return false;
        var totalOutputValue = GetOutputValueTotal();
        var totalInputValue = GetInputValueTotal(bc);
        return Inputs.Length == 0 || totalOutputValue <= totalInputValue;
    }

    public int GetInputValueTotal(
        BlockChain bc
    ) => Inputs.Select(
            input => input.PreviousReference.GetTransaction(bc)!
                .Outputs[input.PreviousOutputIndex]
                .Value
        )
        .Sum();

    public int GetOutputValueTotal() =>
        Outputs.Select(output => output.Value).Sum();

    public static Transaction GetRewardTransaction(
        string address,
        int reward
    ) =>
        new()
        {
            Inputs = new TransactionInput[] { },
            Outputs = new[]
            {
                new TransactionOutput(reward, address)
            }
        };
}
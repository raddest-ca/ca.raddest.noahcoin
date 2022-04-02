namespace NoahCoin.Models.Blockchain;

public record Block : IHashable
{
    public BlockHeader Header {get; init;}
    public BigInteger Nonce {get; init; } = BigInteger.Zero;
    public bool IsValid {get => Header.IsValid; }
    public Hash Hash()
    {
        return Header.Hash().Concat(Nonce.ToByteArray(32));
    }
}
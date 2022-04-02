namespace NoahCoin.Models.Blockchain;

public record Block : IHashable
{
    public BlockHeader Header {get; init;}
    public BigInteger Nonce {get; init; } = BigInteger.Zero;
    public bool IsValid {get => Header.IsValid; }
    public Hash GetHash()
    {
        return Header.GetHash().Concat(Nonce.GetHash());
    }
}
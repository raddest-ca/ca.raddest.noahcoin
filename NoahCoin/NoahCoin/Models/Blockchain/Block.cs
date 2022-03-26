namespace NoahCoin.Models.Blockchain;

public record Block : IHashable
{
    public BlockHeader Header {get; init;}
    public BigInteger Nonce {get; init; } = BigInteger.Zero;
    public bool IsValid {get => Header.IsValid; }
    public byte[] Hash()
    {
        return IHashable.Hash(Header.Hash(), Nonce.ToByteArray(32));
    }
}
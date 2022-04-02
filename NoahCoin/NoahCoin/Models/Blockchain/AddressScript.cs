namespace NoahCoin.Models.Blockchain;

public record AddressScript : IHashable
{
    public string Script {get; init;}
    public Hash GetHash()
    {
        return IHashable.GetHash(Script.ToBytes());
    }
}
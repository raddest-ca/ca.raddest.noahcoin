namespace NoahCoin.Models.Blockchain;


public record Transaction : IHashable
{

    public BigInteger Sender {get; init;}
    public BigInteger Receiver {get; init;}
    public BigInteger Amount {get; init;}
 
    public Hash GetHash() => IHashable.GetHash( Sender, Receiver, Amount );
}
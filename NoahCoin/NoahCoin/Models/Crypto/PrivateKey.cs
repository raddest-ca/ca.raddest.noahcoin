namespace NoahCoin.Models.Crypto;

public record PrivateKey : IPrivateKey
{
    public Generator Generator {get; init;}
    public BigInteger Value {get; init;}

    public bool IsValid
    {
        get
        {
            return Value > 0 && Value < Generator.Order;
        }
    }

    public PrivateKey(BigInteger Value)
    {
        this.Value = Value;
        Generator = Generator.Default;
    }
    public PrivateKey(string phrase)
    {
        Generator = Generator.Default;
        Value = new BigInteger(
            System.Text.Encoding.ASCII.GetBytes(phrase),
            isUnsigned: true,
            isBigEndian: true
        );
    }

    public IPublicKey GetPublicKey()
    {
        return Generator.GeneratePublicKey(this);
    }
}
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

    public PrivateKey() {
        Generator = Generator.Default;
        var rng = RandomNumberGenerator.Create();
        int bitsToGenerate = (int) Math.Ceiling(BigInteger.Log(Generator.Order, 2));
        var data = new byte[bitsToGenerate];
        rng.GetBytes(data);
        Value = new BigInteger(data, isUnsigned: true, isBigEndian: true).Mod(Generator.Order);
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
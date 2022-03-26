namespace NoahCoin.Models.Crypto;

public interface IPrivateKey
{
    IPublicKey GetPublicKey();
}
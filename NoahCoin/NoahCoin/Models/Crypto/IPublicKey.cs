namespace NoahCoin.Models.Crypto;

public interface IPublicKey
{
    string GetAddress(string network, bool compressed);
}
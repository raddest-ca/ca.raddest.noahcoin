namespace NoahCoin.Models.Blockchain;

public record HashPointer<T> where T: IHashable
{
    public T Reference {get; init;}
    public Hash Hash {get; init;}

    public bool IsValid => Reference.GetHash().Equals(Hash);
    public HashPointer(){}
    public HashPointer(T reference)
    {
        Reference = reference;
        Hash = reference.GetHash();
    }
}
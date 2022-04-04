namespace NoahCoin.Models.Datastructures;

public record HashPointer<T> : IHashable where T: IHashable
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

    public Hash GetHash()
    {
        return Hash;
    }
}
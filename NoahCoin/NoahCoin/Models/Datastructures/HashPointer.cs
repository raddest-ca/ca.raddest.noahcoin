using System.Text;

namespace NoahCoin.Models.Datastructures;

public record HashPointer<T> : IHashable where T: IHashable
{
    public T Reference {get; init;}
    public Hash Hash {get; init;}

    public bool IsValid => !IsNull && Reference.GetHash().Equals(Hash);
    public bool IsNullOrValid => IsNull || IsValid;
    public bool IsNull => Reference == null;
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

    public static HashPointer<T> GetNullPointer()
    {
        return new HashPointer<T>
        {
            Hash = new()
        };
    }
}
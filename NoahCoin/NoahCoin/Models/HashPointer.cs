namespace NoahCoin.Models;

public record HashPointer<T> where T: IHashable
{
    public T Reference {get; init;}
    public byte[] Hash {get; init;}

    public bool IsValid
    {
        get
        {
            if (Reference == null) return false;
            var actual = Reference.Hash();
            if (actual.Length != Hash.Length) return false;
            return actual.SequenceEqual(Hash);
        }
    }
    public HashPointer(){}
    public HashPointer(T reference)
    {
        Reference = reference;
        Hash = reference.Hash();
    }
}
namespace NoahCoin.Models;

public record HashPointer<T> where T: IHashable
{
    public T Reference {get; init;}
    public byte[] Hash {get; init;}

    public bool IsValid
    {
        get
        {
            var actual = Reference.Hash();
            if (actual.Count() != Hash.Count()) return false;
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
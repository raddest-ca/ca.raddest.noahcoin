namespace NoahCoin.Models.Crypto;

public record Hash : IHashable
{
    public byte[] Value { get; init; } = Array.Empty<byte>();

    public BigInteger IntegerValue => new BigInteger(Value, isUnsigned: true, isBigEndian: true);

    public string HexValue => Value.ToHexString();

    public Hash()
    {
    }

    public Hash(
        byte[] value
    )
    {
        Value = value;
    }

    public override int GetHashCode()
    {
        return (int) IntegerValue.Mod(Int32.MaxValue);
    }

    public Hash Concat(
        IHashable other
    )
    {
        return IHashable.GetHash(Value, other.GetHash().Value);
    }

    public Hash Concat(
        byte[] other
    )
    {
        return IHashable.GetHash(Value, other);
    }
    public virtual bool Equals(
        Hash? other
    )
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (other.GetType() != GetType()) return false;
        if (Value == null && other.Value == null) return true;
        if (Value == null || other.Value == null) return false;
        if (Value.Length != other.Value.Length) return false;
        return Value.SequenceEqual(other.Value);
    }

    public Hash GetHash()
    {
        return this;
    }
}
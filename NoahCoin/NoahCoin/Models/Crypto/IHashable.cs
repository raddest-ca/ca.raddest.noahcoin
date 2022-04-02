namespace NoahCoin.Models.Crypto;

public interface IHashable
{
    Hash Hash();

    /* Default hash implementation helpers */

    public static Hash Hash(
        params byte[][] content
    )
    {
        return Hash((IEnumerable<byte[]>) content);
    }

    public static Hash Hash(
        params Hash[] hashes
    )
    {
        return Hash(hashes.Select(h => h.Value));
    }

    public static Hash Hash(
        IEnumerable<byte[]> content
    )
    {
        var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        foreach (byte[] chunk in content)
        {
            hasher.AppendData(chunk);
        }
        return new Hash(hasher.GetHashAndReset());
    }

    public static Hash Hash(
        int content
    )
    {
        byte[] bytes = BitConverter.GetBytes(content);
        return Hash(bytes);
    }
}
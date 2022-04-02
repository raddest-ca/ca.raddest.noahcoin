namespace NoahCoin.Models.Crypto;

public interface IHashable
{
    Hash GetHash();

    /* Default hash implementation helpers */

    public static Hash GetHash(
        params byte[][] content
    )
    {
        return GetHash((IEnumerable<byte[]>)content);
    }

    public static Hash GetHash(
        params Hash[] hashes
    )
    {
        return GetHash(hashes.Select(h => h.Value));
    }

    public static Hash GetHash(
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

    public static Hash GetHash(
        int content
    )
    {
        byte[] bytes = BitConverter.GetBytes(content);
        return GetHash(bytes);
    }

    static Hash GetHash(
        string content
    ) =>
        GetHash(System.Text.Encoding.ASCII.GetBytes(content));

    static Hash GetHash(
        params BigInteger[] values
    ) => GetHash(values.Select(v => v.ToByteArray(32)));
}
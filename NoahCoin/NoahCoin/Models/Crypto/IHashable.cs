namespace NoahCoin.Models.Crypto;

public interface IHashable
{
    Hash GetHash();

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
        params byte[][] content
    ) =>
        GetHash((IEnumerable<byte[]>)content);

    public static Hash GetHash(
        params IHashable[] hashes
    ) =>
        GetHash(hashes.Select(h => h.GetHash().Value));

    public static Hash GetHash(
        IEnumerable<IHashable> objs
    ) => GetHash(objs.Select(x => x.GetHash().Value));
    
    public static Hash GetHash(
        int content
    ) =>
        GetHash(BitConverter.GetBytes(content));

    static Hash GetHash(
        string content
    ) =>
        GetHash(System.Text.Encoding.ASCII.GetBytes(content));

    static Hash GetHash(
        params BigInteger[] values
    ) => GetHash(values.Select(v => v.ToByteArray(32)));
}
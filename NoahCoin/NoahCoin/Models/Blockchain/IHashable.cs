namespace NoahCoin.Models.Blockchain;

public interface IHashable
{
    byte[] Hash();

    /* Default hash implementation helpers */

    public static byte[] Hash(params byte[][] content)
    {
        var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        foreach (byte[] chunk in content)
        {
            hasher.AppendData(chunk);
        }
        return hasher.GetHashAndReset();
    }

    public static byte[] Hash(IEnumerable<byte[]> content)
    {
        var hasher = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
        foreach (byte[] chunk in content)
        {
            hasher.AppendData(chunk);
        }
        return hasher.GetHashAndReset();
    }

    public static byte[] Hash(int content)
    {
        byte[] bytes = BitConverter.GetBytes(content);
        return IHashable.Hash(bytes);
    }
}
namespace NoahCoin.Extensions;

public static class ByteArrayExtensions
{
    public static string ToHexString(this byte[] self)
    {
        return Convert.ToHexString(self);
    }

}
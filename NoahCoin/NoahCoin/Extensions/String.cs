using System.Text;

namespace NoahCoin.Extensions;

public static class StringExtensions
{
    public static byte[] ToBytes(this string self)
    {
        return Encoding.ASCII.GetBytes(self);
    }
}
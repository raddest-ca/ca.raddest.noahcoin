using System.Text;

namespace NoahCoin.Extensions;

public static class StringExtensions
{
    public static byte[] ToBytes(this string self)
    {
        return Encoding.ASCII.GetBytes(self);
    }

    public static string Repeat(this string self, int amount)
    {
        return new StringBuilder(self.Length * amount)
            .Insert(0, self, amount)
            .ToString();
    }
}
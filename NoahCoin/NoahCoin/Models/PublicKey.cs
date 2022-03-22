using System.Security.Cryptography;

namespace NoahCoin.Models;

public record PublicKey : Point
{
    
    public PublicKey(Point original) : base(original)
    {
    }

    public PublicKey(Curve Curve, BigInteger X, BigInteger Y) : base(Curve, X, Y)
    {
    }

    public byte[] Encode(bool Compressed, bool DoHash = false)
    {
        byte[] pkb;
        if (Compressed)
        {
            byte prefix;
            // avoiding ternary because value becomes an int instead of a byte lol
            if (Y % 2 == 0)
                prefix = 0x02;
            else
                prefix = 0x03;
            pkb = X.ToByteArray(32).Prepend(prefix);
        }
        else
        {
            pkb = X.ToByteArray(32).Prepend((byte) 0x04).Concat(Y.ToByteArray(32));
        }

        if (DoHash)
        {
            using SHA256 first = SHA256.Create();
            using RIPEMD160 second = RIPEMD160.Create();
            return second.ComputeHash(first.ComputeHash(pkb));
        }
        else
        {
            return pkb;
        }
    }

    public string GetAddress(string net, bool Compressed)
    {
        var pkb_hash = Encode(Compressed, DoHash: true);
        byte version = net switch 
        {
            "main" => 0x00,
            "test" => 0x6f,
            _ => throw new ArgumentException(nameof(net), $"Invalid value: {net}"),
        };
        var ver_pkb_hash = pkb_hash.Prepend(version);
        using var sha256 = SHA256.Create();
        var checksum = sha256.ComputeHash(sha256.ComputeHash(ver_pkb_hash)).Take(4);
        var byte_address = ver_pkb_hash.Concat(checksum);
        var b58check_address = b58encode(byte_address.ToArray());
        return b58check_address;
    }

    private static readonly string Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

    private static string b58encode(byte[] bytes)
    {
        if (bytes.Count() != 25) throw new ArgumentException(nameof(bytes), "Must be length 25");
        BigInteger n = new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
        List<char> chars = new(){};
        while (n > 0)
        {
            n = BigInteger.DivRem(n, 58, out var i);
            chars.Add(Alphabet[(int) i]);
        }
        chars.Reverse();
        var leadingZeroCount = bytes.TakeWhile(b => b == 0x0).Count();
        var result = Alphabet[0].ToString().Repeat(leadingZeroCount) + new string(chars.ToArray());
        return result;
    }
}
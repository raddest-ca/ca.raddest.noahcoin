namespace NoahCoin.Models.Crypto;

public record Curve(
    BigInteger p,
    BigInteger a,
    BigInteger b
)
{
    public static Curve bitcoin_curve = new Curve(
        p: "0FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFEFFFFFC2F".ParseHex(),
        a: "00000000000000000000000000000000000000000000000000000000000000000".ParseHex(), // a = 0
        b: "00000000000000000000000000000000000000000000000000000000000000007".ParseHex() // b = 7
    );
}
using System;
using System.Security.Cryptography;
using System.Text;

namespace NoahCoin.Tests;

public class BuiltinTests
{
    [Fact]
    public void EmptySHA256Hash()
    {
        using SHA256 mySHA256 = SHA256.Create();
        var result = mySHA256.ComputeHash(new byte[0]).ToHexString();
        Assert.Equal("e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855".ToUpperInvariant(), result);
    }

    [Fact]
    public void ABCSHA256Hash()
    {
        // https://www.di-mgt.com.au/sha_testvectors.html
        using SHA256 mySHA256 = SHA256.Create();
        // var resultBytes = mySHA256.ComputeHash(new byte[]{0x61, 0x62, 0x63});
        var result = mySHA256.ComputeHash("abc".ToBytes()).ToHexString();
        Assert.Equal("ba7816bf 8f01cfea 414140de 5dae2223 b00361a3 96177a9c b410ff61 f20015ad".Replace(" ", "").ToUpperInvariant(), result);
    }

    [Fact]
    public void RIPEMD160Hash()
    {
        using RIPEMD160 myHasher = RIPEMD160Managed.Create();
        var result = myHasher.ComputeHash("hello this is a test".ToBytes()).ToHexString();
        Assert.Equal("f51960af7dd4813a587ab26388ddab3b28d1f7b4".ToUpperInvariant(), result);
    }
}
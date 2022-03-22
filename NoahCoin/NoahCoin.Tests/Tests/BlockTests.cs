namespace NoahCoin.Tests;

public class BlockTests
{
    [Fact]
    public void SerializeTest()
    {
        Block b = new Block(
            BigInteger.Zero,
            new Transaction[]{
                new Transaction(
                    BigInteger.One,
                    new BigInteger(2),
                    new BigInteger(25)
                )
            },
            BigInteger.Zero
        );
        var result = b.Hash().ToHexString();
        var expected = "EB3C1A6F5DDB4BEE159CAE986A76D3D8843C7D4420BA9D27C8C589C38DB8A1DA";
        Assert.Equal(expected, result);
    }

    [Fact]
    public void MineTest()
    {
        Miner m = new Miner(BigInteger.Zero, BigInteger.Zero);
        Assert.Equal(0x0, m.MineBlock().Hash()[0]);
    }

    //  todo: test isValid method
}
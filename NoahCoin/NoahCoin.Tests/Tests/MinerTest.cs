namespace NoahCoin.Tests;

public class MinerTests 
{
    [Fact]
    public void MineTest()
    {
        Miner m = new Miner(BigInteger.Zero, BigInteger.Zero){
            Difficulty = 1
        };
        Assert.Equal(0x0, m.MineBlock().Hash()[0]);
    }

    [Fact]
    public void IsValidTest1()
    {
        Miner m = new Miner(BigInteger.Zero, BigInteger.Zero);
        Block b = m.MineBlock();
        b.Nonce--; // if the previous block was valid, it should have been returned by the mine function instead
        Assert.False(m.IsValid(b.Hash()));
    }

    
    [Fact]
    public void IsValidTest2()
    {
        Miner m = new Miner(BigInteger.Zero, BigInteger.Zero){
            Difficulty = 1
        };
        Assert.False(m.IsValid(new byte[]{0x10, 0x00}));
    }
    
    
    [Fact]
    public void IsValidTest3()
    {
        Miner m = new Miner(BigInteger.Zero, BigInteger.Zero){
            Difficulty = 2
        };
        Assert.False(m.IsValid(new byte[]{0x01, 0x00}));
    }
    
    [Fact]
    public void IsValidTest4()
    {
        Miner m = new Miner(BigInteger.Zero, BigInteger.Zero){
            Difficulty = 2
        };
        Assert.True(m.IsValid(new byte[]{0x00, 0x10}));
    }
}
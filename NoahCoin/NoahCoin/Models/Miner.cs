using System.Security.Cryptography;

namespace NoahCoin;

public class Miner
{
    public int Difficulty = 1;
    public BigInteger Owner { get; init; }
    public Block ActiveBlock { get; init; }

    public Miner(BigInteger owner, BigInteger previousBlockId)
    {
        Owner = owner;
        ActiveBlock = new Block(
            previousBlockId,
            new Transaction[]{
                new RewardTransaction(
                    owner,
                    new BigInteger(25)
                )
            },
            BigInteger.Zero
        );
    }

    public Block MineBlock()
    {
        using var Hasher = SHA256.Create();
        while (!IsValid(ActiveBlock.Hash()))
        {
            ActiveBlock.Nonce++;
        }
        return ActiveBlock;
    }

    public bool IsValid(byte[] hash)
    {
        int zeroCount = 0;
        foreach (var b in hash)
        {
            if (b == 0x0)
            {
                zeroCount += 2;
            }
            else if (b <= 0x0F)
            {
                zeroCount += 1;
                break;
            }
            else
            {
                break;
            }
        }
        return zeroCount >= Difficulty;
    }
}
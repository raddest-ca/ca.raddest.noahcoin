using NoahCoin.Models.Datastructures;

namespace NoahCoin.Models.Blockchain;

public class Miner
{
    public Block Block { get; set; }
    public int Difficulty { get; set; } = 1;

    public Block MineBlock()
    {
        using var hasher = SHA256.Create();
        while (!IsValid(Block.GetHash()))
        {
            Block = Block with { Nonce = Block.Nonce + 1 };
        }

        return Block;
    }

    public bool IsValid(
        Hash hash
    )
    {
        int zeroCount = 0;
        foreach (var b in hash.Value)
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
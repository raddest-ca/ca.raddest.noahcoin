namespace NoahCoin.Models.Blockchain;

public class Miner
{
    public int Difficulty { get; set; } = 1;
    public BigInteger Owner { get; init; }
    public Block ActiveBlock { get; set; }

    public Miner(BigInteger owner, BigInteger previousBlockId)
    {
        Owner = owner;
        ActiveBlock = new Block() {
            Header = new (){
                IsGenesisBlock = true,
                Transactions = new HashPointer<Transaction>[]{new(new (){
                    Sender = Owner,
                    Receiver = Owner,
                    Amount = new BigInteger(25)
                })},
            },
            Nonce = BigInteger.Zero,
        };
    }

    public Block MineBlock()
    {
        using var Hasher = SHA256.Create();
        while (!IsValid(ActiveBlock.Hash()))
        {
            ActiveBlock = ActiveBlock with {Nonce = ActiveBlock.Nonce+1};
        }
        return ActiveBlock;
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
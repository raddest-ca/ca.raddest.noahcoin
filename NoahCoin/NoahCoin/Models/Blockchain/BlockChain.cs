namespace NoahCoin.Models.Blockchain;

public record BlockChain
{
    public Block[] Blocks { get; init; }
    public bool IsValid
    {
        get
        {
            if (Blocks.Length == 0) return true;
            return Blocks.Last().IsValid;
        }
    }
}
using System.Collections;

namespace NoahCoin.Models.Blockchain;

public record BlockChain : IEnumerable<HashPointer<Block>>
{
    public int Difficulty { get; init; } = 1;
    public int Reward { get; init; } = 25;

    public HashPointer<Block> Head { get; init; } = HashPointer<Block>.GetNullPointer();

    public bool IsValid()
    {
        foreach (var bp in this)
        {
            if (!bp.Reference.IsValid) return false;
            foreach (var txp in bp.Reference)
            {
                if (!txp.IsValid) return false;
                if (!txp.Reference.IsValid(this)) return false;
            }
        }
        return true;
    }
    
    public BlockChain Append(
        HashPointer<Block> blockPointer
    ) =>
        this with
        {
            Head = blockPointer with
            {
                Reference = blockPointer.Reference with
                {
                    Header = blockPointer.Reference.Header with
                    {
                        PreviousBlock = Head
                    }
                }
            }
        };

    public HashPointer<Block>? GetBlock(
        Hash blockHash
    ) =>
        this.FirstOrDefault(b => b.GetHash() == blockHash);

    public IEnumerator<HashPointer<Block>> GetEnumerator()
    {
        return _blocks().GetEnumerator();
    }

    private IEnumerable<HashPointer<Block>> _blocks()
    {
        var p = Head;
        while (!p.IsNull)
        {
            yield return p;
            p = p.Reference.PreviousBlock;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
using System.Collections;

namespace NoahCoin.Models.Blockchain;

public record Block : IHashable, IEnumerable<HashPointer<Transaction>>
{
    public HashPointer<Block> PreviousBlock => Header.PreviousBlock;

    public BlockHeader Header { get; init; } = new();
    public BigInteger Nonce { get; init; } = BigInteger.Zero;

    public bool IsValid => Header.IsValid;

    public IEnumerator<HashPointer<Transaction>> GetEnumerator()
    {
        return _transactions().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Hash GetHash()
    {
        return Header.GetHash().Concat(Nonce.GetHash());
    }

    public Block Append(
        HashPointer<Transaction> tx
    )
    {
        return this with { Header = Header.Append(tx) };
    }

    public Transaction? GetTransaction(
        int index,
        Hash txHash
    )
    {
        return Header.GetTransaction(index, txHash);
    }

    private IEnumerable<HashPointer<Transaction>> _transactions()
    {
        foreach (var x in Header.Transactions)
            if (x.Reference is Transaction t)
                yield return new HashPointer<Transaction>(t);
    }
}
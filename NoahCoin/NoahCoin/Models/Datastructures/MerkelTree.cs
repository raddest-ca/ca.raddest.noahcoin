using System.Collections;
using System.Net;

namespace NoahCoin.Models.Datastructures;

public record MerkelTree : IEnumerable<IHashable>, IHashable
{
    public HashPointer<IHashable>? Left { get; init; }
    public HashPointer<IHashable>? Right { get; init; }

    public MerkelTree()
    {
    }

    public MerkelTree(
        IHashable? left,
        IHashable? right
    )
    {
        Left = left == null ? null : new(left);
        Right = right == null ? null : new(right);
    }

    public MerkelTree Append(
        IHashable value
    )
    {
        IEnumerable<IHashable?> toJoin = (IEnumerable<IHashable?>) this.Concat(new []{value});
        while (true)
        {
            List<MerkelTree> pairs = new();
            var iter = toJoin.GetEnumerator();
            IHashable? prev = null;
            while (iter.MoveNext())
            {
                if (prev == null) prev = iter.Current;
                else
                {
                    pairs.Add(new(prev, iter.Current));
                    prev = null;
                }
            }

            if (prev != null) pairs.Add(new (prev, null));
            if (pairs.Count == 1) return pairs[0];
            toJoin = pairs;
        }

        // if (Left == null) return this with { Left = new(value)};
        // if (Right == null) return this with { Right = new(value) };
        // return new MerkelTree<T>();
    }

    public Hash GetHash()
    {
        if (Left == null && Right == null) return new();
        if (Left == null) return Right!.GetHash();
        if (Right == null) return Left!.GetHash();
        return IHashable.GetHash(Left, Right);
    }

    public IEnumerator<IHashable> GetEnumerator()
    {
        return new MerkelTreeEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class MerkelTreeEnumerator : IEnumerator<IHashable>
{
    private readonly MerkelTree _tree;
    private readonly List<HashPointer<IHashable>?> _toVisit = new();

    public MerkelTreeEnumerator(
        MerkelTree tree
    )
    {
        _tree = tree;
        Reset();
    }

    public bool MoveNext()
    {
        if (!_toVisit.Any()) return false;
        
        var visiting = _toVisit.Pop();
        if (visiting == null) return MoveNext();
        
        if (visiting.Reference is MerkelTree tree)
        {
            _toVisit.Insert(0, tree.Right);
            _toVisit.Insert(0, tree.Left);
            return MoveNext();
        }
        
        Current = visiting.Reference;
        return true;
    }

    public void Reset()
    {
        _toVisit.Clear();
        _toVisit.Add(_tree.Left);
        _toVisit.Add(_tree.Right);
    }

    public IHashable Current { get; set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}
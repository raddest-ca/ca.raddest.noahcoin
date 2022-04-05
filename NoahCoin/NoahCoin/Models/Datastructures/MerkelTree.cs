using System.Collections;
using System.Net;

namespace NoahCoin.Models.Datastructures;

public record MerkelTree : IEnumerable<HashPointer<IHashable>>, IHashable
{
    public HashPointer<IHashable> Left { get; init; } =
        HashPointer<IHashable>.GetNullPointer<IHashable>();

    public HashPointer<IHashable> Right { get; init; } =
        HashPointer<IHashable>.GetNullPointer<IHashable>();

    private readonly int _count = 0;

    public int Count => _count;

    public MerkelTree()
    {
    }

    private MerkelTree(
        int count
    )
    {
        _count = count;
    }

    public MerkelTree Append(
        HashPointer<IHashable> value
    )
    {
        IEnumerable<HashPointer<IHashable>> toJoin =
            this.Concat(new[] { value }).Where(x => !x.IsNull);
        List<MerkelTree> subtrees = new();
        {
            HashPointer<IHashable>? prev = null;
            foreach (var hashPointer in toJoin)
            {
                if (prev == null)
                    prev = hashPointer;
                else
                {
                    subtrees.Add(new(2) { Left = prev, Right = hashPointer });
                    prev = null;
                }
            }

            if (prev != null) subtrees.Add(new(1) { Left = prev });
        }
        while (subtrees.Count > 1)
        {
            List<MerkelTree> next = new();
            MerkelTree? prev = null;
            foreach (var tree in subtrees)
            {
                if (prev == null)
                    prev = tree;
                else
                {
                    next.Add(
                        new(prev.Count + tree.Count)
                            { Left = new(prev), Right = new(tree) }
                    );
                    prev = null;
                }
            }

            if (prev != null) next.Add(new(prev.Count) { Left = new(prev) });
            subtrees = next;
        }

        return subtrees[0];
    }

    public HashPointer<IHashable> this[
        int index
    ]
    {
        get
        {
            HashPointer<IHashable> layer = new(this);
            while (layer.Reference is MerkelTree tree)
            {
                if (tree.Left.Reference is MerkelTree left)
                {
                    if (index < left.Count)
                        layer = tree.Left;
                    else
                    {
                        layer = tree.Right;
                        index -= left.Count;
                    }
                }
                else
                {
                    layer = index == 0 ? tree.Left : tree.Right;
                }
            }

            return layer;
        }
    }

    public bool IsMember(
        int index,
        Hash hash
    )
    {
        return this[index].GetHash() == hash;
    }

    public Hash GetHash()
    {
        if (Left.IsNull && Right.IsNull) return new();
        if (Left.IsNull) return Right!.GetHash();
        if (Right.IsNull) return Left!.GetHash();
        return IHashable.GetHash(Left, Right);
    }

    public IEnumerator<HashPointer<IHashable>> GetEnumerator()
    {
        return new MerkelTreeEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool IsValid => this.All(x => x.IsValidOrNull);
}

public class MerkelTreeEnumerator : IEnumerator<HashPointer<IHashable>>
{
    private readonly MerkelTree _tree;
    private readonly List<HashPointer<IHashable>> _toVisit = new();

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
        if (visiting.IsNull) return MoveNext();

        if (visiting.Reference is MerkelTree tree)
        {
            _toVisit.Insert(0, tree.Right);
            _toVisit.Insert(0, tree.Left);
            return MoveNext();
        }

        Current = visiting;
        return true;
    }

    public void Reset()
    {
        _toVisit.Clear();
        _toVisit.Add(_tree.Left);
        _toVisit.Add(_tree.Right);
    }

    public HashPointer<IHashable> Current { get; set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}
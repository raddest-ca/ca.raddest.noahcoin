namespace NoahCoin.Models.Blockchain;


// WIP

public class MerkelTree
{

}

public class MerkelTreeNode { }

public class MerkelTreeBranchNode : MerkelTreeNode
{
    public MerkelTreeBranchNode(MerkelTreeNode left, MerkelTreeNode right)
    {
        Left = left;
        Right = right;
    }

    public MerkelTreeNode Left { get; init; }

    public MerkelTreeNode Right { get; init; }
}

public class MerkelTreeLeafNode : MerkelTreeNode
{
    public object Data { get; init; }

    public MerkelTreeLeafNode(object data)
    {
        Data = data;
    }
}
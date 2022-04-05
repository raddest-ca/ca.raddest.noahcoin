using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace NoahCoin.Tests;

public class MerkelTreeTests
{
    [Fact]
    public void EmptyTree()
    {
        var tree = new MerkelTree();
        Assert.Equal(new(), tree.GetHash());
    }

    [Fact]
    public void SoloTree()
    {
        var tree = new MerkelTree();
        var key = new PrivateKey(1);
        tree = tree.Append(new(key));
        Assert.Equal(key.GetHash(), tree.GetHash());
    }

    [Fact]
    public void TenTree()
    {
        var tree = new MerkelTree();
        var max = 10;
        List<PrivateKey> keys = new();
        int i;
        for (i = 0; i < max; i++)
        {
            var key = new PrivateKey(i);
            tree = tree.Append(new(key));
            keys.Add(key);
        }

        i = 0;
        foreach (var entry in tree)
        {
            Assert.Equal(keys[i], entry.Reference);
            i++;
        }
        Assert.Equal(keys.Count, i);
        Assert.Equal(keys.Count, max);
        Assert.Equal(max, i);
        Assert.Equal(max, tree.Count());
    }

    [Fact]
    public void Validate()
    {
        HashPointer<IHashable> left = new(new Hash());
        HashPointer<IHashable> right = new(){Hash = new Hash(new byte[]{0x00, 0x10})};
        MerkelTree tree = new()
        {
            Left = left,
            Right = right
        };

        Assert.True(tree.IsValid);

        // change hash value to no longer match reference
        var badTree = tree with
        {
            Left = left with { Hash = IHashable.GetHash(1) }
        };
        Assert.False(badTree.IsValid);
        
        // change reference to no longer match hash value
        badTree = tree with
        {
            Left = left with { Reference = IHashable.GetHash(1) }
        };
        Assert.False(badTree.IsValid);
    }

    [Fact]
    public void Count()
    {
        var tree = new MerkelTree();
        Assert.Equal(0, tree.Count);
        int max = 10;
        for (var i = 0; i < max; i++)
        {
            tree = tree.Append(new(new Hash()));
            Assert.Equal(i+1, tree.Count);
        }
    }
}
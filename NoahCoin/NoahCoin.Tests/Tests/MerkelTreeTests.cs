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

        // Assert.Equal(max, tree.Count());
    }
}
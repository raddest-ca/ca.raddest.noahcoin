namespace NoahCoin.Models.Blockchain;

public record ScriptEvaluator
{
    public Transaction Transaction { get; }
    public BlockChain BlockChain { get; }

    private readonly Stack<string> _stack = new();

    private Dictionary<string, Func<bool>> _handlers = new();

    public bool TryValidate()
    {
        try
        {
            return Validate();
        }
        catch (Exception)
        {
            throw;
            // return false;
        }
    }

    public bool Validate()
    {
        var script = Transaction.Inputs.Select(
                input =>
                    input.Script + "\n" + input.GetPreviousScript(BlockChain)
                    ?? ""
            )
            .Append("")
            .Aggregate(
                (
                    a,
                    b
                ) => a + "\n" + b
            );
        var lines = script.Replace(Environment.NewLine, " ")
            .Replace("\n", " ")
            .Split(" ", StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            if (_handlers.ContainsKey(line))
            {
                if (!_handlers[line].Invoke()) return false;
            }
            else
            {
                _stack.Push(line);
            }
        }

        return !_stack.Any();
    }

    public ScriptEvaluator(
        BlockChain bc,
        Transaction transaction
    )
    {
        Transaction = transaction;
        BlockChain = bc;
        _handlers.Add(nameof(OP_DUP), OP_DUP);
        _handlers.Add(nameof(OP_GETADDRESS), OP_GETADDRESS);
        _handlers.Add(nameof(OP_EQUALVERIFY), OP_EQUALVERIFY);
        _handlers.Add(nameof(OP_CHECKSIG), OP_CHECKSIG);
    }

    private bool OP_DUP()
    {
        _stack.Push(_stack.Peek());
        return true;
    }

    private bool OP_GETADDRESS()
    {
        var encoded = _stack.Pop();
        var publicKey = new PublicKey
        {
            Point = Point.Decode(encoded)
        };
        var address = publicKey.GetAddress();
        _stack.Push(address);
        return true;
    }

    private bool OP_EQUALVERIFY()
    {
        var a = _stack.Pop();
        var b = _stack.Pop();
        return a == b;
    }

    private bool OP_CHECKSIG()
    {
        var publicKey = PublicKey.Decode(_stack.Pop());
        var signature = Signature.Decode(_stack.Pop());
        var rtn = signature.IsValid(publicKey, Transaction.GetPreSignedHash());
        return rtn;
    }
}
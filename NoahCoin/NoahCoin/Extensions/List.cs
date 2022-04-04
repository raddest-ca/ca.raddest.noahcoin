namespace NoahCoin.Extensions;

public static class ListExtensions
{
    public static T Pop<T>(
        this List<T> self
    )
    {
        var rtn = self[0];
        self.RemoveAt(0);
        return rtn;
    }
}
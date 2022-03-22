namespace NoahCoin.Extensions;

public static class ArrayExtensions
{
    public static T[] Concat<T>(this T[] x, T[] y)
    {
        if (x == null) throw new ArgumentNullException("x");
        if (y == null) throw new ArgumentNullException("y");
        int oldLen = x.Length;
        Array.Resize<T>(ref x, x.Length + y.Length);
        Array.Copy(y, 0, x, oldLen, y.Length);
        return x;
    }

    public static T[] Prepend<T>(this T[] x, T y)
    {
        return Concat(new T[]{y}, x);
    }

    public static T[] Append<T>(this T[] x, T y)
    {
        return Concat(x, new T[]{y});
    }
}
using System.Collections;
using System.Collections.Generic;

namespace NoahCoinService.Tests;

public class Range100: IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        for (BigInteger i = 0; i<100; i++)
            yield return new object[]{i};
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
using System.Collections;
using System.Collections.Generic;

namespace NoahCoinService.Tests;

public class ModTestRange: IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        for (BigInteger i = -100; i<100; i++)
            yield return new object[]{i};
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
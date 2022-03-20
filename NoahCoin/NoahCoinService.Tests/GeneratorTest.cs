using Xunit;

namespace NoahCoinService.Tests;

public class GeneratorTest
{
    [Fact]
    public void GeneratorIsOnCurve()
    {
        Assert.True(Generator.Default.Point.IsMember());
    }
}
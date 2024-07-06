using FluentAssertions;

namespace Proxoft.ValueObjects.Tests;

public class IntValueObjectTest
{
    [Fact]
    public void CreateSpecialIntValueObject()
    {
        SpecialInt v1 = new(1);
        Assert.Equal(1, v1);
        
        SpecialInt v5 = new(5);
        Assert.Equal(5, v5);

        Assert.Equal(-1, SpecialInt.Null);
    }

    [Fact]
    public void CreateSpecialIntValueObjectOutOfRange()
    {
        Action action = () =>
        {
            SpecialInt v0 = new(0);
        };

        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}

internal class SpecialInt(int value) : IntValueObject<SpecialInt>(value, Guard)
{
    public static readonly SpecialInt Null = new(-1);

    private static void Guard(int value)
    {
        if(value == -1)
        {
            return;
        }

        GuardFunctions.ThrowIfNotInRange(value, 1, 5);
    }
}

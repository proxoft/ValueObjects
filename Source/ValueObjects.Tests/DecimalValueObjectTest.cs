using FluentAssertions;

namespace Proxoft.ValueObjects.Tests;

public class DecimalValueObjectTest
{
    [Fact]
    public void CreateLat()
    {
        Lat lat = new(48.568m);
        Assert.Equal(48.568m, lat);
    }

    [Fact]
    public void CreateUndefinedLat()
    {
        Lat undefined = new(-100);
        undefined
            .Should()
            .BeEquivalentTo(Lat.Undefined);
    }

    [Fact]
    public void CreateLatOutOfRange()
    {
        Action action = () =>
        {
            Lat v0 = new(-91);
        };

        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}

internal class Lat(decimal value) : DecimalValueObject<Lat>(
    value,
    Guard)
{
    public static readonly Lat Undefined = new(-100);

    private static void Guard(decimal value)
    {
        if (value == -100)
        {
            return;
        }

        GuardFunctions.ThrowIfNotInRange(value, -90m, 90m);
    }
}

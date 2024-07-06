using FluentAssertions;

namespace Proxoft.ValueObjects.Tests;

public class StringValueObjectTest
{
    [Fact]
    public void NullValueIsConvertedToAbc()
    {
        ConvertToAbcStringValueObject vo = new(null);
        Assert.Equal("abc", vo);
    }

    [Fact]
    public void ThrowsIfLengthIsMoreThan10()
    {
        Action action = () => {
            ConvertToAbcStringValueObject vo = new(new string('a', 11));
        };

        action.Should().Throw<Exception>();
    }

    [Fact]
    public void ValueComparerTest()
    {
        IgnoreCaseStringValueObject s1 = new("Abc");
        IgnoreCaseStringValueObject s2 = new("abC");

        s1.Should().BeEquivalentTo(s2);
    }
}

public class ConvertToAbcStringValueObject(string? value) : StringValueObject<ConvertToAbcStringValueObject>(value, v => GuardFunctions.ThrowIfLength(v, 10), v => v ?? "abc")
{
}

public class IgnoreCaseStringValueObject(string? value) : StringValueObject<IgnoreCaseStringValueObject>(
    value,
    valueComparer: (s1, s2) => s1?.ToLower() == value?.ToLower())
{
}
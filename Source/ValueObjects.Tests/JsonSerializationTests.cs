using System.Text.Json;
using FluentAssertions;
using Proxoft.ValueObjects.JsonSerialization;

namespace Proxoft.ValueObjects.Tests;

public class JsonSerializationTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = {
        new IntValueObjectConverter<TestValueObject>()
    }
    };

    [Fact]
    public void SerializeIntValueObject()
    {
        TestValueObject tvo = new(10);
        string json = JsonSerializer.Serialize(tvo, _options);
        

        json
            .Should()
            .Be("10");
    }

    [Fact]
    public void DeserializeIntValueObject()
    {
        TestValueObject? deserialized = JsonSerializer.Deserialize<TestValueObject>("10", _options);
        deserialized.Should()
           .NotBeNull();

        deserialized!
            .Should()
            .BeEquivalentTo(new TestValueObject(10));
    }
}

public class TestValueObject(int value) : IntValueObject<TestValueObject>(value, minValue: 5)
{
}

using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;
using Proxoft.ValueObjects.JsonSerialization;
using Proxoft.ValueObjects.Tests.Types;

namespace Proxoft.ValueObjects.Tests;

public class DefaultValueObjectJsonConverterTest
{
    private readonly DefaultValueObjectJsonConverter _converter = new();
    private readonly JsonSerializerOptions _options = new();

    [Theory]
    [InlineData(typeof(CharSample), typeof(CharValueObjectJsonConverter<CharSample>))]
    [InlineData(typeof(DecimalSample), typeof(DecimalValueObjectJsonConverter<DecimalSample>))]
    [InlineData(typeof(DoubleSample), typeof(DoubleValueObjectJsonConverter<DoubleSample>))]
    [InlineData(typeof(FloatSample), typeof(FloatValueObjectJsonConverter<FloatSample>))]
    [InlineData(typeof(GuidSample), typeof(GuidValueObjectJsonConverter<GuidSample>))]
    [InlineData(typeof(IntSample), typeof(IntValueObjectJsonConverter<IntSample>))]
    [InlineData(typeof(LongSample), typeof(LongValueObjectJsonConverter<LongSample>))]
    [InlineData(typeof(NullableStringSample), typeof(NullableStringValueObjectJsonConverter<NullableStringSample>))]
    [InlineData(typeof(ShortSample), typeof(ShortValueObjectJsonConverter<ShortSample>))]
    [InlineData(typeof(StringSample), typeof(StringValueObjectJsonConverter<StringSample>))]
    [InlineData(typeof(UIntSample), typeof(UIntValueObjectJsonConverter<UIntSample>))]
    [InlineData(typeof(ULongSample), typeof(ULongValueObjectJsonConverter<ULongSample>))]
    public void CanConvert(Type typeToConvert, Type expectedConverterType)
    {
        bool canConvert = _converter.CanConvert(typeToConvert);
        canConvert.Should().BeTrue();

        JsonConverter? converter = _converter.CreateConverter(typeToConvert, _options);
        converter
            .Should()
            .NotBeNull();

        converter!.GetType()
            .Should()
            .Be(expectedConverterType);
    }

    [Fact]
    public void CannotConvert_SampleString2()
    {
        bool canConvert = _converter.CanConvert(typeof(SampleString2));
        canConvert.Should().BeFalse();
    }

    [Theory]
    [InlineData(typeof(CharSample))]
    [InlineData(typeof(DecimalSample))]
    [InlineData(typeof(DoubleSample))]
    [InlineData(typeof(FloatSample))]
    [InlineData(typeof(GuidSample))]
    [InlineData(typeof(IntSample))]
    [InlineData(typeof(LongSample))]
    [InlineData(typeof(NullableStringSample))]
    [InlineData(typeof(ShortSample))]
    [InlineData(typeof(StringSample))]
    [InlineData(typeof(UIntSample))]
    [InlineData(typeof(ULongSample))]
    public void GivenTypeIsIgnored_ThenCannotConvert(Type typeToConvert)
    {
        DefaultValueObjectJsonConverter converter = new(ignoreTypes: [typeToConvert]);
        bool canConvert = converter.CanConvert(typeToConvert);
        canConvert.Should().BeFalse();
    }

    [Theory]
    [InlineData(typeof(CharSample))]
    [InlineData(typeof(DecimalSample))]
    [InlineData(typeof(DoubleSample))]
    [InlineData(typeof(FloatSample))]
    [InlineData(typeof(GuidSample))]
    [InlineData(typeof(IntSample))]
    [InlineData(typeof(LongSample))]
    [InlineData(typeof(NullableStringSample))]
    [InlineData(typeof(ShortSample))]
    [InlineData(typeof(StringSample))]
    [InlineData(typeof(UIntSample))]
    [InlineData(typeof(ULongSample))]
    public void GivenIgnorePredicateForType_ThenCannotConvert(Type typeToConvert)
    {
        DefaultValueObjectJsonConverter converter = new(type => type == typeToConvert);
        bool canConvert = converter.CanConvert(typeToConvert);
        canConvert.Should().BeFalse();
    }
}

namespace Proxoft.ValueObjects.Tests.Types;

public class CharSample(char value) : CharValueObject<CharSample>(value)
{
}

internal class DecimalSample(decimal value) : DecimalValueObject<DecimalSample>(value)
{
}

internal class DoubleSample(double value) : DoubleValueObject<DoubleSample>(value)
{
}

internal class FloatSample(float value) : FloatValueObject<FloatSample>(value)
{
}

internal class GuidSample(Guid value) : GuidValueObject<GuidSample>(value)
{
}

internal class IntSample(int value) : IntValueObject<IntSample>(value)
{
}

internal class LongSample(long value) : LongValueObject<LongSample>(value)
{
}

public class NullableStringSample(string? value) : NullableStringValueObject<NullableStringSample>(value)
{
}

internal class ShortSample(short value) : ShortValueObject<ShortSample>(value)
{
}

public class StringSample(string? value) : StringValueObject<StringSample>(value)
{
}

internal class UIntSample(uint value) : UIntValueObject<UIntSample>(value)
{
}

internal class ULongSample(ulong value) : ULongValueObject<ULongSample>(value)
{
}

public class SampleString2(string value, int whatever) : StringValueObject<SampleString2>(value)
{
    public int Whatever { get; } = whatever;
}

using System.Diagnostics;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class LongValueObject<T> : ValueObject<T>, IComparable<T>
    where T : LongValueObject<T>
{
    private readonly long _value;

    protected LongValueObject(
        long value) : this(value, v => GuardFunctions.ThrowIfNotInRange(v))
    {
    }

    protected LongValueObject(
        long value,
        long minValue = long.MinValue,
        long maxValue = long.MaxValue
    ) : this(value, v => GuardFunctions.ThrowIfNotInRange(v, minValue, maxValue))
    {
    }

    protected LongValueObject(long value, Action<long>? guard = null)
    {
        guard?.Invoke(value);

        _value = value;
    }

    protected sealed override bool EqualsCore(T other)
    {
        return _value == other._value;
    }

    protected sealed override int GetHashCodeCore()
    {
        return _value.GetHashCode();
    }

    public int CompareTo(T? other)
    {
        ArgumentNullException.ThrowIfNull(other, nameof(other));

        if (this == other)
        {
            return 0;
        }

        return this < other
            ? -1
            : 1;
    }

    public override string ToString()
    {
        return _value.ToString();
    }

    public static implicit operator long(LongValueObject<T> value) => value._value;
}

using System.Diagnostics;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class ULongValueObject<T> : ValueObject<T>, IComparable<T>
    where T : ULongValueObject<T>
{
    private readonly ulong _value;

    protected ULongValueObject(
        ulong value) : this(value, v => GuardFunctions.ThrowIfNotInRange(v))
    {
    }

    protected ULongValueObject(
        ulong value,
        ulong minValue = ulong.MinValue,
        ulong maxValue = ulong.MaxValue
    ) : this(value, v => GuardFunctions.ThrowIfNotInRange(v, minValue, maxValue))
    {
    }

    protected ULongValueObject(ulong value, Action<ulong>? guard = null)
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

    public static implicit operator ulong(ULongValueObject<T> value) => value._value;
}

using System.Diagnostics;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class UIntValueObject<T> : ValueObject<T>, IComparable<T>
    where T : UIntValueObject<T>
{
    private readonly uint _value; 
    
    protected UIntValueObject(
        uint value) : this(value, v => GuardFunctions.ThrowIfNotInRange(v))
    {
    }

    protected UIntValueObject(
        uint value,
        uint minValue = uint.MinValue,
        uint maxValue = uint.MaxValue
        ) : this(value, v => GuardFunctions.ThrowIfNotInRange(v, minValue, maxValue))
    {
    }

    protected UIntValueObject(uint value, Action<uint>? guard = null)
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

    public static implicit operator uint(UIntValueObject<T> value) => value._value;
}

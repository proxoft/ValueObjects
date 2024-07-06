using System.Diagnostics;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class IntValueObject<T> : ValueObject<T>, IComparable<T>
    where T : IntValueObject<T>
{
    private readonly int _value; 
    
    protected IntValueObject(
        int value) : this(value, v => GuardFunctions.ThrowIfNotInRange(v))
    {
    }

    protected IntValueObject(
        int value,
        int minValue = int.MinValue,
        int maxValue = int.MaxValue
        ) : this(value, v => GuardFunctions.ThrowIfNotInRange(v, minValue, maxValue))
    {
    }

    protected IntValueObject(int value, Action<int>? guard = null)
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

    public static implicit operator int(IntValueObject<T> value) => value._value;
}

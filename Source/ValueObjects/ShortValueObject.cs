﻿using System.Diagnostics;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class ShortValueObject<T> : ValueObject<T>, IComparable<T>
    where T : ShortValueObject<T>
{
    private readonly short _value; 
    
    protected ShortValueObject(
        short value) : this(value, null)
    {
    }

    protected ShortValueObject(
        short value,
        short minValue = short.MinValue,
        short maxValue = short.MaxValue
        ) : this(value, v => GuardFunctions.ThrowIfNotInRange(v, minValue, maxValue))
    {
    }

    protected ShortValueObject(short value, Action<short>? guard = null)
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

    public static implicit operator short(ShortValueObject<T> value) => value._value;
}

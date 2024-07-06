using System.Diagnostics;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class CharValueObject<T> : ValueObject<T>, IComparable<T>
    where T: CharValueObject<T>
{
    private readonly char _value;

    protected CharValueObject(
        char value) : this(
            value,
            _ => { })
    {
    }

    protected CharValueObject(char value, Action<char>? guard)
    {
        guard?.Invoke(value);
        _value = value;
    }

    public bool IsWhiteSpace() => char.IsWhiteSpace(_value);

    protected sealed override int GetHashCodeCore()
    {
        return _value.GetHashCode();
    }

    protected sealed override bool EqualsCore(T other)
    {
        return _value == other._value;
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

    public static implicit operator char(CharValueObject<T> value)
    {
        return value._value;
    }
}

using System.Diagnostics;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class StringValueObject<T> : ValueObject<T>, IComparable<T>
    where T: StringValueObject<T>
{
    private readonly string _value;
    private readonly Func<string?, string?, bool> _valueComparer;

    protected StringValueObject(
        string? value) : this(
            value,
            _ => { })
    {
    }

    protected StringValueObject(
        string? value,
        int maxLength) : this(
            value,
            (string? v) => GuardFunctions.ThrowIfLength(v, maxLength))
    {
    }

    protected StringValueObject(
        string? value,
        Action<string?>? guard = null,
        Func<string?, string>? nullValueConversion = null,
        Func<string?, string?, bool>? valueComparer = null
    )
    {
        guard?.Invoke(value);
        _value = nullValueConversion?.Invoke(value) ?? GuardFunctions.NullToEmptyConversion(value);

        Func<string?, string?, bool> defaultStringComparer = (s1, s2) => s1 == s2;
        _valueComparer = valueComparer ?? defaultStringComparer;
    }

    public bool IsEmpty => _value == string.Empty;

    public bool IsNullOrWhitespace => string.IsNullOrWhiteSpace(_value);

    protected sealed override int GetHashCodeCore()
    {
        return _value?.GetHashCode() ?? 0;
    }

    protected sealed override bool EqualsCore(T other)
    {
        return _valueComparer(_value, other._value);
    }

    public int CompareTo(T? other)
    {
        ArgumentNullException.ThrowIfNull(other, nameof(other));

        return string.Compare(this, other);
    }

    public override string ToString()
    {
        return _value;
    }

    public static implicit operator string(StringValueObject<T> value)
    {
        return value._value;
    }
}

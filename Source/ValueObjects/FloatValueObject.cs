using System.Diagnostics;
using System.Globalization;

namespace Proxoft.ValueObjects;

[DebuggerDisplay("{this.GetType().Name}:{_value}")]
public abstract class FloatValueObject<T> : ValueObject<T>, IComparable<T>
    where T : FloatValueObject<T>
{
    private readonly float _value;
    private readonly float _equalityTolerance;

    protected FloatValueObject(
        float value,
        float equalityTolerance = 0.001f)
        : this(value, equalityTolerance, v => GuardFunctions.ThrowIfNotInRange(v))
    {
    }

    protected FloatValueObject(
        float value,
        float equalityTolerance = 0.001f,
        float min = float.MinValue,
        float max = float.MaxValue)
        : this(value, equalityTolerance, v => GuardFunctions.ThrowIfNotInRange(v, min, max))
    {
    }

    protected FloatValueObject(float value, float equalityTolerance = 0.001f, Action<float>? guard = null)
    {
        guard?.Invoke(value);

        _value = value;
        _equalityTolerance = equalityTolerance;
    }

    protected override bool EqualsCore(T other)
    {
        float diff = Math.Abs( _value - other._value );
        return diff <= _equalityTolerance ||
               diff <= Math.Max(Math.Abs(_value), Math.Abs(other._value)) * _equalityTolerance;
    }

    protected override int GetHashCodeCore()
    {
        return _value.GetHashCode();
    }

    public int CompareTo(T? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (Math.Abs(this - other) < _equalityTolerance)
        {
            return 0;
        }

        return this < other
            ? -1
            : 1;
    }

    public override string ToString()
    {
        return _value.ToString(CultureInfo.InvariantCulture);
    }

    public string ToString(string format)
    {
        return _value.ToString(format);
    }

    public static implicit operator float(FloatValueObject<T> valueObject)
    {
        return valueObject._value;
    }
}

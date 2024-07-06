namespace Proxoft.ValueObjects;

public abstract class ValueObject<T> : IEquatable<T>
    where T: ValueObject<T>
{
    protected abstract bool EqualsCore(T other);

    protected abstract int GetHashCodeCore();

    public sealed override int GetHashCode() => this.GetHashCodeCore();

    public bool Equals(T? other)
    {
        if (other is null)
        {
            return false;
        }

        return this.EqualsCore(other);
    }

    public sealed override bool Equals(object? obj)
    {
        return this.Equals(obj as T);
    }

    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
    {
        if (left is null && right is null)
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
    {
        return !(left == right);
    }
}

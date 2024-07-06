namespace Proxoft.ValueObjects;

public static class GuardFunctions
{
    public static void ThrowIfNotInRange(int value, int min = int.MinValue, int max = int.MaxValue)
    {
        if(value < min || value > max)
        {
            throw new ArgumentOutOfRangeException($"value {value} was expected to be between {min} and {max}");
        }
    }

    public static void ThrowIfNotInRange(long value, long min = long.MinValue, long max = int.MaxValue)
    {
        if(value < min || value > max)
        {
            throw new ArgumentOutOfRangeException($"value {value} was expected to be between {min} and {max}");
        }
    }

    public static void ThrowIfNotInRange(ulong value, ulong min = ulong.MinValue, ulong max = int.MaxValue)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException($"value {value} was expected to be between {min} and {max}");
        }
    }

    public static void ThrowIfNotInRange(double value, double min = double.MinValue, double max = double.MaxValue)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException($"value {value} was expected to be between {min} and {max}");
        }
    }
    
    public static void ThrowIfNotInRange(float value, float min = float.MinValue, float max = float.MaxValue)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException($"value {value} was expected to be between {min} and {max}");
        }
    }

    public static void ThrowIfNotInRange(decimal value, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException($"value {value} was expected to be between {min} and {max}");
        }
    }

    public static void ThrowIfLength(string? value, int maxLength)
    {
        ThrowIfLengthNotInRange(value, minLength: 0, maxLength: maxLength);
    }

    public static void ThrowIfLengthNotInRange(string? value, int minLength = 0, int maxLength = int.MaxValue)
    {
        if((value?.Length ?? 0) < minLength || (value?.Length ?? 0) > maxLength)
        {
            throw new ArgumentOutOfRangeException($"the length {value?.Length} was expected to be between {minLength} and {maxLength}");
        }
    }

    public static string NullToEmptyConversion(string? value)
    {
        return value ?? string.Empty;
    }
}
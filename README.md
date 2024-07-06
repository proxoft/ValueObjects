
## Value objects
What is and why should you use a value object: https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects

The library contains some base implementations of Int, String, NullableString, Decimal and Double value objects.

### Usage

``` C#
public class FooId: IntValueObject<FooId>
{
  public static readonly FooId None = new(0);
  
  public class FooId(int value) : base(value, minValue: 0)
  {
  }
  
  public FooId Next()
  {
    return new FooId(this + 1);
  }
}
```

```C#
public class Lat : DecimalValueObject<Lat>
{
    public static readonly Lat Undefined = new(-100); // whatever value outside of the range

    public Lat(decimal value) : base(
        value,
        Guard)
    {
    }

    private static void Guard(decimal value)
    {
        if(value == -100)
        {
            return;
        }

        GuardFunctions.ThrowIfNotInRange(value, -90m, 90m);
    }
}
```
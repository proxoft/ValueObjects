using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.ValueObjects.JsonSerialization;

public class DecimalValueObjectConverter<T> : JsonConverter<T> where T : DecimalValueObject<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        decimal n = reader.GetDecimal();
        return (T)Activator.CreateInstance(typeof(T), n)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.ValueObjects.JsonSerialization;

public class GuidValueObjectConverter<T> : JsonConverter<T> where T : GuidValueObject<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? n = reader.GetString();
        if(!Guid.TryParse(n, out Guid value))
        {
            throw new InvalidCastException($"Value {n} cannot be type casted to GUID");
        }

        return (T)Activator.CreateInstance(typeof(T), value)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.ValueObjects.JsonSerialization;

public class NullableStringValueObjectConverter<T> : JsonConverter<T> where T : NullableStringValueObject<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? n = reader.GetString();
        return (T)Activator.CreateInstance(typeof(T), n)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value);
    }
}

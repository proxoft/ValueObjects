using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.ValueObjects.JsonSerialization;

public class ULongValueObjectConverter<T> : JsonConverter<T> where T : ULongValueObject<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        ulong n = reader.GetUInt64();
        return (T)Activator.CreateInstance(typeof(T), n)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}

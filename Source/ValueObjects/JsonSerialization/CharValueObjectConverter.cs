using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.ValueObjects.JsonSerialization;

public class CharValueObjectConverter<T> : JsonConverter<T> where T : CharValueObject<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? value = reader.GetString();
        char c = value is null
            ? Char.MinValue
            : Convert.ToChar(value);

        return (T)Activator.CreateInstance(typeof(T), c)!;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

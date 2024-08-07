using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.ValueObjects.JsonSerialization;

/// <summary>
/// Initializes new instance of DefaultValueObjectJsonConverter.
/// </summary>
/// <param name="ignorePredicate">CanConvert returns false if ignorePredicate on typeToConvert returns true, otherwise returns by its own logic.</param>
public class DefaultValueObjectJsonConverter(Func<Type, bool> ignorePredicate) : JsonConverterFactory
{
    private readonly Func<Type, bool> _ignorePredicate = ignorePredicate;

    /// <summary>
    /// Initializes new instance of DefaultValueObjectJsonConverter
    /// </summary>
    public DefaultValueObjectJsonConverter() : this((Type _) => false)
    {
    }

    /// <summary>
    /// Initializes new instance of DefaultValueObjectJsonConverter
    /// </summary>
    /// <param name="ignoreTypes">CanConvert will return false if typeToConvert is listed in ignoreTypes. Comparison ignores inheritance.</param>
    public DefaultValueObjectJsonConverter(Type[] ignoreTypes) : this((Type typeToConvert) => ignoreTypes.Contains(typeToConvert))
    {
    }

    public override bool CanConvert(Type typeToConvert)
    {
        if (_ignorePredicate(typeToConvert))
        {
            return false;
        }

        ConstructorInfo[] constructors = typeToConvert.GetConstructors();
        if(!constructors.Where(c => c.GetParameters().Length == 1).Any())
        {
            return false;
        }

        bool inheritsFromValueObjectType = TypesAndConverters.InheritsFromValueObjectType(typeToConvert);
        return inheritsFromValueObjectType;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type? converterGenericType = TypesAndConverters.FindConverterType(typeToConvert);
        if(converterGenericType is null)
        {
            return null;
        }

        Type finalConverterType = converterGenericType.MakeGenericType(typeToConvert);
        object? converter = Activator.CreateInstance(finalConverterType);
        return (JsonConverter?)converter;
    }
}

file static class TypesAndConverters
{
    private static readonly Dictionary<Type, Type> _typeConverters;

    static TypesAndConverters()
    {
        Assembly assembly = typeof(TypesAndConverters).Assembly;

        Type[] valueObjectTypes = assembly
            .GetTypes()
            .Where(t => t.Name is not null
                && t.Name.EndsWith("ValueObject`1")
                && t.Name != "ValueObject`1"
            )
            .ToArray();

        Type[] converterTypes = assembly
            .GetTypes()
            .Where(t => t.Name is not null
                && t.Name.EndsWith("ValueObjectJsonConverter`1"))
            .ToArray();

        _typeConverters = valueObjectTypes
            .Select(valueObjectType => {
                string converterName = valueObjectType.Name![..^2] + "JsonConverter`1";
                Type? converterType = converterTypes.FirstOrDefault(c => c.Name == converterName);
                return (valueObjectType, converterType);
            })
            .Where(pair => pair.converterType is not null)
            .ToDictionary(pair => pair.valueObjectType, pair => pair.converterType!);
    }

    public static bool InheritsFromValueObjectType(Type typeToConvert)
    {
        return _typeConverters.Keys
            .Any(vot => typeToConvert.InheritsFromGenericType(vot));
    }

    public static Type? FindConverterType(Type typeToConvert)
    {
        Type? baseType = _typeConverters.Keys.FirstOrDefault(vot => typeToConvert.InheritsFromGenericType(vot));
        if (baseType is null)
        {
            return null;
        }

        _typeConverters.TryGetValue(baseType, out Type? converterType);
        return converterType;
    }
}

file static class TypeExtensions
{
    public static bool InheritsFromGenericType(this Type type, Type baseType)
    {
        Type? current = type.BaseType;
        while (current is not null)
        {
            if (current.IsGenericType && current.GetGenericTypeDefinition() == baseType)
            {
                return true;
            }

            current = current.BaseType;
        }

        return false;
    }
}
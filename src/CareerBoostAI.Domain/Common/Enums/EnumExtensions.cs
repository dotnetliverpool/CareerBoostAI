using System.Collections;

namespace CareerBoostAI.Domain.Common.Enums;

public static class EnumExtensions
{
    public static IEnumerable<TEnumType> EnumerateValues<TEnumType>() where TEnumType : System.Enum
    {
        IEnumerable values = Enum.GetValues(typeof(TEnumType));
        return values.Cast<TEnumType>();
    }
    
    public static string GetStringValue(this Enum enumValue)
    {
        return
            enumValue
                .ToString();
    }
    
    public static TEnumType ParseToEnum<TEnumType>(this string stringValue) where TEnumType : struct, Enum
    {
        
        if (string.IsNullOrEmpty(stringValue))
        {
            throw new ArgumentException("Input string cannot be null or empty.", nameof(stringValue));
        }
        
        if (Enum.TryParse<TEnumType>(stringValue, ignoreCase: true, out var result))
        {
            return result;
        }

        throw new ArgumentException($"'{stringValue}' is not a valid value for enum type {typeof(TEnumType).Name}.", nameof(stringValue));
    }

}
using CareerBoostAI.Application.Common.Exceptions;

namespace CareerBoostAI.Application.Extensions;

public static class DataConversionExtensions
{
    public static TEnum ToEnum<TEnum>(this string enumString) where TEnum : struct, Enum
    {
        if (!Enum.TryParse<TEnum>(enumString, true, out TEnum result))
        {
            throw new CareerBoostAIApplicationException($"{nameof(ToEnum)} {enumString} is not supported");
        }
        return result;
    }

}
namespace CareerBoostAI.Application.Common.Extension;

public static class DataConversionExtensions
{
    public static TEnum ToEnum<TEnum>(this string enumString) where TEnum : struct, Enum
    {
        if (!Enum.TryParse<TEnum>(enumString, true, out TEnum result))
        {
            throw new ApplicationException($"Storagemedium {enumString} is not supported");
        }
        return result;
    }

}
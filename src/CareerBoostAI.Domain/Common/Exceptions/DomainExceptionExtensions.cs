namespace CareerBoostAI.Domain.Common.Exceptions;

public static class DomainExceptionExtensions
{
    public static void ThrowIfNull<T>(this T value)
    {
        if (value is null)
        {
            throw new EmptyArgumentException(nameof(T));
        }
    }
    
    public static void ThrowIfNull<T>(this IEnumerable<T> values)
    {
        foreach (var valueObject in values.ToList())
        {
            ThrowIfNull(valueObject);
        }
    }

    public static void ThrowIfNullOrEmpty(this string value, string fieldName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyArgumentException(fieldName);
        }
    }
    
    public static void ThrowIfContainsDuplicates<T>(this IEnumerable<T> array) 
    {
        var set = new HashSet<T>();

        foreach (var value in array.ToList())
        {
            if (!set.Add(value)) 
            {
                throw new DuplicatePropertyException(nameof(T));
            }
        }
    }

   
}
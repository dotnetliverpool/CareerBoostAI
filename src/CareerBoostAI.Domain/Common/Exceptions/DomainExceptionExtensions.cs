using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.ValueObjects;

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
            throw new EmptyArgumentException($"{fieldName} cannot be null or empty.");
        }
    }
    
    public static void ThrowIfContainsDuplicates<T>(this IEnumerable<T> array) where T : ValueObject
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

    public static void ThrowIfContainsDuplicates<T>(this IEnumerable<Entity<T>> entities)
    {
        var entityIds = new HashSet<T>();

        foreach (var entity in entities.ToList())
        {
            if (!entityIds.Add(entity.Id)) 
            {
                throw new DuplicateEntityException($"Duplicate entity found with Id: {entity.Id}");
            }
        }
    }
}
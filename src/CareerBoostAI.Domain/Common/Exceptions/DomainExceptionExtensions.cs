using CareerBoostAI.Domain.Common.Abstractions;

namespace CareerBoostAI.Domain.Common.Exceptions;

public static class DomainExceptionExtensions
{
    public static void ThrowIfNull(this object value)
    {
        if (value is null)
        {
            throw new EmptyArgumentException(nameof(value));
        }
    }

    public static void ThrowIfNullOrEmpty(this string value, string fieldName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyArgumentException($"{fieldName} cannot be null or empty.");
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
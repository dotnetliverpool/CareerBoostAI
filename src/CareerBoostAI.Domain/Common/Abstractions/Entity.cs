namespace CareerBoostAI.Domain.Common.Abstractions;

public abstract class Entity<TId> :  IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }

    // Equality check based on Id
    public bool Equals(Entity<TId>? other)
    {
        if (other == null)
            return false;

        return EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
        {
            return false;
        }

        return Equals(other);
    }

    
    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }
    
}


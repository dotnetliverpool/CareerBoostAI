namespace CareerBoostAI.Domain.Common.Abstractions;

public abstract class Entity<TId> :  IEquatable<Entity<TId>>
{
    public TId Id { get; protected init; }
    
    public bool Equals(Entity<TId>? other)
    {
        return other is not null 
               && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> other && Equals(other);
    }
    
    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }
    
}


using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.Common.ValueObjects;

public class EntityId
{
    private EntityId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }
    public static EntityId Create(Guid value)
    {
        value.ThrowIfNull();
        return new(value);
    }
}
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.Common.ValueObjects;

public class EntityId : ValueObject
{
    private EntityId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static EntityId NewId()
    {
        return new(Guid.NewGuid());
    }
    public static EntityId Create(Guid value)
    {
        value.ThrowIfNull();
        return new(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
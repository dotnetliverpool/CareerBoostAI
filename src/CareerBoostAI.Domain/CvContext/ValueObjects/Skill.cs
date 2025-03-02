using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class Skill : ValueObject
{
    public Guid Id { get; private set; }
    public string Value { get; }

    private Skill(Guid id, string value)
    {
        Value = value;
        Id = id;
    }
    
    public Skill() {}

    public static Skill Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(Skill));
        var result = value.Trim().ToLower();
        return new(Guid.NewGuid(), result);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
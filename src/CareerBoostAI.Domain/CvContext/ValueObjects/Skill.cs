using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class Skill : ValueObject
{
    public string Value { get; }

    private Skill(string value)
    {
        Value = value;
    }
    
    public Skill() {}

    public static Skill Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(Skill));
        var result = value.Trim().ToLower();
        return new(result);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
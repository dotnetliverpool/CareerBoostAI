using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class Skill : ValueObject
{
    public string Value { get; }

    private Skill(string value)
    {
        Value = value;
    }

    public static Skill Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(Skill));
        return new(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
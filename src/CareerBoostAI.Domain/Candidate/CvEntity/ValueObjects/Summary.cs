using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class Summary : ValueObject
{
    public string Value { get; }

    private Summary(string value)
    {
        Value = value;
    }

    private static void Validate(string value)
    {
        value.ThrowIfNullOrEmpty(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Summary Create(string value)
    {
        Validate(value);
        return new Summary(value);
    }
}
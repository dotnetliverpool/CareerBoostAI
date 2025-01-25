using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.CandidateContext.ValueObjects;

public class FirstName : ValueObject
{
    public string Value { get;  }

    private FirstName(string value)
    {
        Value = value;
    }

    public static FirstName Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(FirstName));
        return new FirstName(value);
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
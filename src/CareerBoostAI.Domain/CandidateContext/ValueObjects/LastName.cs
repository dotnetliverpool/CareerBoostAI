using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.CandidateContext.ValueObjects;

public class LastName : ValueObject
{
    public string Value { get; }
    
    private LastName(string value)
    {
        Value = value;
    }
    
    public static LastName Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(LastName));
        return new LastName(value);
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

using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

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
    
    public static Summary Create(string value)
    {
        Validate(value);
        return new Summary(value);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    
}
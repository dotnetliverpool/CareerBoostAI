using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Cv.ValueObjects;

public class Description : ValueObject
{
    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Description Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(Description));
        return new Description(value);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

}

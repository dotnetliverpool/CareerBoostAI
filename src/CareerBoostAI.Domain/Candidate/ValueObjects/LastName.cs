using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.ValueObjects;

public class LastName : ValueObject
{
    public string Value { get; }
    
    private LastName(string value)
    {
        Value = value;
    }
    
    public static LastName Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyArgumentException(nameof(LastName));
        }
        return new LastName(value);
    }

    public static LastName CreateTrusted(string value)
    {
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

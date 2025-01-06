using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.ValueObjects;

public class LastName : ValueObject
{
    public string Value { get; }
    
    public LastName(string value)
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


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

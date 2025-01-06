using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

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

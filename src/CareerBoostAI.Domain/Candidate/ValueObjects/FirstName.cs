using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.ValueObjects;

public class FirstName : ValueObject
{
    public string Value { get;  }

    private FirstName(string value)
    {
        

        Value = value;
    }

    public static FirstName Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyArgumentException(nameof(FirstName));
        }

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
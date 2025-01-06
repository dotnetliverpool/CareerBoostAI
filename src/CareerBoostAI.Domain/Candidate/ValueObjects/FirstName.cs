using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

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

    public static implicit operator string(FirstName firstName)
        => firstName.Value;

    public static implicit operator FirstName(string firstName)
        => new(firstName);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
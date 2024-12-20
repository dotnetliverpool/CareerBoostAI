using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateFirstName : ValueObject
{
    public string Value { get;  }

    private CandidateFirstName(string value)
    {
        

        Value = value;
    }

    public static CandidateFirstName Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyArgumentException(nameof(CandidateFirstName));
        }

        return new CandidateFirstName(value);
    }

    public static implicit operator string(CandidateFirstName firstName)
        => firstName.Value;

    public static implicit operator CandidateFirstName(string firstName)
        => new(firstName);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
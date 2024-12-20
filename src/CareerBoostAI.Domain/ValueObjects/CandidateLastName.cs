using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateLastName : ValueObject
{
    public string Value { get; }
    
    public CandidateLastName(string value)
    {
        
        Value = value;
    }
    
    public static CandidateLastName Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyArgumentException(nameof(CandidateLastName));
        }

        return new CandidateLastName(value);
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

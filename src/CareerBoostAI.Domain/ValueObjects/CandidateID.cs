using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateId : ValueObject
{
    public Guid Value { get; }

    private CandidateId(Guid value)
    {
        Value = value;
    }
    
    public static CandidateId New()
    {
        return new CandidateId(Guid.NewGuid());
    }
        
    public static implicit operator Guid(CandidateId id)
        => id.Value;
        
    public static implicit operator CandidateId(Guid id)
        => new(id);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

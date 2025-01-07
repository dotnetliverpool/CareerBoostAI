using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.ValueObjects;

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
        
    public static CandidateId CreateTrusted(Guid id)
    {
        return new CandidateId(id);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }


  
}

using CareerBoostAI.Domain.Common.Exceptions;
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
        
    public static CandidateId Create(Guid id)
    {
        id.ThrowIfNull();
        return new CandidateId(id);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }


  
}

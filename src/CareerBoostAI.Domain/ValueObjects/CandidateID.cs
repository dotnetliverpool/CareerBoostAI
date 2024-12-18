using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateId
{
    public Guid Value { get; }

    public CandidateId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyCandidateIdException();
        }
            
        Value = value;
    }
        
    public static implicit operator Guid(CandidateId id)
        => id.Value;
        
    public static implicit operator CandidateId(Guid id)
        => new(id);
}

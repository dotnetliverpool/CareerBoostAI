using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvId : ValueObject
{
    public Guid Value { get; }

    // Constructor to initialize CandidateCvId with a new Guid
    private CvId(Guid value)
    {
        Value = value;
    }

    // Factory method for generating a new CandidateCvId with a new GUID
    public static CvId New()
    {
        return new CvId(Guid.NewGuid());
    }

    public static CvId Create(Guid value)
    {
        return new CvId(value);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvId : ValueObject
{
    public Guid Value { get; }
    
    private CvId(Guid value)
    {
        Value = value;
    }
    
    public static CvId New()
    {
        return new CvId(Guid.NewGuid());
    }

    public static CvId Create(Guid value)
    {
        value.ThrowIfNull();
        return new CvId(value);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
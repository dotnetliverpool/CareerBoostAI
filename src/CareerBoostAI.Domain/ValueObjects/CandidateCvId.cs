namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateCvId : ValueObject
{
    public Guid Value { get; }

    // Constructor to initialize CandidateCvId with a new Guid
    private CandidateCvId(Guid value)
    {
        Value = value;
    }

    // Factory method for generating a new CandidateCvId with a new GUID
    public static CandidateCvId New()
    {
        return new CandidateCvId(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
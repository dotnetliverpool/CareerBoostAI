namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateCvId
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

    public override bool Equals(object? obj)
    {
        if (obj is CandidateCvId other)
        {
            return Value.Equals(other.Value);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
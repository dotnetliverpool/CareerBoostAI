namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateDOB
{
    public DateTime Value { get; }

    // Constructor to ensure the date of birth is in the past
    public CandidateDOB(DateTime value)
    {
        if (value >= DateTime.Now)
        {
            throw new ArgumentException("Date of birth must be in the past.", nameof(value));
        }

        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is CandidateDOB other)
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
        return Value.ToString("yyyy-MM-dd");
    }
}
namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateLastName
{
    // Private field for last name
    public string Value { get; }

    // Constructor to enforce validation
    public CandidateLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Last name cannot be empty or whitespace.", nameof(value));
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Last name cannot be longer than 100 characters.", nameof(value));
        }

        Value = value;
    }

    // Equality methods to compare value objects
    public override bool Equals(object? obj)
    {
        if (obj is CandidateLastName other)
        {
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Value.ToLowerInvariant().GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}

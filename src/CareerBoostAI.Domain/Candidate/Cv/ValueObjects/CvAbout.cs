namespace CareerBoostAI.Domain.ValueObjects;

public class CvAbout
{
    public string Value { get; }

    public CvAbout(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("About section cannot be empty or whitespace.", nameof(value));

        if (value.Length > 1000) // Example limit for the about section
            throw new ArgumentException("About section cannot exceed 1000 characters.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is CvAbout other && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

    public override string ToString() => Value;
}
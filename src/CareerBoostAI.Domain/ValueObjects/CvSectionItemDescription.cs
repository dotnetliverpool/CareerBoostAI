namespace CareerBoostAI.Domain.ValueObjects;

public class CvSectionItemDescription
{
    public string Value { get; }

    public CvSectionItemDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Description cannot be empty or whitespace.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is CvSectionItemDescription other && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

    public override string ToString() => Value;
}

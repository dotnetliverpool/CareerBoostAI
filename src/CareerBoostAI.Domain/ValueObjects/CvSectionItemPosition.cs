namespace CareerBoostAI.Domain.ValueObjects;

public class CvSectionItemPosition
{
    public string Value { get; }

    public CvSectionItemPosition(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Position cannot be empty or whitespace.", nameof(value));

        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is CvSectionItemPosition other && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();

    public override string ToString() => Value;
}
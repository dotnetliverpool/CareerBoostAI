using System.Text.RegularExpressions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateEmail
{
    public string Value { get; }

    // Constructor with validation for email format
    public CandidateEmail(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be empty or whitespace.", nameof(value));
        }

        if (!IsValidEmail(value))
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        Value = value;
    }

    private bool IsValidEmail(string email)
    {
        // Simple email validation (this can be more complex if needed)
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }

    public override bool Equals(object? obj)
    {
        if (obj is CandidateEmail other)
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
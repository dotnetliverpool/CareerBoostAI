using System.Text.RegularExpressions;
using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateEmail : ValueObject
{
    public string Value { get; }

    // Constructor with validation for email format
    private CandidateEmail(string value)
    {
        Value = value;
    }

    public static CandidateEmail Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyArgumentException(nameof(CandidateEmail));
        }

        if (!IsValidEmail(value))
        {
            throw new InvalidEmailFormatException(value);
        }

        return new CandidateEmail(value);
    }

    private static bool IsValidEmail(string email)
    {
        // Simple email validation (this can be more complex if needed)
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return emailRegex.IsMatch(email);
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
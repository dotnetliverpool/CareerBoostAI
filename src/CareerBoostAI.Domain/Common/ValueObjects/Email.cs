using System.Text.RegularExpressions;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Common.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    // Constructor with validation for email format
    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value, bool isActive = true)
    {
        value.ThrowIfNullOrEmpty(nameof(Email));

        if (!IsValidEmail(value))
        {
            throw new InvalidEmailFormatException(value);
        }
        return new Email(value);
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
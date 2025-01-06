using System.Text.RegularExpressions;
using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }
    public bool IsActive { get;  }

    // Constructor with validation for email format
    private Email(string value, bool isActive)
    {
        Value = value;
        IsActive = isActive;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyArgumentException(nameof(Email));
        }

        if (!IsValidEmail(value))
        {
            throw new InvalidEmailFormatException(value);
        }

        return new Email(value, true);
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
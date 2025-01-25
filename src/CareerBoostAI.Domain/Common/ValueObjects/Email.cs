using System.Text.RegularExpressions;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Domain.Common.ValueObjects;

public class Email : ValueObject
{
    public string Value { get; }

    // Constructor with validation for email format
    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        value.ThrowIfNullOrEmpty(nameof(Email));
        ValidateEmailFormat(value);
        return new Email(value);
    }

    private static void ValidateEmailFormat(string email)
    {
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!emailRegex.IsMatch(email))
        {
            throw new InvalidEmailFormatException(email);
        }
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
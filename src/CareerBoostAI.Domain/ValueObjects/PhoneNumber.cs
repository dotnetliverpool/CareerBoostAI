using System.Text.RegularExpressions;

namespace CareerBoostAI.Domain.ValueObjects;

public class PhoneNumber
{
    public string Value { get; }

    // Constructor with validation for phone number format
    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Phone number cannot be empty or whitespace.", nameof(value));
        }

        if (!IsValidPhoneNumber(value))
        {
            throw new ArgumentException("Invalid phone number format.", nameof(value));
        }

        Value = value;
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
       
        var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$"); 
        return phoneRegex.IsMatch(phoneNumber);
    }

    public override bool Equals(object? obj)
    {
        if (obj is PhoneNumber other)
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
        return Value;
    }
}
using System.Text.RegularExpressions;

namespace CareerBoostAI.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string Code { get; private set; }
    public string Number {get; private set; }

    // Constructor with validation for phone number format
    private PhoneNumber(string code, string number)
    {
        Code = code;
        Number = number;
    }

    public static PhoneNumber Create(string phoneCode, string number)
    {
        if (string.IsNullOrEmpty(phoneCode))
        {
            throw new ArgumentException("Phone code cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(number))
        {
            throw new ArgumentException("Phone number cannot be null or empty.");
        }

        if (!IsValidPhoneNumber(phoneCode + number))
        {
            throw new ArgumentException("Invalid phone number format.");
        }

        return new PhoneNumber(phoneCode, number);
    }
    

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
       
        var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$"); 
        return phoneRegex.IsMatch(phoneNumber);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Number;
    }
}
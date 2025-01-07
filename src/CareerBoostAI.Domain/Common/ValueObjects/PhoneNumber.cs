using System.Text.RegularExpressions;
using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Common.ValueObjects;

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
            throw new EmptyArgumentException("PhoneCode");
        }

        if (string.IsNullOrEmpty(number))
        {
            throw new EmptyArgumentException("PhoneNumber");
        }

        if (!IsValidPhoneNumber(phoneCode + number))
        {
            throw new InvalidPhoneNumberException(phoneCode, number);
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

    public override string ToString()
    {
        return $"{Code} {Number}";
    }
}
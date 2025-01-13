using System.Text.RegularExpressions;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Common.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string Code { get;  }
    public string Number {get;  }
    
    private PhoneNumber(string code, string number)
    {
        Code = code;
        Number = number;
    }

    public static PhoneNumber Create(string code, string number)
    {
        code.ThrowIfNullOrEmpty("PhoneNumber.Code");
        number.ThrowIfNullOrEmpty("PhoneNumber.Number");
        
        if (!IsValidPhoneNumber(code + number))
        {
            throw new InvalidPhoneNumberException(code, number);
        }
        return new PhoneNumber(code, number);
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
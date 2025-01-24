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
            
        }
        return new PhoneNumber(code, number);
    }
    

    private static void IsValidPhoneNumber(string code, string number)
    {
        var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$");
        if (!phoneRegex.IsMatch(phoneNumber))
        {
            throw new InvalidPhoneNumberException(phoneNumber);
        }
        
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Code;
        yield return Number;
    }

    public override string ToString() 
        =>  $"{Code} {Number}";
    
    public static PhoneNumber Parse(string combined)
    {
        var parts = combined.Split(' ');
        return new PhoneNumber(parts[0], parts[1]);
    }
    
}
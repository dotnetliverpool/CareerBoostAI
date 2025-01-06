namespace CareerBoostAI.Domain.Exceptions;

public class InvalidPhoneNumberException : CareerBoostAIDomainException
{
    public InvalidPhoneNumberException(string code, string number) : base($"Invalid phone number: {code} - {number}")
    {
    }
}
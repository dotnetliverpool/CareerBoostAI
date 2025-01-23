namespace CareerBoostAI.Domain.Common.Exceptions;

public class CandidateExceptions
{
    
}

public class AgeNotWithinAcceptedRangeException()
    : CareerBoostAIDomainException("Age must be greater than 10 and less than 120.");


public class InvalidEmailFormatException(string email)
    : CareerBoostAIDomainException($"Email [{email}] does not pass required format");

public class InvalidPhoneNumberException(string code, string number)
    : CareerBoostAIDomainException($"Invalid phone number: {code} - {number}");
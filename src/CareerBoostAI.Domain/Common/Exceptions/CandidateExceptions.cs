using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Domain.Common.Exceptions;

public class CandidateExceptions
{
    
}

public class AgeNotWithinAcceptedRangeException()
    : CareerBoostAiDomainException("Age must be greater than 10 and less than 120.");


public class InvalidEmailFormatException(string email)
    : CareerBoostAiDomainException($"Email [{email}] does not pass required format");

public class InvalidPhoneNumberException(string code, string number)
    : CareerBoostAiDomainException($"Invalid phone number: {code} - {number}");
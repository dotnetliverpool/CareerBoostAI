using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Domain.Common.Exceptions;

public class CvValidationExceptions
{
    
}

public class InvalidProfessionalEntryTimePeriodException()
    : CareerBoostAiDomainException("EndDate cannot be earlier than StartDate.");

public class InvalidEntrySequenceIndexException(string number)
    : CareerBoostAiDomainException($"{number} is not a valid number for entry sequence index");
    
public class ProfessionalEntrySequenceInvalidException(string concreteClass)
    : CareerBoostAiDomainException($"each {concreteClass} must have a sequenceId ranging from 1 to the length of entries");
namespace CareerBoostAI.Domain.Common.Exceptions;

public class CvValidationExceptions
{
    
}

public class InvalidProfessionalEntryTimePeriodException()
    : CareerBoostAIDomainException("EndDate cannot be earlier than StartDate.");

public class InvalidEntrySequenceIndexException(string number)
    : CareerBoostAIDomainException($"{number} is not a valid number for entry sequence index");
    
public class ProfessionalEntrySequenceInvalidException(string concreteClass)
    : CareerBoostAIDomainException($"each {concreteClass} must have a sequenceId ranging from 1 to the length of entries");
namespace CareerBoostAI.Domain.Common.Exceptions;

public class CvValidationExceptions
{
    
}

public class InvalidProfessionalEntryTimePeriodException()
    : CareerBoostAIDomainException("EndDate cannot be earlier than StartDate.");
    
public class ProfessionalEntrySequenceInvalidError(string entry)
    : CareerBoostAIDomainException($"each {entry} must have a sequenceId ranging from 0 to the length of entries - 1.");
namespace CareerBoostAI.Application.Common.Exceptions;

public class CandidateExceptions
{
    
}

public class CandidateProfileAlreadyExistsException(string email)
    : CareerBoostAIApplicationException($"Profile for email: {email} already exists.");

public class CandidateProfileNotFoundException(string email)
    : CareerBoostAIApplicationException($"Profile not found for email: {email}.");

public class CandidateCvNotFoundException(string email)
    : CareerBoostAIApplicationException($"Cv belonging to user [{email}] not found.");
    
public class DocumentExceedsMaximumUploadSizeException(string maxSize)
    : CareerBoostAIApplicationException($"the document exceeds the maximum supported size of {maxSize}mb.");
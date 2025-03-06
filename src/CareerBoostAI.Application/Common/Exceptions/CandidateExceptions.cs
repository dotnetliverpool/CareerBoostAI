using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Application.Common.Exceptions;

public class CandidateExceptions
{
    
}

public class CandidateProfileAlreadyExistsException(string email)
    : CareerBoostAiApplicationException($"Profile for email: {email} already exists.");

public class CandidateProfileNotFoundException(string email)
    : CareerBoostAiApplicationException($"Profile not found for email: {email}.");


public class CandidateCvNotFoundException(string email)
    : CareerBoostAiApplicationException($"Cv belonging to user [{email}] not found.");
    
public class DocumentSizeOutOfBoundsException(string maxSize)
    : CareerBoostAiApplicationException($"the document must not be empty and cannot exceed" +
                                        $" the maximum supported size of {maxSize}mb.");
    
public class UnsupportedFileTypeException(IEnumerable<string> supportedFileTypes) : CareerBoostAiApplicationException(
    $"The uploaded file type is not supported. Supported file types are: {string.Join(", ", 
        supportedFileTypes.Select(type => type.ToString()))}.");

public class DocumentParseFailedException() : CareerBoostAiApplicationException(
    "Failed to parse the document. Try again later. Contact support if the problem persists.");

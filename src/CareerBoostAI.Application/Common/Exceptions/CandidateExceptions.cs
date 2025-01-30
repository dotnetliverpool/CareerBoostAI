using CareerBoostAI.Application.Services.DocumentConstraintsService;

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
    
public class DocumentSizeOutOfBoundsException(string maxSize)
    : CareerBoostAIApplicationException($"the document must not be empty and cannot exceed" +
                                        $" the maximum supported size of {maxSize}mb.");
    
public class UnsupportedFileTypeException(IEnumerable<SupportedDocumentTypes> supportedFileTypes) : CareerBoostAIApplicationException(
    $"The uploaded file type is not supported. Supported file types are: {string.Join(", ", 
        supportedFileTypes.Select(type => type.ToString()))}.");

public class DocumentParseFailedException() : CareerBoostAIApplicationException(
    "Failed to parse the document. Try again later. Contact support if the problem persists.");

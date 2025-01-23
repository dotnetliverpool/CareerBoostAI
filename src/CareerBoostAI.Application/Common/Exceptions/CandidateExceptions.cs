namespace CareerBoostAI.Application.Common.Exceptions;

public class CandidateExceptions
{
    
}

public class CandidateProfileNotFoundException(string email)
    : CareerBoostAIApplicationException($"Profile for email: {email} already exists.");
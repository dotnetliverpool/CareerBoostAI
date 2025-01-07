namespace CareerBoostAI.Domain.Common.Exceptions;

public class DuplicateCandidateProfileException : CareerBoostAIDomainException
{
    public DuplicateCandidateProfileException(string email) : base($"Profile for email: {email} already exists.")
    {
    }
}
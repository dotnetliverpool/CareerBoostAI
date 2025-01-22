namespace CareerBoostAI.Domain.Common.Exceptions;

public class CandidateProfileNotFoundException : CareerBoostAIDomainException
{
    public CandidateProfileNotFoundException(string email) : base($"Profile for email: {email} already exists.")
    {
    }
}
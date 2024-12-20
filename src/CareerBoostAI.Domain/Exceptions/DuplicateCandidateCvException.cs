namespace CareerBoostAI.Domain.Exceptions;

public class DuplicateCandidateCvException : CareerBoostAIDomainException
{
    public DuplicateCandidateCvException(string email, Guid id) : base($"Candidate [{email}] already has cv with Id: {id}")
    {
    }
}
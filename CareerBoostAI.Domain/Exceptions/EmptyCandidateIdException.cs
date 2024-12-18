namespace CareerBoostAI.Domain.Exceptions;

public class EmptyCandidateIdException : CareerBoostAIDomainException
{
    public EmptyCandidateIdException() : base("Candidate ID cannot be empty")
    {
    }
}
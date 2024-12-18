namespace CareerBoostAI.Domain.Exceptions;

public class EmptyCandidateNameException : CareerBoostAIDomainException
{
    public EmptyCandidateNameException() : base("Candidate Name Is Empty")
    {
    }
}
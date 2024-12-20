namespace CareerBoostAI.Domain.Exceptions;

public class AgeNotWithinAcceptedRangeException : CareerBoostAIDomainException
{
    public AgeNotWithinAcceptedRangeException() : base("Age must be greater than 10 and less than 120.")
    {
    }
}
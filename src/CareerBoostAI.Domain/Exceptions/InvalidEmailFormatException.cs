namespace CareerBoostAI.Domain.Exceptions;

public class InvalidEmailFormatException : CareerBoostAIDomainException
{
    public InvalidEmailFormatException(string email) : base($"Email [{email}] does not pass required format")
    {
    }
}
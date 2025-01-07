namespace CareerBoostAI.Domain.Common.Exceptions;

public class InvalidEmailFormatException : CareerBoostAIDomainException
{
    public InvalidEmailFormatException(string email) : base($"Email [{email}] does not pass required format")
    {
    }
}
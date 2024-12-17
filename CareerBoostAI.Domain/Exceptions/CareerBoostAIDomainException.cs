namespace CareerBoostAI.Domain.Exceptions;

public class CareerBoostAIDomainException : Exception
{
    protected CareerBoostAIDomainException(string message) : base(message)
    {}
}
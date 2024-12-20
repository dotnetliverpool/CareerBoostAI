namespace CareerBoostAI.Domain.Exceptions;

public class EmptyArgumentException : CareerBoostAIDomainException
{
    public EmptyArgumentException(string className) : base($"{className} cannot be empty")
    {
    }
}
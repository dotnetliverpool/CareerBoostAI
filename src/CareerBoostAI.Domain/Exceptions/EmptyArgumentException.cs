namespace CareerBoostAI.Domain.Exceptions;

public class EmptyArgumentException : CareerBoostAIDomainException
{
    public EmptyArgumentException(string argName) : base($"{argName} cannot be empty")
    {
    }
}
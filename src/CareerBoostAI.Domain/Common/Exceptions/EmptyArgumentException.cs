namespace CareerBoostAI.Domain.Common.Exceptions;

public class EmptyArgumentException : CareerBoostAIDomainException
{
    public EmptyArgumentException(string argName) : base($"{argName} cannot be empty")
    {
    }
}
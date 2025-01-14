namespace CareerBoostAI.Shared.Abstractions.Exceptions;

public abstract class CareerBoostAIException : Exception
{
    protected CareerBoostAIException(string message) : base(message)
    {}
}
using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Infrastructure.Common.Exception;

public class CareerBoostAIInfrastructureException : CareerBoostAIException
{
    protected CareerBoostAIInfrastructureException(string message) : base(message)
    {
    }
}
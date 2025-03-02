using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Domain.Common.Exceptions;

public class CareerBoostAIDomainException : CareerBoostAIException
{
    protected CareerBoostAIDomainException(string message) : base(message)
    {}
    
}
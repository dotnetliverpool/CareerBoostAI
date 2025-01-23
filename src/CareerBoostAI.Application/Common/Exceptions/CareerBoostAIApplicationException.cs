using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Application.Common.Exceptions;

public class CareerBoostAIApplicationException : CareerBoostAIException
{
    protected CareerBoostAIApplicationException(string message) : base(message)
    {}
}
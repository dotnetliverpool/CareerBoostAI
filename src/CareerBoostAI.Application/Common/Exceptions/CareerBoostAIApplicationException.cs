using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Application.Common.Exceptions;

public class CareerBoostAIApplicationException(string message) : 
    CareerBoostAIException(message);
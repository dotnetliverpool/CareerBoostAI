namespace CareerBoostAI.Shared.Abstractions.Exceptions;

public abstract class CareerBoostAiException(string message) : Exception(message);

public class CareerBoostAiInfrastructureException : CareerBoostAiException
{
    protected CareerBoostAiInfrastructureException(string message) : base(message)
    {
    }
}
public class CareerBoostAiApplicationException(string message) : 
    CareerBoostAiException(message);

public class CareerBoostAiDomainException(string message) :
    CareerBoostAiException(message);

namespace CareerBoostAI.Infrastructure.Common.Exception;

public class CareerBoostAICommonExceptions : CareerBoostAIInfrastructureException
{
    protected CareerBoostAICommonExceptions(string message) : base(message)
    {
    }
}

public class ConfigurationSectionNotFoundException(string section)
    : CareerBoostAIInfrastructureException($"{section} Section Missing in Configuration File");

public class CareerBoostAINotImplementedException(string abstractionName, string? expectedImplementation = null)
    : CareerBoostAIInfrastructureException(string.IsNullOrEmpty(expectedImplementation)
        ? $"No implementation for {abstractionName} was found"
        : $"{abstractionName} has no implementation for {expectedImplementation}");

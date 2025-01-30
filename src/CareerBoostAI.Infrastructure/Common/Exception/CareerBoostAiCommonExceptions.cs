using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Infrastructure.Common.Exception;

internal partial class CareerBoostAiCommonExceptions { }

public class ConfigurationSectionNotFoundException(string section)
    : CareerBoostAiInfrastructureException($"{section} Section Missing in Configuration File");

public class CareerBoostAiNotImplementedException(string abstractionName, string? expectedImplementation = null)
    : CareerBoostAiInfrastructureException(string.IsNullOrEmpty(expectedImplementation)
        ? $"No implementation for {abstractionName} was found"
        : $"{abstractionName} has no implementation for {expectedImplementation}");

using CareerBoostAI.Infrastructure.Common.Exception;

namespace CareerBoostAI.Infrastructure.Extensions;

public class ConfigurationSectionNotFoundException : CareerBoostAIInfrastructureException
{
    public ConfigurationSectionNotFoundException(string section) 
        : base($"{section} Section Missing in Configuration File")
    {
    }
}
namespace CareerBoostAI.Infrastructure.Services.LlmService;

public class OpenAiClientOptions
{
    public string ModelId { get; set; } 
    public string EndpointUrl { get; set; }
    public string ApiKey { get; set; }
}

public class AzureOpenAiClientOptions : OpenAiClientOptions
{
    public string DeploymentName { get; set; }
}
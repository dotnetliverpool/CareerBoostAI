using System.ClientModel;
using CareerBoostAI.Infrastructure.Extensions;
using CareerBoostAI.Infrastructure.Services.LlmService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using OpenAI;

namespace CareerBoostAI.Infrastructure.Services.AiClient;

public static class ServiceInjector
{
    public static IServiceCollection AddOpenAiClient(
        this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<OpenAiClientOptions>("AI:OpenAi");
        var client = new OpenAIClient(
            new ApiKeyCredential(options.ApiKey),
            new OpenAIClientOptions() {Endpoint = new Uri(options.EndpointUrl)}
        );
        services.AddKernel().AddOpenAIChatCompletion(
            options.ModelId, client);
        return services;
    }
    
    public static IServiceCollection AddAzureOpenAiClient(
        this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AzureOpenAiClientOptions>("AI:AzureOpenAi");
        services
            .AddKernel()
            .AddAzureOpenAIChatCompletion(
                deploymentName: options.DeploymentName,
                endpoint: options.EndpointUrl, 
                apiKey: options.ApiKey, modelId: options.ModelId );
        return services;
    }
}
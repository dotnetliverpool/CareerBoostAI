using System.Diagnostics.CodeAnalysis;
using CareerBoostAI.Application.Common.Abstractions.AiDto;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Chat;

namespace CareerBoostAI.Infrastructure.Services.AiClient;

public class SemanticKernelAiClient(Kernel kernel) : IAiClient
{
    [Experimental("SKEXP0010")]
    public async Task<string> InvokePromptAsync(string prompt, string responseSchemaName, 
        IAiResponseSchemaDto jsonResponseSchema, CancellationToken cancellationToken)
    {
        
        var chatResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
            responseSchemaName, jsonResponseSchema.Serialize(), jsonSchemaIsStrict: true);
        var executionSettings = new OpenAIPromptExecutionSettings
        {
            ResponseFormat = chatResponseFormat
        };
        var result =  await kernel.InvokePromptAsync(prompt, new(executionSettings),
            cancellationToken: cancellationToken);
        return result.ToString();
    }
}
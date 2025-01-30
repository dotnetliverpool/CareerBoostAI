using CareerBoostAI.Application.Common.Abstractions.AiDto;

namespace CareerBoostAI.Infrastructure.Services.AiClient;

public interface IAiClient
{
    public Task<string> InvokePromptAsync(string prompt, string responseSchemaName, 
        IAiResponseSchemaDto jsonResponseSchema, CancellationToken cancellationToken );
}
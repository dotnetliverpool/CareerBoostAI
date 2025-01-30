using System.Text.Json;
using System.Text.Json.Serialization;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.AiDto;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.JsonService;
using CareerBoostAI.Infrastructure.Services.AiClient;
using CareerBoostAI.Infrastructure.Services.JsonService;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Services.CvContentParser;

public class OpenAiCvContentParser(IAiClient aiClient,
    [FromKeyedServices("System")] IJsonService jsonService) : ICvDocumentContentParser
{
    public async Task<ParsedCvDocumentDto> ParseAsync(string documentContent, CancellationToken cancellationToken)
    {
        var prompt =  "Given the content of  a cv return the following information if they can be found: \n" +
                      "\"Summary, Experiences, Education, Skills, Languages." +
                      "return all date information in the format yyyy-mm-dd. e.g 2023-02-01. " +
                      "day must always be the first of the month" +
                      "return null if the date is represented by any word that means current" +
                      $"The cv content is: \n {documentContent}\n";
        var promptResponse =  await aiClient.InvokePromptAsync(prompt, 
            "candidate_cv", new ParsedCvDocumentAiResponseDto(), cancellationToken);
        
        var result = jsonService.Deserialize<ParsedCvDocumentDto>(promptResponse);
        return result ?? new ParsedCvDocumentDto();
    }
}
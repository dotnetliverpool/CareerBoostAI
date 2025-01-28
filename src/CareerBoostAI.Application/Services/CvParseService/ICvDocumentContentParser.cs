using CareerBoostAI.Application.Candidate.DTO;

namespace CareerBoostAI.Application.Services.CvParseService;

public interface ICvDocumentContentParser
{
    Task<ParsedCvDocumentDto> ParseAsync(string documentContent, CancellationToken cancellationToken);
}
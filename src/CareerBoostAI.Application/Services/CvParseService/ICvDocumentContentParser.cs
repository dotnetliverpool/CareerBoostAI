using CareerBoostAI.Application.Candidate.DTO;

namespace CareerBoostAI.Application.Services.CvParseService;

public interface ICvDocumentContentParser
{
    Task<ParsedCvDocumentDto> ParseAsync(Stream cvStream, string fileName, CancellationToken cancellationToken);
}
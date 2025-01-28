using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyCvDocumentContentParser : ICvDocumentContentParser
{
    public Task<ParsedCvDocumentDto> ParseAsync(Stream cvStream, string fileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
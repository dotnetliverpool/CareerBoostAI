using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyCvDocumentContentParser : ICvDocumentContentParser
{

    public Task<ParsedCvDocumentDto> ParseAsync(string documentContent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyCvParserService : ICvParserService
{
    public Task<ParsedCvDocumentDto> ParseAsync(Stream cvStream, string fileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
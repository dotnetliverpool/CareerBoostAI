using CareerBoostAI.Application.DTO;
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyCvParseService : ICvParseService
{
    public Task<ParsedCv> ParseCvAsync(Stream cvStream, string fileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
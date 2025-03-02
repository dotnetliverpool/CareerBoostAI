using CareerBoostAI.Application.DTO;

namespace CareerBoostAI.Application.Services.CvParseService;

public interface ICvParseService
{
    Task<ParsedCv> ParseCvAsync(Stream cvStream, string fileName, CancellationToken cancellationToken);
}
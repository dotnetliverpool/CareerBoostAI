namespace CareerBoostAI.Application.Services.CvParseService;

public interface ICvParserService
{
    Task<ParsedCv> ParseAsync(Stream cvStream, string fileName, CancellationToken cancellationToken);
}
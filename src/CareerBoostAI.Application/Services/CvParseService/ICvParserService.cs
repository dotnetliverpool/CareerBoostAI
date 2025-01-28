namespace CareerBoostAI.Application.Services.CvParseService;

public interface ICvParserService
{
    Task<ParsedCvDocumentDto> ParseAsync(Stream cvStream, string fileName, CancellationToken cancellationToken);
}
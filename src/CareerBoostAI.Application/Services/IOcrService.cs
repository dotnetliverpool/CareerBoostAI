using CareerBoostAI.Application.Services.DocumentConstraintsService;

namespace CareerBoostAI.Application.Services;

public interface IOcrService
{
    public Task<string?> ExtractTextAsync(Stream documentStream, SupportedDocumentTypes documentType,
        CancellationToken cancellationToken);
}

public interface IOcrImplementation
{
    public Task<string?> ExtractTextAsync(Stream documentStream, CancellationToken cancellationToken);
}
namespace CareerBoostAI.Application.Services;

public interface IOcrService
{
    public Task<string?> ExtractTextAsync(Stream documentStream, CancellationToken cancellationToken);
}
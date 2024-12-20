namespace CareerBoostAI.Application.Services;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);
}
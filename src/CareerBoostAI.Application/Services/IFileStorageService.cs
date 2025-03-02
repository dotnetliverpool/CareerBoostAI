using CareerBoostAI.Domain.Enums;

namespace CareerBoostAI.Application.Services;

public interface IFileStorageService
{
    Task<Guid> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);
    StorageMedium GetMedium();
}
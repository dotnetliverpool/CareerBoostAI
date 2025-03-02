using CareerBoostAI.Application.Services;
using CareerBoostAI.Domain.Enums;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyFileUploadService : IFileStorageService
{
    public Task<Guid> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public StorageMedium GetMedium()
    {
        throw new NotImplementedException();
    }
}
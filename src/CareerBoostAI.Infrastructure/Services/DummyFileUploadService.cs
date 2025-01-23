using CareerBoostAI.Application.Services;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyFileUploadService : IFileStorageService
{
    public Task<IFileStorageResult> UploadFileAsync(
        StorageContainer container,
        Stream documentStream, string documentName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
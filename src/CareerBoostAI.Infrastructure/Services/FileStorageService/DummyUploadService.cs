using CareerBoostAI.Application.Services;

namespace CareerBoostAI.Infrastructure.Services.FileStorageService;

public class DummyUploadService : IStorageService
{
    public Task<IStorageDocument> UploadFileAsync(
        StorageContainer container,
        Stream documentStream, string documentName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
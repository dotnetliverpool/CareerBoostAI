using CareerBoostAI.Application.Services;
using CareerBoostAI.Infrastructure.Services.FileStorageService.Azure;

namespace CareerBoostAI.Infrastructure.Services.FileStorageService;

public class DummyUploadService : IStorageService
{
    public Task<IStorageDocument> UploadFileAsync(
        StorageContainer container,
        Stream documentStream, string documentName, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid();
        var result =  new AzureBlobUploadDocument
        {
            Id = id,
            Address = $"https://default.dummy/{id}/{documentName}",
            OriginalName = Path.GetFileNameWithoutExtension(documentName),
            FileExtension = Path.GetExtension(documentName)
        };
        return Task.FromResult<IStorageDocument>(result);
    }
}
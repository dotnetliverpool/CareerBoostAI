using CareerBoostAI.Application.Common.Abstractions.Transaction;

namespace CareerBoostAI.Application.Services;


public enum StorageContainer
{
    Cv,
}

public enum StorageMedium
{
    AzureStorageBlob
}
public interface IStorageDocument
{
    public Guid Id { get; init; }
    public string Address { get; init; }
    
    public string OriginalName { get; init; }
    
    public string FileExtension { get; init; }
    
    public StorageMedium StorageMedium { get; }
}

public interface IStorageService
{
    Task<IStorageDocument> UploadFileAsync(
        StorageContainer container,
        Stream documentStream, string documentName, CancellationToken cancellationToken);
    
    Task DeleteAsync(StorageContainer container, string documentName, CancellationToken cancellationToken);
    Task DeleteAsync(string storageAddress, CancellationToken cancellationToken);
    
    IRollBackAction GetUploadRollBackAction(string storageAddress);
    
}
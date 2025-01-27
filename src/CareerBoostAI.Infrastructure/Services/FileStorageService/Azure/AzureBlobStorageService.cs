using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CareerBoostAI.Application.Services;

namespace CareerBoostAI.Infrastructure.Services.FileStorageService.Azure;

public class AzureBlobUploadDocument : IStorageDocument
{
    public Guid Id { get; init; }
    public string Address { get; init; }
    public string OriginalName { get; init; }
    public string FileExtension { get; init; }
    public StorageMedium StorageMedium => StorageMedium.AzureStorageBlob;
}

public class AzureBlobStorageService : IStorageService
{

    private readonly BlobServiceClient _client;

    public AzureBlobStorageService(BlobServiceClient client)
    {
        _client = client;
    }

    public async Task<IStorageDocument> UploadFileAsync(
        StorageContainer container,
        Stream documentStream, string documentName, 
        CancellationToken cancellationToken)
    {
        var containerClient = _client.GetBlobContainerClient(container.ToString());
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob, 
            cancellationToken: cancellationToken);
        var id = Guid.NewGuid();
        var uniqueFileName = $"{id}-{documentName}";

        var blobClient = containerClient.GetBlobClient(uniqueFileName);
        await blobClient.UploadAsync(documentStream,
            new BlobHttpHeaders { ContentType = GetContentType(documentName) },
            cancellationToken: cancellationToken);
        
        var result =  new AzureBlobUploadDocument
        {
            Id = id,
            Address = blobClient.Uri.ToString(),
            OriginalName = Path.GetFileNameWithoutExtension(documentName),
            FileExtension = Path.GetExtension(documentName)
        };
        return result;
    }
    
    private static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".txt" => "text/plain",
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => throw new UnsupportedFileTypeException($"The file type '{extension}' is not supported.")
        };
    }
}

public class UnsupportedFileTypeException : Exception
{
    public UnsupportedFileTypeException(string message) : base(message) { }
}
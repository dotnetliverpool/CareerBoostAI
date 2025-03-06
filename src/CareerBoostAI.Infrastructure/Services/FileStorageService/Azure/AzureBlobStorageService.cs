using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CareerBoostAI.Application.Common.Abstractions.Transaction;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Infrastructure.Common.Exception;

namespace CareerBoostAI.Infrastructure.Services.FileStorageService.Azure;

public class AzureBlobUploadDocument : IStorageDocument
{
    public Guid Id { get; init; }
    public required string Address { get; init; }
    public required string OriginalName { get; init; }
    public required string FileExtension { get; init; }
    public StorageMedium StorageMedium => StorageMedium.AzureStorageBlob;
}

public class AzureBlobStorageService(BlobServiceClient client) : IStorageService
{
    public async Task<IStorageDocument> UploadFileAsync(
        StorageContainer container,
        Stream documentStream, string documentName, 
        CancellationToken cancellationToken)
    {
        var containerClient = client.GetBlobContainerClient(container.ToString());
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

    public async Task DeleteAsync(StorageContainer container, string documentName, CancellationToken cancellationToken)
    {
        var containerClient = client.GetBlobContainerClient(container.ToString());
        var blobClient = containerClient.GetBlobClient(documentName);
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(string storageAddress, CancellationToken cancellationToken)
    {
        var blobClient = new BlobClient(new Uri(storageAddress));
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public IRollBackAction GetUploadRollBackAction(string storageAddress)
    {
        return new UploadDocumentRollBackAction(this, storageAddress);
    }

    private static string GetContentType(string fileName)
    {
        // Have to explicitly ensure it supports filetypes the application supports
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".txt" => "text/plain",
            ".pdf" => "application/pdf",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            _ => throw new UnsupportedFileTypeException($"The file type '{extension}' is not supported.")
        };
    }
}


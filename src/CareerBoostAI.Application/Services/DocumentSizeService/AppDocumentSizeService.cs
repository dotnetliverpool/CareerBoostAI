namespace CareerBoostAI.Application.Services.DocumentSizeService;

public class AppDocumentSizeService : IDocumentSizeService
{
    public long MaxDocumentSize => 5 * 1024 * 1024;
    public bool IsDocumentWithinAppLimit(Stream documentStream)
    {
        return documentStream.Length < MaxDocumentSize;
    }

    public string GetMaxSizeInFormat(DocumentSizeFormat format) => format switch
    {
        DocumentSizeFormat.Kb => $"{MaxDocumentSize / 1024} KB",
        DocumentSizeFormat.Mb => $"{MaxDocumentSize / (1024 * 1024)} MB",
        DocumentSizeFormat.Gb => $"{MaxDocumentSize / (1024 * 1024 * 1024)} GB",
        _ => throw new ApplicationException("Unsupported file format")
    };
}
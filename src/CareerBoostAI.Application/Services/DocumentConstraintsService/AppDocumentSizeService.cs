namespace CareerBoostAI.Application.Services.DocumentConstraintsService;

public class AppDocumentSizeService : IDocumentConstraintsService
{
    public long MaxDocumentSize => 5 * 1024 * 1024;
    public bool SizeWithinLimit(Stream documentStream)
    {
        return documentStream.Length < MaxDocumentSize;
    }

    public bool SupportsDocumentType(string documentName)
    {
        return true;
    }

    public IEnumerable<string> GetSupportedFileTypes()
    {
        return Enumerable.Empty<string>();
    }

    public string GetMaxSizeInFormat(DocumentSizeFormat format) => format switch
    {
        DocumentSizeFormat.Kb => $"{MaxDocumentSize / 1024} KB",
        DocumentSizeFormat.Mb => $"{MaxDocumentSize / (1024 * 1024)} MB",
        DocumentSizeFormat.Gb => $"{MaxDocumentSize / (1024 * 1024 * 1024)} GB",
        _ => throw new ApplicationException("Unsupported file format")
    };
}
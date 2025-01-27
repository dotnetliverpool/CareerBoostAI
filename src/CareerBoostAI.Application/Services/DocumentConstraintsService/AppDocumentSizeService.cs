namespace CareerBoostAI.Application.Services.DocumentConstraintsService;

public class AppDocumentSizeService : IDocumentConstraintsService
{
    private readonly long _maxDocumentSize = 5 * 1024 * 1024;
    
    private readonly HashSet<string> _supportedFileTypes = new()
    {
        ".txt", ".pdf", ".doc", ".docx"
    };
    public bool SizeWithinLimit(Stream documentStream)
    {
        return documentStream.Length < _maxDocumentSize;
    }

    public bool SupportsDocumentType(string documentName)
    {
        var extension = Path.GetExtension(documentName)?.ToLowerInvariant();
        return !string.IsNullOrEmpty(extension) && _supportedFileTypes.Contains(extension);
    }

    public IEnumerable<string> GetSupportedFileTypes()
    {
        return _supportedFileTypes;
    }

    public string GetMaxSizeInFormat(DocumentSizeFormat format) => format switch
    {
        DocumentSizeFormat.Kb => $"{_maxDocumentSize / 1024} KB",
        DocumentSizeFormat.Mb => $"{_maxDocumentSize / (1024 * 1024)} MB",
        DocumentSizeFormat.Gb => $"{_maxDocumentSize / (1024 * 1024 * 1024)} GB",
        _ => throw new ApplicationException("Unsupported file format")
    };
}
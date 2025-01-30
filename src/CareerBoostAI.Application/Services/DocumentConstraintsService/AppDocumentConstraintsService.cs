namespace CareerBoostAI.Application.Services.DocumentConstraintsService;

public class AppDocumentConstraintsService : IDocumentConstraintsService
{
    private readonly long _maxDocumentSize = 5 * 1024 * 1024;
    
    private readonly List<SupportedDocumentTypes> _supportedFileTypes = 
        Enum.GetValues(typeof(SupportedDocumentTypes)).Cast<SupportedDocumentTypes>().ToList();
    public bool SizeWithinLimit(long sizeInBytes)
    {
        return sizeInBytes > 0 
               && sizeInBytes < _maxDocumentSize;
    }

    public bool SupportsDocumentType(string documentName)
    {
        var extension = Path.GetExtension(documentName)?.ToLowerInvariant();
        return !string.IsNullOrEmpty(extension) && _supportedFileTypes
            .Any(type => extension == $".{type.ToString().ToLowerInvariant()}");
    }
    
    public SupportedDocumentTypes? GetDocumentType(string documentName)
    {
        var extension = Path.GetExtension(documentName)?.ToLowerInvariant();

        if (string.IsNullOrEmpty(extension))
            return null;

        return _supportedFileTypes
            .FirstOrDefault(type => extension == $".{type.ToString().ToLowerInvariant()}");
    }

    public IEnumerable<SupportedDocumentTypes> GetSupportedFileTypes()
    {
        return Enum.GetValues(typeof(SupportedDocumentTypes)).Cast<SupportedDocumentTypes>();
    }

    public string GetMaxSizeInFormat(DocumentSizeFormat format) => format switch
    {
        DocumentSizeFormat.Kb => $"{_maxDocumentSize / 1024} KB",
        DocumentSizeFormat.Mb => $"{_maxDocumentSize / (1024 * 1024)} MB",
        DocumentSizeFormat.Gb => $"{_maxDocumentSize / (1024 * 1024 * 1024)} GB",
        _ => throw new ApplicationException("Unsupported file format")
    };
}
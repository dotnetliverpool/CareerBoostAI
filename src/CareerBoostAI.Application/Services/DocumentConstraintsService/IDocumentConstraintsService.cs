namespace CareerBoostAI.Application.Services.DocumentConstraintsService;


public enum SupportedDocumentTypes
{
    Pdf,
    Doc,
    Txt
}
public enum DocumentSizeFormat
{
    Kb,
    Mb,
    Gb
}
public interface IDocumentConstraintsService
{
    bool SizeWithinLimit(long sizeInBytes);

    bool SupportsDocumentType(string documentName);

    public SupportedDocumentTypes? GetDocumentType(string documentName);

    public IEnumerable<SupportedDocumentTypes> GetSupportedFileTypes();
    string GetMaxSizeInFormat(DocumentSizeFormat format);
}
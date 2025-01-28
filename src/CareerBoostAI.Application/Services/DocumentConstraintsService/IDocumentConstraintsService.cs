namespace CareerBoostAI.Application.Services.DocumentConstraintsService;


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

    public IEnumerable<string> GetSupportedFileTypes();
    string GetMaxSizeInFormat(DocumentSizeFormat format);
}
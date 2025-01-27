namespace CareerBoostAI.Application.Services.DocumentConstraintsService;


public enum DocumentSizeFormat
{
    Kb,
    Mb,
    Gb
}
public interface IDocumentConstraintsService
{
    long MaxDocumentSize { get; }
    bool SizeWithinLimit(Stream documentStream);

    bool SupportsDocumentType(string documentName);

    public IEnumerable<string> GetSupportedFileTypes();
    string GetMaxSizeInFormat(DocumentSizeFormat format);
}
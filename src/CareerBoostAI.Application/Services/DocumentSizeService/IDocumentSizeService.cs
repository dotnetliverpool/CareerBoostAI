namespace CareerBoostAI.Application.Services.DocumentSizeService;


public enum DocumentSizeFormat
{
    Kb,
    Mb,
    Gb
}
public interface IDocumentSizeService
{
    long MaxDocumentSize { get; }
    bool IsDocumentWithinAppLimit(Stream documentStream);
    string GetMaxSizeInFormat(DocumentSizeFormat format);
}
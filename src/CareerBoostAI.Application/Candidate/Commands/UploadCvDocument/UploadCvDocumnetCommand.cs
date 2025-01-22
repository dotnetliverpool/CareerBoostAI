namespace CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;

public sealed class UploadCvDocumentCommand
{
    public string Email { get; }
    public string DocumentName { get; }
    public Stream DocumentContent { get; }
    public string ContentType { get; }

    public UploadCvDocumentCommand(string email, string documentName, Stream documentContent, string contentType)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
        DocumentName = documentName ?? throw new ArgumentNullException(nameof(documentName));
        DocumentContent = documentContent ?? throw new ArgumentNullException(nameof(documentContent));
        ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
    }
}

using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using CareerBoostAI.Infrastructure.Common.Exception;
using CareerBoostAI.Infrastructure.Services.OcrService.Implementations;

namespace CareerBoostAI.Infrastructure.Services.OcrService;

public class AppOcrService : IOcrService
{
    private IOcrImplementation GetImplementationFor(SupportedDocumentTypes documentType)
    {
        return documentType switch
        {
            SupportedDocumentTypes.Pdf => new PdfPigOcrImplementation(),
            SupportedDocumentTypes.Docx => new OpenXmlDocxImplementation(),
            SupportedDocumentTypes.Txt => new SystemTxtOctImplementation(),
            _ => throw new CareerBoostAiNotImplementedException(nameof(IOcrImplementation), nameof(documentType))
        };
    }

    public  async Task<string?> ExtractTextAsync(Stream documentStream, 
        SupportedDocumentTypes documentType, CancellationToken cancellationToken)
    {
        var implementation = GetImplementationFor(documentType);
        return  await implementation.ExtractTextAsync(documentStream, cancellationToken);
    }
}
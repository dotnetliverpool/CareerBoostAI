using System.Text;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using CareerBoostAI.Infrastructure.Common.Exception;
using CareerBoostAI.Infrastructure.Services.OcrService.Implementations;
using Microsoft.Extensions.FileProviders;
using UglyToad.PdfPig;

namespace CareerBoostAI.Infrastructure.Services.OcrService;

public class AppOcrService : IOcrService
{
    private IOcrImplementation GetImplementationFor(SupportedDocumentTypes documentType)
    {
        switch (documentType)
        {
           case SupportedDocumentTypes.Pdf:
               return new PdfPigOcrImplementation();
           case SupportedDocumentTypes.Doc:
           case SupportedDocumentTypes.Txt:
           default:
               throw new CareerBoostAiNotImplementedException(
                   nameof(IOcrImplementation), nameof(documentType) );
        }
        
    }

    public  async Task<string?> ExtractTextAsync(Stream documentStream, 
        SupportedDocumentTypes documentType, CancellationToken cancellationToken)
    {
        var implementation = GetImplementationFor(documentType);
        return  await implementation.ExtractTextAsync(documentStream, cancellationToken);
    }
}
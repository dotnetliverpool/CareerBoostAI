using System.Text;
using CareerBoostAI.Application.Services;
using UglyToad.PdfPig;

namespace CareerBoostAI.Infrastructure.Services.OcrService.Implementations;

public class PdfPigOcrImplementation : IOcrImplementation
{
    public async Task<string?> ExtractTextAsync(Stream documentStream, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            var fullText = new StringBuilder();

            using (var document = PdfDocument.Open(documentStream))
            {
                foreach (var page in document.GetPages())
                {
                    fullText.AppendLine(page.Text);
                }
            }
            return fullText.ToString();
        }, cancellationToken);
    }
}
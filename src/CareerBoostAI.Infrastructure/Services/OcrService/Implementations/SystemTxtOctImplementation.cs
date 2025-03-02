using System.Text;
using CareerBoostAI.Application.Services;

namespace CareerBoostAI.Infrastructure.Services.OcrService.Implementations;

public class SystemTxtOctImplementation : IOcrImplementation
{
    public async Task<string?> ExtractTextAsync(Stream documentStream, CancellationToken cancellationToken)
    {
        documentStream.Seek(0, SeekOrigin.Begin);
        var stringBuilder = new StringBuilder();

        using var reader = new StreamReader(documentStream);

        while (await reader.ReadLineAsync(cancellationToken) is { } line)
        {
            stringBuilder.AppendLine(line);
        }

        return stringBuilder.ToString();
    }
}
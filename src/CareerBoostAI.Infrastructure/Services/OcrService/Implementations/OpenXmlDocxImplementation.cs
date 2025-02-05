using System.Buffers;
using System.Text;
using System.Xml;
using CareerBoostAI.Application.Services;
using DocumentFormat.OpenXml.Packaging;

namespace CareerBoostAI.Infrastructure.Services.OcrService.Implementations;

public class OpenXmlDocxImplementation : IOcrImplementation
{
    public async Task<string?> ExtractTextAsync(Stream documentStream, CancellationToken cancellationToken)
    {
        documentStream.Seek(0, SeekOrigin.Begin);

        using var wordProcessingDocument = WordprocessingDocument.Open(documentStream, false);
        var mainDocumentPart = wordProcessingDocument.MainDocumentPart;

        if (mainDocumentPart == null) return null;

        var documentText = await ReadDocumentTextAsync(mainDocumentPart);

        var nameTable = new NameTable();
        var xmlNamespaceManager = new XmlNamespaceManager(nameTable);
        xmlNamespaceManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
        var xmlDocument = new XmlDocument(nameTable);
        xmlDocument.LoadXml(documentText);

        var paragraphNodes = xmlDocument.SelectNodes("//w:p", xmlNamespaceManager);
        if (paragraphNodes == null || paragraphNodes.Count == 0) return null;

        
        return await ProcessParagraphNodesAsync(paragraphNodes, xmlNamespaceManager, cancellationToken);
    }

    public Task<string?> ExtractTextAsyncold(Stream documentStream, CancellationToken cancellationToken)
    {
        StringBuilder stringBuilder;
        documentStream.Seek(0, SeekOrigin.Begin);
        using (var wordProcessingDocument = WordprocessingDocument.Open(documentStream, false))
        {
            var mainDocumentPart = wordProcessingDocument.MainDocumentPart;
            if (mainDocumentPart == null) Task.FromResult<string?>(null);
            var nameTable = new NameTable();
            var xmlNamespaceManager = new XmlNamespaceManager(nameTable);
            var wordMlNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
            xmlNamespaceManager.AddNamespace("w", wordMlNamespace);

            string wordProcessingDocumentText;
            using (var streamReader = new StreamReader(mainDocumentPart!.GetStream()))
            {
                wordProcessingDocumentText = streamReader.ReadToEnd();
            }

            stringBuilder = new StringBuilder(wordProcessingDocumentText.Length);

            var xmlDocument = new XmlDocument(nameTable);
            xmlDocument.LoadXml(wordProcessingDocumentText);

            var paragraphNodes = xmlDocument.SelectNodes("//w:p", xmlNamespaceManager);

            if (paragraphNodes == null || paragraphNodes.Count == 0) return Task.FromResult<string?>(null);

            foreach (XmlNode paragraphNode in paragraphNodes)
            {
                var textNodes = paragraphNode.SelectNodes(".//w:t | .//w:tab | .//w:br", xmlNamespaceManager)!;
                foreach (XmlNode textNode in textNodes)
                    switch (textNode.Name)
                    {
                        case "w:t":
                            stringBuilder.Append(textNode.InnerText);
                            break;

                        case "w:tab":
                            stringBuilder.Append("\t");
                            break;

                        case "w:br":
                            stringBuilder.Append("\v");
                            break;
                    }

                stringBuilder.Append(Environment.NewLine);
            }
        }

        return Task.FromResult(stringBuilder.ToString())!;
    }

    private async Task<string> ReadDocumentTextAsync(MainDocumentPart mainDocumentPart)
    {
        var bufferSize = 4096;
        var encoding = Encoding.UTF8;
        var memoryPool = MemoryPool<char>.Shared;
        using var rentedBuffer = memoryPool.Rent(bufferSize);
        var buffer = rentedBuffer.Memory;

        StringBuilder stringBuilder = new();

        using (var streamReader = new StreamReader(
                   mainDocumentPart.GetStream(), encoding,
                   true, bufferSize, true))
        {
            int charsRead;
            while ((charsRead = await streamReader.ReadAsync(buffer)) > 0)
                stringBuilder.Append(buffer[..charsRead].Span);
        }

        return stringBuilder.ToString();
    }


    private async Task<string?> ProcessParagraphNodesAsync(XmlNodeList paragraphNodes,
        XmlNamespaceManager xmlNamespaceManager, CancellationToken cancellationToken)
    {
        var resultStringBuilder = new StringBuilder();

        var tasks = paragraphNodes.Cast<XmlNode>()
            .Where(paragraphNode => paragraphNode != null)  
            .Select(paragraphNode =>
                ProcessParagraphNodeAsync(paragraphNode, xmlNamespaceManager, resultStringBuilder, cancellationToken)
            );

        
        await Task.WhenAll(tasks);

        return resultStringBuilder.ToString();
    }

    private async Task ProcessParagraphNodeAsync(XmlNode paragraphNode, XmlNamespaceManager xmlNamespaceManager,
        StringBuilder resultStringBuilder, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) return;

        var paragraphText = await ProcessTextNodesAsync(paragraphNode, xmlNamespaceManager, cancellationToken);
        if (!string.IsNullOrEmpty(paragraphText))
            lock (resultStringBuilder) // Safely append to the shared StringBuilder
            {
                resultStringBuilder.Append(paragraphText);
                resultStringBuilder.Append(Environment.NewLine);
            }
    }

    private Task<string?> ProcessTextNodesAsync(XmlNode paragraphNode, XmlNamespaceManager xmlNamespaceManager,
        CancellationToken cancellationToken)
    {
        var textNodes = paragraphNode.SelectNodes(".//w:t | .//w:tab | .//w:br", xmlNamespaceManager);
        if (textNodes == null || textNodes.Count == 0 || cancellationToken.IsCancellationRequested)
            return Task.FromResult<string?>(null);

        var paragraphTextBuilder = new StringBuilder();

        foreach (XmlNode textNode in textNodes)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromResult<string?>(null);

            switch (textNode.Name)
            {
                case "w:t":
                    paragraphTextBuilder.Append(textNode.InnerText);
                    break;
                case "w:tab":
                    paragraphTextBuilder.Append('\t');
                    break;
                case "w:br":
                    paragraphTextBuilder.Append('\n');
                    break;
            }
        }

        return Task.FromResult<string?>(paragraphTextBuilder.ToString());
    }
}
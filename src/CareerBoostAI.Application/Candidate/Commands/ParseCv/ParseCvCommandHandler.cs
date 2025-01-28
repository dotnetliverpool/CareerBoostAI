using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.DocumentConstraintsService;

namespace CareerBoostAI.Application.Candidate.Commands.ParseCv;

public sealed class ParseCvCommandHandler(
    IDocumentConstraintsService documentConstraintsService,
    IOcrService ocrService,
    ICvDocumentContentParser cvDocumentContentParser
    ) : ICommandHandler<ParseCvCommand, ParsedCvDocumentDto>
{
    

    public async Task<ParsedCvDocumentDto> Handle(ParseCvCommand command, CancellationToken cancellationToken)
    {
        Validate(command);
        
        var documentContent = await ocrService.ExtractTextAsync(command.DocumentStream, cancellationToken);
        if (documentContent is null)
        {
            throw new DocumentParseFailedException();
        }
        
        var parsedCv = await cvDocumentContentParser.ParseAsync(documentContent, cancellationToken);
        if (parsedCv is null)
        {
            throw new DocumentParseFailedException();
        }

        return parsedCv;
    }

    private void Validate(ParseCvCommand command)
    {
        if (!documentConstraintsService.SupportsDocumentType(command.DocumentName))
        {
            throw new UnsupportedFileTypeException(documentConstraintsService.GetSupportedFileTypes());
        }

        if (!documentConstraintsService.SizeWithinLimit(command.DocumentStream.Length))
        {
            throw new DocumentSizeOutOfBoundsException(
                documentConstraintsService.GetMaxSizeInFormat(DocumentSizeFormat.Mb));
        }
    }
}
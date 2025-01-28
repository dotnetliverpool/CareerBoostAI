using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Application.Candidate.Commands.ParseCv;

public sealed class ParseCvCommandHandler : ICommandHandler<ParseCvCommand, ParsedCvDocumentDto>
{
    private readonly ICvDocumentContentParser _cvDocumentContentParser;

    public ParseCvCommandHandler(ICvDocumentContentParser cvDocumentContentParser)
    {
        _cvDocumentContentParser = cvDocumentContentParser;
    }

    public async Task<ParsedCvDocumentDto> Handle(ParseCvCommand command, CancellationToken cancellationToken)
    {
        // Check that file does not exceed constrained size
        
        // Check that the file type is supported
        
        // use ocr service to extract content as string
        
        // use ai service to return result as parsed cv dto
        
        // return result
        
        // Use the CV parser service to extract details from the document
        var parsedCv = await _cvDocumentContentParser.ParseAsync(
            command.DocumentContent, command.DocumentName, cancellationToken);

        return parsedCv;
    }
}
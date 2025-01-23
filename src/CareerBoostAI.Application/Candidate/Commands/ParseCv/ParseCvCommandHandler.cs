using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services.CvParseService;

namespace CareerBoostAI.Application.Candidate.Commands.ParseCv;

public sealed class ParseCvCommandHandler : ICommandHandler<ParseCvCommand, ParsedCv>
{
    private readonly ICvParserService _cvParserService;

    public ParseCvCommandHandler(ICvParserService cvParserService)
    {
        _cvParserService = cvParserService;
    }

    public async Task<ParsedCv> Handle(ParseCvCommand command, CancellationToken cancellationToken)
    {
        // Use the CV parser service to extract details from the document
        var parsedCv = await _cvParserService.ParseAsync(
            command.DocumentContent, command.DocumentName, cancellationToken);

        return parsedCv;
    }
}
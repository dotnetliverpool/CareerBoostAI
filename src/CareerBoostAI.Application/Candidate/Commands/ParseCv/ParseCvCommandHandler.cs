public sealed class ParseCvDocumentCommandHandler
{
    private readonly ICvParserService _cvParserService;

    public ParseCvDocumentCommandHandler(ICvParserService cvParserService)
    {
        _cvParserService = cvParserService ?? throw new ArgumentNullException(nameof(cvParserService));
    }

    public async Task<CvDto> HandleAsync(ParseCvDocumentCommand command, CancellationToken cancellationToken)
    {
        // Use the CV parser service to extract details from the document
        var cvDto = await _cvParserService.ParseAsync(command.DocumentContent, command.ContentType, cancellationToken);

        // Ensure no ID is included in the returned CvDto
        cvDto.Id = null;

        return cvDto;
    }
}
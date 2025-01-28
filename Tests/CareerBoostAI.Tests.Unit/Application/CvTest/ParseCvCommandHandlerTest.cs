using CareerBoostAI.Application.Candidate.Commands.ParseCv;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using NSubstitute;

namespace CareerBoostAI.Tests.Unit.Application.CvTest;

public class ParseCvCommandHandlerTest
{
    // Test Exception is raised if document file is beyond allowed size
    
    // Test that Exception is raised if document extension is not supported
    
    // Test CouldNotParseDocumentException is Raised When Ocr Returns Null 
    
    // Test CouldNotParseDocumentException is Raised When Document Parser Returns Null
    
    // Test That ParsedCv Dto is returned when document is parsed sucessfully
    #region ARRANGE

    private readonly ICommandHandler<ParseCvCommand, ParsedCvDocumentDto> _commandHandler;
    private readonly IDocumentConstraintsService _documentConstraintsService;
    private readonly IOcrService _ocrService;
    private readonly ICvDocumentContentParser _cvDocumentContentParser;
    

    public UpdateCvCommandHandlerTest()
    {
        _documentConstraintsService = Substitute.For<IDocumentConstraintsService>();
        _ocrService = Substitute.For<IOcrService>();
        _cvDocumentContentParser = Substitute.For<ICvDocumentContentParser>();
        _commandHandler = new ParseCvCommandHandler(_documentConstraintsService, _ocrService, _cvDocumentContentParser);
    }

    #endregion
}
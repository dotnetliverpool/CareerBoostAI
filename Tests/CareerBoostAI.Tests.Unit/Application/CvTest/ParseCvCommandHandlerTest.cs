using CareerBoostAI.Application.Candidate.Commands.ParseCv;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Application.CvTest;

public class ParseCvCommandHandlerTest
{
    private Task<ParsedCvDocumentDto> ActAsync(ParseCvCommand command)
        => _commandHandler.Handle(command, CancellationToken.None);
    // Test that UnsupportedFileTypeException is raised if document extension is not supported
    [Fact]
    public async Task HandleAsync_Throws_UnsupportedFileTypeException_WhenDocumentExtensionIsNotSupported()
    {
        // ARRANGE
        var command = new ParseCvCommand("cv.docx", Stream.Null);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(false);

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UnsupportedFileTypeException>();
    }
    
    // Test Exception is raised if document file is beyond allowed size
    [Fact]
    public async Task HandleAsync_Throws_DocumentSizeOutOfBoundsException_WhenDocumentFileIsBeyondAllowedSize()
    {
        // ARRANGE
        var command = new ParseCvCommand("cv.pdf", Stream.Null);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(true);
        _documentConstraintsService.SizeWithinLimit(command.DocumentStream.Length).Returns(false);

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<DocumentSizeOutOfBoundsException>();
    }
    
    // Test DocumentParseFailedException is Raised When Ocr Returns Null 
    [Fact]
    public async Task HandleAsync_Throws_CouldNotParseDocumentException_WhenOcrReturnsNull()
    {
        // ARRANGE
        var command = new ParseCvCommand("cv.pdf", Stream.Null);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(true);
        _documentConstraintsService.SizeWithinLimit(command.DocumentStream.Length).Returns(true);
        _ocrService.ExtractTextAsync(command.DocumentStream, CancellationToken.None).Returns((string)null!);

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<DocumentParseFailedException>();
    }
    // Test CouldNotParseDocumentException is Raised When Document Parser Returns Null
    [Fact]
    public async Task HandleAsync_Throws_CouldNotParseDocumentException_WhenDocumentParserReturnsNull()
    {
        // ARRANGE
        var command = new ParseCvCommand("cv.pdf", Stream.Null);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(true);
        _documentConstraintsService.SizeWithinLimit(command.DocumentStream.Length).Returns(true);
        _ocrService.ExtractTextAsync(command.DocumentStream, CancellationToken.None).Returns("Extracted text");
        _cvDocumentContentParser.ParseAsync("Extracted text", CancellationToken.None)
            .Returns((ParsedCvDocumentDto)null!);

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<DocumentParseFailedException>();
    }
    
    // Test That ParsedCv Dto is returned when document is parsed sucessfully
    [Fact]
    public async Task HandleAsync_Returns_ParsedCvDocumentDto_WhenDocumentIsParsedSuccessfully()
    {
        // ARRANGE
        var command = new ParseCvCommand("cv.pdf", Stream.Null);
        var expectedDto = new ParsedCvDocumentDto
        {
            Summary = "Test Summary",
            Experiences = [],
            Educations = [],
            Skills = ["Test Skill"],
            Languages = ["Test Language"],
        };

        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(true);
        _documentConstraintsService.SizeWithinLimit(command.DocumentStream.Length).Returns(true);
        _ocrService.ExtractTextAsync(command.DocumentStream, CancellationToken.None).Returns("Extracted text");
        _cvDocumentContentParser.ParseAsync("Extracted text", CancellationToken.None).Returns(expectedDto);

        // ACT
        var result = await ActAsync(command);

        // ASSERT
        result.ShouldNotBeNull();
        result.ShouldBe(expectedDto);
    }
    
    #region ARRANGE

    private readonly ICommandHandler<ParseCvCommand, ParsedCvDocumentDto> _commandHandler;
    private readonly IDocumentConstraintsService _documentConstraintsService;
    private readonly IOcrService _ocrService;
    private readonly ICvDocumentContentParser _cvDocumentContentParser;
    

    public ParseCvCommandHandlerTest()
    {
        _documentConstraintsService = Substitute.For<IDocumentConstraintsService>();
        _ocrService = Substitute.For<IOcrService>();
        _cvDocumentContentParser = Substitute.For<ICvDocumentContentParser>();
        _commandHandler = new ParseCvCommandHandler(_documentConstraintsService, _ocrService, _cvDocumentContentParser);
    }

    #endregion
}
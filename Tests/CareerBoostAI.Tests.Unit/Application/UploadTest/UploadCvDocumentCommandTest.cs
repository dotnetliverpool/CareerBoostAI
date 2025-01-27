using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;
using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.DocumentSizeService;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.UploadContext;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Application.UploadTest;

public class UploadCvDocumentCommandHandlerTest
{
    private Task ActAsync(UploadCvDocumentCommand command)
        => _commandHandler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task HandleAsync_Throws_CandidateProfileNotFoundException_WhenCandidateDoesNotExist()
    {
        // ARRANGE
        var command = new UploadCvDocumentCommand("johndoe@example.com",
            "validCv.pdf", Stream.Null);
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(false);

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CandidateProfileNotFoundException>();
        exception.Message.ShouldContain(command.Email);
    }

    [Fact]
    public async Task HandleAsync_Throws_DocumentExceedsMaximumUploadSizeException_WhenDocumentIsTooLarge()
    {
        // ARRANGE
        var command = new UploadCvDocumentCommand("johndoe@example.com",
            "validCv.pdf", Stream.Null);
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(false);

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UnsupportedFileTypeException>();
    }
    
    [Fact]
    public async Task HandleAsync_Throws_UnsupportedFileTypeException_WhenDocumentTypeIsNotSupported()
    {
        // ARRANGE
        var command = new UploadCvDocumentCommand("johndoe@example.com",
            "validCv.pdf", Stream.Null);
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(true);
        _documentConstraintsService.SizeWithinLimit(command.DocumentStream).Returns(false);
       

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<DocumentExceedsMaximumUploadSizeException>();
        exception.Message.ShouldContain(command.DocumentName);
    }

    [Fact]
    public async Task HandleAsync_InvokesFileStorageService_WhenValidDataIsProvided()
    {
        // ARRANGE
        var command = new UploadCvDocumentCommand("johndoe@example.com",
            "validCv.pdf", Stream.Null);
        var storedDocument = new TestStorageDocument
        {
            Id = Guid.NewGuid(),
            Address = "ftp://testpath/00000-00000-0000.pdf",
            OriginalName = "validCv",
            FileExtension = ".pdf"
        };
        
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(true);
        _documentConstraintsService.SizeWithinLimit(command.DocumentStream).Returns(true);
        _fileStorageService.UploadFileAsync(
            StorageContainer.Cv, command.DocumentStream, command.DocumentName, CancellationToken.None)
            .Returns(storedDocument);

        // ACT
        await ActAsync(command);

        // ASSERT
        await _fileStorageService.Received(1).UploadFileAsync(
            Arg.Is(StorageContainer.Cv),
            Arg.Is(command.DocumentStream),
            Arg.Is(command.DocumentName),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_CreatesAndSavesUploadEntity_WhenValidDataIsProvided()
    {
        // ARRANGE
        var command = new UploadCvDocumentCommand("johndoe@example.com",
            "validCv.pdf", Stream.Null);
        var storedDocument = new TestStorageDocument
        {
            Id = Guid.NewGuid(),
            Address = "ftp://testpath/00000-00000-0000.pdf",
            OriginalName = "validCv",
            FileExtension = ".pdf"
        };

        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        _documentConstraintsService.SupportsDocumentType(command.DocumentName).Returns(true);
        _documentConstraintsService.SizeWithinLimit(command.DocumentStream).Returns(true);
        _fileStorageService.UploadFileAsync(
            StorageContainer.Cv, command.DocumentStream, command.DocumentName, CancellationToken.None)
            .Returns(storedDocument);

        // ACT
        await ActAsync(command);

        // ASSERT
        _uploadFactory.Received(1).Create(Arg.Any<Guid>(), Arg.Any<string>());
        await _uploadRepository.Received(1).CreateNewAsync(Arg.Any<Upload>(), 
            Arg.Any<CancellationToken>());

        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    #region ARRANGE

    private readonly ICommandHandler<UploadCvDocumentCommand> _commandHandler;
    private readonly IDocumentConstraintsService _documentConstraintsService;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUploadFactory _uploadFactory;
    private readonly IUploadRepository _uploadRepository;
    private readonly ICandidateReadService _candidateReadService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UploadCvDocumentCommandHandlerTest()
    {
        _documentSizeService = Substitute.For<IDocumentConstraintsService>();
        _fileStorageService = Substitute.For<IFileStorageService>();
        _uploadFactory = Substitute.For<IUploadFactory>();
        _uploadRepository = Substitute.For<IUploadRepository>();
        _candidateReadService = Substitute.For<ICandidateReadService>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();

        _commandHandler = new UploadCvDocumentCommandHandler(
            _documentSizeService,
            _fileStorageService,
            _uploadRepository,
            _candidateReadService,
            _unitOfWork,
            _dateTimeProvider);

    }

    #endregion
}

public class TestStorageDocument : IStorageDocument
{
    public Guid Id { get; init; }
    public string Address { get; init; }
    public string OriginalName { get; init; }
    public string FileExtension { get; init; }
    public StorageMedium StorageMedium { get; }
}

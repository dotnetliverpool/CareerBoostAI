using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.Commands.CreateProfile;
using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Factory;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Application.Candidate;

public class CreateProfileCommandTest
{
    Task<Guid> Act(CreateProfileCommand command)
        => _commandHandler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task HandleAsync_Throws_CandidateProfileAlreadyExistsException_WhenCandidateAlreadyExists()
    {
        var command = new CreateProfileCommand(
            "John", "Doe", "johndoe@example.com", DateOnly.Parse("1998-12-12"),
                "+44", "1234567890", _commandFactory.GetValidCreateCvCommand());
        
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        
        
            

        var exception = await Record.ExceptionAsync(() => Act(command));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CandidateProfileAlreadyExistsException>();
        exception.Message.ShouldContain(command.Email);
    }
    
    [Fact]
    public async Task HandleAsync_CallsCreateProfileAndCreateCvOnce_WhenValidDataIsProvided()
    {
        // ARRANGE
        var command = new CreateProfileCommand(
            "John", "Doe", "johndoe@example.com", DateOnly.Parse("1998-12-12"),
            "+44", "1234567890", _commandFactory.GetValidCreateCvCommand()
        );
        var id = Guid.NewGuid();
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(false);
        _candidateFactory.Create(command.FirstName, command.LastName, command.DateOfBirth, command.Email,
                command.PhoneCode, command.PhoneNumber)
            .Returns(_domainFactory.GetCandidateFromCommand(id, command));
        _cvFactory.CreateFromData(command.Email, command.CreateCvCommand.AsDomainCvData())
            .Returns(_domainFactory.GetCvFromCommand(Guid.NewGuid(), command.CreateCvCommand));

        // ACT
        var result = await Act(command);

        // ASSERT
        result.ShouldNotBe(Guid.Empty);
        result.ShouldBe(id);

        _candidateFactory.Received(1).Create(
            Arg.Is(command.FirstName),
            Arg.Is(command.LastName),
            Arg.Is(command.DateOfBirth),
            Arg.Is(command.Email),
            Arg.Is(command.PhoneCode),
            Arg.Is(command.PhoneNumber)
        );

        _cvFactory.Received(1).CreateFromData(
            Arg.Is(command.Email),
            Arg.Any<CvData>());
    }
    
    [Fact]
    public async Task HandleAsync_SavesCandidateAndCvOnceAndCommitsUnitOfWork_WhenValidDataIsProvided()
    {
        // ARRANGE
        var command = new CreateProfileCommand(
            "John", "Doe", "johndoe@example.com", DateOnly.Parse("1998-12-12"),
            "+44", "1234567890", _commandFactory.GetValidCreateCvCommand()
        );
        var id = Guid.NewGuid();
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(false);
        _candidateFactory.Create(command.FirstName, command.LastName, command.DateOfBirth, command.Email,
                command.PhoneCode, command.PhoneNumber)
            .Returns(_domainFactory.GetCandidateFromCommand(id, command));
        _cvFactory.CreateFromData(command.Email, command.CreateCvCommand.AsDomainCvData())
            .Returns(_domainFactory.GetCvFromCommand(Guid.NewGuid(), command.CreateCvCommand));

        // ACT
        await Act(command);

        // ASSERT
        await _candidateRepository.Received(1).CreateNewAsync(
            Arg.Any<CareerBoostAI.Domain.CandidateContext.Candidate>());
        await _cvRepository.Received(1).CreateNewAsync(
            Arg.Any<Cv>());

        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }
    
    // Notification is sent
    
    // Save suceeds if notification is not sent
    
    #region ARRANGE

    private readonly ICommandHandler<CreateProfileCommand, Guid> _commandHandler;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IEmailSender _emailSender;
    private readonly ICandidateFactory _candidateFactory;
    private readonly ICvFactory _cvFactory;
    private readonly ICvRepository _cvRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICandidateReadService _candidateReadService;
    private readonly CommandFactory _commandFactory;
    private readonly TestDomainFactory _domainFactory;

    public CreateProfileCommandTest()
    {
        _candidateReadService = Substitute.For<ICandidateReadService>();
        _candidateRepository = Substitute.For<ICandidateRepository>();
        _candidateFactory = Substitute.For<ICandidateFactory>();
        _cvRepository = Substitute.For<ICvRepository>();
        _cvFactory = Substitute.For<ICvFactory>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _emailSender = Substitute.For<IEmailSender>();

        _commandHandler = new CreateProfileCommandHandler(
            _candidateReadService, _candidateRepository, _candidateFactory,
            _cvRepository, _cvFactory, _unitOfWork, _emailSender
            );
        _commandFactory = new CommandFactory();
        _domainFactory = new TestDomainFactory();

    }

    #endregion
}
using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Notifications;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CandidateContext.Services;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.Services;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Application.CandidateTest;

public class CreateProfileCommandHandlerTest
{
    Task ActAsync(CreateOrUpdateProfileCommand command)
        => _commandHandler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task HandleAsync_RetrievesAndUpdatesCandidateAndCv_WhenCandidateAlreadyExists()
    {
        var command = new CreateOrUpdateProfileCommand(
            "Jane", "Jones", "johndoe@example.com", DateOnly.Parse("1998-01-01"),
                "+1", "98765234", 
            _commandFactory.GetValidCreateCvCommand(5, 4));
        var id = Guid.NewGuid();
        var cvId = Guid.NewGuid();
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        _candidateRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(_domainFactory.GetDefaultCandidate(
                id, "John", "Doe", DateOnly.Parse("1998-12-01"), phoneCode: "+44"));

        _cvRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(_domainFactory.GetDefaultCv(cvId));
        
        // ACT
        await ActAsync(command);
        
        // ASSERT
        _candidateProfileUpdateDomainService
            .Received(1)
            .Update(Arg.Any<Candidate>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<DateOnly>(),
                Arg.Any<string>(), Arg.Any<string>());
        _cvUpdateService
            .Received(1)
            .Update(Arg.Any<Cv>(), Arg.Any<CvData>());
        await _candidateRepository
            .Received(1)
            .UpdateAsync(Arg.Any<Candidate>(), Arg.Any<CancellationToken>());
        await _cvRepository
            .Received(1)
            .UpdateAsync(Arg.Any<Cv>(), Arg.Any<CancellationToken>());
        await _unitOfWork
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());

    }
    
    [Fact]
    public async Task HandleAsync_DoesNotCreateCandidateOrCv_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new CreateOrUpdateProfileCommand(
            "Jane", "Jones", "johndoe@example.com", DateOnly.Parse("1998-01-01"),
            "+1", "98765234", 
            _commandFactory.GetValidCreateCvCommand(5, 4));
        var id = Guid.NewGuid();
        var cvId = Guid.NewGuid();
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        _candidateRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(_domainFactory.GetDefaultCandidate(
                id, "John", "Doe", DateOnly.Parse("1998-12-01"), phoneCode: "+44"));

        _cvRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(_domainFactory.GetDefaultCv(cvId));

        // Act
        await ActAsync(command);

        // Assert
        _candidateFactory
            .DidNotReceive()
            .Create(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateOnly>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        _cvFactory
            .DidNotReceive()
            .CreateFromData(Arg.Any<string>(), Arg.Any<CvData>());
        await _candidateRepository
            .DidNotReceive()
            .CreateNewAsync(Arg.Any<Candidate>(), Arg.Any<CancellationToken>());
        await _cvRepository
            .DidNotReceive()
            .CreateNewAsync(Arg.Any<Cv>(), Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task HandleAsync_SendsCandidateProfileUpdatedNotification_WhenCandidateExists()
    {
        // ARRANGE
        var command = new CreateOrUpdateProfileCommand(
            "Jane", "Jones", "johndoe@example.com", DateOnly.Parse("1998-01-01"),
            "+1", "98765234", 
            _commandFactory.GetValidCreateCvCommand(5, 4));
        var id = Guid.NewGuid();
        var cvId = Guid.NewGuid();
        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(true);
        _candidateRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(_domainFactory.GetDefaultCandidate(
                id, "John", "Doe", DateOnly.Parse("1998-12-01"), phoneCode: "+44"));

        _cvRepository.GetByEmailAsync(command.Email, Arg.Any<CancellationToken>())
            .Returns(_domainFactory.GetDefaultCv(cvId));
        
        // ACT
        await ActAsync(command);

        // Assert
        await _emailService
            .Received(1)
            .SendToAdminAsync(Arg.Any<CandidateProfileUpdatedNotification>());

        await _emailService
            .DidNotReceive()
            .SendToAdminAsync(Arg.Any<CandidateProfileCreatedNotification>());
    }

    
    [Fact]
    public async Task HandleAsync_CreatesCandidateAndCv_WhenEmailDoesNotExist()
    {
        // Arrange
        var command = new CreateOrUpdateProfileCommand(
            "Jane", "Jones", "janedoe@example.com", DateOnly.Parse("1998-01-01"),
            "+1", "98765234", _commandFactory.GetValidCreateCvCommand(5, 4));

        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(false);
        _candidateFactory.Create(
            command.FirstName, command.LastName,
            DateOnly.Parse("1998-01-01"),
            command.Email, command.PhoneCode, command.PhoneNumber)
            .Returns(_domainFactory.GetDefaultCandidate(Guid.NewGuid()));
        _cvFactory
            .CreateFromData(command.Email, Arg.Any<CvData>())
            .Returns(_domainFactory.GetDefaultCv(Guid.NewGuid()));

        // Act
        await ActAsync(command);

        // Assert
        _candidateFactory
            .Received(1)
            .Create(command.FirstName, command.LastName, 
                command.DateOfBirth, command.Email,
                command.PhoneCode, command.PhoneNumber );
        _cvFactory
            .Received(1)
            .CreateFromData(command.Email, Arg.Any<CvData>());

        await _candidateRepository
            .Received(1)
            .CreateNewAsync(Arg.Any<Candidate>(), Arg.Any<CancellationToken>());
    
        await _cvRepository
            .Received(1)
            .CreateNewAsync(Arg.Any<Cv>(), Arg.Any<CancellationToken>());
    
        await _unitOfWork
            .Received(1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task HandleAsync_DoesNotUpdateCandidateOrCv_WhenEmailDoesNotExist()
    {
        // Arrange
        var command = new CreateOrUpdateProfileCommand(
            "Jane", "Jones", "janedoe@example.com", DateOnly.Parse("1998-01-01"),
            "+1", "98765234", _commandFactory.GetValidCreateCvCommand(5, 4));

        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(false);
        _candidateFactory.Create(
                command.FirstName, command.LastName,
                DateOnly.Parse("1998-01-01"),
                command.Email, command.PhoneCode, command.PhoneNumber)
            .Returns(_domainFactory.GetDefaultCandidate(Guid.NewGuid()));
        _cvFactory
            .CreateFromData(command.Email, Arg.Any<CvData>())
            .Returns(_domainFactory.GetDefaultCv(Guid.NewGuid()));

        // Act
        await ActAsync(command);

        // Assert
        _candidateProfileUpdateDomainService
            .DidNotReceive()
            .Update(Arg.Any<Candidate>(), Arg.Any<string>(), 
                Arg.Any<string>(), Arg.Any<DateOnly>(), 
                Arg.Any<string>(), Arg.Any<string>());
    
        _cvUpdateService
            .DidNotReceive()
            .Update(Arg.Any<Cv>(), Arg.Any<CvData>());

        await _candidateRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Candidate>(), Arg.Any<CancellationToken>());
    
        await _cvRepository
            .DidNotReceive()
            .UpdateAsync(Arg.Any<Cv>(), Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task HandleAsync_SendsCandidateProfileCreatedNotification_WhenEmailDoesNotExist()
    {
        // Arrange
        var command = new CreateOrUpdateProfileCommand(
            "Jane", "Jones", "janedoe@example.com", DateOnly.Parse("1998-01-01"),
            "+1", "98765234", _commandFactory.GetValidCreateCvCommand(5, 4));

        _candidateReadService.CandidateExistsByEmailAsync(command.Email, CancellationToken.None).Returns(false);
        _candidateFactory.Create(
                command.FirstName, command.LastName,
                DateOnly.Parse("1998-01-01"),
                command.Email, command.PhoneCode, command.PhoneNumber)
            .Returns(_domainFactory.GetDefaultCandidate(Guid.NewGuid()));
        _cvFactory
            .CreateFromData(command.Email, Arg.Any<CvData>())
            .Returns(_domainFactory.GetDefaultCv(Guid.NewGuid()));

        // Act
        await ActAsync(command);

        // Assert
        await _emailService
            .Received(1)
            .SendToAdminAsync(Arg.Any<CandidateProfileCreatedNotification>());
    
        await _emailService
            .DidNotReceive()
            .SendToAdminAsync(Arg.Any<CandidateProfileUpdatedNotification>());
    }



    #region ARRANGE

    private readonly ICommandHandler<CreateOrUpdateProfileCommand> _commandHandler;
    private readonly ICandidateProfileUpdateDomainService _candidateProfileUpdateDomainService;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IEmailService _emailService;
    private readonly ICandidateFactory _candidateFactory;
    private readonly ICvFactory _cvFactory;
    private readonly ICvRepository _cvRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICandidateReadService _candidateReadService;
    private readonly ICvUpdateService _cvUpdateService;
    private readonly CommandFactory _commandFactory;
    private readonly TestDomainFactory _domainFactory;

    public CreateProfileCommandHandlerTest()
    {
        _candidateReadService = Substitute.For<ICandidateReadService>();
        _candidateRepository = Substitute.For<ICandidateRepository>();
        _candidateProfileUpdateDomainService = Substitute.For<ICandidateProfileUpdateDomainService>();
        _candidateFactory = Substitute.For<ICandidateFactory>();
        _cvRepository = Substitute.For<ICvRepository>();
        _cvUpdateService = Substitute.For<ICvUpdateService>();
        _cvFactory = Substitute.For<ICvFactory>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _emailService = Substitute.For<IEmailService>();

        _commandHandler = new CreateProfileCommandHandler(
            _candidateReadService, _candidateRepository, _candidateFactory,
            _candidateProfileUpdateDomainService,
            _cvRepository, _cvUpdateService, _cvFactory, _unitOfWork, _emailService
            );
        _commandFactory = new CommandFactory();
        _domainFactory = new TestDomainFactory();

    }

    #endregion
}
using CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Abstractions.Transaction;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.Services;
using NSubstitute;

namespace CareerBoostAI.Tests.Unit.Application.CvTest;

public class UpdateCvCommandHandlerTest  
{
    Task ActAsync(UpdateCvCommand command)
        => _commandHandler.Handle(command, CancellationToken.None);

    [Fact]
    public async Task HandleAsync_Throws_CandidateCvNotFoundException_WhenCvDoesNotExist()
    {
        // ARRANGE
        var command = new UpdateCvCommand(
            "johndoe@example.com", "Initial Cv Summary", 
            _commandFactory.GetValidCreateExperiences(), _commandFactory.GetValidCreateEducations(),
            _commandFactory.GetValidSkills(), _commandFactory.GetValidLanguages());
        _cvRepository.GetByEmailAsync(command.Email, CancellationToken.None).Returns((Cv)null!);

        // ACT
        var exception = await Record.ExceptionAsync(() => ActAsync(command));

        // ASSERT
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<CandidateCvNotFoundException>();
        exception.Message.ShouldContain(command.Email);
    }

    [Fact]
    public async Task HandleAsync_UpdatesCvAndCommitsUnitOfWork_WhenValidDataIsProvided()
    {
        var (createCommand, updateCommand) = _commandFactory.GetValidUpdateCvCommand("johndoe@example.com");
        var id = Guid.NewGuid();
        var cv = _domainFactory.GetCvFromCommand(id, createCommand);

        _cvRepository.GetByEmailAsync(updateCommand.Email, CancellationToken.None).Returns(cv);

        // ACT
        await ActAsync(updateCommand);

        // ASSERT
        await _cvRepository.Received(1).GetByEmailAsync(Arg.Is(updateCommand.Email), Arg.Any<CancellationToken>());
        await _cvRepository.Received(1).UpdateAsync(Arg.Is(cv), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task HandleAsync_InvokesCvInformationUpdateService_WhenValidDataIsProvided()
    {
        // ARRANGE
        var (createCommand, updateCommand) = _commandFactory.GetValidUpdateCvCommand("johndoe@example.com");
        var id = Guid.NewGuid();
        var cv = _domainFactory.GetCvFromCommand(id, createCommand);

        _cvRepository.GetByEmailAsync(updateCommand.Email, CancellationToken.None).Returns(cv);

        // ACT
        await ActAsync(updateCommand);
        
        // Assert
        _cvUpdateService.Received(1).Update(Arg.Any<Cv>(), Arg.Any<CvData>());
    }
    
    
    // Test Notification is sent after

    #region ARRANGE

    private readonly ICommandHandler<UpdateCvCommand> _commandHandler;
    private readonly ICvRepository _cvRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICvUpdateService _cvUpdateService;
    private readonly CommandFactory _commandFactory;
    private readonly TestDomainFactory _domainFactory;

    public UpdateCvCommandHandlerTest()
    {
        _cvRepository = Substitute.For<ICvRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _cvUpdateService = Substitute.For<ICvUpdateService>();
        _commandHandler = new UpdateCvCommandHandler(_cvRepository, _cvUpdateService, _unitOfWork);
        _commandFactory = new CommandFactory();
        _domainFactory = new TestDomainFactory();
    }

    #endregion
}

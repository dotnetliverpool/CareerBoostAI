using MediatR;

namespace CareerBoostAI.Application.Abstractions.Mediator;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, object>
    where TCommand : ICommand
{
    
}
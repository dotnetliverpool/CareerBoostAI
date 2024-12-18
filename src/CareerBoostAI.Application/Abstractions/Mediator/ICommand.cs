using MediatR;

namespace CareerBoostAI.Application.Abstractions.Mediator;

public interface ICommand : IRequest
{ }

public interface ICommand<TResult> : IRequest<TResult>
{ }
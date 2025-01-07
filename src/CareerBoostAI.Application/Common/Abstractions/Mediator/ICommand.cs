using MediatR;

namespace CareerBoostAI.Application.Common.Abstractions.Mediator;

public interface ICommand : IRequest
{ }

public interface ICommand<TResult> : IRequest<TResult>
{ }
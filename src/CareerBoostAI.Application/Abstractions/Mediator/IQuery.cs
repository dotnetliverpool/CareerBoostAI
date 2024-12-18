using MediatR;

namespace CareerBoostAI.Application.Abstractions.Mediator;

public interface IQuery : IRequest
{ }

public interface IQuery<TResult> : IRequest<TResult>
{}
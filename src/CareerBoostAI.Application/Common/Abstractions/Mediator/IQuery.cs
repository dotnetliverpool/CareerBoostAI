using MediatR;

namespace CareerBoostAI.Application.Common.Abstractions.Mediator;

public interface IQuery : IRequest
{ }

public interface IQuery<TResult> : IRequest<TResult>
{}
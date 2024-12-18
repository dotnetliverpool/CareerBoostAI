using MediatR;

namespace CareerBoostAI.Application.Abstractions.Mediator;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{}

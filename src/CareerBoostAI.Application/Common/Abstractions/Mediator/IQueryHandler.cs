using MediatR;

namespace CareerBoostAI.Application.Common.Abstractions.Mediator;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{}

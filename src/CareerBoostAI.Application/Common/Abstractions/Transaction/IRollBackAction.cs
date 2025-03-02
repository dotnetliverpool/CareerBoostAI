namespace CareerBoostAI.Application.Common.Abstractions.Transaction;

public interface IRollBackAction
{
    Task RollBackAsync(CancellationToken cancellationToken);
}


namespace CareerBoostAI.Application.Common.Abstractions.Transaction;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
    
    void RegisterRollBackAction(IRollBackAction rollBackAction, CancellationToken cancellationToken);
}
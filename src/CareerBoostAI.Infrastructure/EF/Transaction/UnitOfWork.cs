using CareerBoostAI.Application.Common.Abstractions.Transaction;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Shared.Abstractions.Exceptions;

namespace CareerBoostAI.Infrastructure.EF.Transaction;

internal sealed class UnitOfWork(CareerBoostWriteDbContext context) : IUnitOfWork
{
    private readonly HashSet<IRollBackAction> _rollBackActions = new HashSet<IRollBackAction>();

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            foreach (var action in _rollBackActions)
            {
                await action.RollBackAsync(cancellationToken);
            }
            throw;
        }
        finally
        {
            ClearRollBackActions();
        }

    }

    public void RegisterRollBackAction(IRollBackAction rollBackAction, CancellationToken cancellationToken)
    {
        _rollBackActions.Add(rollBackAction);
    }

    private void ClearRollBackActions()
    {
        // Clear the rollback actions after processing
        _rollBackActions.Clear();
    }
}
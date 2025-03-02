using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.CvContext;

public interface ICvRepository
{
    Task<Cv?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Cv?> GetByIdAsync(EntityId id, CancellationToken cancellationToken);

    Task CreateNewAsync(Cv cv, CancellationToken cancellationToken);

    Task UpdateAsync(Cv cv, CancellationToken cancellationToken);
}
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.CvContext;

public interface ICvRepository
{
    Task<Cv?> GetByEmailAsync(string email);
    Task<Cv?> GetByIdAsync(EntityId id);

    Task CreateNewAsync(Cv cv);

    Task UpdateAsync(Cv cv);
}
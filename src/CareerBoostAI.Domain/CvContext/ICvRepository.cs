namespace CareerBoostAI.Domain.CvContext;

public interface ICvRepository
{
    Cv GetByEmailAsync();
    Cv GetByIdAsync();
    void SaveAsync(Cv cv);
    void UpdateAsync(Cv cv);
}
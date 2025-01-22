namespace CareerBoostAI.Domain.CvContext;

public interface ICvRepository
{
    Task<Cv?> GetByEmailAsync(string email);
    Task<Cv?> GetByIdAsync();

    Task CreateNewAsync(Cv cv);

    Task DeleteAsync(Cv cv);

}
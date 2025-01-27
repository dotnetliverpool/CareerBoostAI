using CareerBoostAI.Domain.CvContext.Factory;

namespace CareerBoostAI.Domain.CvContext.Services;

public interface ICvUpdateService
{
    public void Update(Cv cv, CvData data);
}
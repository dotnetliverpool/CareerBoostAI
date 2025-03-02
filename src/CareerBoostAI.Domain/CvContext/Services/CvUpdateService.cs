using CareerBoostAI.Domain.CvContext.Factory;

namespace CareerBoostAI.Domain.CvContext.Services;

public class CvUpdateService : ICvUpdateService
{
    public void Update(Cv cv, CvData data)
    {
        cv.UpdateSummary(data.Summary);
        cv.UpdateSkills(data.Skills);
        cv.UpdateLanguages(data.Languages);
        cv.UpdateExperiences(data.Experiences);
        cv.UpdateEducations(data.Educations);
    }
}
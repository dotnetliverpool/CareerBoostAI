using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Factory;

namespace CareerBoostAI.Domain.Services;

public static class CvInformationUpdateService
{
    public static void Update(Cv cv, CvData data)
    {
        cv.UpdateSummary(data.Summary);
        cv.UpdateSkills(data.Skills);
        cv.UpdateLanguages(data.Languages);
        cv.ClearExperiences();
        cv.ClearEducations();
        cv.AddExperiences(data.Experiences);
        cv.AddEducations(data.Educations);
    }
    
    
}
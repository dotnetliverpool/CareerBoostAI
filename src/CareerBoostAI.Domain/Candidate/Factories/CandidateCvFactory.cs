using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public class CandidateCvFactory : ICandidateCvFactory
{

    public CvEntity.Cv Create(CvFile file)
    {
        var result = new CvEntity.Cv(CvId.New(), file);
        return result;
    }
}
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public class CandidateCvFactory : ICandidateCvFactory
{

    public Cv.Cv Create(CvFile file)
    {
        var result = new Cv.Cv(CvId.New(), file);
        return result;
    }
}
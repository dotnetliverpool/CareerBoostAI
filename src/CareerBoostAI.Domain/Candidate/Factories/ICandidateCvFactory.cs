using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public interface ICandidateCvFactory
{
    CvEntity.Cv Create(CvFile cvFile);
}
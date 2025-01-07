using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public interface ICandidateCvFactory
{
    Cv.Cv Create(CvFile cvFile);
}
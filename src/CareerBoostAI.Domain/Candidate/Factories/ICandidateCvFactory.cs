using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public interface ICandidateCvFactory
{
    Entities.Cv Create(CvId id, CvFile cvFile);
}
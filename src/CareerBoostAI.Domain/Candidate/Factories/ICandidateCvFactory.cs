using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Factories;

public interface ICandidateCvFactory
{
    Cv Create(CandidateCvId id, CvFile cvFile);
}
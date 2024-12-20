using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Factories;

public interface ICandidateCvFactory
{
    CandidateCv Create(CandidateCvId id, CvFile cvFile);
}
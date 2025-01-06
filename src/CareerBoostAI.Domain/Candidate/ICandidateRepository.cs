using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.ValueObjects;

namespace CareerBoostAI.Domain.Candidate;

public interface ICandidateRepository
{
    Task<Candidate> GetAsync(CandidateId id);
    Task AddCandidateAsync(Candidate candidate);

    Task AddCandidateCvAsync(Cv.Cv cv);
    Task UpdateAsync(Candidate candidate);
}
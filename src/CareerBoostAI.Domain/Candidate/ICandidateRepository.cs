using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Repositories;

public interface ICandidateRepository
{
    Task<Candidate> GetAsync(CandidateId id);
    Task AddCandidateAsync(Candidate candidate);

    Task AddCandidateCvAsync(Cv cv);
    Task UpdateAsync(Candidate candidate);
}
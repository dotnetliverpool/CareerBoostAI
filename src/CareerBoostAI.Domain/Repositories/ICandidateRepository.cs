using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Repositories;

public interface ICandidateRepository
{
    Task<Candidate> GetAsync(CandidateId id);
    Task AddAsync(Candidate candidate);
    Task UpdateAsync(Candidate candidate);
}
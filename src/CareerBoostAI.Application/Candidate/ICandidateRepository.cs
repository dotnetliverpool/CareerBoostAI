using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Domain.Candidate;

namespace CareerBoostAI.Application.Candidate;

public interface ICandidateRepository
{
    Task<CandidateDto?> GetAsync(Guid id);
    Task AddAsync(CandidateAggregate candidate);
    Task UpdateAsync(CandidateAggregate candidate);
}
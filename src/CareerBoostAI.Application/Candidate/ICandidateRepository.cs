using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Domain.Candidate;

namespace CareerBoostAI.Application.Candidate;

public interface ICandidateRepository
{
    Task<CandidateAggregate?> GetAsync(Guid id);
    Task CreateNewAsync(CandidateAggregate candidate);
}
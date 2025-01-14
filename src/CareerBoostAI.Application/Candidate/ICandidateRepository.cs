using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.ValueObjects;

namespace CareerBoostAI.Application.Candidate;

public interface ICandidateRepository
{
    Task<CandidateAggregate?> GetAsync(CandidateId id);
    Task CreateNewAsync(CandidateAggregate candidate);
}
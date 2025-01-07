using CareerBoostAI.Application.Candidate.DTO;

namespace CareerBoostAI.Application.Candidate;

public interface ICandidateRepository
{
    Task<CandidateDto> GetAsync(Guid id);
    Task AddAsync(CandidateDto candidate);
    Task UpdateAsync(CandidateDto candidate);
}
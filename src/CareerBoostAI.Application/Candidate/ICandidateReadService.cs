using CareerBoostAI.Application.Candidate.DTO;

namespace CareerBoostAI.Application.Candidate;

public interface ICandidateReadService
{
    Task<bool> CandidateExistsByEmailAsync(string email, CancellationToken cancellationToken);
}
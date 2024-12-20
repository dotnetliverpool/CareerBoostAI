using CareerBoostAI.Application.DTO;

namespace CareerBoostAI.Application.Services.ReadService.CandidateReadService;

public interface ICandidateReadService
{
    Task<CandidateDTO> SearchCandidateByEmailAsync(string email, CancellationToken cancellationToken);
}
using CareerBoostAI.Application.DTO;

namespace CareerBoostAI.Application.Services.ReadService.CandidateReadService;

public interface ICandidateReadService
{
    Task<CandidateDto> SearchCandidateByEmailAsync(string email, CancellationToken cancellationToken);
}
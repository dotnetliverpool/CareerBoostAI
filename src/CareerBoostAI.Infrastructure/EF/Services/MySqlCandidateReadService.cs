using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Services;

internal sealed class MySqlCandidateReadService(CareerBoostDbContext context) : ICandidateReadService
{
    
    private readonly DbSet<Candidate> _candidates = context.Candidates;
    

    public Task<bool> CandidateExistsByEmailAsync(string email, CancellationToken cancellationToken)
        => _candidates.AnyAsync(e => e.Email == email, cancellationToken);
    
}
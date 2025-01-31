using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Services;

internal sealed class MySqlCandidateReadService(CareerBoostReadDbContext context) : ICandidateReadService
{
    
    private readonly DbSet<CandidateReadModel> _candidates = context.Candidates;
    

    public Task<bool> CandidateExistsByEmailAsync(string email, CancellationToken cancellationToken)
        =>  _candidates
            .Where(c => c.Email == email)
            .Select(c => 1) 
            .AnyAsync(cancellationToken);
}
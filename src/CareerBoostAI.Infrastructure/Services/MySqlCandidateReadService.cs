using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.Services;

internal sealed class MySqlCandidateReadService(CareerBoostDbContext context) : ICandidateReadService
{
    
    private readonly DbSet<Candidate> _candidates = context.Candidates;
    private readonly DbSet<Email> _emails = context.Emails;

    public Task<bool> CandidateExistsByEmailAsync(string email, CancellationToken cancellationToken)
        => _emails.AnyAsync(e => e.Address == email, cancellationToken);
    
}
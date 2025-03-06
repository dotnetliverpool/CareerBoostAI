using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
namespace CareerBoostAI.Infrastructure.EF.Repositories;

internal sealed class MySqlCandidateRepository(CareerBoostWriteDbContext context) : ICandidateRepository
{
    
    private readonly DbSet<Candidate> _candidates = context.Candidates;

    public async Task CreateNewAsync(Candidate candidate, 
        CancellationToken cancellationToken)
    {
        await _candidates.AddAsync(candidate, cancellationToken);
    }

    public async Task<Candidate?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _candidates
            .FirstOrDefaultAsync(candidate => candidate.Email == Email.Create(email), cancellationToken);
    }

    public Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken)
    {
        _candidates.Update(candidate);
        return Task.CompletedTask;
    }
}
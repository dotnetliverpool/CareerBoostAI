using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using CareerBoostAI.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;
namespace CareerBoostAI.Infrastructure.EF.Repositories;

internal sealed class MySqlCandidateRepository(CareerBoostWriteDbContext context) : ICandidateRepository
{
    
    private readonly DbSet<Candidate> _candidates = context.Candidates;
    private readonly CareerBoostWriteDbContext _context = context;

    public async Task CreateNewAsync(Candidate candidate, 
        CancellationToken cancellationToken)
    {
        await _candidates.AddAsync(candidate, cancellationToken);
    }

    public Task<Candidate?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Candidate candidate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
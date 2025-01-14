using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.MappingExtensions;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Repositories;

internal sealed class MySqlCandidateRepository(CareerBoostDbContext context) : ICandidateRepository
{
    
    private readonly DbSet<Candidate> _candidates = context.Candidates;
    private readonly CareerBoostDbContext _context = context;

    public async Task<CandidateAggregate?> GetAsync(Guid id)
    {
        return await _candidates
            .Include(c => c.Uploads)
            .Include(c => c.Cv)
            .SingleOrDefaultAsync(c => c.Id == id);

        
    }

    public async Task CreateNewAsync(CandidateAggregate candidate)
    {
        await _candidates.AddAsync(candidate);
    }
    
}
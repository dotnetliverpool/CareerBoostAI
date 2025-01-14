using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.MappingExtensions;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Repositories;

internal sealed class MySqlCandidateRepository(CareerBoostWriteDbContext context) : ICandidateRepository
{
    
    private readonly DbSet<CandidateAggregate> _candidates = context.Candidates;
    private readonly CareerBoostWriteDbContext _context = context;

    public async Task<CandidateAggregate?> GetAsync(CandidateId id)
    {
        return await _candidates
            .Include(c => c.CandidateCv)
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateNewAsync(CandidateAggregate candidate)
    {
        await _candidates.AddAsync(candidate);
    }
    
}
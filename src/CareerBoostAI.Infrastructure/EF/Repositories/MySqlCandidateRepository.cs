using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.MappingExtensions;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Repositories;

internal sealed class MySqlCandidateRepository(CareerBoostDbContext context) : ICandidateRepository
{
    
    private readonly DbSet<Candidate> _candidates = context.Candidates;
    private readonly CareerBoostDbContext _context = context;

    public async Task<CandidateDto?> GetAsync(Guid id)
    {
        var candidateModel = await _candidates
            .Include(c => c.Cvs)
            .SingleOrDefaultAsync(c => c.Id == id);

        return candidateModel?.AsDto();
    }

    public async Task AddAsync(CandidateDto candidate)
    {
        var model = candidate.AsModel();
        await _candidates.AddAsync(model);
    }

    public Task UpdateAsync(CandidateDto candidate)
    {
        throw new NotImplementedException();
    }
}
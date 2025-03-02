using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.MappingExtensions;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Skill = CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects.Skill;

namespace CareerBoostAI.Infrastructure.EF.Repositories;

internal sealed class MySqlCandidateRepository(CareerBoostWriteDbContext context) : ICandidateRepository
{
    
    private readonly DbSet<CandidateAggregate> _candidates = context.Candidates;
    private readonly DbSet<Skill> _skills = context.Skills;
    private readonly CareerBoostWriteDbContext _context = context;

    public async Task<CandidateAggregate?> GetAsync(CandidateId id)
    {
        return await _candidates
            .Include(c => c.CandidateCv)
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task CreateNewAsync(CandidateAggregate candidate)
    {
        var existing = FindNonExistingSkills(candidate.CandidateCv.Skills);
        await _candidates.AddAsync(candidate);
    }

    private async Task<IEnumerable<Skill>> FindNonExistingSkills(IEnumerable<Skill> skills)
    {
        var skillNames = skills.Select(s => s.Value).Distinct().ToList();
        var res = await _context.Skills
            .Where(s => skillNames.Contains(s.Value)).ToListAsync();
        return res;
    }
    
}
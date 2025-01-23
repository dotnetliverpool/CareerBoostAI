using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using CareerBoostAI.Infrastructure.EF.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Repositories;

public class MySqlCvRepository(CareerBoostWriteDbContext context) : ICvRepository
{
    private readonly DbSet<Cv> _cvs = context.Cvs;
    private readonly DbSet<Skill> _skills = context.Skills;
    private readonly CareerBoostWriteDbContext _context = context;
    public async Task<Cv?> GetByEmailAsync(string email)
    {
        return await _cvs
            .FirstOrDefaultAsync(cv => cv.CandidateEmail == Email.Create(email));
    }

    public async Task<Cv?> GetByIdAsync(EntityId id)
    {
        return await _cvs.FindAsync(id);
    }

    public async Task CreateNewAsync(Cv cv)
    {
        // handle skills and language m - m relationship
        await _cvs.AddAsync(cv);
    }

    public Task UpdateAsync(Cv cv)
    {
        throw new NotImplementedException();
    }

    private async Task<IEnumerable<Skill>> FindNonExistingSkills(IEnumerable<Skill> skills)
    {
        var skillNames = skills.Select(s => s.Value).Distinct().ToList();
        var res = await _skills
            .Where(s => skillNames.Contains(s.Value)).ToListAsync();
        return res;
    }
}
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.UserUpload;
using CareerBoostAI.Infrastructure.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

public class CareerBoostWriteDbContext(DbContextOptions<CareerBoostWriteDbContext> options) : DbContext(options)
{
    public DbSet<CandidateAggregate> Candidates { get; set; }
    public DbSet<Skill> Skills { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        var configuration = new WriteDbConfiguration();
        modelBuilder.ApplyConfiguration<CandidateAggregate>(configuration);
        // modelBuilder.ApplyConfiguration<CandidateCv>(configuration);
        // modelBuilder.ApplyConfiguration<WorkExperience>(configuration);
        // modelBuilder.ApplyConfiguration<Education>(configuration);
        // modelBuilder.ApplyConfiguration<Skill>(configuration);
        // modelBuilder.ApplyConfiguration<Language>(configuration);
        base.OnModelCreating(modelBuilder);
    }
}
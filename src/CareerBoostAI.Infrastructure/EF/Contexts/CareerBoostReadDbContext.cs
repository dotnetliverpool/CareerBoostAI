using CareerBoostAI.Infrastructure.EF.Configuration;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

internal sealed class CareerBoostReadDbContext: DbContext
{
    
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Cv> Cvs { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Language> Languages { get; set; }
    public CareerBoostReadDbContext(DbContextOptions<CareerBoostReadDbContext> options) : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        var configuration = new ReadDbConfiguration();
        modelBuilder.ApplyConfiguration<Candidate>(configuration);
        modelBuilder.ApplyConfiguration<Cv>(configuration);
        modelBuilder.ApplyConfiguration<Experience>(configuration);
        modelBuilder.ApplyConfiguration<Education>(configuration);
        modelBuilder.ApplyConfiguration<Skill>(configuration);
        modelBuilder.ApplyConfiguration<Language>(configuration);
        modelBuilder.ApplyConfiguration<CvSkill>(configuration);
        modelBuilder.ApplyConfiguration<CvLanguage>(configuration);
        modelBuilder.ApplyConfiguration<Upload>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
    
}
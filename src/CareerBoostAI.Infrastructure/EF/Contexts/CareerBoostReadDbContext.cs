using CareerBoostAI.Infrastructure.EF.Configuration;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

internal sealed class CareerBoostReadDbContext: DbContext
{
    
    public DbSet<CandidateReadModel> Candidates { get; set; }
    public DbSet<CvReadModel> Cvs { get; set; }
    public DbSet<ExperienceReadModel> Experiences { get; set; }
    public DbSet<EducationReadModel> Educations { get; set; }
    public DbSet<SkillReadModel> Skills { get; set; }
    public DbSet<LanguageReadModel> Languages { get; set; }
    public CareerBoostReadDbContext(DbContextOptions<CareerBoostReadDbContext> options) : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        var configuration = new ReadDbConfiguration();
        modelBuilder.ApplyConfiguration<CandidateReadModel>(configuration);
        modelBuilder.ApplyConfiguration<CvReadModel>(configuration);
        modelBuilder.ApplyConfiguration<ExperienceReadModel>(configuration);
        modelBuilder.ApplyConfiguration<EducationReadModel>(configuration);
        modelBuilder.ApplyConfiguration<SkillReadModel>(configuration);
        modelBuilder.ApplyConfiguration<LanguageReadModel>(configuration);
        // modelBuilder.ApplyConfiguration<CvSkill>(configuration);
        // modelBuilder.ApplyConfiguration<CvLanguage>(configuration);
        modelBuilder.ApplyConfiguration<UploadReadModel>(configuration);
        
        base.OnModelCreating(modelBuilder);
    }
    
}
using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using CareerBoostAI.Domain.UploadContext;
using CareerBoostAI.Infrastructure.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

public class CareerBoostWriteDbContext(DbContextOptions<CareerBoostWriteDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Cv> Cvs { get; set; }
    
    public DbSet<Upload> Uploads { get; set; }
    public DbSet<Skill> Skills { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        var configuration = new WriteDbConfiguration();
        modelBuilder.ApplyConfiguration<Candidate>(configuration);
        modelBuilder.ApplyConfiguration<Cv>(configuration);
        modelBuilder.ApplyConfiguration<Upload>(configuration);
        base.OnModelCreating(modelBuilder);
    }
}
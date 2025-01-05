using CareerBoostAI.Infrastructure.EF.Configuration;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

internal sealed class CareerBoostDbContext: DbContext
{
    
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Cv> Cvs { get; set; }
    public CareerBoostDbContext(DbContextOptions<CareerBoostDbContext> options) : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var candidateConfig = new CandidateDbConfiguration();
        modelBuilder.ApplyConfiguration<Candidate>(candidateConfig);
        base.OnModelCreating(modelBuilder);
    }
    
}
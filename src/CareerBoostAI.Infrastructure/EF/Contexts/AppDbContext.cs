using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Infrastructure.EF.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

internal sealed class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("CareerBoostAI");

        var configuration = new AppDbConfiguration();
        modelBuilder.ApplyConfiguration<Candidate>(configuration);
        base.OnModelCreating(modelBuilder);
    }
    
}
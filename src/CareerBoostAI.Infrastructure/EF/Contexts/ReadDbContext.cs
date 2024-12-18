using Microsoft.EntityFrameworkCore;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

internal sealed class ReadDbContext : DbContext
{
    public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("CareerBoostAI");
        base.OnModelCreating(modelBuilder);
    }
}
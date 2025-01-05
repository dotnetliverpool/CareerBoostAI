using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

internal class CandidateDbConfiguration : IEntityTypeConfiguration<Candidate>, IEntityTypeConfiguration<Cv>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);
        
    }

    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        throw new NotImplementedException();
    }
}
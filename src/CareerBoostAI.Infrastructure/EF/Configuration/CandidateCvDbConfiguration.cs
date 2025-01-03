using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

public class CandidateCvDbConfiguration :  IEntityTypeConfiguration<Cv>
{
    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        
    }
}
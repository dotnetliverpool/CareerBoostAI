using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

internal class CandidateDbPropertyConfiguration : IEntityTypeConfiguration<Candidate>, 
    IEntityTypeConfiguration<Cv>, IEntityTypeConfiguration<CvSection>,
    IEntityTypeConfiguration<CvSectionItem>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder
            .HasMany(c => c.Cvs)
            .WithOne(cv => cv.Candidate);
    }

    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        
        builder
            .HasMany(cv => cv.Sections)
            .WithOne(section => section.Cv)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<CvSection> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder
            .HasMany(section => section.SectionItems)
            .WithOne(item => item.Section)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void Configure(EntityTypeBuilder<CvSectionItem> builder)
    {
        builder.HasKey(c => c.Id);
    }
}
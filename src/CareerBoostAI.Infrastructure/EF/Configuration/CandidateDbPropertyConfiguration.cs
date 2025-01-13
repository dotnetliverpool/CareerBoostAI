using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

internal class CandidateDbPropertyConfiguration : IEntityTypeConfiguration<Candidate>, 
    IEntityTypeConfiguration<Cv>, IEntityTypeConfiguration<CvSkill>,
    IEntityTypeConfiguration<CvLanguage>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();

        builder
            .HasOne(c => c.Cv)
            .WithOne(cv => cv.Candidate);

        builder
            .HasMany(c => c.Uploads)
            .WithOne(up => up.Candidate);

    }

    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        
        builder
            .HasMany(cv => cv.Experiences)
            .WithOne(exp => exp.Cv)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(cv => cv.Educations)
            .WithOne(exp => exp.Cv)
            .OnDelete(DeleteBehavior.Cascade);
    }
    

    public void Configure(EntityTypeBuilder<CvSectionItem> builder)
    {
        builder.HasKey(c => c.Id);
    }

    public void Configure(EntityTypeBuilder<CvSkill> builder)
    {
        builder
            .HasKey(cs => new { cs.CvId, cs.SkillId });

        builder
            .HasOne(cs => cs.Cv)
            .WithMany(c => c.CvSkills)
            .HasForeignKey(cs => cs.CvId);

        builder
            .HasOne(cs => cs.Skill)
            .WithMany(s => s.CvSkills)
            .HasForeignKey(cs => cs.SkillId);
    }

    public void Configure(EntityTypeBuilder<CvLanguage> builder)
    {
        builder
            .HasKey(cl => new { cl.CvId, cl.LanguageId });

        builder
            .HasOne(cl => cl.Language)
            .WithMany(c => c.CvLanguages)
            .HasForeignKey(cl => cl.CvId);

        builder
            .HasOne(cl => cl.Language)
            .WithMany(l => l.CvLanguages)
            .HasForeignKey(cl => cl.LanguageId);
    }
}

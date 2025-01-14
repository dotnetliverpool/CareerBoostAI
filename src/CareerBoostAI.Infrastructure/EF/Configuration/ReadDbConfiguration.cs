using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cv = CareerBoostAI.Infrastructure.EF.Models.Cv;
using Education = CareerBoostAI.Infrastructure.EF.Models.Education;
using Language = CareerBoostAI.Infrastructure.EF.Models.Language;
using Skill = CareerBoostAI.Infrastructure.EF.Models.Skill;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

internal class ReadDbConfiguration : IEntityTypeConfiguration<Candidate>, 
    IEntityTypeConfiguration<Cv>, IEntityTypeConfiguration<Skill>,
    IEntityTypeConfiguration<Experience>, IEntityTypeConfiguration<Education>,
    IEntityTypeConfiguration<CvLanguage>, IEntityTypeConfiguration<CvSkill>,
    IEntityTypeConfiguration<Language>, IEntityTypeConfiguration<Upload>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder
            .HasKey(c => c.Id);
        
        builder
            .Property(c => c.Id)
            .ValueGeneratedNever();
        
        builder
            .HasOne(c => c.Cv)
            .WithOne(cv => cv.Candidate);
    }

    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.HasKey(cv => cv.Id);
        builder
        .Property(cv => cv.Id)
        .ValueGeneratedNever();
        
        builder
            .HasOne(cv => cv.Candidate) 
            .WithOne(candidate => candidate.Cv)  
            .HasForeignKey<Cv>(cv => cv.CandidateId)  
            .IsRequired();
        
        builder
            .HasMany(cv => cv.Experiences)
            .WithOne(exp => exp.Cv)
            .HasForeignKey(exp => exp.CvId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(cv => cv.Educations)
            .WithOne(edu => edu.Cv)
            .HasForeignKey(edu => edu.CvId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(c => c.Skills)
            .WithMany(s => s.Cvs)
            .UsingEntity<CvSkill>(
                j => j.HasOne(cs => cs.Skill)
                    .WithMany()
                    .HasForeignKey(cs => cs.SkillId),
                j => j.HasOne(cs => cs.Cv)
                    .WithMany()
                    .HasForeignKey(cs => cs.CvId)
            );
        
        builder
            .HasMany(c => c.Languages)
            .WithMany(s => s.Cvs)
            .UsingEntity<CvLanguage>(
                j => j.HasOne(cl => cl.Language)
                    .WithMany()
                    .HasForeignKey(cl => cl.LanguageId),
                j => j.HasOne(cl => cl.Cv)
                    .WithMany()
                    .HasForeignKey(cl => cl.CvId)
            );
    }
    
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder
            .HasKey(exp => new {exp.Id, exp.CvId});
    }

    public void Configure(EntityTypeBuilder<Education> builder)
    {
        
        builder
            .HasKey(edu => new {edu.Id, edu.CvId});
    }
    
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder
            .HasKey(sk => sk.Id);
    }

    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder
            .HasKey(lng => lng.Id);
    }


    public void Configure(EntityTypeBuilder<CvLanguage> builder)
    {
        builder
            .HasKey(cl => new { cl.CvId, cl.LanguageId });

    }

    public void Configure(EntityTypeBuilder<CvSkill> builder)
    {
        builder
            .HasKey(cl => new { cl.CvId, cl.SkillId });
    }

    public void Configure(EntityTypeBuilder<Upload> builder)
    {
        builder.HasKey(up => up.Id);
        builder
            .HasOne(up => up.Candidate)
            .WithMany(c => c.Uploads)
            .HasForeignKey(up => up.CandidateId);

    }
}

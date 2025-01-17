using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

internal class ReadDbConfiguration : IEntityTypeConfiguration<CandidateReadModel>, 
    IEntityTypeConfiguration<CvReadModel>, IEntityTypeConfiguration<SkillReadModel>,
    IEntityTypeConfiguration<ExperienceReadModel>, IEntityTypeConfiguration<EducationReadModel>,
    IEntityTypeConfiguration<CvLanguage>, IEntityTypeConfiguration<CvSkill>,
    IEntityTypeConfiguration<LanguageReadModel>, IEntityTypeConfiguration<Upload>
{
    public void Configure(EntityTypeBuilder<CandidateReadModel> builder)
    {
        builder
            .HasKey(c => c.Id);
        
        builder
            .Property(c => c.Id)
            .ValueGeneratedNever();
        
        builder
            .HasOne(c => c.CvReadModel)
            .WithOne(cv => cv.CandidateReadModel);
    }

    public void Configure(EntityTypeBuilder<CvReadModel> builder)
    {
        builder.HasKey(cv => cv.Id);
        builder
        .Property(cv => cv.Id)
        .ValueGeneratedNever();
        
        builder
            .HasOne(cv => cv.CandidateReadModel) 
            .WithOne(candidate => candidate.CvReadModel)  
            .HasForeignKey<CvReadModel>(cv => cv.CandidateId)  
            .IsRequired();
        
        builder
            .HasMany(cv => cv.Experiences)
            .WithOne(exp => exp.CvReadModel)
            .HasForeignKey(exp => exp.CvId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(cv => cv.Educations)
            .WithOne(edu => edu.CvReadModel)
            .HasForeignKey(edu => edu.CvId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(c => c.Skills)
            .WithMany(s => s.Cvs)
            .UsingEntity<CvSkill>(
                j => j.HasOne(cs => cs.SkillReadModel)
                    .WithMany()
                    .HasForeignKey(cs => cs.SkillId),
                j => j.HasOne(cs => cs.CvReadModel)
                    .WithMany()
                    .HasForeignKey(cs => cs.CvId)
            );
        
        builder
            .HasMany(c => c.Languages)
            .WithMany(s => s.Cvs)
            .UsingEntity<CvLanguage>(
                j => j.HasOne(cl => cl.LanguageReadModel)
                    .WithMany()
                    .HasForeignKey(cl => cl.LanguageId),
                j => j.HasOne(cl => cl.CvReadModel)
                    .WithMany()
                    .HasForeignKey(cl => cl.CvId)
            );
    }
    
    public void Configure(EntityTypeBuilder<ExperienceReadModel> builder)
    {
        builder
            .HasKey(exp => new {exp.Id, exp.CvId});
    }

    public void Configure(EntityTypeBuilder<EducationReadModel> builder)
    {
        
        builder
            .HasKey(edu => new {edu.Id, edu.CvId});
    }
    
    public void Configure(EntityTypeBuilder<SkillReadModel> builder)
    {
        builder
            .HasKey(sk => sk.Id);
    }

    public void Configure(EntityTypeBuilder<LanguageReadModel> builder)
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
            .HasOne(up => up.CandidateReadModel)
            .WithMany(c => c.Uploads)
            .HasForeignKey(up => up.CandidateId);

    }
}

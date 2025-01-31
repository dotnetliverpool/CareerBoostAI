using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

internal class ReadDbConfiguration : IEntityTypeConfiguration<CandidateReadModel>, 
    IEntityTypeConfiguration<CvReadModel>, IEntityTypeConfiguration<SkillReadModel>,
    IEntityTypeConfiguration<ExperienceReadModel>, IEntityTypeConfiguration<EducationReadModel>,
    IEntityTypeConfiguration<CvLanguage>, IEntityTypeConfiguration<CvSkill>,
    IEntityTypeConfiguration<LanguageReadModel>, IEntityTypeConfiguration<UploadReadModel>
{
    public void Configure(EntityTypeBuilder<CandidateReadModel> builder)
    {
        builder
            .HasKey(c => c.Id);
        
        builder.HasIndex(candidate => candidate.Email).IsUnique();
        
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
            .HasForeignKey<CvReadModel>(cv => cv.CandidateEmail)
            .HasPrincipalKey<CandidateReadModel>(candidate => candidate.Email)
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
            .HasMany(cv => cv.Skills)
            .WithOne(skill => skill.Cv)
            .HasForeignKey(skill => skill.CvId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(cv => cv.Languages)
            .WithOne(lng => lng.Cv)
            .HasForeignKey(lng => lng.CvId)
            .OnDelete(DeleteBehavior.Cascade);
       
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
            .HasKey(sk => sk.Name);
    }

    public void Configure(EntityTypeBuilder<LanguageReadModel> builder)
    {
        builder
            .HasKey(lng => lng.Name);
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

    public void Configure(EntityTypeBuilder<UploadReadModel> builder)
    {
        builder.HasKey(up => up.Id);
        
        builder
            .HasOne(up => up.CandidateReadModel)
            .WithMany(c => c.Uploads)
            .HasForeignKey(up => up.CandidateEmail)
            .HasPrincipalKey(c => c.Email);

    }
}

using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cv = CareerBoostAI.Infrastructure.EF.Models.Cv;
using Education = CareerBoostAI.Infrastructure.EF.Models.Education;
using Language = CareerBoostAI.Infrastructure.EF.Models.Language;
using Skill = CareerBoostAI.Infrastructure.EF.Models.Skill;

namespace CareerBoostAI.Infrastructure.EF.Configuration;

internal class CandidateDbPropertyConfiguration : IEntityTypeConfiguration<Candidate>, 
    IEntityTypeConfiguration<Cv>, IEntityTypeConfiguration<Skill>,
    IEntityTypeConfiguration<Experience>, IEntityTypeConfiguration<Education>,
    IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                value => CandidateId.Create(value),
                id => id.Value
            );
        
        builder
            .Property(c => c.FirstName)
            .HasConversion(
                value => FirstName.Create(value),
                fn => fn.Value
            );
        
        builder
            .Property(c => c.LastName)
            .HasConversion(
                value => LastName.Create(value),
                ln => ln.Value
            );
        
        builder
            .Property(c => c.Email)
            .HasConversion(
                value => Email.Create(value),
                em => em.Value
            );

        builder
            .Property(c => c.PhoneNumber)
            .HasConversion(
                dbValue => PhoneNumber.Parse(dbValue),
                phone => $"{phone.Code} {phone.Number}"

            )
            .HasColumnName("PhoneNumber");
        
        builder
            .HasOne(c => c.Cv)
            .WithOne(cv => cv.Candidate);
        
        

    }

    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .ValueGeneratedNever();
        
        builder
            .HasOne(c => c.Candidate) 
            .WithOne(cand => cand.Cv)  
            .HasForeignKey<Cv>(cv => cv.CandidateId)  
            .IsRequired();
        
        builder
            .HasMany(cv => cv.Experiences)
            .WithOne(exp => exp.Cv)
            .HasForeignKey(exp => exp.CvId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata
            .FindNavigation(nameof(Cv.Experiences))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder
            .HasMany(cv => cv.Educations)
            .WithOne(edu => edu.Cv)
            .HasForeignKey(edu => edu.CvId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata
            .FindNavigation(nameof(Cv.Educations))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder
            .HasMany(c => c.Skills)
            .WithMany(s => s.Cvs)
            .UsingEntity<Dictionary<string, object>>(
                "CandidateSkill",
                j => j.HasOne<Skill>().WithMany().HasForeignKey("SkillId"),
                j => j.HasOne<Cv>().WithMany().HasForeignKey("CvId")
            );
        builder
            .Property(c => c.Skills)
            .Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        
        builder
            .HasMany(c => c.Languages)
            .WithMany(s => s.Cvs)
            .UsingEntity<Dictionary<string, object>>(
                "CandidateLanguage", 
                j => j.HasOne<Language>().WithMany().HasForeignKey("LanguageId"),
                j => j.HasOne<Cv>().WithMany().HasForeignKey("CvId")
            );
        
        builder
            .Property(c => c.Languages)
            .Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
        
    }
    
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder
            .HasKey(exp => new {exp.Id, exp.CvId});

        builder
            .Property(exp => exp.Id)
            .HasConversion(
                value => ProfessionalEntryId.Create(value),
                id => id.Value);
        
        builder
            .Property(exp => exp.OrganisationName)
            .HasConversion(
                value => OrganisationName.Create(value),
                orgName => orgName.Value);
        
        builder
            .Property(exp => exp.Description)
            .HasConversion(
                value => Description.Create(value),
                desc => desc.Value);
        
        builder
            .Property(exp => exp.Index)
            .HasConversion(
                value => SequenceIndex.Create(value),
                idx => idx.Value);
        
        builder.OwnsOne(exp => Location.Create(exp.City, exp.Country), loc =>
        {
            loc.Property(l => l.City).HasColumnName("City");
            loc.Property(l => l.Country).HasColumnName("Country");
        });
        
        builder.OwnsOne(exp => Period.Create(exp.StartDate, exp.EndDate), prd =>
        {
            prd.Property(p => p.StartDate).HasColumnName("StartDate");
            prd.Property(p => p.EndDate).HasColumnName("EndDate");
        });
    }

    public void Configure(EntityTypeBuilder<Education> builder)
    {
        
        builder
            .HasKey(edu => new {edu.Id, edu.CvId});

        builder
            .Property(edu => edu.Id)
            .HasConversion(
                value => ProfessionalEntryId.Create(value),
                id => id.Value);
        
        builder
            .Property(edu => edu.OrganisationName)
            .HasConversion(
                value => OrganisationName.Create(value),
                orgName => orgName.Value);
        
        builder.OwnsOne(edu => EducationGrade.Create(edu.Program, edu.Grade), 
            grd =>
        {
            grd.Property(g => g.Program).HasColumnName("Program");
            grd.Property(g => g.Grade).HasColumnName("Grade");
        });
       
        
        builder
            .Property(edu => edu.Index)
            .HasConversion(
                value => SequenceIndex.Create(value),
                idx => idx.Value);
        
        builder.OwnsOne(edu => Location.Create(edu.City, edu.Country), loc =>
        {
            loc.Property(l => l.City).HasColumnName("City");
            loc.Property(l => l.Country).HasColumnName("Country");
        });
        
        builder.OwnsOne(edu => Period.Create(edu.StartDate, edu.EndDate), prd =>
        {
            prd.Property(p => p.StartDate).HasColumnName("StartDate");
            prd.Property(p => p.EndDate).HasColumnName("EndDate");
        });
    }
    
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder
            .HasKey(sk => sk.Id);

        builder
            .Property(sk => sk.Name)
            .HasConversion(
                value => CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects.Skill.Create(value),
                skName => skName.Value);
    }

    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder
            .HasKey(lng => lng.Id);

        builder
            .Property(lng => lng.Name)
            .HasConversion(
                value => CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects.Language.Create(value),
                lng => lng.Value);
    }

    
}

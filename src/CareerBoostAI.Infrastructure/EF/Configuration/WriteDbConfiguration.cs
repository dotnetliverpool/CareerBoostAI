using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using CareerBoostAI.Domain.UploadContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerBoostAI.Infrastructure.EF.Configuration;


internal class WriteDbConfiguration : 
    IEntityTypeConfiguration<Candidate>, IEntityTypeConfiguration<Cv>,
    IEntityTypeConfiguration<Upload>

{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("candidates");
        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => EntityId.Create(value)
            );

        builder.OwnsOne(c => c.Name, nb =>
        {
            nb.Property(n => n.FirstName).HasColumnName("FirstName");
            nb.Property(n => n.LastName).HasColumnName("LastName");
        });

        builder
            .Property(c => c.DateOfBirth)
            .HasConversion(
                dob => dob.Value,
                value => DateOfBirth.Create(value));
        
        builder
            .Property(c => c.Email)
            .HasConversion(
                em => em.Value,
                value => Email.Create(value)
            );

        builder
            .Property(c => c.PhoneNumber)
            .HasConversion(
                phone => $"{phone.Code} {phone.Number}",
                dbValue => PhoneNumber.Parse(dbValue)
            )
            .HasColumnName("PhoneNumber");
    }

    public void Configure(EntityTypeBuilder<Cv> builder)
    {
        builder.ToTable("cvs");
        builder.HasKey(cv => cv.Id);
        builder.Property(cv => cv.Id )
            .HasConversion(
                writeObject => writeObject.Value,
                storeValue => EntityId.Create(storeValue));
                
        builder.Property(cv => cv.Summary)
            .HasConversion(
                writeObject => writeObject.Value,
                storeValue => Summary.Create(storeValue));

        builder.Property(cv => cv.CandidateEmail)
            .HasConversion(
                writeObject => writeObject.Value,
                storeValue => Email.Create(storeValue));
                
        builder.OwnsMany(cv => cv.Experiences, BuildCvExperience);
        builder.OwnsMany(cv => cv.Educations, BuildCvEducation);
        builder.OwnsMany(cv => cv.Skills, sb =>
        {
            sb.ToTable("skills");
            sb.Property(s => s.Value).HasColumnName("Name");
            sb.WithOwner().HasForeignKey("CvId");
        });
        builder.OwnsMany(cv => cv.Languages, lb =>
        {
            lb.ToTable("languages");
            lb.Property(l => l.Value).HasColumnName("Name");
            lb.WithOwner().HasForeignKey("CvId");
        });
                
        builder.Navigation(cv => cv.Experiences)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
                
        builder.Navigation(cv => cv.Educations)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
                
        builder.Navigation(cv => cv.Skills)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
                
        builder.Navigation(cv => cv.Languages)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void BuildCvExperience(OwnedNavigationBuilder<Cv, Experience> builder)
    {
        builder.ToTable("experiences");
        builder.WithOwner().HasForeignKey("CvId");
        builder
            .Property(exp => exp.Id)
            .HasConversion(
                id => id.Value,
                value => EntityId.Create(value))
            .ValueGeneratedNever();
        
        builder
            .Property(exp => exp.OrganisationName)
            .HasConversion(
                orgName => orgName.Value,
                value => OrganisationName.Create(value));
        
        builder
            .Property(exp => exp.Description)
            .HasConversion(
                desc => desc.Value,
                value => Description.Create(value));
        
        builder
            .OwnsOne(exp => exp.Location, lb =>
            {
                lb.Property(l => l.City).HasColumnName("City");
                lb.Property(l => l.Country).HasColumnName("Country");
            });
        
        builder
            .OwnsOne(exp => exp.TimePeriod, prd =>
            {
                prd.Property(p => p.StartDate).HasColumnName("StartDate");
                prd.Property(p => p.EndDate).HasColumnName("EndDate");
            });
    }
    
    private static void BuildCvEducation(OwnedNavigationBuilder<Cv, Education> builder)
    {
        builder.ToTable("educations");
        builder.WithOwner().HasForeignKey("CvId");
        builder
            .Property(edu => edu.Id)
            .HasConversion(
                id => id.Value,
                value => EntityId.Create(value));
        
        builder
            .Property(edu => edu.OrganisationName)
            .HasConversion(
                orgName => orgName.Value,
                value => OrganisationName.Create(value));
        
        builder
            .OwnsOne(edu => edu.Location, lb =>
            {
                lb.Property(l => l.City).HasColumnName("City");
                lb.Property(l => l.Country).HasColumnName("Country");
            });
        
        builder
            .OwnsOne(edu => edu.TimePeriod, prd =>
            {
                prd.Property(p => p.StartDate).HasColumnName("StartDate");
                prd.Property(p => p.EndDate).HasColumnName("EndDate");
            });
        
        builder.OwnsOne(edu => edu.EducationalGrade, grd =>
        {
            grd.Property(g => g.Program).HasColumnName("Program");
            grd.Property(g => g.Grade).HasColumnName("Grade");
        });
    }

    public void Configure(EntityTypeBuilder<Upload> builder)
    {
        builder.ToTable("uploads");
        builder.HasKey(up => up.Id);
        builder.Property(up => up.Id)
            .HasConversion(
                writeObject => writeObject.Value,
                storageValue => EntityId.Create(storageValue));

        builder.Property(up => up.UserEmailAddress)
            .HasColumnName("CandidateEmail");
        
        builder.Property(up => up.UserEmailAddress)
            .HasConversion(
                writeObject => writeObject.Value,
                storeValue => Email.Create(storeValue));
        
        builder.OwnsOne(up => up.Document, docb =>
        {
            docb.Property(doc => doc.Address).HasColumnName("StorageAddress");
            docb.Property(doc => doc.Medium).HasColumnName("StorageMedium");
            docb.Property(doc => doc.FileName).HasColumnName("FileName");
            docb.Property(doc => doc.Extension).HasColumnName("Extension");
        });
    }
}

using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.UserUpload;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace CareerBoostAI.Infrastructure.EF.Configuration;


internal class WriteDbConfiguration : IEntityTypeConfiguration<CandidateAggregate>

{
    public void Configure(EntityTypeBuilder<CandidateAggregate> builder)
    {
        builder.ToTable("candidates");
        builder.HasKey(c => c.Id);
        builder
            .Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => CandidateId.Create(value)
                );
        
        builder
            .Property(c => c.FirstName)
            .HasConversion<string>(
                objectValue => objectValue.Value,
                value => FirstName.Create(value)
            );
        
        builder
            .Property(c => c.LastName)
            .HasConversion(
                ln => ln.Value,
                value => LastName.Create(value)
            );

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

        builder
            .OwnsOne(candidate => candidate.CandidateCv, CongigureCandidateCv);
    }

    private static void CongigureCandidateCv(OwnedNavigationBuilder<CandidateAggregate, CandidateCv> cvb)
    {
        cvb.ToTable("cvs");
        cvb.WithOwner().HasForeignKey("CandidateId");
        cvb.HasKey(cv => cv.Id);
        cvb.Property(cv => cv.Id )
            .HasConversion(
                writeObject => writeObject.Value,
                storeValue => CvId.Create(storeValue));
                
        cvb.Property(cv => cv.Summary)
            .HasConversion(
                writeObject => writeObject.Value,
                storeValue => Summary.Create(storeValue));
                
        cvb.OwnsMany(cv => cv.Experiences, BuildCvExperience);
        cvb.OwnsMany(cv => cv.Educations, BuildCvEducation);
        cvb.OwnsMany(cv => cv.Skills, sb =>
        {
            sb.ToTable("skills");
            sb.HasKey(s => s.Value);
            sb.Property(s => s.Value).HasColumnName("Name");
            sb.WithOwner().HasForeignKey("CvId");
        });
        cvb.OwnsMany(cv => cv.Languages, lb =>
        {
            lb.ToTable("languages");
            lb.HasKey(l => l.Value);
            lb.Property(l => l.Value).HasColumnName("Name");
            lb.WithOwner().HasForeignKey("CvId");
        });
                
        cvb.Navigation(cv => cv.Experiences)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
                
        cvb.Navigation(cv => cv.Educations)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
                
        cvb.Navigation(cv => cv.Skills)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
                
        cvb.Navigation(cv => cv.Languages)
            .Metadata
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void BuildCvExperience(OwnedNavigationBuilder<CandidateCv, WorkExperience> builder)
    {
        builder.ToTable("experiences");
        builder.WithOwner().HasForeignKey("CvId");
        builder
            .Property(exp => exp.Id)
            .HasConversion(
                id => id.Value,
                value => ProfessionalEntryId.Create(value))
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
            .Property(exp => exp.SequenceIndex)
            .HasConversion(
                idx => idx.Value,
                value => SequenceIndex.Create(value));

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

    private static void BuildCvEducation(OwnedNavigationBuilder<CandidateCv, Education> builder)
    {
        builder.ToTable("educations");
        builder.WithOwner().HasForeignKey("CvId");
        builder
            .Property(edu => edu.Id)
            .HasConversion(
                id => id.Value,
                value => ProfessionalEntryId.Create(value));
        
        builder
            .Property(edu => edu.OrganisationName)
            .HasConversion(
                orgName => orgName.Value,
                value => OrganisationName.Create(value));
        
        builder
            .Property(edu => edu.SequenceIndex)
            .HasConversion(
                idx => idx.Value,
                value => SequenceIndex.Create(value));

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
        
        builder.OwnsOne(edu => edu.Grade, grd =>
        {
            grd.Property(g => g.Program).HasColumnName("Program");
            grd.Property(g => g.Grade).HasColumnName("Grade");
        });
    }
    
}
